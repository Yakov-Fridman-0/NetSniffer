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

        public void StartRefreshing()
        {
            refreshTimer.Start();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            var connections = _host.TcpConnectionsAsReadOnly;

            var newConnections = connections.Except(items.Keys).ToList();
            var oldConnections = items.Keys.Intersect(connections).ToList();
            //var connectionsRemoved = items.Keys.Except(connections).ToList();

            
            foreach (var conn in newConnections)
            {
                int port;
                int remotePort;
                uint dataSent;
                uint dataReceived;
                IPAddress remoteAddress;

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

                var newItem = new ListViewItem(new[] { port.ToString(), "->", remoteAddress.ToString(), remotePort.ToString(), dataSent.ToString(), dataReceived.ToString() })
                {
                    Name = port.ToString()
                };

                items.Add(conn, newItem);

                streamsListView.SuspendLayout();
                streamsListView.Items.Add(newItem);
                streamsListView.ResumeLayout();
            }

            foreach (var conn in oldConnections)
            {
                uint dataSent;
                uint dataReceived;
                ListViewItem item;

                if (conn.ConnectorEndPoint.Address.Equals(_host.IPAddress))
                {
                    dataSent = conn.DataSentByConnector;
                    dataReceived = conn.DataSentByListener;
                    item = streamsListView.Items[conn.ConnectorEndPoint.Port.ToString()];
                }
                else
                {
                    dataSent = conn.DataSentByListener;
                    dataReceived = conn.DataSentByConnector;
                    item = streamsListView.Items[conn.ListenerEndPoint.Port.ToString()];
                }

               

                if (item.SubItems[4].Text != dataSent.ToString())
                {
                    item.SubItems[4].Text = dataSent.ToString();
                    streamsListView.Invalidate();
                }

                if (item.SubItems[5].Text != dataReceived.ToString())
                {
                    item.SubItems[5].Text = dataReceived.ToString();                    
                    streamsListView.Invalidate();
                }
            }


            foreach (var conn in items.Keys)
            {
                if (!connections.Contains(conn))
                {
                    items.Remove(conn, out ListViewItem item);
                    streamsListView.Items.Remove(item);
                }
            }


        }
    }
}
