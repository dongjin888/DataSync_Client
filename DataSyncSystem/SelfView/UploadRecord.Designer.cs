namespace DataSyncSystem.SelfView
{
    partial class UploadRecord
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
            this.labPltfm = new System.Windows.Forms.Label();
            this.labPdct = new System.Windows.Forms.Label();
            this.labDate = new System.Windows.Forms.Label();
            this.labInfo = new System.Windows.Forms.Label();
            this.btDownload = new System.Windows.Forms.Button();
            this.btLink = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labPltfm
            // 
            this.labPltfm.AutoSize = true;
            this.labPltfm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPltfm.Location = new System.Drawing.Point(16, 13);
            this.labPltfm.Name = "labPltfm";
            this.labPltfm.Size = new System.Drawing.Size(49, 24);
            this.labPltfm.TabIndex = 0;
            this.labPltfm.Text = "pltfm";
            // 
            // labPdct
            // 
            this.labPdct.AutoSize = true;
            this.labPdct.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPdct.Location = new System.Drawing.Point(93, 13);
            this.labPdct.Name = "labPdct";
            this.labPdct.Size = new System.Drawing.Size(46, 24);
            this.labPdct.TabIndex = 1;
            this.labPdct.Text = "pdct";
            // 
            // labDate
            // 
            this.labDate.AutoSize = true;
            this.labDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labDate.Location = new System.Drawing.Point(178, 13);
            this.labDate.Name = "labDate";
            this.labDate.Size = new System.Drawing.Size(46, 24);
            this.labDate.TabIndex = 2;
            this.labDate.Text = "date";
            // 
            // labInfo
            // 
            this.labInfo.AutoEllipsis = true;
            this.labInfo.AutoSize = true;
            this.labInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfo.Location = new System.Drawing.Point(418, 13);
            this.labInfo.MaximumSize = new System.Drawing.Size(270, 24);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(40, 24);
            this.labInfo.TabIndex = 3;
            this.labInfo.Text = "info";
            // 
            // btDownload
            // 
            this.btDownload.BackColor = System.Drawing.Color.White;
            this.btDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btDownload.Location = new System.Drawing.Point(736, 7);
            this.btDownload.Name = "btDownload";
            this.btDownload.Size = new System.Drawing.Size(105, 33);
            this.btDownload.TabIndex = 4;
            this.btDownload.Text = "download";
            this.btDownload.UseVisualStyleBackColor = false;
            this.btDownload.Click += new System.EventHandler(this.btDownload_Click);
            this.btDownload.MouseEnter += new System.EventHandler(this.btDownload_MouseEnter);
            this.btDownload.MouseLeave += new System.EventHandler(this.btDownload_MouseLeave);
            // 
            // btLink
            // 
            this.btLink.BackColor = System.Drawing.Color.White;
            this.btLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLink.ForeColor = System.Drawing.Color.Blue;
            this.btLink.Location = new System.Drawing.Point(631, 7);
            this.btLink.Name = "btLink";
            this.btLink.Size = new System.Drawing.Size(75, 32);
            this.btLink.TabIndex = 5;
            this.btLink.Text = "link";
            this.btLink.UseVisualStyleBackColor = false;
            this.btLink.Click += new System.EventHandler(this.btLink_Click);
            this.btLink.MouseEnter += new System.EventHandler(this.btLink_MouseEnter);
            this.btLink.MouseLeave += new System.EventHandler(this.btLink_MouseLeave);
            // 
            // UploadRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btLink);
            this.Controls.Add(this.btDownload);
            this.Controls.Add(this.labInfo);
            this.Controls.Add(this.labDate);
            this.Controls.Add(this.labPdct);
            this.Controls.Add(this.labPltfm);
            this.Name = "UploadRecord";
            this.Size = new System.Drawing.Size(858, 48);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.UploadRecord_Paint);
            this.MouseEnter += new System.EventHandler(this.UploadRecord_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UploadRecord_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labPltfm;
        private System.Windows.Forms.Label labPdct;
        private System.Windows.Forms.Label labDate;
        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.Button btDownload;
        private System.Windows.Forms.Button btLink;
    }
}
