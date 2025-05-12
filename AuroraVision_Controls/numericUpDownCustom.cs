using System;
using System.ComponentModel;
using System.Windows.Forms;
using AuroraVision_Controls.Properties;
using System.Windows.Media;
using HMI;
using LiveCharts.Wpf;
using System.Drawing;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(CameraContol), "Icons.component.png")]
    public partial class numericUpDownCustom : NumericUpDown
    {
        private float _value;
        private int _decimalPlacesValue = 1;

        private System.Drawing.Color _numpadBackColor = System.Drawing.Color.White;


        public numericUpDownCustom()
        {
            InitializeComponent();
            this.DecimalPlaces = _decimalPlacesValue;
            this.MouseDown += new MouseEventHandler(mouseClick);
            this.Select(this.Text.Length, 0);

        }

        [HMI.HMIPortProperty(HMIPortDirection.Output)]
        public float numericValue
        {
            get { return _value; }
            set
            {
                _value = value;
                this.Value = (decimal)_value;
            }
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            using (numericUpDownKeyboard keyboard = new numericUpDownKeyboard(_value, this.Maximum, this.Minimum,_numpadBackColor))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    // Klavye formundan dönen değeri alıyoruz
                    _value = keyboard.ResultValue;
                    this.Value = (decimal)_value;
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Decimal Places Count")]
        public int DecimalPlacesValue
        {
            get { return _decimalPlacesValue; }
            set
            {
                _decimalPlacesValue = value;
                this.DecimalPlaces = value;
                this.Invalidate();
                this.Refresh();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Numpad Background Color")]
        public System.Drawing.Color numpadBackColor
        {
            get { return _numpadBackColor; }
            set
            {
                _numpadBackColor = value;
            }
        }
    }
}
