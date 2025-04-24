using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraVision_Controls
{
    [ToolboxBitmap(typeof(toggleSwitch), "Icons.toggleSwitch.png")]
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    public partial class toggleSwitch: UserControl
    {
        private bool _checked = false;


        public toggleSwitch()
        {
            InitializeComponent();
            
        }


        [HMI.HMIPortProperty(HMI.HMIPortDirection.Output)]
        public bool Checked
        {
            get 
            { 
                if (toggleSwitch1.IsOn == true) { return true; return _checked; }
                else { return false; }
            }
            set { _checked = value; }
        }

    }

}
