namespace New_CBC_CBS
{
    partial class Form1
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
            this.btnCBC = new System.Windows.Forms.Button();
            this.btnCBS = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCBC
            // 
            this.btnCBC.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCBC.Location = new System.Drawing.Point(16, 15);
            this.btnCBC.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCBC.Name = "btnCBC";
            this.btnCBC.Size = new System.Drawing.Size(177, 58);
            this.btnCBC.TabIndex = 0;
            this.btnCBC.Text = "CBC";
            this.btnCBC.UseVisualStyleBackColor = true;
            // 
            // btnCBS
            // 
            this.btnCBS.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCBS.Location = new System.Drawing.Point(16, 79);
            this.btnCBS.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.btnCBS.Name = "btnCBS";
            this.btnCBS.Size = new System.Drawing.Size(177, 58);
            this.btnCBS.TabIndex = 1;
            this.btnCBS.Text = "CBS";
            this.btnCBS.UseVisualStyleBackColor = true;
            this.btnCBS.Click += new System.EventHandler(this.btnCBS_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(209, 152);
            this.Controls.Add(this.btnCBS);
            this.Controls.Add(this.btnCBC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(7, 6, 7, 6);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCBC;
        private System.Windows.Forms.Button btnCBS;
    }
}

