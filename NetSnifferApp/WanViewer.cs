using System;
using System.Collections.Generic;
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

        int centerX;
        int centerY;

        int maxIterations = 50;

        public bool IsLive { get; set; } = false;

        const double angleDiff = Math.PI / 3;
        const int radious = 50;

        const int minRadious = 100;
        const int maxRadious = 700;

        //readonly Dictionary<WanHostControl, double> angle = new();

        readonly Random random = new(10);

        public WanViewer()
        {
            InitializeComponent();

            centerX = Width / 2;
            centerY = Height/ 2;
        }

        public void Clear()
        {
            lock (hostControls)
            {
                foreach (var control in hostControls.Values)
                {
                    Controls.Remove(control);
                }

                hostControls.Clear();
            }
        }

        private void label1_Paint(object sender, PaintEventArgs e)
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
            var control = new WanHostControl
            {
                IsLive = IsLive,

                Host = host,
                Visible = true,

                Width = 60,
                Height = 52
            };

            var item = new WanHostItem(host);

            //TODO: fix error, handle required
            Invoke(new MethodInvoker(() =>
            { 
                Controls.Add(control);
                comboBox1.Items.Add(item);
            }));

            hostControls.Add(host, control);
            hostItems.Add(host, item);

            PlaceHost(control);
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
                comboBox1.Items.Remove(item);
            }
        }

        public void AddLanRouter(WanHost host)
        {
            routers.Add(host);
            hostControls.Remove(host);

            lanRouters.Add(host);

            var control = new WanHostControl
            {
                Host = host
            };

            control.BecomeRouter();
            hostControls.Add(host, control);

            Invoke(new MethodInvoker(() => Controls.Add(control)));

            PlaceLanRouter(control);

            //OrderConnectedHosts(host);

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


        double GetRandomAngle()
        {
            return random.NextDouble() * Math.Tau;
        }

        double GetRandomRadious()
        {
            return minRadious + random.NextDouble() * (maxRadious - minRadious);
        }

        public void AddConnection(WanHost host1, WanHost host2)
        {
            if (!connections.Contains((host1, host2)))
                connections.Add((host1, host2));  
        }

        int numbersOfDrawings = 0;

        public void ShowConnections()
        {
            var connected = connections.SelectMany(hostPair => new WanHost[] { hostPair.Item1, hostPair.Item2 }).Distinct().Count();
            if (connected > orderedHosts.Count)
            {
                if (PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts.Count > numbersOfDrawings)
                {
                    foreach (var router in lanRouters)
                    {
                        //if (AllConnectedHostsAndChildren(router, null).Exists(host => !orderedHosts.Contains(host)))
                        //{
                        OrderConnectedHosts(router);
                        //}
                    }

                    numbersOfDrawings++;
                }
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var graphics = e.Graphics;
            var pen = new Pen(Color.Black);

            foreach (var (host1, host2) in connections)
            {
                var point1 = hostControls[host1].Location;
                var point2 = hostControls[host2].Location;

                graphics.DrawLine(pen, point1, point2);
            }
        }

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
            //var angle = GetRandomAngle();
            //var radious = GetRandomRadious();

            //var xFromCenter = (int)(radious * Math.Cos(angle) * Height / Width);
            //var yFromCenter = (int)(radious * Math.Sin(angle) * 0.7);

            //var quater = random.NextDouble();

            //int randomX = 0, randomY = 0;

            //switch (quater)
            //{
            //    case > 0.75:
            //        break;
            //    case > 0.5:
            //        break;
            //    case > 0.25:
            //        break;
            //    default:
            //        break;
            //}
            //return new Point(centerX + xFromCenter - control.CenterX, centerY + yFromCenter - control.CenterY);

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
            //var angle = GetRandomAngle();
            //var radious = GetRandomRadious();

            //var xFromCenter = (int)(radious * Math.Cos(angle) * Height / Width);
            //var yFromCenter = (int)(radious * Math.Sin(angle) * 0.7);

            //var quater = random.NextDouble();

            //int randomX = 0, randomY = 0;

            //switch (quater)
            //{
            //    case > 0.75:
            //        break;
            //    case > 0.5:
            //        break;
            //    case > 0.25:
            //        break;
            //    default:
            //        break;
            //}
            //return new Point(centerX + xFromCenter - control.CenterX, centerY + yFromCenter - control.CenterY);

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

        void OrderConnectedHosts(WanHost router)
        {
            var control = hostControls[router];
            var angle = Math.Atan2(control.Location.Y - centerY, control.Location.X - centerX);

            var prevLocation = new Point((int)(control.Location.X - radious * Math.Cos(angle)), (int)(control.Location.Y - radious * Math.Sin(angle)));
            
            if (!orderedHosts.Contains(router))
                orderedHosts.Add(router);

            BuildChain(control, angle, prevLocation, null);
        }

        void BuildChain(WanHostControl control, double baseAngle, Point baseLocation, WanHost prevHost)
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
                var tracert = PacketAnalyzer.Analyzer.topologyBuilder.wanMapBuilder.CompletedTracerts.
                        FirstOrDefault(aTracert => aTracert.Exists(aHost => ReferenceEquals(aHost, host)));

                if (tracert == null)
                    return;

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
                        nextLocationX = (int)(nextLocationX + radious * Math.Cos(baseAngle));
                        nextLocationY = (int)(nextLocationY + radious * Math.Sin(baseAngle));

                        newLocation = new Point(nextLocationX, nextLocationY);
                    }
                    else
                    {
                        BuildChain(hostControls[aNextHost], baseAngle, newLocation, null);
                        return;
                    }
                }
            }
            else
            {
                //var nextHost = connectedHosts[0];
                //BuildChain(hostControls[nextHost], baseAngle, newLocation, host);

                foreach (var (otherHost, index) in connectedHosts.Select((host, i) => (host, i)))
                {
                    //if (AllConnectedHostsAndChildren(otherHost, host).Exists(host => !orderedHosts.Contains(host)))
                    //{
                        var sign = (index % 2 - 0.5) * 2;
                        var newAngle = baseAngle + sign * (angleDiff * ((index + 1) / 2));
                        BuildChain(hostControls[otherHost], newAngle, newLocation, host);
                    //}
                }
            }
        }

        private void WanViewer_Resize(object sender, EventArgs e)
        {
            centerX = Width / 2;
            centerY = Height / 2;

            label1.Location = new Point(centerX - label1.Width / 2, centerY - label1.Height / 2);
        }

        WanHostControl markedHost = null;

/*        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var text = searchTextBox.Text;

            if (Regex.IsMatch(text, @"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$"))
            {
                var address = IPAddress.Parse(text);
                var host = hostControls.FirstOrDefault(kvp => kvp.Key.IPAddress.Equals(address)).Value;
                
                if (host != null)
                {
                    host.Mark();
                    markedHost = host;
                }
            }
            else
            {
                if (markedHost != null)
                    markedHost.UnMark();
            }
        }*/

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = (WanHostItem)(comboBox1.SelectedItem);

            if (item != null)
            {
                if (markedHost?.Marked ?? false)
                    markedHost.UnMark();

                markedHost = hostControls[item.Host];
                markedHost.Mark();
            }
        }
    }
}
