namespace DataSyncSystem.SelfView
{
    partial class FmBchDnldProgrs
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
            this.labInfo = new System.Windows.Forms.Label();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.labPersent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labInfo
            // 
            this.labInfo.AutoEllipsis = true;
            this.labInfo.AutoSize = true;
            this.labInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfo.ForeColor = System.Drawing.Color.White;
            this.labInfo.Location = new System.Drawing.Point(70, 103);
            this.labInfo.MaximumSize = new System.Drawing.Size(540, 60);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(40, 20);
            this.labInfo.TabIndex = 3;
            this.labInfo.Text = "info";
            this.labInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(12, 37);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(603, 42);
            this.progress.TabIndex = 2;
            // 
            // labPersent
            // 
            this.labPersent.AutoSize = true;
            this.labPersent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPersent.Location = new System.Drawing.Point(634, 50);
            this.labPersent.Name = "labPersent";
            this.labPersent.Size = new System.Drawing.Size(0, 20);
            this.labPersent.TabIndex = 4;
            // 
            // FmBchDnldProgrs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 143);
            this.Controls.Add(this.labPersent);
            this.Controls.Add(this.labInfo);
            this.Controls.Add(this.progress);
            this.Name = "FmBchDnldProgrs";
            this.Text = "FmBchDnldProgrs";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label labPersent;
    }
}