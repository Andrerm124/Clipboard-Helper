namespace Clipboard_Helper
{
    partial class CustomDeleteButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDeleteButton));
            this.SuspendLayout();
            // 
            // CustomDeleteButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DoubleBuffered = true;
            this.Name = "CustomDeleteButton";
            this.Size = new System.Drawing.Size(62, 62);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CustomDeleteButton_Paint);
            this.MouseEnter += new System.EventHandler(this.CustomDeleteButton_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.CustomDeleteButton_MouseLeave);
            this.ResumeLayout(false);

        }

        #endregion

    }
}
