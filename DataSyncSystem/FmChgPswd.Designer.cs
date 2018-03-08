namespace DataSyncSystem
{
    partial class FmChgPswd
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picPswd2 = new System.Windows.Forms.PictureBox();
            this.picPswd1 = new System.Windows.Forms.PictureBox();
            this.txtPswd2 = new System.Windows.Forms.TextBox();
            this.txtPswd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btSure = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPswd2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPswd1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picPswd2);
            this.groupBox1.Controls.Add(this.picPswd1);
            this.groupBox1.Controls.Add(this.txtPswd2);
            this.groupBox1.Controls.Add(this.txtPswd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ForeColor = System.Drawing.Color.Red;
            this.groupBox1.Location = new System.Drawing.Point(68, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "password";
            // 
            // picPswd2
            // 
            this.picPswd2.Image = global::DataSyncSystem.Properties.Resources.hide;
            this.picPswd2.Location = new System.Drawing.Point(507, 95);
            this.picPswd2.Name = "picPswd2";
            this.picPswd2.Size = new System.Drawing.Size(35, 24);
            this.picPswd2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPswd2.TabIndex = 8;
            this.picPswd2.TabStop = false;
            this.picPswd2.Click += new System.EventHandler(this.picPswd2_Click);
            // 
            // picPswd1
            // 
            this.picPswd1.Image = global::DataSyncSystem.Properties.Resources.hide;
            this.picPswd1.Location = new System.Drawing.Point(507, 53);
            this.picPswd1.Name = "picPswd1";
            this.picPswd1.Size = new System.Drawing.Size(35, 24);
            this.picPswd1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPswd1.TabIndex = 7;
            this.picPswd1.TabStop = false;
            this.picPswd1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPswd1_MouseClick);
            // 
            // txtPswd2
            // 
            this.txtPswd2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPswd2.Location = new System.Drawing.Point(265, 95);
            this.txtPswd2.Name = "txtPswd2";
            this.txtPswd2.PasswordChar = '*';
            this.txtPswd2.Size = new System.Drawing.Size(224, 24);
            this.txtPswd2.TabIndex = 6;
            // 
            // txtPswd
            // 
            this.txtPswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPswd.Location = new System.Drawing.Point(265, 53);
            this.txtPswd.Name = "txtPswd";
            this.txtPswd.PasswordChar = '*';
            this.txtPswd.Size = new System.Drawing.Size(224, 24);
            this.txtPswd.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(139, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(86, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Again Password";
            // 
            // btSure
            // 
            this.btSure.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btSure.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btSure.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btSure.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btSure.Location = new System.Drawing.Point(298, 330);
            this.btSure.Name = "btSure";
            this.btSure.Size = new System.Drawing.Size(75, 35);
            this.btSure.TabIndex = 7;
            this.btSure.Text = "OK";
            this.btSure.UseVisualStyleBackColor = false;
            this.btSure.Click += new System.EventHandler(this.btSure_Click);
            // 
            // FmChgPswd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 421);
            this.Controls.Add(this.btSure);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmChgPswd";
            this.Text = "Change Password";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPswd2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPswd1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPswd2;
        private System.Windows.Forms.TextBox txtPswd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox picPswd1;
        private System.Windows.Forms.PictureBox picPswd2;
        private System.Windows.Forms.Button btSure;
    }
}