namespace AuroraVision_Controls
{
    partial class progressBarH
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
            this.label1 = new System.Windows.Forms.Label();
            this.siticoneHSlider1 = new SiticoneNetFrameworkUI.SiticoneHSlider();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.MinimumSize = new System.Drawing.Size(0, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 40);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // siticoneHSlider1
            // 
            this.siticoneHSlider1.AccessibleDescription = "A horizontal slider control that allows users to select a value within a specifie" +
    "d range.";
            this.siticoneHSlider1.AccessibleName = "Horizontal Slider";
            this.siticoneHSlider1.AccessibleRole = System.Windows.Forms.AccessibleRole.Slider;
            this.siticoneHSlider1.AllowDrop = true;
            this.siticoneHSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.siticoneHSlider1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneHSlider1.CanBeep = false;
            this.siticoneHSlider1.CanShake = true;
            this.siticoneHSlider1.ContextMenuFont = new System.Drawing.Font("Segoe UI", 12F);
            this.siticoneHSlider1.ControlMargin = new System.Windows.Forms.Padding(0);
            this.siticoneHSlider1.ElapsedTrackColor = System.Drawing.Color.Blue;
            this.siticoneHSlider1.GlowColor = System.Drawing.Color.LightBlue;
            this.siticoneHSlider1.GlowOffset = 0;
            this.siticoneHSlider1.GlowSize = 8;
            this.siticoneHSlider1.HoverAnimationInterval = 15;
            this.siticoneHSlider1.HoverEffects = false;
            this.siticoneHSlider1.ImeMode = System.Windows.Forms.ImeMode.On;
            this.siticoneHSlider1.IsReadOnly = false;
            this.siticoneHSlider1.Location = new System.Drawing.Point(89, 0);
            this.siticoneHSlider1.Maximum = 100;
            this.siticoneHSlider1.Minimum = 0;
            this.siticoneHSlider1.MinimumSize = new System.Drawing.Size(255, 40);
            this.siticoneHSlider1.MouseWheelDelta = 1;
            this.siticoneHSlider1.Name = "siticoneHSlider1";
            this.siticoneHSlider1.ReadOnlyBorderColor = System.Drawing.Color.DimGray;
            this.siticoneHSlider1.ReadOnlyElapsedTrackColor = System.Drawing.Color.DarkGray;
            this.siticoneHSlider1.ReadOnlyThumbColor = System.Drawing.Color.Gray;
            this.siticoneHSlider1.ReadOnlyThumbSize = 12;
            this.siticoneHSlider1.ReadOnlyTrackColor = System.Drawing.Color.LightGray;
            this.siticoneHSlider1.ShadowEnabled = false;
            this.siticoneHSlider1.ShakeAnimationInterval = 50;
            this.siticoneHSlider1.ShowToolTip = true;
            this.siticoneHSlider1.Size = new System.Drawing.Size(255, 40);
            this.siticoneHSlider1.TabIndex = 2;
            this.siticoneHSlider1.Text = "Progress Bar";
            this.siticoneHSlider1.ThumbBorderColor = System.Drawing.Color.MediumBlue;
            this.siticoneHSlider1.ThumbColor = System.Drawing.Color.Blue;
            this.siticoneHSlider1.ThumbPressShrink = 2;
            this.siticoneHSlider1.ThumbSize = 10;
            this.siticoneHSlider1.ThumbType = SiticoneNetFrameworkUI.Helpers.Enum.ThumbType.Solid;
            this.siticoneHSlider1.TrackColor = System.Drawing.Color.Gray;
            this.siticoneHSlider1.Value = 50;
            this.siticoneHSlider1.ValueAnimationInterval = 1;
            // 
            // progressBarH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.siticoneHSlider1);
            this.Controls.Add(this.label1);
            this.MinimumSize = new System.Drawing.Size(150, 40);
            this.Name = "progressBarH";
            this.Size = new System.Drawing.Size(373, 40);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private SiticoneNetFrameworkUI.SiticoneHSlider siticoneHSlider1;
    }
}
