using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Topology;
using NetSnifferLib.Analysis;

namespace NetSnifferApp
{
    public partial class TcpStreamsForm : Form
    {
        public TcpStreamsForm()
        {
            InitializeComponent();
        }

        private void hostsComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            connectionsListView.Items.Clear();

            var host = (WanHost)hostsComboBox.SelectedItem;
            
            if (host != null)
            {
                foreach (var (port, connection) in host.ConnectionManager.connections)
                {
                    if (connection == null)
                        return;

                    if (connection.ReceiverEndPoint.Address.Equals(host.IPAddress))
                    {
                        connectionsListView.Items.Add(new ListViewItem(new[]
                        {
                        port.ToString(),
                        connection.ReceiverEndPoint.Address.ToString(),
                        connection.ReceiverEndPoint.Port.ToString(),
                        connection.DataSent.ToString(),
                        connection.DataReceived.ToString()
                        })
                        {
                            Name = port.ToString()
                        });
                    }
                    else
                    {
                        connectionsListView.Items.Add(new ListViewItem(new[]
{
                        port.ToString(),
                        connection.SenderEndPoint.Address.ToString(),
                        connection.SenderEndPoint.Port.ToString(),
                        connection.DataSent.ToString(),
                        connection.DataReceived.ToString()
                        })
                        {
                            Name = port.ToString()
                        });
                    }

                }
            }
        }

        private void TcpStreamsForm_Load(object sender, EventArgs e)
        {
            hostsComboBox.Items.AddRange(PacketAnalyzer.GetOriginalWanHosts().ToArray());
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var newHosts = PacketAnalyzer.GetOriginalWanHosts();
            var hosts = hostsComboBox.Items.Cast<WanHost>();

            foreach (var newHost in newHosts)
            {
                if (hosts.Contains(newHost))
                {
                    if (hostsComboBox.SelectedItem == newHost)
                    {
                        foreach (var (port, connection) in newHost.ConnectionManager.connections)
                        {
                            if (connection == null)
                                return;

                            if (connection.ReceiverEndPoint.Address.Equals(newHost.IPAddress))
                            {
                                connectionsListView.Items.Add(new ListViewItem(new[]
                                {
                        port.ToString(),
                        connection.ReceiverEndPoint.Address.ToString(),
                        connection.ReceiverEndPoint.Port.ToString(),
                        connection.DataSent.ToString(),
                        connection.DataReceived.ToString()
                        })
                                {
                                    Name = port.ToString()
                                });
                            }
                            else
                            {
                                connectionsListView.Items.Add(new ListViewItem(new[]
        {
                        port.ToString(),
                        connection.SenderEndPoint.Address.ToString(),
                        connection.SenderEndPoint.Port.ToString(),
                        connection.DataSent.ToString(),
                        connection.DataReceived.ToString()
                        })
                                {
                                    Name = port.ToString()
                                });
                            }
                        }
                    }
                }
                else
                {
                    hostsComboBox.Items.Add(newHost);
                }
            }

            foreach (var host in hosts)
            {
                if (!newHosts.Contains(host))
                    hostsComboBox.Items.Remove(host);
            }
        }
    }
}
