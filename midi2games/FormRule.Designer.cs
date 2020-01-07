﻿namespace midi2games
{
    partial class FormRule
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRule));
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabAdditionalProperties = new System.Windows.Forms.TabControl();
            this.tabPageAdditionalPropertiesRule = new System.Windows.Forms.TabPage();
            this.tabPageAdditionalPropertiesAction = new System.Windows.Forms.TabPage();
            this.editName = new System.Windows.Forms.TextBox();
            this.editStopProcessing = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxRuleType = new System.Windows.Forms.ListBox();
            this.listBoxActionType = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tsbPrevRule = new System.Windows.Forms.ToolStripButton();
            this.tsbNextRule = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            this.tabAdditionalProperties.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.listBoxActionType);
            this.panel1.Controls.Add(this.listBoxRuleType);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.editStopProcessing);
            this.panel1.Controls.Add(this.editName);
            this.panel1.Location = new System.Drawing.Point(8, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 242);
            this.panel1.TabIndex = 1;
            // 
            // tabAdditionalProperties
            // 
            this.tabAdditionalProperties.Controls.Add(this.tabPageAdditionalPropertiesRule);
            this.tabAdditionalProperties.Controls.Add(this.tabPageAdditionalPropertiesAction);
            this.tabAdditionalProperties.Location = new System.Drawing.Point(8, 276);
            this.tabAdditionalProperties.Name = "tabAdditionalProperties";
            this.tabAdditionalProperties.SelectedIndex = 0;
            this.tabAdditionalProperties.Size = new System.Drawing.Size(661, 186);
            this.tabAdditionalProperties.TabIndex = 2;
            // 
            // tabPageAdditionalPropertiesRule
            // 
            this.tabPageAdditionalPropertiesRule.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdditionalPropertiesRule.Name = "tabPageAdditionalPropertiesRule";
            this.tabPageAdditionalPropertiesRule.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdditionalPropertiesRule.Size = new System.Drawing.Size(653, 160);
            this.tabPageAdditionalPropertiesRule.TabIndex = 0;
            this.tabPageAdditionalPropertiesRule.Text = "Rule";
            this.tabPageAdditionalPropertiesRule.UseVisualStyleBackColor = true;
            // 
            // tabPageAdditionalPropertiesAction
            // 
            this.tabPageAdditionalPropertiesAction.Location = new System.Drawing.Point(4, 22);
            this.tabPageAdditionalPropertiesAction.Name = "tabPageAdditionalPropertiesAction";
            this.tabPageAdditionalPropertiesAction.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAdditionalPropertiesAction.Size = new System.Drawing.Size(653, 217);
            this.tabPageAdditionalPropertiesAction.TabIndex = 1;
            this.tabPageAdditionalPropertiesAction.Text = "Action";
            this.tabPageAdditionalPropertiesAction.UseVisualStyleBackColor = true;
            // 
            // editName
            // 
            this.editName.Location = new System.Drawing.Point(178, 21);
            this.editName.Name = "editName";
            this.editName.Size = new System.Drawing.Size(279, 20);
            this.editName.TabIndex = 0;
            // 
            // editStopProcessing
            // 
            this.editStopProcessing.AutoSize = true;
            this.editStopProcessing.Location = new System.Drawing.Point(178, 47);
            this.editStopProcessing.Name = "editStopProcessing";
            this.editStopProcessing.Size = new System.Drawing.Size(135, 17);
            this.editStopProcessing.TabIndex = 1;
            this.editStopProcessing.Text = "Stop further processign";
            this.editStopProcessing.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Rule name";
            // 
            // listBoxRuleType
            // 
            this.listBoxRuleType.FormattingEnabled = true;
            this.listBoxRuleType.Location = new System.Drawing.Point(178, 96);
            this.listBoxRuleType.Name = "listBoxRuleType";
            this.listBoxRuleType.Size = new System.Drawing.Size(232, 134);
            this.listBoxRuleType.TabIndex = 3;
            this.listBoxRuleType.SelectedIndexChanged += new System.EventHandler(this.listBoxRuleType_SelectedIndexChanged);
            // 
            // listBoxActionType
            // 
            this.listBoxActionType.FormattingEnabled = true;
            this.listBoxActionType.Location = new System.Drawing.Point(419, 96);
            this.listBoxActionType.Name = "listBoxActionType";
            this.listBoxActionType.Size = new System.Drawing.Size(232, 134);
            this.listBoxActionType.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(175, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Type rule";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(416, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Type action";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbSave,
            this.tsbPrevRule,
            this.tsbNextRule});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(675, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbSave
            // 
            this.tsbSave.Image = ((System.Drawing.Image)(resources.GetObject("tsbSave.Image")));
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(51, 22);
            this.tsbSave.Text = "Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tsbPrevRule
            // 
            this.tsbPrevRule.Image = ((System.Drawing.Image)(resources.GetObject("tsbPrevRule.Image")));
            this.tsbPrevRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbPrevRule.Name = "tsbPrevRule";
            this.tsbPrevRule.Size = new System.Drawing.Size(95, 22);
            this.tsbPrevRule.Text = "Previous rule";
            this.tsbPrevRule.Click += new System.EventHandler(this.tsbPrevRule_Click);
            // 
            // tsbNextRule
            // 
            this.tsbNextRule.Image = ((System.Drawing.Image)(resources.GetObject("tsbNextRule.Image")));
            this.tsbNextRule.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNextRule.Name = "tsbNextRule";
            this.tsbNextRule.Size = new System.Drawing.Size(75, 22);
            this.tsbNextRule.Text = "Next rule";
            this.tsbNextRule.Click += new System.EventHandler(this.tsbNextRule_Click);
            // 
            // FormRule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 467);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tabAdditionalProperties);
            this.Controls.Add(this.panel1);
            this.Name = "FormRule";
            this.Text = "FormRule";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabAdditionalProperties.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxActionType;
        private System.Windows.Forms.ListBox listBoxRuleType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox editStopProcessing;
        private System.Windows.Forms.TextBox editName;
        private System.Windows.Forms.TabControl tabAdditionalProperties;
        private System.Windows.Forms.TabPage tabPageAdditionalPropertiesRule;
        private System.Windows.Forms.TabPage tabPageAdditionalPropertiesAction;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbSave;
        private System.Windows.Forms.ToolStripButton tsbPrevRule;
        private System.Windows.Forms.ToolStripButton tsbNextRule;
    }
}