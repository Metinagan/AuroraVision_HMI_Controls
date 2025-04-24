using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using HMI;
using System.Windows.Media;
using System.Linq;
using System.Drawing;
using AuroraVision_Controls.Properties;
using System.ComponentModel;

namespace AuroraVision_Controls
{

    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(signalChart), "Icons.vibration.png")]
    public partial class signalChart : UserControl
    {
        //ChartSetting
        private int _chartLineSmoothness = 1;
        private int _chartStrokeThickness = 3;
        private int _chartPointGeometrySize = 3;
        private decimal _centerY = 0;  // centerY'nin başlangıç değeri 0
        private decimal _chartMaxY = 5;
        private decimal _chartMinY = -5;
        private int _chartFontSize = 12;

        private const int MAX_POINTS = 100;
        private System.Drawing.Color _backColor;
        private System.Drawing.Color _foreColor;
        private List<ChartValues<double>> _chartValuesList = new List<ChartValues<double>>();
        private SeriesCollection _seriesCollection = new SeriesCollection();

        //data1
        private decimal _data1;
        private String _data1Label = "data1";
        private System.Drawing.Color _data1Color = System.Drawing.Color.Cyan;
        //data2
        private decimal _data2;
        private String _data2Label = "data2";
        private System.Drawing.Color _data2Color;
        //data3
        private decimal _data3;
        private String _data3Label = "data3";
        private System.Drawing.Color _data3Color;
        //data4
        private decimal _data4;
        private String _data4Label = "data4";
        private System.Drawing.Color _data4Color;
        //data5
        private decimal _data5;
        private String _data5Label = "data5";
        private System.Drawing.Color _data5Color;

        public signalChart()
        {
            InitializeComponent();
            InitializeChart();
            chart1.DisableAnimations = true;
            //chart1.AnimationsSpeed = TimeSpan.FromMilliseconds(1);  
            chart1.Invalidate();
        }

        private void InitializeChart()
        {
            chart1.Series = _seriesCollection;
            AddNewSignal(_data1Color.ToMediaColor()); // data1 için
            AddNewSignal(System.Windows.Media.Colors.Magenta); // data2 için
            AddNewSignal(System.Windows.Media.Colors.Red); // data3 için
            AddNewSignal(System.Windows.Media.Colors.Yellow); // data4 için
            AddNewSignal(System.Windows.Media.Colors.Orange); // data5 için

            chart1.AxisX.Add(new Axis
            {
                Title = "Zaman",
                Labels = Enumerable.Range(0, MAX_POINTS).Select(i => i.ToString()).ToArray(),
                Separator = new Separator { StrokeThickness = 1 }
            });

            chart1.AxisY.Add(new Axis
            {
                Title = "Değer",
                LabelFormatter = value => value.ToString("0.0000"),
                Separator = new Separator { StrokeThickness = 1 }
            });

            // Yalnızca Y ekseninde zoom yapılmasını sağlıyoruz
            chart1.Zoom = ZoomingOptions.Y;

            UpdateYAxis();
        }

        public void AddNewSignal(System.Windows.Media.Color color)
        {
            var chartValues = new ChartValues<double>();
            _chartValuesList.Add(chartValues);

            _seriesCollection.Add(new LineSeries
            {
                Values = chartValues,
                LineSmoothness = chartLineSmoothness,
                StrokeThickness = chartStrokeThickness,
                PointGeometrySize = chartPointGeometrySize,
                PointGeometry = DefaultGeometries.Circle,
                Stroke = new SolidColorBrush(color),
                Foreground = new SolidColorBrush(Colors.White)
                
            });
        }

        public void UpdateSignal(int index, decimal newValue)
        {
            if (index < 0 || index >= _chartValuesList.Count) return;

            var values = _chartValuesList[index];
            
            if (values.Count >= MAX_POINTS)
                values.RemoveAt(0);
            
            values.Add(Convert.ToDouble(newValue));
            _seriesCollection[index].Values = values;
            chart1.Invalidate();
        }


        //Data1
        [HMIPortProperty(HMIPortDirection.Input)]
        public decimal data1
        {
            get { return _data1; }
            set
            {
                _data1 = value;
                UpdateSignal(0, value);
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data1 Color")]
        public System.Drawing.Color data1Color
        {
            get { return _data1Color; }
            set 
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[0] is LineSeries lineSeries)
                {
                    lineSeries.Stroke = new SolidColorBrush(value.ToMediaColor());
                    
                }
                chart1.Invalidate();
            }
        }
        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data1 Label")]
        public String data1Label
        {
            get { return _data1Label; }
            set 
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[0] is LineSeries lineSeries)
                {
                   lineSeries.Title = value;

                }
                chart1.Invalidate();
            }
        }


        //Data2
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public decimal data2
        {
            get { return _data2; }
            set
            {
                _data2 = value;
                UpdateSignal(1, value);
                chart1.Invalidate();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data2 Color")]
        public System.Drawing.Color data2Color
        {
            get { return _data1Color; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[1] is LineSeries lineSeries)
                {
                    lineSeries.Stroke = new SolidColorBrush(value.ToMediaColor());
                }
            }
        }
        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data2 Label")]
        public String data2Label
        {
            get { return _data2Label; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[1] is LineSeries lineSeries)
                {
                    lineSeries.Title = value;

                }
            }
        }

        //data3
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public decimal data3
        {
            get { return _data3; }
            set
            {
                _data3 = value;
                UpdateSignal(2, value);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data3 Color")]
        public System.Drawing.Color data3Color
        {
            get { return _data1Color; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[2] is LineSeries lineSeries)
                {
                    lineSeries.Stroke = new SolidColorBrush(value.ToMediaColor());
                }
            }
        }
        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data3 Label")]
        public String data3Label
        {
            get { return _data1Label; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[2] is LineSeries lineSeries)
                {
                    lineSeries.Title = value;

                }
            }
        }

        //Data4
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public decimal data4
        {
            get { return _data4; }
            set
            {
                _data4 = value;
                UpdateSignal(3, value);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data4 Color")]
        public System.Drawing.Color data4Color
        {
            get { return _data1Color; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[3] is LineSeries lineSeries)
                {
                    lineSeries.Stroke = new SolidColorBrush(value.ToMediaColor());
                }
            }
        }
        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data4 Label")]
        public String data4Label
        {
            get { return _data1Label; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[3] is LineSeries lineSeries)
                {
                    lineSeries.Title = value;

                }
            }
        }
        //Data5
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public decimal data5
        {
            get { return _data5; }
            set
            {
                _data5 = value;
                UpdateSignal(4, value);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data5 Color")]
        public System.Drawing.Color data5Color
        {
            get { return _data1Color; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[4] is LineSeries lineSeries)
                {
                    lineSeries.Stroke = new SolidColorBrush(value.ToMediaColor());
                }
            }
        }
        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data5 LAbel")]
        public String data5Label
        {
            get { return _data1Label; }
            set
            {
                if (_seriesCollection.Count > 0 && _seriesCollection[4] is LineSeries lineSeries)
                {
                    lineSeries.Title = value;

                }
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Line-Chart Back Color")]
        public System.Drawing.Color signalChartBackColor
        {
            get { return _backColor; }
            set { _backColor = value; chart1.BackColor = value; }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Line-Chart Text Color")]
        public System.Drawing.Color signalChartForeColor
        {
            get { return _foreColor; }
            set 
            { 
                _foreColor = value;
                var mediaColor = value.ToMediaColor();
                chart1.AxisX[0].Foreground = new SolidColorBrush(mediaColor);
                chart1.AxisY[0].Foreground = new SolidColorBrush(mediaColor);
                chart1.ForeColor = value; 
                chart1.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Line Smoothness")]
        public int chartLineSmoothness
        {
            get { return _chartLineSmoothness; }
            set
            {
                _chartLineSmoothness = value;
                foreach (var series in _seriesCollection)
                {
                    if (series is LineSeries lineSeries)
                    {
                        lineSeries.LineSmoothness = value;
                    }
                }
                chart1.Invalidate();
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Line Thickness")]
        public int chartStrokeThickness
        {
            get { return _chartStrokeThickness; }
            set
            {
                _chartStrokeThickness = value;
                foreach (var series in _seriesCollection)
                {
                    if (series is LineSeries lineSeries)
                    {
                        lineSeries.StrokeThickness = value;
                    }
                }
                chart1.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("changes the Size of the Points at the Corners of the Line")]
        public int chartPointGeometrySize
        {
            get { return _chartPointGeometrySize; }
            set
            {
                _chartPointGeometrySize = value;
                foreach (var series in _seriesCollection)
                {
                    if (series is LineSeries lineSeries)
                    {
                        lineSeries.PointGeometrySize = value;
                    }
                }
                chart1.Invalidate();
            }
        }

        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public decimal centerY
        {
            get { return _centerY; }
            set
            {
                _centerY = value;
                UpdateYAxis();  // centerY değiştiğinde Y eksenini güncelle
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Change the maximum note on the y-axis")]
        public decimal chartYMax
        {
            get { return _chartMaxY; }
            set
            {
                _chartMaxY = value;
                UpdateYAxis();  // centerY değiştiğinde Y eksenini güncelle
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Change the minimum note on the y-axis")]
        public decimal chartYMib
        {
            get { return _chartMinY; }
            set
            {
                _chartMinY = value;
                UpdateYAxis();  // centerY değiştiğinde Y eksenini güncelle
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Change the Line-Chart Text Size")]
        public int chartFontSize
        {
            get { return _chartFontSize; }
            set
            {
                _chartFontSize = value;
                chart1.AxisX[0].FontSize = value;
                chart1.AxisY[0].FontSize = value;
                chart1.Invalidate();
            }
        }



        private void UpdateYAxis()
        {
            chart1.AxisY[0].MinValue = Convert.ToDouble(_chartMinY);
            chart1.AxisY[0].MaxValue = Convert.ToDouble(_chartMaxY);
            chart1.Invalidate();
        }
    }
}