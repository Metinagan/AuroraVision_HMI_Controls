using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HMI;
using Newtonsoft.Json.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    public partial class PointChart: UserControl
    {
        //Point list
        private List<PointF> points = new List<PointF>();
        private Point _lastPoint;

        //Chart Size Settings
        private int _printWidth = 500;
        private int _printHeight = 0;

        //Other Settings
        private int _textSize = 8;
        private int _pointSize = 8;
        private Boolean _showGrid = true;

        //Color Settings
        private Color _pointColor = Color.Red;
        private Color _backgroundColor = Color.White;
        private Color _foreColor = Color.Black;
        private Color _chartBackColor = Color.White;

        //Interval Values
        private int _axisXInterval = 100;
        private int _axisYInterval = 100;

        //Point Values
        private int _pointX = 0;
        private int _pointY = 0;

        //Title texts
        private String _axisXTitle = "";
        private String _axisYTitle = "";

        private MarkerStyle _chartMarkerStyle = MarkerStyle.Circle;
        public PointChart()
        {
            InitializeComponent();
            chart1.Invalidate();
            this.Size = new Size(500, 500);
            chart1.ChartAreas[0].AxisY.Interval = _axisXInterval;
            chart1.ChartAreas[0].AxisX.Interval = _axisYInterval;
            chart1.Series[0].MarkerStyle = _chartMarkerStyle;
            
        }

        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public int PointX
        {
            get { return _pointX; }
            set
            {
                _pointX = value;

                if (_pointX != null && PointY != null)
                {
                    _lastPoint.X = _pointX;
                    _lastPoint.Y = _pointY;

                    if (!points.Contains(_lastPoint))
                    {
                        points.Add(_lastPoint);
                        chart1.Series["Series1"].Points.AddXY(_pointX, _pointY);
                        chart1.Invalidate();
                    }
                }
            }

        }
        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public int PointY
        {
            get { return _pointY; }
            set
            {
                _pointY = value;

            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Chart GridLine Enabled")]
        public Boolean ShowGrid
        {
            get { return _showGrid; }
            set
            {
                _showGrid = value;
                if (_showGrid == true)
                {
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
                }
                else
                {
                    chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
                    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Chart Point Size")]
        public int PointSize
        {
            get { return _pointSize; }
            set
            {
                _pointSize = value;
                chart1.Series["Series1"].MarkerSize = _pointSize;

            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Chart Text Size")]
        public int TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
                chart1.ChartAreas[0].AxisX.LabelStyle.Font = new Font("Arial", _textSize);
                chart1.ChartAreas[0].AxisY.LabelStyle.Font = new Font("Arial", _textSize);
                chart1.ChartAreas[0].AxisX.TitleFont = new Font("Arial", _textSize);
                chart1.ChartAreas[0].AxisY.TitleFont = new Font("Arial", _textSize);

            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Chart Text Color")]
        public Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = _foreColor;
                chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = _foreColor;
                chart1.ChartAreas[0].AxisX.TitleForeColor = _foreColor;
                chart1.ChartAreas[0].AxisY.TitleForeColor = _foreColor;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Chart Points Color")]
        public Color PointColor
        {
            get { return _pointColor; }
            set
            {
                _pointColor = value;
                chart1.Series["Series1"].Color = _pointColor;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Change the Chart Background Color")]
        public Color BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;
                chart1.BackColor = _backgroundColor;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Change the Chart Background Color")]
        public Color ChartBackColor
        {
            get { return _chartBackColor; }
            set
            {
                _chartBackColor = value;
                chart1.ChartAreas[0].BackColor = _chartBackColor;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Axis Y Interval Value")]
        public int AxisYInterval
        {
            get { return _axisYInterval; }
            set
            {
                _axisYInterval = value;
                chart1.ChartAreas[0].AxisY.Interval = _axisYInterval;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Axis X Interval Value")]
        public int AxisXInterval
        {
            get { return _axisXInterval; }
            set
            {
                _axisXInterval = value;
                chart1.ChartAreas[0].AxisX.Interval = _axisXInterval;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Print Width")]
        public int PrintWidth
        {
            get { return _printWidth; }
            set
            {
                _printWidth = value;
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisX.Maximum = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Title Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Axis X Title")]
        public String AxisXTitle
        {
            get { return _axisXTitle; }
            set
            {
                _axisXTitle = value;
                chart1.ChartAreas[0].AxisX.Title = _axisXTitle;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Title Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Axis Y Title")]
        public String AxisYTitle
        {
            get { return _axisYTitle; }
            set
            {
                _axisYTitle = value;
                chart1.ChartAreas[0].AxisY.Title = _axisYTitle;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Chart Point Style")]
        public MarkerStyle ChartMarkerStyle
        {
            get
            {
                return _chartMarkerStyle;
            }
            set
            {
                _chartMarkerStyle = value;
                chart1.Series[0].MarkerStyle = _chartMarkerStyle;
                chart1.Invalidate();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public int PrintHeight
        {
            get { return _printHeight; }
            set
            {
                if (value < 400)
                {
                    _printHeight = 500;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Maximum = 500;
                }
                else
                {
                    _printHeight = value;
                    chart1.ChartAreas[0].AxisY.Minimum = 0;
                    chart1.ChartAreas[0].AxisY.Maximum = value + 100;
                }

            }
        }

    }
}
