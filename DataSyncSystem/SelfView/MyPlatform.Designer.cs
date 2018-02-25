namespace DataSyncSystem.SelfView
{
    partial class MyPlatform
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
            this.labPltfmName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labPltfmName
            // 
            this.labPltfmName.AutoSize = true;
            this.labPltfmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPltfmName.Location = new System.Drawing.Point(49, 60);
            this.labPltfmName.Name = "labPltfmName";
            this.labPltfmName.Size = new System.Drawing.Size(55, 24);
            this.labPltfmName.TabIndex = 0;
            this.labPltfmName.Text = "Pltfm";
            // 
            // MyPlatform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labPltfmName);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "MyPlatform";
            this.Click += new System.EventHandler(this.MyPlatform_Click);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MyPlatform_Paint);
            this.MouseEnter += new System.EventHandler(this.MyPlatform_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MyPlatform_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPltfmName;
    }
}
