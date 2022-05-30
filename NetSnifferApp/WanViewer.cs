using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Analysis;
using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    public partial class WanViewer : UserControl
    {
        readonly Dictionary<WanHost, WanHostControl> hostControls = new();
        readonly Dictionary<WanHost, WanHostItem> hostItems = new();

        readonly List<WanHost> orderedHosts = new();

        readonly List<WanHost> lanRouters = new();

        readonly List<WanHost> routers = new();

        readonly List<(WanHost, WanHost)> connections = new();

        readonly Dictionary<WanHost, bool> hasForwardMissingConnection = new();

        int centerX;
        int centerY;

        const int maxIterations = 50;

        public bool IsLive { get; set; } = false;

        const double angleDiff = Math.PI / 3;
        const int radious = 50;

        readonly Random random = new(10);

        WanHostControl selectedHostControl = null;

        readonly Dictionary<WanHostControl, Point> prevLocations = new();

        public WanViewer()
        {
            InitializeComponent();

            centerX = Width / 2;
            centerY = Height/ 2;

            hostsComboBox.Items.Add(WanHostItem.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            foreach (var host in PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts.Keys) 
            {
                pendingTracerts.Enqueue(host);
            }
        }

        public void Clear()
        {
            lock (hostControls)
            {
                foreach (var control in hostControls.Values)
                {
                    Controls.Remove(control);
                }

                connections.Clear();
                hostControls.Clear();
            }
        }

        private void Label1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(
                e.Graphics, label1.DisplayRectangle, 
                Color.Black, 3, ButtonBorderStyle.Solid,
                Color.Black, 3, ButtonBorderStyle.Solid,
                Color.Black, 3, ButtonBorderStyle.Solid,
                Color.Black, 3, ButtonBorderStyle.Solid
                );
        }

        public bool ContainsHost(WanHost host)
        {
            return hostControls.ContainsKey(host);
        }

        static List<WanHost> AllConnectedHostsAndChildren(WanHost host, WanHost hostToExculde)
        {
            var directConnections = new List<WanHost>(host.ConnectedHosts);
            directConnections.Remove(hostToExculde);

            if (directConnections.Count == 0)
                return new List<WanHost>();
            else
                return directConnections.SelectMany(otherHost => AllConnectedHostsAndChildren(otherHost, host)).ToList();
        }

        public void AddHost(WanHost host)
        {
            if (hostControls.ContainsKey(host))
                return;

            var control = new WanHostControl
            {
                IsLive = IsLive,

                Host = host,
                Visible = true,

                Width = 60,
                Height = 52
            };

            control.TracertCompleted += Control_TracertCompleted;
            control.SelectionStateChangedByUser += WanHostControl_SelectionStateChangedByUser;

            var item = new WanHostItem(host);

            //TODO: fix error, handle required
            Invoke(new MethodInvoker(() =>
            { 
                Controls.Add(control);
                hostsComboBox.Items.Add(item);
            }));

            hostControls.Add(host, control);
            hostItems.Add(host, item);

            PlaceHost(control);
        }

        readonly Dictionary<WanHostControl, double> angleOfHostControl = new();
        readonly Dictionary<WanHostControl, WanHostControl> prevControlOfHostControl = new();

        readonly ConcurrentQueue<WanHost> pendingTracerts = new();

        private void Control_TracertCompleted(object sender, EventArgs e)
        {
            var senderControl = (WanHostControl)sender;
            
            senderControl.TracertCompleted -= Control_TracertCompleted;

            pendingTracerts.Enqueue(senderControl.Host);
        }

        public void ShowNewTracert()
        {
            if (pendingTracerts.TryDequeue(out WanHost host))
                ShowTracertSimple(hostControls[host]);
                //ShowTracert(hostControls[host]);
        }

        void ShowTracertSimple(WanHostControl senderControl)
        {
            foreach (var (hostControl, prevLocation) in prevLocations)
            {
                hostControl.Location = prevLocation;
                hostControl.ReturnToBaseColors();
            }

            if (tracertControl != null)
                tracertControl.ReturnToBaseColors();
            tracertControl = senderControl;

            prevLocations.Clear();

            List<WanHost> hops = PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts[senderControl.Host];

            if (hops.Count(hop => hop == null) == hops.Count)
            {
                return;
            }

            var copy = new List<WanHost>(hops);
            copy.RemoveAt(0);

            //var router = copy[0];
            //var routerControl = hostControls[router];

            //var angle = Math.Atan2(routerControl.Location.Y - centerY, routerControl.Location.X - centerX);

            //var prevLocation = new Point((int)(routerControl.Location.X - radious * Math.Cos(angle)), (int)(routerControl.Location.Y - radious * Math.Sin(angle)));

            BuildChainFromTracertSimple(copy);
        }

        void ShowTracert(WanHostControl senderControl)
        {
            List<WanHost> hops = PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts[senderControl.Host];

            if (hops.Count(hop => hop == null) == hops.Count)
            {
                return;
            }

            var copy = new List<WanHost>(hops);
            copy.RemoveAt(0);

            //var index = hops.FindIndex(host => !orderedHosts.Contains(host));
            var index = hops.FindLastIndex(copy => orderedHosts.Contains(copy));

            var prevIndex = index - 1;

            if (index == -1)
            {
                var host = copy[0];

                WanHostControl control;
                
                if (!hostControls.ContainsKey(host))
                {
                    AddHost(host);
                }

                control = hostControls[host];

                var angle = Math.Atan2(control.Location.Y - centerY, control.Location.X - centerX);

                var prevLocation = new Point((int)(control.Location.X - radious * Math.Cos(angle)), (int)(control.Location.Y - radious * Math.Sin(angle)));

                BuildChainFromTracert(copy, control, angle, prevLocation, null);
            }
            else
            {
                var control = hostControls[hops[index]];
                var prevControl = hostControls[hops[prevIndex]];

                BuildChainFromTracert(hops, control, angleOfHostControl[control], prevControl.Location, prevControl?.Host);
            }
        }


        private void WanHostControl_SelectionStateChangedByUser(object sender, EventArgs e)
        {
            var control = (WanHostControl)sender;

            if (selectedHostControl != null)
                selectedHostControl.UnMarkSelection();

            if (control.IsSelected)
            {
                selectedHostControl = control;
                control.MarkSelection();

                hostsComboBox.SelectedIndex = hostsComboBox.Items.Cast<WanHostItem>().ToList().FindIndex(1, item => item.Host.Equals(control.Host));
            }
            else
            {
                selectedHostControl.UnMarkSelection();
                selectedHostControl = null;

                hostsComboBox.SelectedIndex = 0;
            }
        }

        public Task AddHostAsync(WanHost host)
        {
            return Task.Run(() => AddHost(host));
        }

        public void RemoveHost(WanHost host)
        {
            if (!routers.Contains(host))
            {
                hostControls.Remove(host, out WanHostControl control);
                Controls.Remove(control);

                hostItems.Remove(host, out WanHostItem item);
                hostsComboBox.Items.Remove(item);
            }
        }

        public void AddLanRouter(WanHost host)
        {
            routers.Add(host);

            lanRouters.Add(host);

            var control = new WanHostControl
            {
                Host = host
            };

            var isServer = hostControls.GetValueOrDefault(host)?.IsServer ?? false;

            hostControls[host] = control;

            control.BecomeRouter();
            if (isServer)
                control.BecomeServer();

            control.TracertCompleted += Control_TracertCompleted;
            control.SelectionStateChangedByUser += WanHostControl_SelectionStateChangedByUser;

            Invoke(new MethodInvoker(() => Controls.Add(control)));

            PlaceLanRouter(control);

            control.BecomeLanRouter();
        }

        public Task AddLanRouterAsync(WanHost host)
        {
            return Task.Run(() => AddLanRouter(host));
        }

        public void MakeHostWanRouter(WanHost host)
        {
            hostControls[host].BecomeRouter();
            hostControls[host].BecomeWanRouter();
        }

        public void MakeHostServer(WanHost host)
        {
            var control = hostControls.GetValueOrDefault(host);

            if (control != null)
                control.BecomeServer();
        }

        public void PlaceHost(WanHostControl control)
        {
            SuspendLayout();

            var location = GetRandomLocationForHost(control);
            var borderLocation = location + control.Size;

            control.Invoke(new MethodInvoker(() => control.Location = location));

            int iterations = 0;

            while (iterations < maxIterations && (location.X < 0 || borderLocation.X > Width || location.Y < 0 || borderLocation.Y > Height || GetShadowedControls(control).Count > 0))
            {
                iterations++;

                location = GetRandomLocationForHost(control);
                borderLocation = location + control.Size;

                control.Invoke(new MethodInvoker(() => control.Location = location));
            }

            control.Invoke(new MethodInvoker(() => control.Location = location));

            ResumeLayout();

            Invalidate();
        }

        public void AddConnection(WanHost host1, WanHost host2)
        {
            if (!connections.Contains((host1, host2)))
                connections.Add((host1, host2));  
        }

        int numbersOfDrawings = 0;

        //public void ShowConnections()
        //{
        //    var connected = connections.SelectMany(hostPair => new WanHost[] { hostPair.Item1, hostPair.Item2 }).Distinct().Count();
        //    if (connected > orderedHosts.Count)
        //    {
        //        if (PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts.Count > numbersOfDrawings)
        //        {
        //            foreach (var router in lanRouters)
        //            {
        //                OrderConnectedHosts(router);
        //            }

        //            numbersOfDrawings++;
        //        }
        //    }

        //    Invalidate();
        //}

        List<WanHostControl> GetShadowedControls(WanHostControl control)
        {
            List<WanHostControl> controls = new();

            foreach (var otherControl in hostControls.Values)
            {
                if (otherControl != control && otherControl.Bounds.IntersectsWith(control.Bounds))
                    controls.Add(otherControl);
            }

            return controls;
        }

        Point GetRandomLocationForHost(WanHostControl control)
        {
            var randomXDis = random.Next(200, 650);
            var randomYDis = random.Next(100, 400);

            var option = random.NextDouble();

            switch (option)
            {
                case > 0.75:
                    break;
                case > 0.5:
                    randomXDis = -randomXDis;
                    break;
                case > 0.25:
                    randomYDis = -randomYDis;
                    break;
                default:
                    randomYDis = -randomYDis;
                    randomXDis = -randomXDis;
                    break;
            }

            return new Point(centerX + randomXDis - control.CenterX, centerY + randomYDis - control.CenterY);
        }

        Point GetRandomLocationForLanRouter(WanHostControl control)
        {
            var randomXDis = random.Next(40, 60);
            var randomYDis = random.Next(40, 60);

            var option = random.NextDouble();

            switch (option)
            {
                case > 0.75:
                    break;
                case > 0.5:
                    randomXDis = -randomXDis;
                    break;
                case > 0.25:
                    randomYDis = -randomYDis;
                    break;
                default:
                    randomYDis = -randomYDis;
                    randomXDis = -randomXDis;
                    break;
            }

            return new Point(centerX + randomXDis - control.CenterX, centerY + randomYDis - control.CenterY);
        }

        public void PlaceLanRouter(WanHostControl control)
        {
            SuspendLayout();

            var location = GetRandomLocationForLanRouter(control);

            Invoke(new MethodInvoker(() => control.Location = location));
            ResumeLayout();
        }

        void BuildChainFromTracert(List<WanHost> tracert, WanHostControl control, double baseAngle, Point baseLocation, WanHost prevHost)
        {
            var nextLocationX = (int)(baseLocation.X + radious * Math.Cos(baseAngle));
            var nextLocationY = (int)(baseLocation.Y + radious * Math.Sin(baseAngle));

            var newLocation = new Point(nextLocationX, nextLocationY);

            var host = control.Host;

            if (!orderedHosts.Contains(host))
            {
                Invoke(new MethodInvoker(() => control.Location = newLocation));

                lock (orderedHosts)
                    orderedHosts.Add(host);
            }

            var connectedHosts = new List<WanHost>(control.Host.ConnectedHosts);
            connectedHosts.Remove(prevHost);

            if (connectedHosts.Count == 0)
            {
                var copy = new List<WanHost>(tracert);

                copy.Reverse();

                copy = copy.SkipWhile(new Func<WanHost, bool>(host => host == null)).ToList();
                copy.Reverse();

                var startingIndex = copy.IndexOf(host);

                if (startingIndex == copy.Count - 1)
                    return;

                var nextHosts = copy.Skip(startingIndex + 1);

                if (!nextHosts.Any())
                    return;

                foreach (var aNextHost in nextHosts)
                {
                    if (aNextHost == null)
                    {
                        hasForwardMissingConnection[host] = true;
                        nextLocationX = (int)(nextLocationX + radious * Math.Cos(baseAngle));
                        nextLocationY = (int)(nextLocationY + radious * Math.Sin(baseAngle));

                        newLocation = new Point(nextLocationX, nextLocationY);
                    }
                    else
                    {
                        var nextControl = hostControls[aNextHost];

                        angleOfHostControl.Add(nextControl, baseAngle);

                        BuildChainFromTracert(tracert, nextControl, baseAngle, newLocation, null);
                        return;
                    }
                }
            }
            else
            {
                var nextHostIndex = connectedHosts.FindIndex(host => tracert.Contains(host));
                var nextHost = connectedHosts[nextHostIndex];

                var completeIndex = nextHostIndex + (hasForwardMissingConnection.GetValueOrDefault(host, false) ? 1 : 0);
                var sign = (completeIndex % 2 - 0.5) * 2;
                var newAngle = baseAngle + sign * (angleDiff * ((completeIndex + 1) / 2));

                var nextControl = hostControls.GetValueOrDefault(nextHost, null);

                if (nextControl == null)
                {
                    AddHost(nextHost);
                    nextControl = hostControls[nextHost];
                }


                if (!angleOfHostControl.ContainsKey(nextControl))
                    angleOfHostControl.Add(nextControl, newAngle);

                BuildChainFromTracert(tracert, nextControl, newAngle, newLocation, host);
            }
        }

        WanHostControl tracertControl = null;

        void BuildChainFromTracertSimple(List<WanHost> tracert)
        {
            var router = tracert[0];
            var routerControl = hostControls[router];

            var angle = Math.Atan2(routerControl.Location.Y - centerY, routerControl.Location.X - centerX);

            Point newLocation = routerControl.Location;

            foreach (var host in tracert.Skip(1))
            {
                var nextLocationX = (int)(newLocation.X + radious * Math.Cos(angle));
                var nextLocationY = (int)(newLocation.Y + radious * Math.Sin(angle));

                newLocation = new Point(nextLocationX, nextLocationY);

                if (host != null)
                {
                    var control = hostControls[host];
                    
                    prevLocations[control] = control.Location;
                    control.ReturnToSpeicalColors();

                    control.Location = newLocation;
                }
            }
        }

        private void WanViewer_Resize(object sender, EventArgs e)
        {
            centerX = Width / 2;
            centerY = Height / 2;

            label1.Location = new Point(centerX - label1.Width / 2, centerY - label1.Height / 2);
        }
        
        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (WanHostItem)hostsComboBox.SelectedItem;

            if (hostsComboBox.SelectedIndex == 0)
            {
                if (selectedHostControl != null)
                    selectedHostControl.UnMarkSelection();

                selectedHostControl = null;
            }
            else if (item != null)
            {
                if (selectedHostControl != null)
                    selectedHostControl.UnMarkSelection();

                selectedHostControl = hostControls[item.Host];
                selectedHostControl.MarkSelection();
            }
        }
    }
}
