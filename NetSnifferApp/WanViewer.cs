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
    public partial class WanViewer : UserControl
    {
        readonly Dictionary<WanHost, WanHostControl> hostControls = new();
        readonly List<WanHost> routers = new();

        readonly List<(WanHost, WanHost)> connections = new();

        int numberOfLanRouters = 0;

        int centerX;
        int centerY;

        int minRadious = 100;
        int maxRadious = 700;

        readonly Random random = new(10);

        public WanViewer()
        {
            InitializeComponent();

            centerX = Width / 2;
            centerY = Height/ 2;
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

        public void AddHost(WanHost host)
        {
            var control = new WanHostControl
            {
                Host = host
            };
            Controls.Add(control);

            hostControls.Add(host, control);
            

            PlaceHost(control);
        }

        public void RemoveHost(WanHost host)
        {
            if (!routers.Contains(host))
            {
                hostControls.Remove(host, out WanHostControl control);
                Controls.Remove(control);
            }
        }

        public void AddLanRouter(WanHost host)
        {
            routers.Add(host);
            hostControls.Remove(host);

            numberOfLanRouters++;

            var control = new WanHostControl
            {
                Host = host
            };
            control.BecomeRouter();
            hostControls.Add(host, control);

            PlaceLanRouter(control);
        }

        public void MakeHostWanRouter(WanHost host)
        {
            hostControls[host].BecomeRouter();
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
            Controls.Add(control);

            var location = GetRandomLocationForHost(control);
            var borderLocation = location + control.Size;

            control.Location = location;

            while (location.X < 0 || borderLocation.X > Width || location.Y < 0 || borderLocation.Y > Height || IsInOtherControl(control).Count > 0)
            {
                location = GetRandomLocationForHost(control);
                borderLocation = location + control.Size;

                control.Location = location;
            }

            var hidden = IsInOtherControl(control);

            control.Location = location;

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

        List<WanHostControl> IsInOtherControl(WanHostControl control)
        {
            /*            var borderLocation = location + control.Size;

                        return hostControls.Values.Where(otherControl =>
                        {
                            if (otherControl == control)
                                return false;

                            var otherLocation = otherControl.Location;
                            var otherBordeLocation = otherLocation + otherControl.Size;

                            return 
                            (otherBordeLocation.X > borderLocation.X && otherLocation.X - location.X < control.Width) || 
                            (borderLocation.X > otherBordeLocation.X && location.X - otherLocation.X < otherControl.Width) ||
                            (otherBordeLocation.Y > borderLocation.Y && otherLocation.Y - location.Y < control.Height) || 
                            (borderLocation.Y > otherBordeLocation.Y && location.Y - otherLocation.Y < otherControl.Height);
                        }).ToList();*/


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
            Controls.Add(control);

            control.Location = GetRandomLocationForLanRouter(control);

            ResumeLayout();
        }

        private void WanViewer_Resize(object sender, EventArgs e)
        {
            centerX = Width / 2;
            centerY = Height / 2;

            label1.Location = new Point(centerX - label1.Width / 2, centerY - label1.Height / 2);
        }
    }
}
