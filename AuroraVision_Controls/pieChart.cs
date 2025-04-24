using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HMI;
using LiveCharts;
using LiveCharts.Wpf;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(pieChart),"Icons.pie-chart.png")]
    public partial class pieChart : UserControl
    {
        private SeriesCollection seriesCollection = new SeriesCollection(); // Serileri tutacak koleksiyon

        private Color _backColor = Color.Black;
        private Color _foreColor = Color.White;

        private int _innerRadiusValue = 0;
        private int _pushOutValue = 8;

        private int _data1 = 50;
        private string _label1 = "data1";
        private Color _color1 = Color.Blue;

        private int _data2 = 50;
        private string _label2 = "data2";
        private Color _color2 = Color.Green;

        private int _data3;
        private string _label3 = "data3";
        private Color _color3 = Color.Red;

        private int _data4;
        private string _label4 = "data4";
        private Color _color4 = Color.Yellow;

        private int _data5;
        private string _label5 = "data5";
        private Color _color5 = Color.Purple;

        public pieChart()
        {
            InitializeComponent();
            InitializePieChart();
            UpdateChart();
            pieChart1.DisableAnimations=true;
        }

        private void InitializePieChart()
        {
            if (pieChart1 != null)
            {
                pieChart1.Series = seriesCollection;
                pieChart1.DataTooltip = null;
                pieChart1.HoverPushOut = 0;
                pieChart1.InnerRadius = _innerRadiusValue;
                
            }
        }

        /// <summary>
        /// Yeni veri ekleme fonksiyonu
        /// </summary>
        private void UpdateChart()
        {
            if (pieChart1 == null) return;

            // Önce eski verileri temizle
            ClearData();

            if (_data1 > 0 && _label1 != null)
            {
                AddData(Convert.ToDouble(_data1), _label1, _color1);
            }

            if (_data2 > 0 && _label2 != null)
            {
                AddData(Convert.ToDouble(_data2), _label2, _color2);
            }

            if (_data3 > 0 && _label3 != null)
            {
                AddData(Convert.ToDouble(_data3), _label3, _color3);
            }

            if (_data4 > 0 && _label4 != null)
            {
                AddData(Convert.ToDouble(_data4), _label4, _color4);
            }

            if (_data5 > 0 && _label5 != null)
            {
                AddData(Convert.ToDouble(_data5), _label5, _color5);
            }


            // Güncellenmiş verileri PieChart’a ata
            pieChart1.Series = seriesCollection;
        }

        public void AddData(double value, string label, Color? color = null)
        {
            if (pieChart1 == null) return;

            PieSeries newSeries = new PieSeries
            {
                Values = new ChartValues<double> { value },
                Title = label,
                DataLabels = true, // 📌 Dilimin içine etiket ekle
                LabelPoint = chartPoint => $"{chartPoint.SeriesView.Title} ({chartPoint.Participation:P1})",
                FontSize = 12,
                PushOut = _pushOutValue,
                Fill = color.HasValue ? new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B))
                    : null
            };

            seriesCollection.Add(newSeries);
        }


        public void ClearData()
        {
            if (pieChart1 == null) return;
            seriesCollection.Clear();
        }

        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data1 Label")]
        public string label1
        {
            get { return _label1; }
            set
            {
                _label1 = value;
                UpdateChart();
            }
        }

        [HMIPortProperty(HMIPortDirection.Input)]
        public int data1
        {
            get { return _data1; }
            set
            {
                _data1 = value;
                UpdateChart(); // Veri değiştiğinde güncelle
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data1 Color")]
        public Color color1
        {
            get { return _color1; }
            set { _color1 = value; UpdateChart(); }
        }

        [HMIPortProperty(HMIPortDirection.Input)]
        public int data2
        {
            get { return _data2; }
            set
            {
                _data2 = value;
                UpdateChart(); // Veri değiştiğinde güncelle
            }
        }

        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data2 Label")]
        public string label2
        {
            get { return _label2; }
            set
            {
                _label2 = value;
                UpdateChart();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data2 Color")]
        public Color color2
        {
            get { return _color2; }
            set { _color2 = value; UpdateChart(); }
        }


        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public int data3
        {
            get { return _data3; }
            set
            {
                _data3 = value;
                UpdateChart(); 
            }
        }

        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data3 Label")]
        public string label3
        {
            get { return _label3; }
            set
            {
                _label3 = value;
                UpdateChart();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data3 Color")]
        public Color color3
        {
            get { return _color3; }
            set { _color3 = value; UpdateChart(); }
        }

        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public int data4
        {
            get { return _data4; }
            set
            {
                _data4 = value;
                UpdateChart(); 
            }
        }

        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data4 Label")]
        public string label4
        {
            get { return _label4; }
            set
            {
                _label4 = value;
                UpdateChart();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data4 Color")]
        public Color color4
        {
            get { return _color4; }
            set { _color4 = value; UpdateChart(); }
        }

        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        public int data5
        {
            get { return _data5; }
            set
            {
                _data5 = value;
                UpdateChart(); 
            }
        }


        [Browsable(true)]
        [HMIBrowsable(true)]
        [Category("Labels Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data5 LAbel")]
        public string label5
        {
            get { return _label5; }
            set
            {
                _label5 = value;
                UpdateChart();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Data5 Color")]
        public Color color5
        {
            get { return _color5; }
            set { _color5 = value; UpdateChart(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Pie-Chart Back Color")]
        public Color pieChartBackColor
        {
            get { return _backColor; }
            set { _backColor = value; this.BackColor = value; pieChart1.BackColor = value; }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Pie-Chart Text Color")]
        public Color pieChartForeColor
        {
            get { return _foreColor; }
            set { _foreColor = value; pieChart1.ForeColor = value;  }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Size of the Circle in the Center")]
        public int innerRadiusValue
        {
            get { return _innerRadiusValue; }
            set { _innerRadiusValue = value;pieChart1.InnerRadius = value; UpdateChart(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Adjusts the Distance Between Slices")]
        public int pushOutValue
        {
            get { return _pushOutValue; }
            set { _pushOutValue = value ; UpdateChart(); }
        }

    }
}
