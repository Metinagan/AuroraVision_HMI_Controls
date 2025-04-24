using HMI;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(progressBarRadial), "Icons.split.png")]
    public partial class splitContainerCustom : SplitContainer
    {
        private int _p1size, _p2size;
        private int _p1ratio, _p2ratio;
        private int _splitterSize;

        public splitContainerCustom()
        {
            InitializeComponent();
            InitializeSplitContainer();

            this.SplitterMoved += SplitContainerCustom_SplitterMoved;
            
            _splitterSize = this.SplitterWidth;


            _p1size = Panel1.Width;
            _p2size = Panel2.Width;
            _p1ratio = (_p1size * 100) / (Panel1.Width + Panel2.Width);
            _p2ratio = 100 - _p1ratio;
            UpdatePanelSizes();
        }

        private void InitializeSplitContainer()
        {
            this.Panel1.Controls.Add(new Button() { Text = "Panel 1" });
            this.Panel2.Controls.Add(new Button() { Text = "Panel 2" });
        }

        private void UpdatePanelSizes()
        {
            int totalSize = (this.Orientation == Orientation.Vertical)
                ? this.Height
                : this.Width;


            _p1size = this.SplitterDistance;
            _p2size = totalSize - _p1size;
        }

        private void SplitContainerCustom_SplitterMoved(object sender, SplitterEventArgs e)
        {
            UpdatePanelSizes();

            int totalSize = (this.Orientation == Orientation.Vertical)
                ? this.Height - _splitterSize
                : this.Width - _splitterSize;

            _p1ratio = (_p1size * 100) / totalSize;
            _p2ratio = 100 - _p1ratio;
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel1 Size")]
        public int p1size
        {
            get { return _p1size; }
            set
            {
                int totalSize = (this.Orientation == Orientation.Vertical)
                    ? this.Height - _splitterSize
                    : this.Width - _splitterSize;

                _p1size = value;
                this.SplitterDistance = _p1size;
                _p2size = totalSize - _p1size;
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel1 Ratio")]
        public int p1ratio
        {
            get { return _p1ratio; }
            set
            {
                int totalSize = (this.Orientation == Orientation.Vertical)
                    ? this.Height - _splitterSize
                    : this.Width - _splitterSize;

                _p1ratio = value;
                _p1size = (totalSize * _p1ratio) / 100;
                this.SplitterDistance = _p1size;
                _p2size = totalSize - _p1size;
                _p2ratio = 100 - _p1ratio;
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel2 Size")]
        public int p2size
        {
            get { return _p2size; }
            set
            {
                int totalSize = (this.Orientation == Orientation.Vertical)
                    ? this.Height - _splitterSize
                    : this.Width - _splitterSize;

                _p2size = value;
                _p1size = totalSize - _p2size;
                this.SplitterDistance = _p1size;
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel2 Ratio")]
        public int p2ratio
        {
            get { return _p2ratio; }
            set
            {
                int totalSize = (this.Orientation == Orientation.Vertical)
                    ? this.Height - _splitterSize
                    : this.Width - _splitterSize;

                _p2ratio = value;
                _p2size = (totalSize * _p2ratio) / 100;
                _p1size = totalSize - _p2size;
                this.SplitterDistance = _p1size;
                this.Invalidate();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Size Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Splitter Size")]
        public int splitterSize
        {
            get { return _splitterSize; }
            set
            {
                _splitterSize = value;
                SplitterWidth = value;
                UpdatePanelSizes();
            }
        }
    }
}