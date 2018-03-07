namespace DataSyncSystem
{
    partial class FmFactory
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.labTitle = new System.Windows.Forms.Label();
            this.btChgPswd = new System.Windows.Forms.Button();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.btBrowser = new System.Windows.Forms.Button();
            this.btUpld = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupInfo = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labActivator = new System.Windows.Forms.Label();
            this.labOperator = new System.Windows.Forms.Label();
            this.labPltfm = new System.Windows.Forms.Label();
            this.labPdct = new System.Windows.Forms.Label();
            this.labInfo = new System.Windows.Forms.Label();
            this.labOther = new System.Windows.Forms.Label();
            this.btInfoModify = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel1.Controls.Add(this.labTitle);
            this.panel1.Location = new System.Drawing.Point(-3, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1170, 50);
            this.panel1.TabIndex = 0;
            // 
            // labTitle
            // 
            this.labTitle.AutoSize = true;
            this.labTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labTitle.ForeColor = System.Drawing.Color.White;
            this.labTitle.Location = new System.Drawing.Point(9, 13);
            this.labTitle.Name = "labTitle";
            this.labTitle.Size = new System.Drawing.Size(0, 25);
            this.labTitle.TabIndex = 1;
            // 
            // btChgPswd
            // 
            this.btChgPswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btChgPswd.ForeColor = System.Drawing.Color.Red;
            this.btChgPswd.Location = new System.Drawing.Point(645, 511);
            this.btChgPswd.Name = "btChgPswd";
            this.btChgPswd.Size = new System.Drawing.Size(171, 30);
            this.btChgPswd.TabIndex = 2;
            this.btChgPswd.Text = "change password";
            this.btChgPswd.UseVisualStyleBackColor = true;
            // 
            // txtFolder
            // 
            this.txtFolder.Enabled = false;
            this.txtFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFolder.Location = new System.Drawing.Point(107, 146);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(343, 26);
            this.txtFolder.TabIndex = 1;
            // 
            // btBrowser
            // 
            this.btBrowser.BackColor = System.Drawing.Color.Silver;
            this.btBrowser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btBrowser.Location = new System.Drawing.Point(456, 141);
            this.btBrowser.Name = "btBrowser";
            this.btBrowser.Size = new System.Drawing.Size(220, 37);
            this.btBrowser.TabIndex = 2;
            this.btBrowser.Text = "Browse upload folder";
            this.btBrowser.UseVisualStyleBackColor = false;
            this.btBrowser.Click += new System.EventHandler(this.btBrowser_Click);
            // 
            // btUpld
            // 
            this.btUpld.BackColor = System.Drawing.Color.Silver;
            this.btUpld.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btUpld.Location = new System.Drawing.Point(335, 201);
            this.btUpld.Name = "btUpld";
            this.btUpld.Size = new System.Drawing.Size(137, 48);
            this.btUpld.TabIndex = 3;
            this.btUpld.Text = "upload";
            this.btUpld.UseVisualStyleBackColor = false;
            this.btUpld.Click += new System.EventHandler(this.btUpld_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "status";
            // 
            // groupInfo
            // 
            this.groupInfo.Controls.Add(this.btInfoModify);
            this.groupInfo.Controls.Add(this.labOther);
            this.groupInfo.Controls.Add(this.labInfo);
            this.groupInfo.Controls.Add(this.labPdct);
            this.groupInfo.Controls.Add(this.labPltfm);
            this.groupInfo.Controls.Add(this.labOperator);
            this.groupInfo.Controls.Add(this.labActivator);
            this.groupInfo.Controls.Add(this.label7);
            this.groupInfo.Controls.Add(this.label6);
            this.groupInfo.Controls.Add(this.label5);
            this.groupInfo.Controls.Add(this.label4);
            this.groupInfo.Controls.Add(this.label3);
            this.groupInfo.Controls.Add(this.label2);
            this.groupInfo.ForeColor = System.Drawing.Color.Red;
            this.groupInfo.Location = new System.Drawing.Point(107, 272);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(580, 185);
            this.groupInfo.TabIndex = 5;
            this.groupInfo.TabStop = false;
            this.groupInfo.Text = "trial info";
            this.groupInfo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(146, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Activator";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(145, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Operator";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(150, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Platform";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(154, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 17);
            this.label5.TabIndex = 3;
            this.label5.Text = "Product";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(183, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Info";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(169, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "Other";
            // 
            // labActivator
            // 
            this.labActivator.AutoSize = true;
            this.labActivator.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labActivator.Location = new System.Drawing.Point(237, 23);
            this.labActivator.Name = "labActivator";
            this.labActivator.Size = new System.Drawing.Size(52, 17);
            this.labActivator.TabIndex = 6;
            this.labActivator.Text = "label8";
            // 
            // labOperator
            // 
            this.labOperator.AutoSize = true;
            this.labOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOperator.Location = new System.Drawing.Point(237, 51);
            this.labOperator.Name = "labOperator";
            this.labOperator.Size = new System.Drawing.Size(52, 17);
            this.labOperator.TabIndex = 7;
            this.labOperator.Text = "label9";
            // 
            // labPltfm
            // 
            this.labPltfm.AutoSize = true;
            this.labPltfm.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPltfm.Location = new System.Drawing.Point(237, 77);
            this.labPltfm.Name = "labPltfm";
            this.labPltfm.Size = new System.Drawing.Size(61, 17);
            this.labPltfm.TabIndex = 8;
            this.labPltfm.Text = "label10";
            // 
            // labPdct
            // 
            this.labPdct.AutoSize = true;
            this.labPdct.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPdct.Location = new System.Drawing.Point(237, 105);
            this.labPdct.Name = "labPdct";
            this.labPdct.Size = new System.Drawing.Size(61, 17);
            this.labPdct.TabIndex = 9;
            this.labPdct.Text = "label11";
            // 
            // labInfo
            // 
            this.labInfo.AutoSize = true;
            this.labInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labInfo.Location = new System.Drawing.Point(237, 133);
            this.labInfo.Name = "labInfo";
            this.labInfo.Size = new System.Drawing.Size(61, 17);
            this.labInfo.TabIndex = 10;
            this.labInfo.Text = "label12";
            // 
            // labOther
            // 
            this.labOther.AutoSize = true;
            this.labOther.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labOther.Location = new System.Drawing.Point(237, 158);
            this.labOther.Name = "labOther";
            this.labOther.Size = new System.Drawing.Size(61, 17);
            this.labOther.TabIndex = 11;
            this.labOther.Text = "label13";
            // 
            // btInfoModify
            // 
            this.btInfoModify.BackColor = System.Drawing.Color.Gray;
            this.btInfoModify.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btInfoModify.ForeColor = System.Drawing.Color.White;
            this.btInfoModify.Location = new System.Drawing.Point(469, 146);
            this.btInfoModify.Name = "btInfoModify";
            this.btInfoModify.Size = new System.Drawing.Size(75, 28);
            this.btInfoModify.TabIndex = 12;
            this.btInfoModify.Text = "Modify";
            this.btInfoModify.UseVisualStyleBackColor = false;
            this.btInfoModify.Click += new System.EventHandler(this.btInfoModify_Click);
            // 
            // FmFactory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 541);
            this.Controls.Add(this.groupInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btChgPswd);
            this.Controls.Add(this.btUpld);
            this.Controls.Add(this.btBrowser);
            this.Controls.Add(this.txtFolder);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmFactory";
            this.Text = "DataSync-Factory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmFactory_FormClosing);
            this.Load += new System.EventHandler(this.FmFactory_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupInfo.ResumeLayout(false);
            this.groupInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btChgPswd;
        private System.Windows.Forms.Label labTitle;
        private System.Windows.Forms.TextBox txtFolder;
        private System.Windows.Forms.Button btBrowser;
        private System.Windows.Forms.Button btUpld;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupInfo;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labOther;
        private System.Windows.Forms.Label labInfo;
        private System.Windows.Forms.Label labPdct;
        private System.Windows.Forms.Label labPltfm;
        private System.Windows.Forms.Label labOperator;
        private System.Windows.Forms.Label labActivator;
        private System.Windows.Forms.Button btInfoModify;
    }
}