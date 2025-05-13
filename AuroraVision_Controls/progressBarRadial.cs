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

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(progressBarRadial), "Icons.progressBarRadial.png")]
    public partial class progressBarRadial : UserControl
    {
        private float _progressValue;
        private Color _backColor = Color.Transparent;
        public int _thickness = 15;
        public int _textSize = 14;
        public Color _textColor = Color.Black;

        public Color _progressBaseColor = Color.DimGray;

        public int _value1 = 20;
        public Color _value1Color = Color.Red;
        public int _value2 = 35;
        public Color _value2Color = Color.Orange;
        public int _value3 = 50;
        public Color _value3Color = Color.Yellow;
        public int _value4 = 70;
        public Color _value4Color = Color.YellowGreen;
        public int _value5 = 85;
        public Color _value5Color = Color.Green;
        public Color _value6Color = Color.GreenYellow;

        public progressBarRadial()
        {
            InitializeComponent();
            this.Resize += ProgressBarRadial_Resize;
            CenterLabel();
            label1.ForeColor = _textColor;
            label1.BackColor = Color.Transparent;
            radialProgressBar1.ProgressBaseColor = _progressBaseColor;
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the text size")]
        public int TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
                label1.Font = new Font(label1.Font.FontFamily,
                    value, label1.Font.Style);
                _textSize = value;
                radialProgressBar1.ProgressFont =
                    new Font(radialProgressBar1.ProgressFont.FontFamily,
                    _textSize, radialProgressBar1.ProgressFont.Style);
                CenterLabel();
            }
        }

        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public float progressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                label1.Text = value.ToString("0.##") + "%";
                CenterLabel();

                radialProgressBar1.Value = value;
                if (value <= _value1)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(_value1Color);
                }
                else if (value > _value1 && value <= _value2)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(_value2Color);
                }
                else if (value > _value2 && value<=_value3)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(Value3Color);
                }
                else if (value > _value3 && value <= _value4)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(_value4Color);
                }
                else if (value > _value4 && value <= _value5)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(_value5Color);
                }
                else
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(_value6Color);
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 1 Color")]
        public Color Value1Color
        {
            get { return _value1Color; }
            set
            {
                _value1Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value1Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 2 Color")]
        public Color Value2Color
        {
            get { return _value2Color; }
            set
            {
                _value2Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value2Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 3 Color")]
        public Color Value3Color
        {
            get { return _value3Color; }
            set
            {
                _value3Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value3Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 4 Color")]
        public Color Value4Color
        {
            get { return _value4Color; }
            set
            {
                _value4Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value4Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 5 Color")]
        public Color Value5Color
        {
            get { return _value5Color; }
            set
            {
                _value5Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value5Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 6 Color")]
        public Color Value6Color
        {
            get { return _value6Color; }
            set
            {
                _value6Color = value;
                radialProgressBar1.ColorAnimationCycle.Clear();
                radialProgressBar1.ColorAnimationCycle.Add(_value6Color);
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Value Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 1")]
        public int Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Value Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 2")]
        public int Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Value Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 3")]
        public int Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Value Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 4")]
        public int Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Value Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Value 5")]
        public int Value5
        {
            get { return _value5; }
            set
            {
                _value5 = value;
            }
        }


        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Background Color")]
        public Color BackgroundColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                label1.BackColor = value;
                this.BackColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("ColorsSettings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Text Fore Color")]
        public Color foreColor
        {
            get { return _textColor; }
            set
            {
                _textColor = value;
                label1.ForeColor = value;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Progress Thickness")]
        public int ProgressThicknes
        {
            get { return _thickness; }
            set
            {
                _thickness = value;
                radialProgressBar1.ProgressThickness = value;
            }
        }

        private void ProgressBarRadial_Resize(object sender, EventArgs e)
        {
            CenterLabel();
        }

        private void CenterLabel()
        {
            if (label1 != null)
            {
                label1.Location = new Point(
                    (radialProgressBar1.Width - label1.Width) / 2,
                    (radialProgressBar1.Height - label1.Height) / 2
                );
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Color Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Progress Bar Base Color")]
        public Color ProgressBaseColor
        {
            get { return _progressBaseColor; }
            set
            {
                _progressBaseColor = value;
                radialProgressBar1.ProgressBaseColor = value;
            }
        }


    }
}
