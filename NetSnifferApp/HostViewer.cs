using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using NetSnifferLib.Topology;

namespace NetSnifferApp
{
    public partial class HostViewer : UserControl
    {
        public bool IsFull => hostsCount == MAX_HOST_COUNT;

        public const int ROWS = 3;
        public const int COLUMNS = 3;

        public const int MAX_HOST_COUNT = ROWS * COLUMNS;

        int hostsCount = 0;

        readonly ConcurrentDictionary<LanHost, LanHostControl> controlsByHost = new();
        readonly LanHostControl[,] controls = new LanHostControl[ROWS, COLUMNS];

        public HostViewer()
        {
            InitializeComponent();

            tableLayoutPanel.RowCount = ROWS;
            tableLayoutPanel.ColumnCount = COLUMNS;

            InitHostControls();
            PlaceHostControls();
        }

        public void Clear()
        {
            foreach (var control in controls)
            {
                Controls.Remove(control);
            }

            controlsByHost.Clear();

            InitHostControls();
        }

        void InitHostControls()
        {
            for (int row = 0; row < ROWS; row++) 
            {
                for (int column = 0; column < COLUMNS; column++) 
                {
                    var control = new LanHostControl
                    {
                        Host = null,
                        Visible = false
                    };

                    controls[row, column] = control;

                    Controls.Add(control);
                }
            }
        }

        void PlaceHostControls()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int column = 0; column < COLUMNS; column++)
                {
                    tableLayoutPanel.Controls.Add(controls[row, column], column, row);
                }
            }
        }

        int currentColumn = 0;
        int currentRow = 0;

        public void AddHost(LanHost host)
        {
            if (IsFull)
                throw new InvalidOperationException($"HostViewer is full");

            LanHostControl control;

            lock (controls)
                control = controls[currentRow, currentColumn];

            Invoke((MethodInvoker) delegate
            {
                control.Host = host;
                control.Visible = true;
            });

            if (!controlsByHost.TryAdd(host, control)) 
            {
                throw new ArgumentException($"{nameof(host)} is already in HostViewer");
            }


            Interlocked.Increment(ref hostsCount);
            Interlocked.Increment(ref currentColumn);

            if (currentColumn == 3) 
            {
                Interlocked.Exchange(ref currentColumn, 0);
                Interlocked.Increment(ref currentRow);
            }
        }

        public void RemoveHost(LanHost host)
        {

        }

        public bool ContainsHost(LanHost host)
        {
            return controlsByHost.ContainsKey(host);
        }

        public void AssociateWanHostWithLanHost(LanHost lanHost, WanHost wanHost)
        {
            if (controlsByHost.TryGetValue(lanHost, out LanHostControl control))
            {
                control.WanHost = wanHost;
            }
            else
            {
                throw new ArgumentException($"{nameof(lanHost)} is not in HostViewer");
            }
        }

        public bool IsHostIPAddressShown(LanHost host)
        {
            return controlsByHost[host].IsIPAddressShown;
        }

        public void ShowHostIPAddress(LanHost host)
        {
            controlsByHost[host].ShowIPAddress();
        }

        public void HideHostIPAddress(LanHost host)
        {
            controlsByHost[host].HideIPAddress();
        }

        public void MakeHostRouter(LanHost host)
        {
            controlsByHost[host].BecomeRouter();
        }

        public void MakeHostServer(LanHost host)
        {
            controlsByHost[host].BecomeServer();
        }
    }
}
