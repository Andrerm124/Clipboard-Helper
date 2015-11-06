namespace Clipboard_Helper
{
    partial class SettingsForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlBackground = new Clipboard_Helper.CustomPanel();
            this.customPanel1 = new Clipboard_Helper.CustomPanel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.customDeleteButton2 = new Clipboard_Helper.CustomDeleteButton();
            this.pnlBackground.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBackground
            // 
            this.pnlBackground.Controls.Add(this.customPanel1);
            this.pnlBackground.Controls.Add(this.customDeleteButton2);
            this.pnlBackground.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pnlBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBackground.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(460, 104);
            this.pnlBackground.TabIndex = 0;
            this.pnlBackground.Tag = "Settings Menu";
            this.pnlBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlBackground_Paint);
            this.pnlBackground.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlBackground_MouseDown);
            this.pnlBackground.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlBackground_MouseMove);
            this.pnlBackground.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlBackground_MouseUp);
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.btnBrowse);
            this.customPanel1.Controls.Add(this.txtDirectory);
            this.customPanel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.customPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.customPanel1.Location = new System.Drawing.Point(13, 38);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(435, 52);
            this.customPanel1.TabIndex = 3;
            this.customPanel1.Tag = "Save Location";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(3, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(111, 27);
            this.btnBrowse.TabIndex = 1;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.txtDirectory.Location = new System.Drawing.Point(120, 22);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.ReadOnly = true;
            this.txtDirectory.Size = new System.Drawing.Size(312, 27);
            this.txtDirectory.TabIndex = 0;
            this.txtDirectory.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // customDeleteButton2
            // 
            this.customDeleteButton2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customDeleteButton2.BackgroundImage")));
            this.customDeleteButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customDeleteButton2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customDeleteButton2.Location = new System.Drawing.Point(435, 1);
            this.customDeleteButton2.Margin = new System.Windows.Forms.Padding(1);
            this.customDeleteButton2.Name = "customDeleteButton2";
            this.customDeleteButton2.Size = new System.Drawing.Size(24, 24);
            this.customDeleteButton2.TabIndex = 2;
            this.customDeleteButton2.Click += new System.EventHandler(this.customDeleteButton2_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.ClientSize = new System.Drawing.Size(460, 104);
            this.Controls.Add(this.pnlBackground);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.pnlBackground.ResumeLayout(false);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanel pnlBackground;
        private CustomDeleteButton customDeleteButton2;
        private CustomPanel customPanel1;
        private System.Windows.Forms.TextBox txtDirectory;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}