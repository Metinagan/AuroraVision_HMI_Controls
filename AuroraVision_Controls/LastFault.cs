using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using HMI;
using System.ComponentModel;

namespace AuroraVision_Controls
{
    [HMI.HMIToolboxAttribute("S-PrintMachineUI")]
    [ToolboxBitmap(typeof(pieChart), "Icons.errors.png")]
    public partial class LastFault : UserControl
    {
        private Image _normalImage;
        private Image _defectImage;
        private bool _isDefectEmpty = true;
        private int _faultSize = 0;

        private int _maxDefects = 3; //kullanılacak pb sayısı
        private int count = 0;
        private int _totalFault = 0;

        //pb lerin olduğu liste
        private List<PictureBox> defectPictureBoxes = new List<PictureBox>();

        //Resimlerin saklanacağı alanlar
        private Bitmap[] defectImages;
        private Bitmap[] normalImages;
        private int[] faultSizes;

        private Color _backColor = Color.White;

        private BorderStyle _bs = BorderStyle.FixedSingle;

        public LastFault()
        {
            InitializeComponent();
            UpdatePictureBoxesLayout();
            this.BackColor = _backColor;
        }

        // Defect resimlerinin saklanacağı alan
        private void InitializeDefectStorage()
        {
            defectImages = new Bitmap[_maxDefects];
            normalImages = new Bitmap[_maxDefects];
            faultSizes = new int[_maxDefects];
            defectPictureBoxes.Clear();
        }

        // PictureBox'ları dinamik oluşturma
        private void CreateDefectPictureBoxes()
        {
            // Önceki PictureBox'ları sil
            foreach (var pb in this.Controls.OfType<PictureBox>().ToList())
            {
                this.Controls.Remove(pb);
                pb.Dispose();
            }

            // Yeni PictureBox'ları oluştur
            for (int i = 0; i < _maxDefects; i++)
            {
                PictureBox pb = new PictureBox
                {
                    Size = new Size(100, 100), // Boyutları başlangıçta sabit belirliyoruz
                    BorderStyle = _bs,
                    BackColor = _backColor,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Tag = i // Indexi Tag özelliğine atıyoruz
                };

                // Tıklama olayını her PictureBox'a ekleyelim
                pb.Click += PictureBox_Click;

                this.Controls.Add(pb);
                defectPictureBoxes.Add(pb);
            }

            // Ekran yeniden boyutlandığında PictureBox'ları güncelle
            this.Resize += (s, e) => UpdatePictureBoxesLayout();
        }
        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox clickedPictureBox = sender as PictureBox;
            if (clickedPictureBox != null)
            {
                //tıklanan pb index al
                int index = (int)clickedPictureBox.Tag;

                // Hem normal hem defect resmini al
                Bitmap normal = normalImages[index];
                Bitmap defect = defectImages[index];
                int fsize = faultSizes[index];
                
                //Message box'a ilgili verileri gönder
                LastFault2 lf2 = new LastFault2(normal, defect,index,_totalFault,fsize);
                lf2.ShowDialog();
            }
        }



        // PictureBox'ların boyut ve konumlarını ekran boyutuna göre güncelleme
        private void UpdatePictureBoxesLayout()
        {
            if (defectPictureBoxes == null || defectPictureBoxes.Count == 0)
                return; // PictureBox yoksa işlem yapma

            int formWidth = this.ClientSize.Width;
            int formHeight = this.ClientSize.Height;
            int spacing = 10;

            int availableHeight = formHeight - 2 * spacing;

            int newWidth = (formWidth - (defectPictureBoxes.Count - 1) * spacing) / defectPictureBoxes.Count;
            int newHeight = availableHeight;

            for (int i = 0; i < defectPictureBoxes.Count; i++)
            {
                int xPos = i * (newWidth + spacing);
                int yPos = spacing;
                defectPictureBoxes[i].Size = new Size(newWidth, newHeight);
                defectPictureBoxes[i].Location = new Point(xPos, yPos);
            }
        }


        //Normal resim girişi
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        [Description("Description: Normal image input")]
        public Image LastImage
        {
            get { return _normalImage; }
            set
            {
                _normalImage = value;
                //Hatalı resim varsa
                if (!_isDefectEmpty && _defectImage != null)
                {
                    // Resimleri Bitmap olarak kaydediyoruz
                    defectImages[count] = new Bitmap(DefectImage);
                    defectPictureBoxes[count].Image = defectImages[count];

                    normalImages[count] = new Bitmap(_normalImage);

                    faultSizes[count] = _faultSize;

                    // Sayacı dairesel yaparak en son defecti göster
                    count = (count + 1) % _maxDefects;
                    _totalFault++;
                }
            }
        }

        //Hatalı resim girişi
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        [Description("Description: Fault image input")]
        public Image DefectImage
        {
            get { return _defectImage; }
            set { _defectImage = value; }
        }

        //Resimde hatalı bölgenin olup/olmadığının kontrolü
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        [Description("Description: Is there a defect area in the image?")]
        public bool IsDefectEmpty
        {
            get { return _isDefectEmpty; }
            set
            {
                _isDefectEmpty = value;
            }
        }

        //Hatalı bölgenin alanı(piksel)
        [HMI.HMIPortProperty(HMI.HMIPortDirection.Input)]
        [Description("Description: Area of ​​defect region (Total pixel count) ")]
        public int FaultSize
        {
            get { return _faultSize; }
            set
            {
                _faultSize= value;

            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Picture Box Count")]
        public int maxDefects
        {
            get { return _maxDefects; }
            set
            {
                _maxDefects = value;
                InitializeDefectStorage();
                CreateDefectPictureBoxes();
                UpdatePictureBoxesLayout(); 
                this.Refresh(); 
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Picture Box Border Style")]
        public BorderStyle PbBorderStyle
        {
            get
            {
                return _bs;
            }
            set
            {
                _bs = value;
                InitializeDefectStorage();
                CreateDefectPictureBoxes();
                UpdatePictureBoxesLayout();
                this.Refresh();
            }
        }

        [HMIBrowsable(true)]
        [Browsable(true)]
        [Category("Custom Settings")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [HMI.HMIPortProperty(HMIPortDirection.HiddenInput)]
        [Description("Changes the Background Color")]
        public Color Backgroundcolor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                InitializeDefectStorage();
                CreateDefectPictureBoxes();
                UpdatePictureBoxesLayout();
                this.BackColor = value;
                this.Refresh();
            }
        }
    }
}