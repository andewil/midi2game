namespace midi2games
{
    partial class FormMidi2Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMidi2Game));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsComboBoxDevice = new System.Windows.Forms.ToolStripComboBox();
            this.tsbOpenDevice = new System.Windows.Forms.ToolStripButton();
            this.tsbCloseDevice = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenDataDirectory = new System.Windows.Forms.ToolStripButton();
            this.tsbMonitor = new System.Windows.Forms.ToolStripButton();
            this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.ContentPanel
            // 
            this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(800, 401);
            this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer1.Name = "toolStripContainer1";
            this.toolStripContainer1.Size = new System.Drawing.Size(800, 450);
            this.toolStripContainer1.TabIndex = 2;
            this.toolStripContainer1.Text = "toolStripContainer1";
            // 
            // toolStripContainer1.TopToolStripPanel
            // 
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
            this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStripMain);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuMain";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // toolStripMain
            // 
            this.toolStripMain.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsComboBoxDevice,
            this.tsbOpenDevice,
            this.tsbCloseDevice,
            this.tsbOpenDataDirectory,
            this.tsbMonitor});
            this.toolStripMain.Location = new System.Drawing.Point(3, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(687, 25);
            this.toolStripMain.TabIndex = 0;
            // 
            // tsComboBoxDevice
            // 
            this.tsComboBoxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tsComboBoxDevice.Name = "tsComboBoxDevice";
            this.tsComboBoxDevice.Size = new System.Drawing.Size(300, 25);
            // 
            // tsbOpenDevice
            // 
            this.tsbOpenDevice.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenDevice.Image")));
            this.tsbOpenDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenDevice.Name = "tsbOpenDevice";
            this.tsbOpenDevice.Size = new System.Drawing.Size(56, 22);
            this.tsbOpenDevice.Text = "Open";
            this.tsbOpenDevice.Click += new System.EventHandler(this.tsbOpenDevice_Click);
            // 
            // tsbCloseDevice
            // 
            this.tsbCloseDevice.Image = ((System.Drawing.Image)(resources.GetObject("tsbCloseDevice.Image")));
            this.tsbCloseDevice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCloseDevice.Name = "tsbCloseDevice";
            this.tsbCloseDevice.Size = new System.Drawing.Size(56, 22);
            this.tsbCloseDevice.Text = "Close";
            this.tsbCloseDevice.Click += new System.EventHandler(this.tsbCloseDevice_Click);
            // 
            // tsbOpenDataDirectory
            // 
            this.tsbOpenDataDirectory.Image = ((System.Drawing.Image)(resources.GetObject("tsbOpenDataDirectory.Image")));
            this.tsbOpenDataDirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbOpenDataDirectory.Name = "tsbOpenDataDirectory";
            this.tsbOpenDataDirectory.Size = new System.Drawing.Size(132, 22);
            this.tsbOpenDataDirectory.Text = "Open data directory";
            this.tsbOpenDataDirectory.Click += new System.EventHandler(this.tsbOpenDataDirectory_Click);
            // 
            // tsbMonitor
            // 
            this.tsbMonitor.Image = ((System.Drawing.Image)(resources.GetObject("tsbMonitor.Image")));
            this.tsbMonitor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMonitor.Name = "tsbMonitor";
            this.tsbMonitor.Size = new System.Drawing.Size(98, 22);
            this.tsbMonitor.Text = "MIDI monitor";
            this.tsbMonitor.Click += new System.EventHandler(this.tsbMonitor_Click);
            // 
            // FormMidi2Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripContainer1);
            this.Name = "FormMidi2Game";
            this.Text = "MIDI2game";
            this.Load += new System.EventHandler(this.FormMidi2Game_Load);
            this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer1.TopToolStripPanel.PerformLayout();
            this.toolStripContainer1.ResumeLayout(false);
            this.toolStripContainer1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripContainer toolStripContainer1;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox tsComboBoxDevice;
        private System.Windows.Forms.ToolStripButton tsbOpenDevice;
        private System.Windows.Forms.ToolStripButton tsbCloseDevice;
        private System.Windows.Forms.ToolStripButton tsbOpenDataDirectory;
        private System.Windows.Forms.ToolStripButton tsbMonitor;
    }
}

