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

        public progressBarRadial()
        {
            InitializeComponent();
            this.Resize += ProgressBarRadial_Resize;
            CenterLabel();
            label1.ForeColor = _textColor;
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

                radialProgressBar1.Value = value;
                if (value <= 20)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(Color.Red);
                }
                else if (value > 20 && value <= 80)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(Color.Orange);
                }
                else if (value > 80)
                {
                    radialProgressBar1.ColorAnimationCycle.Clear();
                    radialProgressBar1.ColorAnimationCycle.Add(Color.Green);
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
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
        [Category("Colors Settings")]
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
    }
}
