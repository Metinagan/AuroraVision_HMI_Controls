using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HMI;
using SiticoneNetFrameworkUI;
using System.Collections.Generic;
using System.Linq;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(pieChart), "Icons.table.png")]
    public partial class DynamicTable : UserControl
    {
        private int _rowCount = 3;
        private int _columnCount = 3;
        private SiticoneButton[,] tableCells;

        private int buttonWidth = 100;
        private int buttonHeight = 30;
        private int margin = 5;

        private int fonstSize = 10;

        private Color _backColor = Color.White;
        private Color _borderColor = Color.Black;
        private int _borderWidth = 2;
        private Color _cellColor = Color.Gray;
        private Color _textColor = Color.White;

        private List<string> _headers = new List<string>();
        private List<string> _data = new List<string>();

        private Label _mainTitleLabel = new Label();
        private string _mainTitle = "Başlık";


        private String _Title = "Başlık";
        private int _TitleHeight = 30;
        private int _TitleTextSize = 10;


        public DynamicTable()
        {
            InitializeComponent();
            GenerateTable();
            this.BackColor = _backColor;
        }

        private void GenerateTable()
        {
            this.Controls.Clear();

            _mainTitleLabel.Text = _Title;
            _mainTitleLabel.Font = new Font("Arial", _TitleTextSize, FontStyle.Bold);
            _mainTitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            _mainTitleLabel.ForeColor = _textColor;
            _mainTitleLabel.BackColor = _cellColor;
           

            _mainTitleLabel.Width = _columnCount * (buttonWidth + margin) - margin;
            _mainTitleLabel.Height = _TitleHeight;
            _mainTitleLabel.Location = new Point(0, 0);

            this.Controls.Add(_mainTitleLabel);

            int yOffset = _mainTitleLabel.Height + margin;

            tableCells = new SiticoneButton[_rowCount, _columnCount];

            for (int row = 0; row < _rowCount; row++)
            {
                for (int col = 0; col < _columnCount; col++)
                {
                    var button = new SiticoneButton
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Location = new Point(col * (buttonWidth + margin), yOffset + row * (buttonHeight + margin)),
                        Name = $"btn_{row}_{col}",
                        Text = $"[{row},{col}]",
                        Font = new Font("Arial", fonstSize),
                        ButtonBackColor = _cellColor,
                        BorderColor = _borderColor,
                        BorderWidth = _borderWidth,
                        ForeColor = _textColor,
                        HoverTextColor = _textColor,
                        ReadOnlyTextColor = _textColor,
                        DisabledTextColor = _textColor,
                        TextColor = _textColor,
                        IsReadOnly = true,
                    };

                    this.Controls.Add(button);
                    tableCells[row, col] = button;
                }
            }

            this.Width = _columnCount * (buttonWidth + margin);
            this.Height = yOffset + _rowCount * (buttonHeight + margin);

            TryRenderTable();
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Title Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("")]
        public String Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                _mainTitleLabel.Text = value;
                GenerateTable();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Title Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("")]
        public int TitleHeight
        {
            get { return _TitleHeight; }
            set
            {
                _TitleHeight = value;
                _mainTitleLabel.Height = value;
                GenerateTable();
            }
        }
        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Title Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("")]
        public int TitleTextSize
        {
            get { return _TitleTextSize; }
            set
            {
                _TitleTextSize = value;
                _mainTitleLabel.Font = new Font("Arial", _TitleTextSize, FontStyle.Bold);
                GenerateTable();
            }
        }

        private void TryRenderTable()
        {
            if (_headers == null || _data == null || tableCells == null)
                return;

            int requiredHeaderCount = (_columnCount - 1) + (_rowCount - 1);
            if (_headers.Count < requiredHeaderCount)
                return;

            for (int col = 1; col < _columnCount; col++)
            {
                tableCells[0, col].Text = _headers.ElementAtOrDefault(col - 1) ?? "";
            }

            for (int row = 1; row < _rowCount; row++)
            {
                tableCells[row, 0].Text = _headers.ElementAtOrDefault((_columnCount - 1) + row - 1) ?? "";
            }

            tableCells[0, 0].Text = "";

            int dataIndex = 0;
            for (int row = 1; row < _rowCount; row++)
            {
                for (int col = 1; col < _columnCount; col++)
                {
                    tableCells[row, col].Text = dataIndex < _data.Count ? _data[dataIndex] : "";
                    dataIndex++;
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Header Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Main Title Text for the Table")]
        public string MainTitle
        {
            get => _mainTitle;
            set
            {
                _mainTitle = value;
                _mainTitleLabel.Text = value;
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Component Color")]
        public Color BackgroundColor
        {
            get { return _backColor; }
            set { _backColor = value; this.BackColor = value; this.Invalidate(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Cells Border Color")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Cells Back Color")]
        public Color CellColor
        {
            get { return _cellColor; }
            set { _cellColor = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Colors Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Cells Text Color")]
        public Color TextForeColor
        {
            get { return _textColor; }
            set { _textColor = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Cells Border Thickness")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Tables Row Count")]
        public int RowCount
        {
            get => _rowCount;
            set { _rowCount = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Tables Column Count")]
        public int ColumnCount
        {
            get => _columnCount;
            set { _columnCount = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changs Cellse Text Size")]
        public int fontSize
        {
            get => fonstSize;
            set
            {
                fonstSize = value;
                foreach (var button in tableCells)
                {
                    button.Font = new Font("Arial", fonstSize);
                }
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Tables Width Size")]
        public int CellWidth
        {
            get => buttonWidth;
            set { buttonWidth = value; GenerateTable(); }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Other Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes Tables Height Size")]
        public int CellHeight
        {
            get => buttonHeight;
            set { buttonHeight = value; GenerateTable(); }
        }

        [HMIPortProperty(HMIPortDirection.Input)]
        [Description("Title Data: List of strings for the first row and first column.")]
        public List<string> Headers
        {
            get => _headers;
            set { _headers = value ?? new List<string>(); TryRenderTable(); }
        }

        [HMIPortProperty(HMIPortDirection.Input)]
        [Description("Datas: List of strings in order to fill all cells row by row.")]
        public List<string> Data
        {
            get => _data;
            set { _data = value ?? new List<string>(); TryRenderTable(); }
        }
    }
}
