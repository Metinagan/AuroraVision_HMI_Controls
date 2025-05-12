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
using SiticoneNetFrameworkUI;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(CameraContol), "Icons.grid.png")]
    public partial class table: UserControl
    {

        private List<string> labelTitles = new List<string>();
        private List<float> labelValues = new List<float>();
        private List<Color> titleColors = new List<Color>();
        private List<Color> valueColors = new List<Color>();


        private string _label1title= "Label 1";
        private string _label2title = "Label 2";
        private string _label3title = "Label 3";
        private string _label4title = "Label 4";
        private string _label5title = "Label 5";
        private string _label6title = "Label 6";
        private string _label7title = "Label 7";
        private string _label8title = "Label 8";
        private string _label9title = "Label 9";
        private string _label10title = "Label 10";

        private float _label1value = 0.0f;
        private float _label2value = 0.0f;
        private float _label3value = 0.0f;
        private float _label4value = 0.0f;
        private float _label5value = 0.0f;
        private float _label6value = 0.0f;
        private float _label7value = 0.0f;
        private float _label8value = 0.0f;
        private float _label9value = 0.0f;
        private float _label10value = 0.0f;

        private Color _label1color = Color.LightGray;
        private Color _label2color = Color.LightGray;
        private Color _label3color = Color.LightGray;
        private Color _label4color = Color.LightGray;
        private Color _label5color = Color.LightGray;
        private Color _label6color = Color.LightGray;
        private Color _label7color = Color.LightGray;
        private Color _label8color = Color.LightGray;
        private Color _label9color = Color.LightGray;
        private Color _label10color = Color.LightGray;

        private Color _value1color = Color.LightBlue;
        private Color _value2color = Color.LightBlue;
        private Color _value3color = Color.LightBlue;
        private Color _value4color = Color.LightBlue;
        private Color _value5color = Color.LightBlue;
        private Color _value6color = Color.LightBlue;
        private Color _value7color = Color.LightBlue;
        private Color _value8color = Color.LightBlue;
        private Color _value9color = Color.LightBlue;
        private Color _value10color = Color.LightBlue;

        private Color _BackColor = Color.Transparent;

        private int _labelCount = 5;
        private int _labelTextSize = 10;
        private int _valueTextSize = 10;
        private int _labelPaddingVertical = 35;
        private int _labelPaddinghorizontal = 200;

        private int _labelWidth = 150;
        private int _labelDataHeight = 30;
        private int _dataWidth = 150;

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label Component Width")]
        public int LabelWidth
        {
            get { return _labelWidth; }
            set
            {
                if (value > 0 && value != _labelWidth)
                {
                    _labelWidth = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label Component Width")]
        public int DataWidth
        {
            get { return _dataWidth; }
            set
            {
                if (value > 0 && value != _dataWidth)
                {
                    _dataWidth = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Back Color")]
        public Color BackgroundColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
               

                siticoneCard1.ShadowColor = _BackColor;

                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Displays the number of label rows (max 10).")]
        public int LabelCount
        {
            get { return _labelCount; }
            set
            {
                if (value > 0 && value != _labelCount)
                {
                    _labelCount = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the font size of the label titles.")]
        public int LabelTextSize
        {
            get { return _labelTextSize; }
            set
            {
                if (value > 0 && value != _labelTextSize)
                {
                    _labelTextSize = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the font size of the label values.")]
        public int ValueTextSize
        {
            get { return _valueTextSize; }
            set
            {
                if (value > 0 && value != _valueTextSize)
                {
                    _valueTextSize = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the vertical spacing between each label row.")]
        public int LabelPaddingVertical
        {
            get { return _labelPaddingVertical; }
            set
            {
                if (value > 0 && value != _labelPaddingVertical)
                {
                    _labelPaddingVertical = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the horizontal spacing between label title and value.")]
        public int LabelPaddingHorizontal
        {
            get { return _labelPaddinghorizontal; }
            set
            {
                if (value > 0 && value != _labelPaddinghorizontal)
                {
                    _labelPaddinghorizontal = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the horizontal spacing between label title and value.")]
        public int LabelDataHeight
        {
            get { return _labelDataHeight; }
            set
            {
                if (value > 0 && value != _labelDataHeight)
                {
                    _labelDataHeight = value;
                    GenerateLabels();
                    UpdateLabels();
                    this.Invalidate();
                }
            }
        }


        public table()
        {
            InitializeComponent();
            siticoneCard1.BackColor = _BackColor;
            siticoneCard1.BackgroundColor1 = _BackColor;
            siticoneCard1.BackgroundColor2 = _BackColor;
            siticoneCard1.ShadowColor = Color.Transparent;
            siticoneCard1.BorderColor1 = Color.Black;
            siticoneCard1.BorderColor2 = Color.Black;
            siticoneCard1.ShadowDepth = 75;
            siticoneCard1.ShadowOpacity = 75;

            this.BackColor = Color.Transparent;

            labelTitles.Add(_label1title);  titleColors.Add(_label1color);  valueColors.Add(_value1color);
            labelTitles.Add(_label2title);  titleColors.Add(_label2color);  valueColors.Add(_value2color);
            labelTitles.Add(_label3title);  titleColors.Add(_label3color);  valueColors.Add(_value3color);
            labelTitles.Add(_label4title);  titleColors.Add(_label4color);  valueColors.Add(_value4color);
            labelTitles.Add(_label5title);  titleColors.Add(_label5color);  valueColors.Add(_value5color);
            labelTitles.Add(_label6title);  titleColors.Add(_label6color);  valueColors.Add(_value6color);
            labelTitles.Add(_label7title);  titleColors.Add(_label7color);  valueColors.Add(_value7color);
            labelTitles.Add(_label8title);  titleColors.Add(_label8color);  valueColors.Add(_value8color);
            labelTitles.Add(_label9title);  titleColors.Add(_label9color);  valueColors.Add(_value9color);
            labelTitles.Add(_label10title); titleColors.Add(_label10color); valueColors.Add(_value10color);
            GenerateLabels();
            UpdateLabels();
        }

        private void GenerateLabels()
        {
            siticoneCard1.Controls.Clear();
            labelValues.Clear();

            for (int i = 0; i < _labelCount; i++)
            {
                // Başlık chip'i
                var chipTitle = new SiticoneChip
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", _labelTextSize, FontStyle.Regular),
                    Text = labelTitles[i],
                    Location = new Point(_labelTextSize + 10, (i * _labelPaddingVertical) + 20),
                    FillColor = (titleColors.Count > i) ? titleColors[i] : Color.LightGray,
                    Size = new Size(_labelWidth, _labelDataHeight)
                };

                // Değer chip'i
                var chipValue = new SiticoneChip
                {
                    AutoSize = true,
                    Font = new Font("Segoe UI", _valueTextSize, FontStyle.Bold),
                    Text = "0.00",
                    Location = new Point(_valueTextSize + 20 + _labelPaddinghorizontal, (i * _labelPaddingVertical) + 20),
                    FillColor = (valueColors.Count > i) ? valueColors[i] : Color.LightBlue,
                    Size = new Size(_dataWidth, _labelDataHeight)
                };

                siticoneCard1.Controls.Add(chipTitle);
                siticoneCard1.Controls.Add(chipValue);

                labelValues.Add(0);
            }
        }




        public void UpdateLabels()
        {
            for (int i = 0; i < _labelCount; i++)
            {
                int baseIndex = i * 2;

                var chipTitle = siticoneCard1.Controls[baseIndex] as SiticoneChip;
                var chipValue = siticoneCard1.Controls[baseIndex + 1] as SiticoneChip;

                if (chipTitle != null && chipValue != null)
                {
                    chipTitle.Text = labelTitles[i];
                    chipValue.Text = labelValues[i].ToString("F2");

                    if (titleColors.Count > i)
                        chipTitle.FillColor = titleColors[i];

                    if (valueColors.Count > i)
                        chipValue.FillColor = valueColors[i];
                }
            }
        }




        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public float data1
        {
            get { return _label1value; }
            set
            {
                _label1value = value;
                labelValues[0] = value;
                UpdateLabels();
            }
        }
        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public float data2
        {
            get { return _label2value; }
            set
            {
                _label2value = value;
                labelValues[1] = value;
                UpdateLabels();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public float data3
        {
            get { return _label3value; }
            set
            {
                _label3value = value;
                labelValues[2] = value;
                UpdateLabels();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public float data4
        {
            get { return _label4value; }
            set
            {
                _label4value = value;
                labelValues[3] = value;
                UpdateLabels();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.Input)]
        public float data5
        {
            get { return _label5value; }
            set
            {
                _label5value = value;
                labelValues[4] = value;
                UpdateLabels();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        public float data6
        {
            get { return _label6value; }
            set
            {
                _label6value = value;
                labelValues[5] = value;
                UpdateLabels();
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        public float data7
        {
            get { return _label7value; }
            set
            {
                _label7value = value;
                labelValues[6] = value;
                UpdateLabels();
            }
        }
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        public float data8
        {
            get { return _label8value; }
            set
            {
                _label8value = value;
                labelValues[7] = value;
                UpdateLabels();
            }
        }
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        public float data9
        {
            get { return _label9value; }
            set
            {
                _label9value = value;
                labelValues[8] = value;
                UpdateLabels();
            }
        }
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        public float data10
        {
            get { return _label10value; }
            set
            {
                _label10value = value;
                labelValues[9] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the first label.")]
        public string Data1Title
        {
            get { return _label1title; }
            set
            {
                _label1title = value;
                labelTitles[0] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the second label.")]
        public string Data2Title
        {
            get { return _label2title; }
            set
            {
                _label2title = value;
                labelTitles[1] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the third label.")]
        public string Data3Title
        {
            get { return _label3title; }
            set
            {
                _label3title = value;
                labelTitles[2] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the fourth label.")]
        public string Data4Title
        {
            get { return _label4title; }
            set
            {
                _label4title = value;
                labelTitles[3] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the fifth label.")]
        public string Data5Title
        {
            get { return _label5title; }
            set
            {
                _label5title = value;
                labelTitles[4] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the sixth label.")]
        public string Data6Title
        {
            get { return _label6title; }
            set
            {
                _label6title = value;
                labelTitles[5] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the seventh label.")]
        public string Data7Title
        {
            get { return _label7title; }
            set
            {
                _label7title = value;
                labelTitles[6] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the eighth label.")]
        public string Data8Title
        {
            get { return _label8title; }
            set
            {
                _label8title = value;
                labelTitles[7] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the ninth label.")]
        public string Data9Title
        {
            get { return _label9title; }
            set
            {
                _label9title = value;
                labelTitles[8] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Label Text Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets the title text of the tenth label.")]
        public string Data10Title
        {
            get { return _label10title; }
            set
            {
                _label10title = value;
                labelTitles[9] = value;
                UpdateLabels();
                this.Invalidate();
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 1 Background Color")]
        public Color Label1BackColor
        {
            get { return _label1color; }
            set
            {
                _label1color = value;
                titleColors[0] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 2 Background Color")]
        public Color Label2BackColor
        {
            get { return _label2color; }
            set
            {
                _label2color = value;
                titleColors[1] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 3 Background Color")]
        public Color Label3BackColor
        {
            get { return _label3color; }
            set
            {
                _label3color = value;
                titleColors[2] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 4 Background Color")]
        public Color Label4BackColor
        {
            get { return _label4color; }
            set
            {
                _label4color = value;
                titleColors[3] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 5 Background Color")]
        public Color Label5BackColor
        {
            get { return _label5color; }
            set
            {
                _label5color = value;
                titleColors[4] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 6 Background Color")]
        public Color Label6BackColor
        {
            get { return _label6color; }
            set
            {
                _label6color = value;
                titleColors[5] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 7 Background Color")]
        public Color Label7BackColor
        {
            get { return _label7color; }
            set
            {
                _label7color = value;
                titleColors[6] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 8 Background Color")]
        public Color Label8BackColor
        {
            get { return _label8color; }
            set
            {
                _label8color = value;
                titleColors[7] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 9 Background Color")]
        public Color Label9BackColor
        {
            get { return _label9color; }
            set
            {
                _label9color = value;
                titleColors[8] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Labels Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Label 10 Background Color")]
        public Color Label10BackColor
        {
            get { return _label10color; }
            set
            {
                _label10color = value;
                titleColors[9] = value;
                UpdateLabels();
            }
        }



        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 1 Background Color")]
        public Color Value1BackColor
        {
            get { return _value1color; }
            set
            {
                _value1color = value;
                valueColors[0] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 2 Background Color")]
        public Color Value2BackColor
        {
            get { return _value2color; }
            set
            {
                _value2color = value;
                valueColors[1] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 3 Background Color")]
        public Color Value3BackColor
        {
            get { return _value3color; }
            set
            {
                _value3color = value;
                valueColors[2] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 4 Background Color")]
        public Color Value4BackColor
        {
            get { return _value4color; }
            set
            {
                _value4color = value;
                valueColors[3] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 5 Background Color")]
        public Color Value5BackColor
        {
            get { return _value5color; }
            set
            {
                _value5color = value;
                valueColors[4] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 6 Background Color")]
        public Color Value6BackColor
        {
            get { return _value6color; }
            set
            {
                _value6color = value;
                valueColors[5] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 7 Background Color")]
        public Color Value7BackColor
        {
            get { return _value7color; }
            set
            {
                _value7color = value;
                valueColors[6] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 8 Background Color")]
        public Color Value8BackColor
        {
            get { return _value8color; }
            set
            {
                _value8color = value;
                valueColors[7] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 9 Background Color")]
        public Color Value9BackColor
        {
            get { return _value9color; }
            set
            {
                _value9color = value;
                valueColors[8] = value;
                UpdateLabels();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Values Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 10 Background Color")]
        public Color Value10BackColor
        {
            get { return _value10color; }
            set
            {
                _value10color = value;
                valueColors[9] = value;
                UpdateLabels();
            }
        }
    }
}
