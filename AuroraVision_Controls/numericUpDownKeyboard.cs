using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace AuroraVision_Controls
{
    public partial class numericUpDownKeyboard : Form
    {
        private float _resultValue;
        private bool isNegative = false;
        private bool isFirstInput = true;
        private decimal _min = 0;
        private decimal _max = 0;
        private int _increment;

        public bool choseeAll = true;

        public float ResultValue
        {
            get { return _resultValue; }
        }

        public numericUpDownKeyboard(float value,decimal max,decimal min,Color numpadBackColor)
        {
            InitializeComponent();
            this.ControlBox = false;           // Tüm kontrol butonlarını (küçült, büyüt, kapat) gizler
            this.MinimizeBox = false;          // Küçültme butonunu gizler
            this.MaximizeBox = false;          // Büyütme butonunu gizler
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.FormBorderStyle = FormBorderStyle.None;
            textBox1.BackColor = System.Drawing.Color.Black;

            this.BackColor = numpadBackColor;

            _min = min;
            _max = max;
            


            label1.Text="Min: " + min.ToString() + "   /   Max: " + max.ToString();

            StartPosition = FormStartPosition.CenterParent;

            if (choseeAll)
            {
                textBox1.BackColor = System.Drawing.Color.AliceBlue;
            }

            SetValue(value);
            textBox1.SelectAll();

            pictureBox0.Click += (s, e) => { AddDigit("0"); ResetTextBoxBackground(); };
            pictureBox1.Click += (s, e) => { AddDigit("1"); ResetTextBoxBackground(); };
            pictureBox2.Click += (s, e) => { AddDigit("2"); ResetTextBoxBackground(); };
            pictureBox3.Click += (s, e) => { AddDigit("3"); ResetTextBoxBackground(); };
            pictureBox4.Click += (s, e) => { AddDigit("4"); ResetTextBoxBackground(); };
            pictureBox5.Click += (s, e) => { AddDigit("5"); ResetTextBoxBackground(); };
            pictureBox6.Click += (s, e) => { AddDigit("6"); ResetTextBoxBackground(); };
            pictureBox7.Click += (s, e) => { AddDigit("7"); ResetTextBoxBackground(); };
            pictureBox8.Click += (s, e) => { AddDigit("8"); ResetTextBoxBackground(); };
            pictureBox9.Click += (s, e) => { AddDigit("9"); ResetTextBoxBackground(); };
            pictureBoxsil.Click += (s, e) => { RemoveLastDigit(); ResetTextBoxBackground(); };
            pictureBoxnokta.Click += (s, e) => { AddComma(); ResetTextBoxBackground(); };
            pictureBoxeksi.Click += (s, e) => { ToggleSign(s, e); ResetTextBoxBackground(); };
            
            textBox1.Click += nudclick;
        }



        private void AddDigit(string digit)
        {
            HandleFirstInput();

            if (textBox1.Text == "0") textBox1.Text = "";
            textBox1.Text += digit;
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void RemoveLastDigit()
        {
            if (textBox1.SelectionLength == textBox1.Text.Length)
            {
                textBox1.Text = "";
            }
            else if (textBox1.Text.Length > 0)
            {
                textBox1.Text = textBox1.Text.Substring(0, textBox1.Text.Length - 1);
            }

            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void AddComma()
        {
            HandleFirstInput();

            if (!textBox1.Text.Contains("."))
            {
                if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text == "-")
                {
                    textBox1.Text += "0.";
                }
                else
                {
                    textBox1.Text += ".";
                }
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }


        private void ToggleSign(object sender, EventArgs e)
        {
            HandleFirstInput();

            if (textBox1.Text.StartsWith("-"))
            {
                textBox1.Text = textBox1.Text.Substring(1);
            }
            else
            {
                textBox1.Text = "-" + textBox1.Text;
            }
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private void SetValue(float value)
        {
            if (value % 1 == 0)
                textBox1.Text = ((int)value).ToString();
            else
                textBox1.Text = value.ToString("0.000");

            textBox1.SelectionStart = textBox1.Text.Length;
        }

        private float SafeParse(string text)
        {
            // Nokta kullanımı için InvariantCulture ile parse et
            if (float.TryParse(text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float result))
                return result;
            return 0f;
        }



        private void nudclick(object sender, EventArgs e)
        {
            if (sender is TextBox tb)
            {
                textBox1.BackColor = System.Drawing.Color.White;
                choseeAll = false;
                tb.SelectionLength = 0;
                tb.SelectionStart = tb.Text.Length;
            }
        }

        private void ResetTextBoxBackground()
        {
            textBox1.BackColor = System.Drawing.Color.Black;
            textBox1.ForeColor = System.Drawing.Color.White;
        }

        private void HandleFirstInput()
        {
            if (isFirstInput)
            {
                textBox1.Text = "";
                isFirstInput = false;
            }
        }

        private void pictureBoxOnayla(object sender, EventArgs e)
        {
            _resultValue = SafeParse(textBox1.Text);

            // Min ve max kontrolü
            if (_resultValue < (float)_min)
            {
                _resultValue = (float)_min;
            }
            else if (_resultValue > (float)_max)
            {
                _resultValue = (float)_max;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void pictureboxX(object sender, EventArgs e)
        {
            this.Close();
        }

        private void formload(object sender, EventArgs e)
        {
            textBox1.BackColor = System.Drawing.Color.Black;
            textBox1.ForeColor = System.Drawing.Color.Blue;
        }
    }
}
