namespace ERP_PROJESİ
{
    partial class EklemeEkranı
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
            this.SuspendLayout();
            // 
            // EklemeEkranı
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EklemeEkranı";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EklemeEkranı";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EklemeEkranı_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EklemeEkranı_FormClosed);
            this.Load += new System.EventHandler(this.EklemeEkranı_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EklemeEkranı_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.EklemeEkranı_KeyPress);
            this.ResumeLayout(false);

        }

        #endregion
    }
}