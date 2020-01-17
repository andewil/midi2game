namespace midi2games
{
    partial class FormRuleControlValueIncDec
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
            this.editControlNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // editControlNumber
            // 
            this.editControlNumber.Location = new System.Drawing.Point(125, 10);
            this.editControlNumber.Name = "editControlNumber";
            this.editControlNumber.Size = new System.Drawing.Size(100, 20);
            this.editControlNumber.TabIndex = 0;
            this.editControlNumber.TextChanged += new System.EventHandler(this.editControlNumber_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Control number";
            // 
            // FormRuleControlValueIncDec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 146);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editControlNumber);
            this.Name = "FormRuleControlValueIncDec";
            this.Text = "FormControlValueIncDec";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox editControlNumber;
        private System.Windows.Forms.Label label1;
    }
}