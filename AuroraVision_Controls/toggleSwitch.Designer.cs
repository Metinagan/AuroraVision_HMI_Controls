namespace AuroraVision_Controls
{
    partial class toggleSwitch
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
            this.toggleSwitch1 = new SiticoneNetFrameworkUI.SiticoneToggleSwitch();
            this.SuspendLayout();
            // 
            // toggleSwitch1
            // 
            this.toggleSwitch1.AccessibleDescription = "A customizable toggle switch that can be turned on or off.";
            this.toggleSwitch1.AccessibleName = "Siticone Toggle Switch";
            this.toggleSwitch1.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.toggleSwitch1.AnimationRipple = true;
            this.toggleSwitch1.CanBeep = true;
            this.toggleSwitch1.CanShake = true;
            this.toggleSwitch1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.toggleSwitch1.DisallowToggling = false;
            this.toggleSwitch1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toggleSwitch1.EnableGlowEffect = true;
            this.toggleSwitch1.EnableHoverAnimation = true;
            this.toggleSwitch1.EnablePressAnimation = true;
            this.toggleSwitch1.EnableRippleEffect = true;
            this.toggleSwitch1.ExtraThumbSize = 2;
            this.toggleSwitch1.IsReadOnly = false;
            this.toggleSwitch1.IsRequired = false;
            this.toggleSwitch1.LabelColor = System.Drawing.Color.Black;
            this.toggleSwitch1.LabelFont = new System.Drawing.Font("Segoe UI", 9F);
            this.toggleSwitch1.Location = new System.Drawing.Point(0, 0);
            this.toggleSwitch1.Logger = null;
            this.toggleSwitch1.Name = "toggleSwitch1";
            this.toggleSwitch1.OffAnimationSpeed = 115;
            this.toggleSwitch1.OffBackColor1 = System.Drawing.Color.Silver;
            this.toggleSwitch1.OffBackColor2 = System.Drawing.Color.Silver;
            this.toggleSwitch1.OffBorderColor1 = System.Drawing.Color.Silver;
            this.toggleSwitch1.OffBorderColor2 = System.Drawing.Color.Silver;
            this.toggleSwitch1.OffIcon = null;
            this.toggleSwitch1.OffThumbColor1 = System.Drawing.Color.White;
            this.toggleSwitch1.OffThumbColor2 = System.Drawing.Color.White;
            this.toggleSwitch1.OnAnimationSpeed = 115;
            this.toggleSwitch1.OnBackColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.OnBackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.OnBorderColor1 = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.OnBorderColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.OnIcon = null;
            this.toggleSwitch1.OnThumbColor1 = System.Drawing.Color.White;
            this.toggleSwitch1.OnThumbColor2 = System.Drawing.Color.White;
            this.toggleSwitch1.PreventToggleOff = false;
            this.toggleSwitch1.RippleColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.toggleSwitch1.RippleExpansionRate = 2F;
            this.toggleSwitch1.RippleOpacityDecay = 0.02F;
            this.toggleSwitch1.Size = new System.Drawing.Size(70, 42);
            this.toggleSwitch1.TabIndex = 0;
            this.toggleSwitch1.ToggleOffSoundPath = "";
            this.toggleSwitch1.ToggleOnSoundPath = "";
            this.toggleSwitch1.ToolTipText = "";
            this.toggleSwitch1.TrackDeviceTheme = true;
            // 
            // toggleSwitch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toggleSwitch1);
            this.Name = "toggleSwitch";
            this.Size = new System.Drawing.Size(70, 42);
            this.ResumeLayout(false);

        }

        #endregion

        private SiticoneNetFrameworkUI.SiticoneToggleSwitch toggleSwitch1;
    }
}
