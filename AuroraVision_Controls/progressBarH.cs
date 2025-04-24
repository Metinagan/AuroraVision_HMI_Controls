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

namespace AuroraVision_Controls
{
    [ToolboxBitmap(typeof(progressBarH),"Icons.progressBarH.png")]
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    public partial class progressBarH : UserControl
    {
        public int _progressValue;
        //public int _outValue;
        public Color _progressBarcolor = Color.Blue;
        public Color _thumbColor = Color.Blue;
        public Color _thumbBorderColor = Color.MediumBlue;
        public Color _trackColor = Color.Gray;
        public Color _backColor = Color.DarkGray;
        public int _thumbSize = 10;
        public String _title = "";
        public int _textSize = 12;
        public Color _foreColor = Color.Black;

        public progressBarH()
        {
            InitializeComponent();
            _progressValue = siticoneHSlider1.Value;
            _title = "% " + siticoneHSlider1.Value.ToString();
            siticoneHSlider1.ValueChanged += Slider_ValueChanged;
        }

        private void Slider_ValueChanged(object sender, EventArgs e)
        {

            _progressValue = siticoneHSlider1.Value;
            _title = " % " + siticoneHSlider1.Value.ToString();
            label1.Text = _title;
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the size of the dot on the progress bar.")]
        public int ProgressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                siticoneHSlider1.Value = value;
                _title =" % " + siticoneHSlider1.Value.ToString();
                label1.Text = _title;
            }
        }

        [HMI.HMIPortProperty(HMIPortDirection.HiddenOutput)]
        public int OutValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                _title =" % " + siticoneHSlider1.Value.ToString();
                label1.Text = _title;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the size of the dot on the progress bar.")]
        public int ThumbSize
        {
            get { return _thumbSize; }
            set 
            { 
                _thumbSize = value;
                siticoneHSlider1.ThumbSize = _thumbSize;
                label1.Text = _title;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the text size of the label")]
        public int TextSize
        {
            get { return _textSize; }
            set
            {
                _textSize = value;
                label1.Font = new Font(label1.Font.FontFamily, _textSize, label1.Font.Style);
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
            get { return _title; }
            set
            {
                _title =" % "+ siticoneHSlider1.Value.ToString();
                label1.Text = _title;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Progress Bar Color")]
        public Color TextColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                label1.ForeColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Progress Bar Color")]
        public Color barColor
        {
            get { return _progressBarcolor; }
            set
            {
                _progressBarcolor = value;
                siticoneHSlider1.ElapsedTrackColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the inner color of the dot in the progress bar")]
        public Color ThumbColor
        {
            get { return _thumbColor; }
            set
            {
                _thumbColor = value;
                siticoneHSlider1.ThumbColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the outer color of the dot in the progress bar")]
        public Color ThumbBorderColor
        {
            get { return _thumbBorderColor; }
            set
            {
                _thumbBorderColor = value;
                siticoneHSlider1.ThumbBorderColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Incomplete area color in progress bar")]
        public Color TrackColor
        {
            get { return _trackColor; }
            set
            {
                _trackColor = value;
                siticoneHSlider1.TrackColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Incomplete area color in progress bar")]
        public Color BackgroundColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                this.BackColor = value;
            }
        }

    }
}
