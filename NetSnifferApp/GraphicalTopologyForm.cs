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

namespace NetSnifferApp
{
    public partial class GraphicalTopologyForm : Form
    {
        public GraphicalTopologyForm()
        {
            InitializeComponent();
        }

        public EventHandler UpdateRequested;

        public void UpdateLanMap(LanMap map)
        {
            LanMapDiff mapDiff = map.GetDiff(topologyViewer.LanMap);

            if (!mapDiff.IsEmpty)
                topologyViewer.UpdateLanMap(mapDiff);
        }

        public void UpdateWanMap(WanMap map)
        {
            WanMapDiff mapDiff = map.GetDiff(topologyViewer.WanMap);

            if (!mapDiff.IsEmpty)
                topologyViewer.UpdateWanMap(mapDiff);

        }

        private void updateTimer_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(() => UpdateRequested?.Invoke(this, EventArgs.Empty));
        }
    }
}
