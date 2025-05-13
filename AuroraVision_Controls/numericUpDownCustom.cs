using System;
using System.ComponentModel;
using System.Windows.Forms;
using AuroraVision_Controls.Properties;
using System.Drawing;
using HMI;
using System.Globalization;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(CameraContol), "Icons.component.png")]
    public partial class numericUpDownCustom : NumericUpDown, ISupportInitialize, IValueSourceControl
    {
        private int _decimalPlacesValue = 1;
        private Color _numpadBackColor = Color.White;

        private decimal storedValue;
        private bool initializing;

        private event EventHandler autoSourceValueChanged;

        public numericUpDownCustom()
        {
            InitializeComponent();
            this.DecimalPlaces = _decimalPlacesValue;
            this.MouseDown += new MouseEventHandler(mouseClick);
            this.Select(this.Text.Length, 0);
        }

        private void mouseClick(object sender, MouseEventArgs e)
        {
            using (numericUpDownKeyboard keyboard = new numericUpDownKeyboard((float)this.Value, this.Maximum,this.Minimum, this.DecimalPlaces, _numpadBackColor))
            {
                if (keyboard.ShowDialog() == DialogResult.OK)
                {
                    this.Value = (decimal)keyboard.ResultValue;
                }
            }
        }

        [HMIBrowsable(true)]
        [HMIPortProperty(HMIPortDirection.Input | HMIPortDirection.Output)]
        [HMIStatePort]
        public new decimal Value
        {
            get => storedValue;
            set
            {
                if (initializing)
                {
                    base.Value = value;
                    return;
                }

                // Sınırlar dahilinde değer ayarlanır
                base.Value = Math.Max(this.Minimum, Math.Min(this.Maximum, value));
                storedValue = base.Value;
            }
        }
        /*
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
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
        */
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Numpad Background Color")]
        public Color numpadBackColor
        {
            get { return _numpadBackColor; }
            set { _numpadBackColor = value; }
        }

        public new void BeginInit()
        {
            initializing = true;
            base.BeginInit();
        }

        public new void EndInit()
        {
            initializing = false;
            base.EndInit();
            storedValue = base.Value;
        }

        protected override void OnValueChanged(EventArgs e)
        {
            storedValue = base.Value;
            base.OnValueChanged(e);
        }

        protected override void UpdateEditText()
        {
            base.UpdateEditText();
            if (!initializing)
            {
                OnAutoSourceValueChanged();
            }
        }

        private void OnAutoSourceValueChanged()
        {
            autoSourceValueChanged?.Invoke(this, EventArgs.Empty);
        }

        private string GetValueAsString(decimal value)
        {
            if (base.Hexadecimal)
            {
                return ((long)value).ToString("X", CultureInfo.InvariantCulture);
            }

            return value.ToString((base.ThousandsSeparator ? "N" : "F") + base.DecimalPlaces.ToString(CultureInfo.CurrentCulture), CultureInfo.CurrentCulture);
        }

        string IValueSourceControl.GetValue()
        {
            return GetValueAsString(storedValue);
        }

        event EventHandler IValueSourceControl.ValueChanged
        {
            add { autoSourceValueChanged += value; }
            remove { autoSourceValueChanged -= value; }
        }
    }
}
