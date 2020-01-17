namespace midi2games
{
    partial class FormActionMouseAbs
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
            this.editX = new System.Windows.Forms.TextBox();
            this.editY = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(89, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y";
            // 
            // editX
            // 
            this.editX.Location = new System.Drawing.Point(120, 10);
            this.editX.Name = "editX";
            this.editX.Size = new System.Drawing.Size(100, 20);
            this.editX.TabIndex = 2;
            this.editX.TextChanged += new System.EventHandler(this.editX_TextChanged);
            // 
            // editY
            // 
            this.editY.Location = new System.Drawing.Point(120, 36);
            this.editY.Name = "editY";
            this.editY.Size = new System.Drawing.Size(100, 20);
            this.editY.TabIndex = 3;
            this.editY.TextChanged += new System.EventHandler(this.editY_TextChanged);
            // 
            // FormActionMouseAbs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.editY);
            this.Controls.Add(this.editX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormActionMouseAbs";
            this.Text = "FormActionMouseAbs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editX;
        private System.Windows.Forms.TextBox editY;
    }
}