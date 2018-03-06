namespace DataSyncSystem
{
    partial class FmLogin
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
            this.labName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.labPass = new System.Windows.Forms.Label();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.btLogin = new System.Windows.Forms.Button();
            this.labLoginStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labName
            // 
            this.labName.AutoSize = true;
            this.labName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labName.Location = new System.Drawing.Point(220, 115);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(63, 20);
            this.labName.TabIndex = 0;
            this.labName.Text = "User Id";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(316, 110);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 22);
            this.txtName.TabIndex = 1;
            this.txtName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtName_MouseClick);
            // 
            // labPass
            // 
            this.labPass.AutoSize = true;
            this.labPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labPass.Location = new System.Drawing.Point(200, 173);
            this.labPass.Name = "labPass";
            this.labPass.Size = new System.Drawing.Size(83, 20);
            this.labPass.TabIndex = 2;
            this.labPass.Text = "Password";
            // 
            // txtPass
            // 
            this.txtPass.Location = new System.Drawing.Point(316, 167);
            this.txtPass.Name = "txtPass";
            this.txtPass.Size = new System.Drawing.Size(100, 22);
            this.txtPass.TabIndex = 3;
            this.txtPass.UseSystemPasswordChar = true;
            this.txtPass.TextChanged += new System.EventHandler(this.txtPass_TextChanged);
            // 
            // btLogin
            // 
            this.btLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btLogin.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btLogin.Location = new System.Drawing.Point(251, 249);
            this.btLogin.Name = "btLogin";
            this.btLogin.Size = new System.Drawing.Size(127, 35);
            this.btLogin.TabIndex = 4;
            this.btLogin.Text = "Login";
            this.btLogin.UseVisualStyleBackColor = true;
            this.btLogin.Click += new System.EventHandler(this.btLogin_Click);
            // 
            // labLoginStatus
            // 
            this.labLoginStatus.AutoSize = true;
            this.labLoginStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labLoginStatus.ForeColor = System.Drawing.Color.Red;
            this.labLoginStatus.Location = new System.Drawing.Point(220, 342);
            this.labLoginStatus.Name = "labLoginStatus";
            this.labLoginStatus.Size = new System.Drawing.Size(0, 25);
            this.labLoginStatus.TabIndex = 6;
            // 
            // FmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(668, 421);
            this.Controls.Add(this.labLoginStatus);
            this.Controls.Add(this.btLogin);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.labPass);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labName);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmLogin";
            this.Text = "User Login";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FmLogin_FormClosing);
            this.Load += new System.EventHandler(this.FmLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label labPass;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Button btLogin;
        private System.Windows.Forms.Label labLoginStatus;
    }
}

