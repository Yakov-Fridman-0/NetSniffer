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

namespace NetSnifferApp
{
    public partial class InterfaceInfo : UserControl
    {
        NetworkInterface _interface = null;

        public NetworkInterface Interface 
        {
            get => _interface;
          
            set
            {
                 _interface = value;

                if (_interface != null)
                {
                    nameLabel.Text = _interface.Name;
                    idLabel.Text = _interface.Id;
                    desciptionLabel.Text = _interface.Description;

                    intercaeTypeLabel.Text = _interface.NetworkInterfaceType.ToString();

                    physicalAddressLabel.Text = _interface.GetPhysicalAddress().ToString();

                    var properties = _interface.GetIPProperties();

                    var address = properties.UnicastAddresses.Select(information => information.Address);
                    ipV4AddressesLabel.Text = string.Join(", ", address.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
                    ipV6AddressesLabel.Text = string.Join(", ", address.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6));

                    var gateways = properties.GatewayAddresses.Select(information => information.Address);
                    gatewaysLabel.Text = string.Join(", ", gateways.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
                    ipV6GatewaysLabel.Text = string.Join(", ", gateways.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6));

                    var dhcpServers = properties.DhcpServerAddresses;
                    dhcpServersLabel.Text = string.Join(", ", dhcpServers.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
                    ipV6DhcpServersLabel.Text = string.Join(", ", dhcpServers.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6));


                    var dnsServers = properties.DnsAddresses;
                    dnsServersLabel.Text = string.Join(", ", dnsServers.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork));
                    ipV6DnsServersLabel.Text = string.Join(", ", dnsServers.Where(address => address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6));


                    switch (_interface.OperationalStatus)
                    {
                        case OperationalStatus.Up:
                            warningLabel.Visible = false;
                            break;
                        case OperationalStatus.Testing:
                            warningLabel.Visible = true;
                            warningLabel.ForeColor = Color.Yellow;
                            warningLabel.Text = "Intercase is running tests";
                            break;
                        case OperationalStatus.Down or OperationalStatus.LowerLayerDown or OperationalStatus.Dormant or OperationalStatus.NotPresent:
                            warningLabel.Visible = true;
                            warningLabel.ForeColor = Color.Red;
                            warningLabel.Text = "Interface isn't operational";
                            break;
                        case OperationalStatus.Unknown:
                            warningLabel.Visible = true;
                            warningLabel.ForeColor = Color.Yellow;
                            warningLabel.Text = "Interface status is unknown";
                            break;
                    }
                }
                else
                {
                    nameLabel.Text = "No name";
                    desciptionLabel.Text = "No Description";

                    intercaeTypeLabel.Text = "N/A";

                    physicalAddressLabel.Text = "00:00:00:00:00:00";

                    ipV4AddressesLabel.Text = "0.0.0.0";
                    dnsServersLabel.Text = "0.0.0.0";
                    dhcpServersLabel.Text = "0.0.0.0";
                    gatewaysLabel.Text = "0.0.0.0";

                    ipV6AddressesLabel.Text = "0:::::::";
                    ipV6GatewaysLabel.Text = "0:::::::";
                    ipV6DhcpServersLabel.Text = "0:::::::";
                    ipV6DnsServersLabel.Text = "0:::::::";

                    warningLabel.Visible = false;
                }
            }
        }

        public InterfaceInfo()
        {
            InitializeComponent();
        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void intercaeTypeLabel_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
