namespace Clipboard_Helper
{
    partial class MasterForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterForm));
            this.trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlBase = new Clipboard_Helper.CustomPanel();
            this.customDeleteButton1 = new Clipboard_Helper.CustomDeleteButton();
            this.lstClips = new Clipboard_Helper.CustomListBox();
            this.clipboardMonitor1 = new Clipboard_Helper.ClipboardMonitor();
            this.button = new Clipboard_Helper.CustomDeleteButton();
            this.trayIconMenu.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.lstClips.SuspendLayout();
            this.SuspendLayout();
            // 
            // trayIcon
            // 
            this.trayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.trayIcon.BalloonTipText = "Your clipboard is being recorded in the background";
            this.trayIcon.BalloonTipTitle = "Clipboard History";
            this.trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            this.trayIcon.Text = "Clipboard History";
            this.trayIcon.BalloonTipClicked += new System.EventHandler(this.trayIcon_BalloonTipClicked);
            this.trayIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.trayIcon_MouseClick);
            // 
            // trayIconMenu
            // 
            this.trayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem,
            this.settingsToolStripMenuItem});
            this.trayIconMenu.Name = "trayIconMenu";
            this.trayIconMenu.Size = new System.Drawing.Size(117, 48);
            this.trayIconMenu.Text = "Clipboard History";
            this.trayIconMenu.Opening += new System.ComponentModel.CancelEventHandler(this.trayIconMenu_Opening);
            this.trayIconMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.trayIconMenu_ItemClicked);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.pnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBase.Controls.Add(this.customDeleteButton1);
            this.pnlBase.Controls.Add(this.lstClips);
            this.pnlBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.pnlBase.Location = new System.Drawing.Point(0, 0);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(284, 261);
            this.pnlBase.TabIndex = 0;
            this.pnlBase.Tag = "Clipboard History";
            this.pnlBase.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.pnlBase.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.pnlBase.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // customDeleteButton1
            // 
            this.customDeleteButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("customDeleteButton1.BackgroundImage")));
            this.customDeleteButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.customDeleteButton1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customDeleteButton1.Location = new System.Drawing.Point(254, 1);
            this.customDeleteButton1.Margin = new System.Windows.Forms.Padding(1);
            this.customDeleteButton1.Name = "customDeleteButton1";
            this.customDeleteButton1.Size = new System.Drawing.Size(27, 27);
            this.customDeleteButton1.TabIndex = 1;
            this.customDeleteButton1.Click += new System.EventHandler(this.customDeleteButton1_Click);
            // 
            // lstClips
            // 
            this.lstClips.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.lstClips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstClips.Controls.Add(this.clipboardMonitor1);
            this.lstClips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lstClips.Location = new System.Drawing.Point(4, 30);
            this.lstClips.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.lstClips.Name = "lstClips";
            this.lstClips.Size = new System.Drawing.Size(275, 226);
            this.lstClips.TabIndex = 0;
            this.lstClips.Tag = "";
            // 
            // clipboardMonitor1
            // 
            this.clipboardMonitor1.BackColor = System.Drawing.Color.Red;
            this.clipboardMonitor1.Location = new System.Drawing.Point(92, 198);
            this.clipboardMonitor1.Name = "clipboardMonitor1";
            this.clipboardMonitor1.Size = new System.Drawing.Size(75, 23);
            this.clipboardMonitor1.TabIndex = 1;
            this.clipboardMonitor1.Text = "clipboardMonitor1";
            this.clipboardMonitor1.Visible = false;
            this.clipboardMonitor1.ClipboardChanged += new System.EventHandler<Clipboard_Helper.ClipboardChangedEventArgs>(this.clipboardMonitor1_ClipboardChanged);
            // 
            // button
            // 
            this.button.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button.BackgroundImage")));
            this.button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.button.Location = new System.Drawing.Point(0, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(34, 35);
            this.button.TabIndex = 0;
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pnlBase);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MasterForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MasterForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MasterForm_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.MasterForm_Resize);
            this.trayIconMenu.ResumeLayout(false);
            this.pnlBase.ResumeLayout(false);
            this.lstClips.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomPanel pnlBase;
        public CustomListBox lstClips;
        private ClipboardMonitor clipboardMonitor1;
        private CustomDeleteButton button;
        private CustomDeleteButton customDeleteButton1;
        public System.Windows.Forms.NotifyIcon trayIcon;
        private System.Windows.Forms.ContextMenuStrip trayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
    }
}

