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
using System.Globalization;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    public partial class nud : NumericUpDown, ISupportInitialize, IValueSourceControl
    {
        private bool initializing;

        private decimal storedValue;

        [HMIBrowsable(true)]
        [HMIPortProperty(HMIPortDirection.Input | HMIPortDirection.HiddenOutput)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        [HMIBrowsable(true)]
        [HMIPortProperty(HMIPortDirection.Input | HMIPortDirection.Output)]
        [HMIStatePort]
        public new decimal Value
        {
            get
            {
                return storedValue;
            }
            set
            {
                if (initializing)
                {
                    base.Value = value;
                    return;
                }
                base.Value = Math.Max(base.Minimum, Math.Min(base.Maximum, value));
                storedValue = base.Value;
            }
        }

        private event EventHandler autoSourceValueChanged;

        event EventHandler IValueSourceControl.ValueChanged
        {
            add
            {
                autoSourceValueChanged += value;
            }
            remove
            {
                autoSourceValueChanged -= value;
            }
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

        private void OnAutoSourceValueChanged()
        {
            if (this.autoSourceValueChanged != null)
            {
                this.autoSourceValueChanged(this, EventArgs.Empty);
            }
        }
    }
}
