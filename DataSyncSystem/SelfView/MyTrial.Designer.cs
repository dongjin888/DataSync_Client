namespace DataSyncSystem.SelfView
{
    partial class MyTrial
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
            this.btUser = new System.Windows.Forms.Button();
            this.labTrialInfo = new System.Windows.Forms.Label();
            this.labTrialDate = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btUser
            // 
            this.btUser.BackColor = System.Drawing.Color.Black;
            this.btUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUser.ForeColor = System.Drawing.Color.White;
            this.btUser.Location = new System.Drawing.Point(0, 0);
            this.btUser.Name = "btUser";
            this.btUser.Size = new System.Drawing.Size(170, 29);
            this.btUser.TabIndex = 2;
            this.btUser.Text = "userid";
            this.btUser.UseVisualStyleBackColor = false;
            this.btUser.Click += new System.EventHandler(this.btUser_Click);
            // 
            // labTrialInfo
            // 
            this.labTrialInfo.AutoEllipsis = true;
            this.labTrialInfo.AutoSize = true;
            this.labTrialInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTrialInfo.Location = new System.Drawing.Point(8, 43);
            this.labTrialInfo.MaximumSize = new System.Drawing.Size(180, 18);
            this.labTrialInfo.Name = "labTrialInfo";
            this.labTrialInfo.Size = new System.Drawing.Size(64, 18);
            this.labTrialInfo.TabIndex = 3;
            this.labTrialInfo.Text = "trialInfo";
            // 
            // labTrialDate
            // 
            this.labTrialDate.AutoEllipsis = true;
            this.labTrialDate.AutoSize = true;
            this.labTrialDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTrialDate.Location = new System.Drawing.Point(8, 74);
            this.labTrialDate.MaximumSize = new System.Drawing.Size(185, 18);
            this.labTrialDate.Name = "labTrialDate";
            this.labTrialDate.Size = new System.Drawing.Size(71, 18);
            this.labTrialDate.TabIndex = 4;
            this.labTrialDate.Text = "trialDate";
            // 
            // MyTrial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.Controls.Add(this.labTrialDate);
            this.Controls.Add(this.labTrialInfo);
            this.Controls.Add(this.btUser);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Name = "MyTrial";
            this.Size = new System.Drawing.Size(170, 106);
            this.Click += new System.EventHandler(this.MyTrial_Click);
            this.MouseEnter += new System.EventHandler(this.MyTrial_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MyTrial_MouseLeave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btUser;
        private System.Windows.Forms.Label labTrialInfo;
        private System.Windows.Forms.Label labTrialDate;
    }
}
