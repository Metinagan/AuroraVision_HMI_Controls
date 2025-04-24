namespace AuroraVision_Controls
{
    partial class Buttons
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
            this.indicatorPB = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.indicatorPB)).BeginInit();
            this.SuspendLayout();
            // 
            // indicatorPB
            // 
            this.indicatorPB.Image = global::AuroraVision_Controls.Properties.Resources.selected2;
            this.indicatorPB.Location = new System.Drawing.Point(3, 137);
            this.indicatorPB.Name = "indicatorPB";
            this.indicatorPB.Size = new System.Drawing.Size(40, 40);
            this.indicatorPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.indicatorPB.TabIndex = 0;
            this.indicatorPB.TabStop = false;
            // 
            // Buttons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.indicatorPB);
            this.DoubleBuffered = true;
            this.Name = "Buttons";
            this.Size = new System.Drawing.Size(200, 180);
            ((System.ComponentModel.ISupportInitialize)(this.indicatorPB)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox indicatorPB;
    }
}
