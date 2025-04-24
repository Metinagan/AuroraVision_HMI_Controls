namespace AuroraVision_Controls
{
    partial class ClickDragDrop
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Right_Slider = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_Left_Slider = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 500);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.panel2.Controls.Add(this.btn_cancel);
            this.panel2.Controls.Add(this.btn_ok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(206, 400);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(408, 100);
            this.panel2.TabIndex = 7;
            // 
            // btn_cancel
            // 
            this.btn_cancel.AutoEllipsis = true;
            this.btn_cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_cancel.FlatAppearance.BorderSize = 0;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_cancel.ForeColor = System.Drawing.Color.White;
            this.btn_cancel.Image = global::AuroraVision_Controls.Properties.Resources.Cancel_50X50;
            this.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.Location = new System.Drawing.Point(168, 24);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(0);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(203, 50);
            this.btn_cancel.TabIndex = 1;
            this.btn_cancel.Text = "       CANCEL";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.UseMnemonic = false;
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // btn_ok
            // 
            this.btn_ok.AutoEllipsis = true;
            this.btn_ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_ok.FlatAppearance.BorderSize = 0;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_ok.ForeColor = System.Drawing.Color.White;
            this.btn_ok.Image = global::AuroraVision_Controls.Properties.Resources.Ok_50X50;
            this.btn_ok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok.Location = new System.Drawing.Point(38, 24);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(0);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(130, 50);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "       OK";
            this.btn_ok.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_ok.UseMnemonic = false;
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.flowLayoutPanel2.Controls.Add(this.btn_Right_Slider);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(614, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(206, 500);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // btn_Right_Slider
            // 
            this.btn_Right_Slider.AutoEllipsis = true;
            this.btn_Right_Slider.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Right_Slider.FlatAppearance.BorderSize = 0;
            this.btn_Right_Slider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Right_Slider.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Right_Slider.ForeColor = System.Drawing.Color.White;
            this.btn_Right_Slider.Image = global::AuroraVision_Controls.Properties.Resources.Arrow_Right50X50;
            this.btn_Right_Slider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Right_Slider.Location = new System.Drawing.Point(0, 0);
            this.btn_Right_Slider.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Right_Slider.Name = "btn_Right_Slider";
            this.btn_Right_Slider.Size = new System.Drawing.Size(240, 50);
            this.btn_Right_Slider.TabIndex = 1;
            this.btn_Right_Slider.Text = "       Colors";
            this.btn_Right_Slider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Right_Slider.UseMnemonic = false;
            this.btn_Right_Slider.UseVisualStyleBackColor = true;
            this.btn_Right_Slider.Click += new System.EventHandler(this.btn_Right_Slider_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(39)))), ((int)(((byte)(43)))));
            this.flowLayoutPanel1.Controls.Add(this.btn_Left_Slider);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(206, 500);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btn_Left_Slider
            // 
            this.btn_Left_Slider.AutoEllipsis = true;
            this.btn_Left_Slider.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_Left_Slider.FlatAppearance.BorderSize = 0;
            this.btn_Left_Slider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Left_Slider.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btn_Left_Slider.ForeColor = System.Drawing.Color.White;
            this.btn_Left_Slider.Image = global::AuroraVision_Controls.Properties.Resources.Arrow_Left50X50;
            this.btn_Left_Slider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Left_Slider.Location = new System.Drawing.Point(0, 0);
            this.btn_Left_Slider.Margin = new System.Windows.Forms.Padding(0);
            this.btn_Left_Slider.Name = "btn_Left_Slider";
            this.btn_Left_Slider.Size = new System.Drawing.Size(240, 50);
            this.btn_Left_Slider.TabIndex = 1;
            this.btn_Left_Slider.Text = "       Colors";
            this.btn_Left_Slider.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_Left_Slider.UseMnemonic = false;
            this.btn_Left_Slider.UseVisualStyleBackColor = true;
            this.btn_Left_Slider.Click += new System.EventHandler(this.btn_Left_Slider_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(820, 500);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ClickDragDrop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ClickDragDrop";
            this.Size = new System.Drawing.Size(820, 500);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button btn_Right_Slider;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btn_Left_Slider;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
    }
}
