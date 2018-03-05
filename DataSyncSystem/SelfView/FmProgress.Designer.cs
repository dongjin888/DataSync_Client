namespace DataSyncSystem.SelfView
{
    partial class FmProgress
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
            this.progress = new System.Windows.Forms.ProgressBar();
            this.labInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progress
            // 
            this.progress.Location = new System.Drawing.Point(32, 44);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(603, 42);
            this.progress.TabIndex = 0;
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfo.ForeColor = System.Drawing.Color.White;
            this.labInfo.Location = new System.Drawing.Point(245, 105);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(40, 20);
            this.labInfo.TabIndex = 1;
            this.labInfo.Text = "info";
            // 
            // FmProgress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 143);
            this.Controls.Add(this.labInfo);
            this.Controls.Add(this.progress);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmProgress";
            this.Text = "FmProgress";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FmProgress_FormClosed);
            this.Load += new System.EventHandler(this.FmProgress_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label labInfo;
    }
}