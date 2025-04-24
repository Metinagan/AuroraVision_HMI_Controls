using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HMI;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(tableLayoutCustom),"Icons.tableLayout.png")]
    public partial class tableLayoutCustom : TableLayoutPanel
    {
        private int _cellHeight;
        private int _cellWidth;

        private int _rowCount = 2;
        private int _columnCount = 2;
        private int _selectRowIndex = 0;
        private int _selectColumnIndex = 0;

        public tableLayoutCustom()
        {
            InitializeComponent();
            UpdateTableLayout();
            getCellHeight();
            getCellWidth();
            this.Resize += new EventHandler(TableLayout_Resize);
            this.Layout += new LayoutEventHandler(TableLayout_LayoutUpdated);
        }
        protected override void OnDockChanged(EventArgs e)
        {
            base.OnDockChanged(e);
            getCellHeight();
            getCellWidth();
        }


        private void TableLayout_LayoutUpdated(object sender, LayoutEventArgs e)
        {
            getCellHeight();
            getCellWidth();
        }
        private void TableLayout_Resize(object sender, EventArgs e)
        {
            getCellHeight();
            getCellWidth();
        }
        private void UpdateTableLayout()
        {
            this.RowStyles.Clear();
            this.ColumnStyles.Clear();

            this.RowCount = _rowCount;
            this.ColumnCount = _columnCount;

            // Eşit boyutlu hücreler için RowStyles ve ColumnStyles ekliyoruz
            for (int i = 0; i < _rowCount; i++)
            {
                this.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / _rowCount)); 
            }

            for (int i = 0; i < _columnCount; i++)
            {
                this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / _columnCount));
            }

            this.Invalidate();
        }




        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel Row Count")]
        public int RowCountPanel
        {
            get { return _rowCount; }
            set
            {
                _rowCount = value;
                UpdateTableLayout();
                getCellHeight();
                getCellWidth();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel Column Count")]
        public int ColumnCountPanel
        {
            get { return _columnCount; }
            set
            {
                _columnCount = value;
                UpdateTableLayout();
                getCellHeight();
                getCellWidth();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Select Panel Row Index")]
        public int SelectRowIndex
        {
            get { return _selectRowIndex; }
            set
            {
                _selectRowIndex = value;
                getCellHeight();
                getCellWidth();
            }
        }
        public void getCellHeight()
        {
            // RowCount'tan daha büyük bir indeks kontrolü yapalım
            if (_selectRowIndex >= 0 && _selectRowIndex < this.RowCount)
            {
                // Belirtilen satırın yüksekliğini alıyoruz
                _cellHeight = this.GetRowHeights()[_selectRowIndex];
            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "Invalid row index.");
            }
        }
        public void getCellWidth()
        {
            // RowCount'tan daha büyük bir indeks kontrolü yapalım
            if (_selectColumnIndex >= 0 && _selectColumnIndex < this.RowCount)
            {
                // Belirtilen satırın yüksekliğini alıyoruz
                _cellWidth = this.GetColumnWidths()[_selectColumnIndex];
            }
            else
            {
                throw new ArgumentOutOfRangeException("rowIndex", "Invalid row index.");
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Select Panel Column Index")]
        public int SelectColumnIndex
        {
            get { return _selectColumnIndex; }
            set
            {
                _selectColumnIndex = value;
                getCellHeight();
                getCellWidth();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel Row Height")]
        public int CellHeight
        {
            get { return _cellHeight; }
            set
            {
                _cellHeight = value;
                if (_selectRowIndex >= 0 && _selectRowIndex < this.RowCount)
                {
                    this.RowStyles[_selectRowIndex].SizeType = SizeType.Absolute;
                    this.RowStyles[_selectRowIndex].Height = value;
                    this.Invalidate(); // Güncellenmiş görünümü yenile
                }
                else
                {
                    throw new ArgumentOutOfRangeException("columnIndex", "Invalid column index.");
                }
                getCellHeight();
                getCellWidth();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Cell Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Sets Panel Column Width")]
        public int CellWidth
        {
            get { return _cellWidth; }
            set
            {
                _cellWidth = value;
                if (_selectColumnIndex >= 0 && _selectColumnIndex < this.ColumnCount)
                {
                    this.ColumnStyles[_selectColumnIndex].SizeType = SizeType.Absolute;
                    this.ColumnStyles[_selectColumnIndex].Width = value;
                    this.Invalidate(); // Güncellenmiş görünümü yenile
                }
                else
                {
                    throw new ArgumentOutOfRangeException("columnIndex", "Invalid column index.");
                }
                getCellHeight();
                getCellWidth();
            }
        }



    }
}
