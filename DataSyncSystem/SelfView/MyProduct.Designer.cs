namespace DataSyncSystem.SelfView
{
    partial class MyProduct
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labPdctName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labPdctName
            // 
            this.labPdctName.AutoSize = true;
            this.labPdctName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPdctName.Location = new System.Drawing.Point(39, 58);
            this.labPdctName.Name = "labPdctName";
            this.labPdctName.Size = new System.Drawing.Size(79, 24);
            this.labPdctName.TabIndex = 0;
            this.labPdctName.Text = "labPdct";
            // 
            // MyProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labPdctName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "MyProduct";
            this.Click += new System.EventHandler(this.MyProduct_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyProduct_Paint);
            this.MouseEnter += new System.EventHandler(this.MyProduct_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MyProduct_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPdctName;
    }
}
