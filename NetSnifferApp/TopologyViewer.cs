using System;
using System.Net;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    public partial class TopologyViewer : UserControl
    {
        readonly Dictionary<Control, double> angles = new();

        public LanMap LanMap { get; private set; } = LanMap.Empty;

        public WanMap WanMap { get; private set; } = WanMap.Empty;

        readonly Random random = new();

        int lanRows, lanColumns;
        readonly (int Rows, int Columns)[] lanLayouts = new[] { (2, 3), (3, 4), (4, 6) , (5, 6), (5, 7), (20, 20)};
        int layoutIndex = 0;

        int currentLanRow = 0, currentLanColumn = 0;


        int marginTop = 5, marginButton = 5;
        int marginLeft = 3, marginRight = 3;
        
        Padding hostMargin;


        int sizeWidth = 128;
        int sizeHeight = 143;
        
        Size hostSize;

        Size totalHostSize;

        int connectionLength = 150;
        
        //Pen pen = new(Color.Black);
        //Graphics graphics;

        Point Center => new(Width / 2, Height / 2);

        Point LanTableLayoutPanelCenter => new(lanTableLayoutPanel.Width / 2, lanTableLayoutPanel.Height / 2);

        public TopologyViewer()
        {
            InitializeComponent();
            InitializeLanLayout();

            VScroll = true;
            HScroll = false;

            //graphics = CreateGraphics();
        }

        public void UpdateLanMap(LanMapDiff mapDiff)
        {
            foreach (var host in mapDiff.HostsAdded)
            {
                AddLanHost(host);
                LanMap.Hosts.Add(host);
            }

            foreach (var host in mapDiff.HostsRemoved)
            {
                //
                LanMap.Hosts.Remove(host);
            }

            foreach (var router in mapDiff.RoutersAdded)
            {
                MakeLanHostRouter(router);
                LanMap.Routers.Add(router);
            }

            foreach (var router in mapDiff.RoutersRemoved)
            {
                //
                LanMap.Routers.Remove(router);
            }

            foreach (var server in mapDiff.DhcpServersAdded)
            {
                MakeLanHostServer(server);
                LanMap.DhcpServers.Add(server);
            }

            foreach (var server in mapDiff.DhcpServersRemoved)
            {
                //
                LanMap.DhcpServers.Remove(server);
            }

            foreach (var mapping in mapDiff.PhysicalAddressIPAddressMappingModified)
            {
                var hostControl = (LanHostControl)lanTableLayoutPanel.Controls[mapping.PhysicalAddress.ToString()];
                hostControl.SetIPAddress(mapping.IPAddress);

                LanMap.GetHostByPhysicalAddress(mapping.PhysicalAddress).IPAddress = mapping.IPAddress;
            }
        }

        public void UpdateWanMap(WanMapDiff mapDiff)
        {
            foreach (var host in mapDiff.HostsAdded)
            {
                AddWanHost(host);
                WanMap.Hosts.Add(host);
            }

            foreach (var host in mapDiff.HostRemoved)
            {
                //
                WanMap.Hosts.Remove(host);
            }

            foreach (var lanRouter in mapDiff.LanRouterAdded)
            {
                IPAddress address = lanRouter.IPAddress;

                foreach (var control in lanTableLayoutPanel.Controls)
                {
                    var hostControl = (LanHostControl)control;

                    if (address.Equals(hostControl.Host.IPAddress))
                    {
                        hostControl.WanHost = lanRouter;
                    }
                }

                WanMap.LanRouters.Add(lanRouter);
            }

            var addedConnections = mapDiff.ConnectionsAdded;
            
            var immidiateConnections = addedConnections.FindAll(
                connection => 
                WanMap.LanRouters.Exists(
                    router => 
                    router.IPAddress.Equals(connection.Address1) || 
                    router.IPAddress.Equals(connection.Address2)));


            addedConnections = addedConnections.Except(immidiateConnections).ToList();

            foreach (var connection in immidiateConnections)
            {
                WanHost connectedHost, lanRouter = WanMap.GetHostByIPAddress(connection.Address1);
                
                if (lanRouter == null)
                {
                    lanRouter = WanMap.GetHostByIPAddress(connection.Address2);
                    connectedHost = WanMap.GetHostByIPAddress(connection.Address1);
                }
                else
                {
                    connectedHost = WanMap.GetHostByIPAddress(connection.Address2);
                }

                var lanRouterControl = lanTableLayoutPanel.Controls.Cast<LanHostControl>().
                    Where(control => control.IsRouter)
                    .FirstOrDefault(control => control.WanHost.Equals(lanRouter));

                var connectedHostControl = Controls.Cast<Control>().
                    Where(control => control is WanHostControl).
                    Cast<WanHostControl>().
                    FirstOrDefault(control => control.Host.Equals(lanRouter));

                var routerPosition = lanTableLayoutPanel.GetPositionFromControl(lanRouterControl);
                int row = routerPosition.Row, column = routerPosition.Column;

                double teta = 0;

                int otherHostsCount = lanRouter.ConnectedHosts.Count;
                
                int offset = otherHostsCount / 2;
                int sign = otherHostsCount % 2 == 0 ? 1 : -1;

                if (row == 0 && column == 0)
                {
                    teta = -3 * Math.PI / 4;
                }
                else if (row == 0 && column == lanColumns)
                {
                    teta = -Math.PI / 4;
                }
                else if (row == lanRows && column == lanColumns)
                {
                    teta = Math.PI / 4;
                }
                else if (row == lanRows && column == 0)
                {
                    teta = 3 * Math.PI / 4;
                }

                teta += sign * offset * Math.PI / 12;

                var location = lanRouterControl.Location;
                
                var newLocation = location;

                newLocation.X += (int)Math.Cos(teta) * connectionLength;
                newLocation.Y += (int)Math.Sin(teta) * connectionLength;

                connectedHostControl.Location = newLocation;
                angles.Add(connectedHostControl, teta);
            }

            while (immidiateConnections.Count > 0)
            {
                immidiateConnections = addedConnections.FindAll(
                    connection => 
                    WanMap.Hosts.Exists(
                        host => 
                        host.IPAddress.Equals(connection.Address1) || 
                        host.IPAddress.Equals(connection.Address2)));

                addedConnections = addedConnections.Except(immidiateConnections).ToList();

                foreach (var connection in addedConnections)
                {
                    WanHostControl connectedHostControl, hostControl = (WanHostControl)angles.Keys.FirstOrDefault(control => (control as WanHostControl)?.Host?.IPAddress?.Equals(connection.Address1) ?? false);

                    if (hostControl == null)
                    {
                        hostControl = (WanHostControl)angles.Keys.FirstOrDefault(control => (control as WanHostControl)?.Host?.IPAddress?.Equals(connection.Address2) ?? false);
                        connectedHostControl = Controls.Cast<Control>().Where(control => control is WanHostControl).Cast<WanHostControl>().First(control => control.Host.IPAddress.Equals(connection.Address1));
                    }
                    else
                    {
                        connectedHostControl = Controls.Cast<Control>().Where(control => control is WanHostControl).Cast<WanHostControl>().First(control => control.Host.IPAddress.Equals(connection.Address2));
                    }

                    var teta = angles[hostControl];

                    int otherHostsCount = hostControl.Host.ConnectedHosts.Count;

                    int offset = otherHostsCount / 2;
                    int sign = otherHostsCount % 2 == 0 ? 1 : -1;

                    teta += sign * offset * Math.PI / 12;

                    angles.Remove(hostControl);
                    angles.Add(connectedHostControl, teta);

                    var location = hostControl.Location;
                    var newLocation = new Point(location.X + (int)Math.Sin(teta) * connectionLength, location.Y + (int)Math.Cos(teta) * connectionLength);

                    connectedHostControl.Location = newLocation;
                }
            }

            foreach (var lanRouter in mapDiff.LanRouterRemoved)
            {
                //
                WanMap.LanRouters.Remove(lanRouter);
            }

            foreach (var router in mapDiff.WanRoutersAdded)
            {
                MakeWanHostRouter(router);

                WanMap.WanRouters.Add(router);
            }

            foreach (var router in mapDiff.WanRouterRemoved)
            {
                //
                WanMap.WanRouters.Remove(router);
            }

            foreach (var server in mapDiff.DnsServersAdded)
            {
                MakeWanHostServer(server);

                WanMap.DnsServers.Add(server);
            }

            foreach (var server in mapDiff.DnsServersRemoved) 
            {
                //
                WanMap.DnsServers.Remove(server);
            }
        }

        void InitializeLanLayout()
        {
            hostMargin = new Padding(marginLeft, marginTop, marginRight, marginButton);
            hostSize = new Size(sizeWidth, sizeHeight);

            totalHostSize = hostMargin.Size + hostSize;
            
            (lanRows, lanColumns) = lanLayouts[layoutIndex];

            lanTableLayoutPanel.Width = totalHostSize.Width * lanColumns;
            lanTableLayoutPanel.Height = totalHostSize.Height * lanRows;

            lanTableLayoutPanel.Location = Center - ((Size)LanTableLayoutPanelCenter);
            Update();

            layoutIndex++;
        }

        void ChangeLanLayout()
        {
            (int newLanRows, int newLanColumns) = lanLayouts[layoutIndex];

            var temp2D = new LanHostControl[lanRows, lanColumns];

            int count = lanTableLayoutPanel.Controls.Count;

            for (int n = 0, i = 0; i < lanRows; i++) 
            {
                for (int j = 0; j < lanColumns && n <= count; j++, n++) 
                {
                    temp2D[i, j] = (LanHostControl)lanTableLayoutPanel.GetControlFromPosition(j, i);
                }
            }

            lanRows = newLanRows;
            lanColumns = newLanColumns;

            lanTableLayoutPanel.RowCount = lanRows;
            lanTableLayoutPanel.ColumnCount = lanColumns;

            var temp1D = temp2D.Cast<LanHostControl>().ToArray();

            for (int n = 0, i = 0; i < lanRows; i++) 
            {
                for (int j = 0; j < lanColumns && n < count; j++, n++)  
                {
                    lanTableLayoutPanel.Controls.Add(temp1D[n], j, i);
                    lanTableLayoutPanel.Location = new Point();
                }
            }

            currentLanRow = count / lanColumns;
            currentLanColumn = count - currentLanRow * lanColumns;

            lanTableLayoutPanel.Size = new Size(lanColumns * totalHostSize.Width, lanRows * totalHostSize.Height);
            lanTableLayoutPanel.Location = Center - ((Size)LanTableLayoutPanelCenter);

            Update();
            
            layoutIndex++;
        }

        void AddLanHost(LanHost host)
        {
            if (currentLanColumn == lanColumns)
            {
                currentLanColumn = 0;
                currentLanRow++;

                if (currentLanRow == lanRows)
                {
                    ChangeLanLayout();
                    AddLanHost(host);
                }
            }

            var control = new LanHostControl() 
            { 
                Host = host, 
                Margin = hostMargin, 
                Size = hostSize, 
                Name = host.PhysicalAddress.ToString()
            };

            lanTableLayoutPanel.Controls.Add(control, currentLanColumn, currentLanRow);
            lanTableLayoutPanel.Invalidate();
            Update();

            currentLanColumn++;
        }

        void AddWanHost(WanHost host)
        {
            double teta = 2 * Math.PI * random.NextDouble();
            double r = random.Next((int)Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2)) - 600, (int)Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2)) + 600);

            var control = new WanHostControl()
            {
                Host = host,
                Location = new Point((int)(Width / 2 + r * Math.Cos(teta)), (int)(Height / 2 + r * Math.Sin(teta))),
                Name = host.IPAddress.ToString()
            };

            Controls.Add(control);
            Invalidate();
        }

        void MakeLanHostRouter(LanHost host)
        {
            LanHostControl hostControl = (LanHostControl)lanTableLayoutPanel.Controls[host.PhysicalAddress.ToString()], otherHostControl = null;


            if (hostControl.IsServer)
                hostControl.BecomeRouterAndServer();
            else
                hostControl.BecomeRouter();

            var position = lanTableLayoutPanel.GetPositionFromControl(hostControl);

            int row = position.Row, column = position.Column;
            int otherRow = -1, otherColumn = -1;

            void SwapControls()
            {
                lanTableLayoutPanel.Controls.Remove(hostControl);
                lanTableLayoutPanel.Controls.Remove(otherHostControl);

                lanTableLayoutPanel.Controls.Add(hostControl, otherColumn, otherRow);
                lanTableLayoutPanel.Controls.Add(otherHostControl, column, row);

                Invalidate();
            }

            bool PositionFits()
            {
                otherHostControl = (LanHostControl)lanTableLayoutPanel.GetControlFromPosition(otherRow, otherColumn);
                return otherHostControl != null && !otherHostControl.IsRouter;
            }

            if (row != 0 && row == lanRows - 1 && column != 0 && column != lanColumns - 1)
            {
                var corners = new (int Row, int Column)[] { (0, 0), (0, lanColumns - 1), (currentLanRow, lanColumns -1), (currentLanRow, 0) };

                foreach (var (cornerRow, cornerColumn) in corners)
                {
                    otherRow = cornerRow;
                    otherColumn = cornerColumn;
                    
                    if (PositionFits())
                    {
                        SwapControls();
                        return;
                    }
                }

                int maxRows, maxColumn = currentLanRow != 0 ? lanColumns - 1 : currentLanColumn;

                for (otherRow = 0, otherColumn = 0; otherRow < currentLanRow; otherRow++)
                {
                    if (PositionFits())
                    {
                        SwapControls();
                        return;
                    }
                }

                for (otherRow = 0, otherColumn = 0; otherColumn <= maxColumn; otherColumn ++)
                {
                    if (PositionFits())
                    {
                        SwapControls();
                        return;
                    }
                }

                if (currentLanRow != 0)
                {
                    maxRows = currentLanColumn == lanColumns - 1 ? currentLanRow : currentLanRow - 1;

                    for (otherRow = 0, otherColumn = lanColumns - 1; otherRow <= maxRows; otherRow ++)
                    {
                        if (PositionFits())
                        {
                            SwapControls();
                            return;
                        }
                    }
                }

                if (currentLanRow == lanRows - 1)
                {
                    for (otherRow = lanRows - 1, otherColumn = 0; otherRow < currentLanRow; otherRow++)
                    {
                        if (PositionFits())
                        {
                            SwapControls();
                            return;
                        }
                    }
                }
            }
        }

        void MakeWanHostRouter(WanHost host)
        {
            var hostControl = (WanHostControl)lanTableLayoutPanel.Controls[host.IPAddress.ToString()];
            MakeHostControlRouter(hostControl);
        }

        static void MakeHostControlRouter(IHostControl control)
        {
            if (control.IsServer)
                control.BecomeRouterAndServer();
            else
                control.BecomeRouter();
        }

        void MakeLanHostServer(LanHost host)
        {
            var control = (LanHostControl)lanTableLayoutPanel.Controls[host.PhysicalAddress.ToString()];
            MakeHostControlServer(control);
        }

        void MakeWanHostServer(WanHost host)
        {
            var control = (WanHostControl)lanTableLayoutPanel.Controls[host.IPAddress.ToString()];
            MakeHostControlServer(control);
        }

        static void MakeHostControlServer(IHostControl control)
        {
            if (control.IsRouter)
                control.BecomeRouterAndServer();
            else
                control.BecomeServer();
        }
    }
}
