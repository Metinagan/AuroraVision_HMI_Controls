using AuroraVision_Controls.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuroraVision_Controls
{
    public partial class LastFault2: Form
    {

        private Bitmap _normalImage;
        private Bitmap _defectImage;

        private int _defectsCount = 0;
        private int _totalFault = 0;
        private int _faultSize = 0;

        private Boolean _checked = false;



        public LastFault2(Bitmap normalImage, Bitmap defectImage, int defectsCount, int totalFault, int faultSize)
        {
            InitializeComponent();

            // Form yeniden boyutlandırılamasın
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Gelen verileri al
            _normalImage = normalImage;
            _defectImage = defectImage;
            _faultSize = faultSize;

            int width = _normalImage.Width;
            int height = _normalImage.Height;
            pictureBox1.Size = new Size(width, height);

            // Ekran çözünürlüğünü al ve pencere boyutunu 2/3 yap
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            this.Width = (int)(25 + width + 75 + label1.Width + 10);
            this.Height = (int)(height + 100);
            pictureBox1.Location = new Point(25, 25);

            // Ortaya yerleştir
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(
                (screen.Width - this.Width) / 2,
                (screen.Height - this.Height) / 2
            );



            button1.BackgroundImage = Resources.eye;
            pictureBox1.Image = _defectImage;

            _defectsCount = defectsCount++;
            this.Text = "Fault: " + defectsCount + ". Box";
            _totalFault = totalFault;
            label1.Text = "Total Fault: " + _totalFault.ToString();
            label2.Text = "Fault Size: " + _faultSize.ToString();
        }



        private void ts1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_checked == false)
            {
                _checked = true;
                pictureBox1.Image = _normalImage;
                button1.BackgroundImage = Resources.hidden;

            }
            else
            {
                _checked = false;
                pictureBox1.Image = _defectImage;
                button1.BackgroundImage = Resources.eye;
            }
        }
    }
}
