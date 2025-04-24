using System;
using System.ComponentModel;
using System.Windows.Forms;
using HMI;

namespace AuroraVision_Controls
{
    public partial class numericUpDownCustom : NumericUpDown
    {
        private float _value;
        private int _decimalPlacesValue = 1;


        public numericUpDownCustom()
        {
            InitializeComponent();
            this.DecimalPlaces = _decimalPlacesValue;
            this.MouseDown += new MouseEventHandler(mouseClick);
            this.Select(this.Text.Length, 0);
            this.Maximum = 10000; // Maksimum değeri 10000 olarak ayarladık
            this.Minimum = -10000;

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
            using (numericUpDownKeyboard keyboard = new numericUpDownKeyboard(_value))
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
    }
}
