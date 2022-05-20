using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Topology;
using NetSnifferLib.StatefulAnalysis.Tcp;

namespace NetSnifferApp
{
    public partial class TcpStreamsForm : Form
    {
        WanHost _host;

        readonly Dictionary<TcpConnection, ListViewItem> items = new();

        public WanHost Host 
        {
            get => _host;
            set
            {
                _host = value;
                ipAddressLabel.Text = _host.IPAddress.ToString();
            }
        }

        public TcpStreamsForm()
        {
            InitializeComponent();

            streamsListView.Sorting = SortOrder.Ascending;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            StartRefreshing();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            StopRefreshing();
        }

        public void StartRefreshing()
        {
            refreshTimer.Start();
        }

        public void StopRefreshing()
        {
            refreshTimer.Stop();
        }

        Task RefreshConnectionsAsync()
        {
            return Task.Run(() => RefreshConnections());
        }

        void RefreshConnections()
        {
            var connections = _host.TcpConnectionsAsReadOnly;

            var newConnections = connections.Except(items.Keys).ToList();
            var oldConnections = items.Keys.Intersect(connections).ToList();

            foreach (var conn in newConnections)
            {
                int port;
                int remotePort;
                uint dataSent;
                uint dataReceived;
                IPAddress remoteAddress;

                TcpConnectionStatus status = conn.Status;

                if (conn.ConnectorEndPoint.Address.Equals(_host.IPAddress))
                {
                    port = conn.ConnectorEndPoint.Port;

                    remoteAddress = conn.ListenerEndPoint.Address;
                    remotePort = conn.ListenerEndPoint.Port;

                    dataSent = conn.DataSentByConnector;
                    dataReceived = conn.DataSentByListener;
                }
                else
                {
                    port = conn.ListenerEndPoint.Port;

                    remoteAddress = conn.ConnectorEndPoint.Address;
                    remotePort = conn.ConnectorEndPoint.Port;

                    dataSent = conn.DataSentByListener;
                    dataReceived = conn.DataSentByConnector;
                }

                ListViewItem newItem = null;

                Invoke(new MethodInvoker(() =>
                newItem = new ListViewItem(
                    new[] {
                        port.ToString(),
                        "->",
                        remotePort.ToString(),
                        remoteAddress.ToString(),
                        dataSent.ToString(),
                        dataReceived.ToString(),
                        status switch
                        {
                            TcpConnectionStatus.Syn or TcpConnectionStatus.SynAck => "Opening",
                            TcpConnectionStatus.Established => "Established",
                            TcpConnectionStatus.Fin or TcpConnectionStatus.FinAck => "Closing",
                            TcpConnectionStatus.Closed => "Closed",
                            TcpConnectionStatus.Rst => "Reseted",
                            _ => string.Empty
                        }
                    }
                    )
                {
                    Name = port.ToString()
                }));

                items.Add(conn, newItem);
                streamsListView.Invoke(
                new MethodInvoker(() =>
                { 
                    //streamsListView.SuspendLayout();
                    streamsListView.Items.Add(newItem);
                    //streamsListView.ResumeLayout();
                }));
            }

            foreach (var conn in oldConnections)
            {
                uint dataSent;
                uint dataReceived;
                ListViewItem item;

                string statusString = conn.Status switch
                {
                    TcpConnectionStatus.Syn or TcpConnectionStatus.SynAck => "Opening",
                    TcpConnectionStatus.Established => "Established",
                    TcpConnectionStatus.Fin or TcpConnectionStatus.FinAck => "Closing",
                    TcpConnectionStatus.Closed => "Closed",
                    TcpConnectionStatus.Rst => "Reseted",
                    _ => string.Empty
                };

                if (conn.ConnectorEndPoint.Address.Equals(_host.IPAddress))
                {
                    dataSent = conn.DataSentByConnector;
                    dataReceived = conn.DataSentByListener;
                    item = (ListViewItem)Invoke(new Func<ListViewItem>(() => streamsListView.Items[conn.ConnectorEndPoint.Port.ToString()]));
                }
                else
                {
                    dataSent = conn.DataSentByListener;
                    dataReceived = conn.DataSentByConnector;
                    item = (ListViewItem)Invoke(new Func<ListViewItem>(() => streamsListView.Items[conn.ListenerEndPoint.Port.ToString()]));
                }

                streamsListView.Invoke(
                    new MethodInvoker(() =>
                    {
                        if (item.SubItems[4].Text != dataSent.ToString())
                        //{
                            item.SubItems[4].Text = dataSent.ToString();
                        //streamsListView.Invalidate();
                        //}

                        if (item.SubItems[5].Text != dataReceived.ToString())
                        //{
                            item.SubItems[5].Text = dataReceived.ToString();
                        //streamsListView.Invalidate();
                        //}

                        if (item.SubItems[6].Text != statusString)
                        //{
                            item.SubItems[6].Text = statusString;
                        //streamsListView.Invalidate();
                        //}
                    }));
            }

            foreach (var conn in items.Keys)
            {
                if (!connections.Contains(conn))
                {
                    items.Remove(conn, out ListViewItem item);
                    streamsListView.Invoke(new MethodInvoker(() => streamsListView.Items.Remove(item)));
                }
            }
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            refreshTimer.Stop();
            RefreshConnections();
            refreshTimer.Start();
        }
    }
}
