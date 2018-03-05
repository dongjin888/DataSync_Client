namespace DataSyncSystem
{
    partial class FmAnalyzer
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
            this.labNoAnalyze = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labNoAnalyze
            // 
            this.labNoAnalyze.AutoSize = true;
            this.labNoAnalyze.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.labNoAnalyze.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labNoAnalyze.Location = new System.Drawing.Point(104, 179);
            this.labNoAnalyze.Name = "labNoAnalyze";
            this.labNoAnalyze.Size = new System.Drawing.Size(616, 29);
            this.labNoAnalyze.TabIndex = 0;
            this.labNoAnalyze.Text = "Sorry,curren did\'t have any valuable analyze tool.";
            this.labNoAnalyze.Visible = false;
            // 
            // FmAnalyzer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 437);
            this.Controls.Add(this.labNoAnalyze);
            this.Name = "FmAnalyzer";
            this.Text = "Choose Analyze Type";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labNoAnalyze;
    }
}