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
using System.Runtime.CompilerServices;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(progressBarRadial),"Icons.progressBarRadial.png")]
    public partial class progressBarRadial : UserControl
    {
        private int _progressValue;
        private Color _backColor = Color.Gray;
        public int _thickness = 15;
        public int _textSize = 14;
        
        public progressBarRadial()
        {
            InitializeComponent();
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
                radialProgressBar1.ProgressFont = 
                    new Font(radialProgressBar1.ProgressFont.FontFamily, 
                    _textSize, radialProgressBar1.ProgressFont.Style);
            }
        }


        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        public int progressValue
        {
            get { return _progressValue; }
            set
            {
                _progressValue = value;
                radialProgressBar1.Value = _progressValue;
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
                this.BackColor = value;
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Background Color")]
        public int ProgressThicknes
        {
            get { return _thickness; }
            set { radialProgressBar1.ProgressThickness = value; _thickness = value; }
        }

    }
}
