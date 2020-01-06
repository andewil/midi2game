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
            this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
            this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMain = new System.Windows.Forms.ToolStrip();
            this.tsComboBoxDevice = new System.Windows.Forms.ToolStripComboBox();
            this.tsbOpenDevice = new System.Windows.Forms.ToolStripButton();
            this.tsbCloseDevice = new System.Windows.Forms.ToolStripButton();
            this.tsbOpenDataDirectory = new System.Windows.Forms.ToolStripButton();
            this.tsbMonitor = new System.Windows.Forms.ToolStripButton();
            this.listView1 = new System.Windows.Forms.ListView();
            this.chName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRule = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAction = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.toolStripMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // BottomToolStripPanel
            // 
            this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.BottomToolStripPanel.Name = "BottomToolStripPanel";
            this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // RightToolStripPanel
            // 
            this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.RightToolStripPanel.Name = "RightToolStripPanel";
            this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // LeftToolStripPanel
            // 
            this.LeftToolStripPanel.Location = new System.Drawing.Point(0, 0);
            this.LeftToolStripPanel.Name = "LeftToolStripPanel";
            this.LeftToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(800, 425);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 3;
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
            this.toolStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsComboBoxDevice,
            this.tsbOpenDevice,
            this.tsbCloseDevice,
            this.tsbOpenDataDirectory,
            this.tsbMonitor});
            this.toolStripMain.Location = new System.Drawing.Point(0, 24);
            this.toolStripMain.Name = "toolStripMain";
            this.toolStripMain.Size = new System.Drawing.Size(800, 25);
            this.toolStripMain.TabIndex = 2;
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
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chNo,
            this.chName,
            this.chRule,
            this.chAction});
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(284, 73);
            this.listView1.Name = "listView1";
            this.listView1.ShowGroups = false;
            this.listView1.Size = new System.Drawing.Size(504, 365);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // chName
            // 
            this.chName.Text = "Name";
            this.chName.Width = 150;
            // 
            // chRule
            // 
            this.chRule.Text = "Rule";
            this.chRule.Width = 150;
            // 
            // chAction
            // 
            this.chAction.Text = "Action";
            this.chAction.Width = 150;
            // 
            // chNo
            // 
            this.chNo.Text = "No";
            // 
            // FormMidi2Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.toolStripMain);
            this.Controls.Add(this.menuStrip1);
            this.Name = "FormMidi2Game";
            this.Text = "MIDI2game";
            this.Load += new System.EventHandler(this.FormMidi2Game_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStripMain.ResumeLayout(false);
            this.toolStripMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripPanel BottomToolStripPanel;
        private System.Windows.Forms.ToolStripPanel RightToolStripPanel;
        private System.Windows.Forms.ToolStripPanel LeftToolStripPanel;
        private System.Windows.Forms.ToolStripContentPanel ContentPanel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStripMain;
        private System.Windows.Forms.ToolStripComboBox tsComboBoxDevice;
        private System.Windows.Forms.ToolStripButton tsbOpenDevice;
        private System.Windows.Forms.ToolStripButton tsbCloseDevice;
        private System.Windows.Forms.ToolStripButton tsbOpenDataDirectory;
        private System.Windows.Forms.ToolStripButton tsbMonitor;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader chName;
        private System.Windows.Forms.ColumnHeader chRule;
        private System.Windows.Forms.ColumnHeader chAction;
        private System.Windows.Forms.ColumnHeader chNo;
    }
}

