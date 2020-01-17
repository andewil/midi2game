namespace midi2games
{
    partial class FormRuleControlValeMatch
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.editCN = new System.Windows.Forms.TextBox();
            this.editCV = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Control number";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Value";
            // 
            // editCN
            // 
            this.editCN.Location = new System.Drawing.Point(125, 10);
            this.editCN.Name = "editCN";
            this.editCN.Size = new System.Drawing.Size(100, 20);
            this.editCN.TabIndex = 2;
            this.editCN.TextChanged += new System.EventHandler(this.editCN_TextChanged);
            // 
            // editCV
            // 
            this.editCV.Location = new System.Drawing.Point(125, 36);
            this.editCV.Name = "editCV";
            this.editCV.Size = new System.Drawing.Size(100, 20);
            this.editCV.TabIndex = 3;
            this.editCV.TextChanged += new System.EventHandler(this.editCV_TextChanged);
            // 
            // FormRuleControlValeMatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(252, 167);
            this.Controls.Add(this.editCV);
            this.Controls.Add(this.editCN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormRuleControlValeMatch";
            this.Text = "FormControlValeMatch";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editCN;
        private System.Windows.Forms.TextBox editCV;
    }
}