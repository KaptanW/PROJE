namespace ERP_PROJESİ
{
    partial class Find
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
            this.aramatxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // aramatxt
            // 
            this.aramatxt.Location = new System.Drawing.Point(116, 67);
            this.aramatxt.Margin = new System.Windows.Forms.Padding(2);
            this.aramatxt.Name = "aramatxt";
            this.aramatxt.Size = new System.Drawing.Size(125, 20);
            this.aramatxt.TabIndex = 0;
            this.aramatxt.TextChanged += new System.EventHandler(this.aramatxt_TextChanged);
            this.aramatxt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(106, 27);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Aramak istediğiniz şeyi yazınız";
            // 
            // Find
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(372, 136);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.aramatxt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Find";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Find";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Find_FormClosing);
            this.Load += new System.EventHandler(this.Find_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox aramatxt;
        private System.Windows.Forms.Label label1;
    }
}