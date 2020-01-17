namespace midi2games
{
    partial class FormActionMouseOffset
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
            this.rbVertical = new System.Windows.Forms.RadioButton();
            this.rbHorizontal = new System.Windows.Forms.RadioButton();
            this.labelOffset = new System.Windows.Forms.Label();
            this.editOffset = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // rbVertical
            // 
            this.rbVertical.AutoSize = true;
            this.rbVertical.Location = new System.Drawing.Point(12, 12);
            this.rbVertical.Name = "rbVertical";
            this.rbVertical.Size = new System.Drawing.Size(60, 17);
            this.rbVertical.TabIndex = 0;
            this.rbVertical.TabStop = true;
            this.rbVertical.Text = "Vertical";
            this.rbVertical.UseVisualStyleBackColor = true;
            this.rbVertical.CheckedChanged += new System.EventHandler(this.rbVertical_CheckedChanged);
            // 
            // rbHorizontal
            // 
            this.rbHorizontal.AutoSize = true;
            this.rbHorizontal.Location = new System.Drawing.Point(12, 35);
            this.rbHorizontal.Name = "rbHorizontal";
            this.rbHorizontal.Size = new System.Drawing.Size(72, 17);
            this.rbHorizontal.TabIndex = 1;
            this.rbHorizontal.TabStop = true;
            this.rbHorizontal.Text = "Horizontal";
            this.rbHorizontal.UseVisualStyleBackColor = true;
            this.rbHorizontal.CheckedChanged += new System.EventHandler(this.rbHorizontal_CheckedChanged);
            // 
            // labelOffset
            // 
            this.labelOffset.AutoSize = true;
            this.labelOffset.Location = new System.Drawing.Point(16, 62);
            this.labelOffset.Name = "labelOffset";
            this.labelOffset.Size = new System.Drawing.Size(35, 13);
            this.labelOffset.TabIndex = 2;
            this.labelOffset.Text = "Offset";
            // 
            // editOffset
            // 
            this.editOffset.Location = new System.Drawing.Point(120, 59);
            this.editOffset.Name = "editOffset";
            this.editOffset.Size = new System.Drawing.Size(100, 20);
            this.editOffset.TabIndex = 3;
            this.editOffset.TextChanged += new System.EventHandler(this.editOffset_TextChanged);
            // 
            // FormActionMouseOffset
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(407, 236);
            this.Controls.Add(this.editOffset);
            this.Controls.Add(this.labelOffset);
            this.Controls.Add(this.rbHorizontal);
            this.Controls.Add(this.rbVertical);
            this.Name = "FormActionMouseOffset";
            this.Text = "FormActionMouseOffset";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbVertical;
        private System.Windows.Forms.RadioButton rbHorizontal;
        private System.Windows.Forms.Label labelOffset;
        private System.Windows.Forms.TextBox editOffset;
    }
}