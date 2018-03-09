namespace DataSyncSystem
{
    partial class FmDbgFiles
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
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.picboxDnld = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rdoStrict = new System.Windows.Forms.RadioButton();
            this.rdoNotStrict = new System.Windows.Forms.RadioButton();
            this.txtDnldFolder = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labSelectNum = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picboxDnld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilter.Location = new System.Drawing.Point(98, 34);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(254, 30);
            this.txtFilter.TabIndex = 1;
            this.txtFilter.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFilter_KeyDown);
            // 
            // listView1
            // 
            this.listView1.AllowColumnReorder = true;
            this.listView1.CheckBoxes = true;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(51, 79);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(732, 328);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listView1_ItemCheck);
            // 
            // picboxDnld
            // 
            this.picboxDnld.Image = global::DataSyncSystem.Properties.Resources.dnldlev;
            this.picboxDnld.Location = new System.Drawing.Point(724, 413);
            this.picboxDnld.Name = "picboxDnld";
            this.picboxDnld.Size = new System.Drawing.Size(59, 40);
            this.picboxDnld.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxDnld.TabIndex = 3;
            this.picboxDnld.TabStop = false;
            this.picboxDnld.Click += new System.EventHandler(this.picboxDnld_Click);
            this.picboxDnld.MouseEnter += new System.EventHandler(this.picboxDnld_MouseEnter);
            this.picboxDnld.MouseLeave += new System.EventHandler(this.picboxDnld_MouseLeave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataSyncSystem.Properties.Resources.filter;
            this.pictureBox1.Location = new System.Drawing.Point(40, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(55, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // rdoStrict
            // 
            this.rdoStrict.AutoSize = true;
            this.rdoStrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoStrict.Location = new System.Drawing.Point(362, 38);
            this.rdoStrict.Name = "rdoStrict";
            this.rdoStrict.Size = new System.Drawing.Size(68, 24);
            this.rdoStrict.TabIndex = 5;
            this.rdoStrict.TabStop = true;
            this.rdoStrict.Text = "strict";
            this.rdoStrict.UseVisualStyleBackColor = true;
            // 
            // rdoNotStrict
            // 
            this.rdoNotStrict.AutoSize = true;
            this.rdoNotStrict.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoNotStrict.Location = new System.Drawing.Point(436, 39);
            this.rdoNotStrict.Name = "rdoNotStrict";
            this.rdoNotStrict.Size = new System.Drawing.Size(96, 24);
            this.rdoNotStrict.TabIndex = 6;
            this.rdoNotStrict.TabStop = true;
            this.rdoNotStrict.Text = "not strict";
            this.rdoNotStrict.UseVisualStyleBackColor = true;
            // 
            // txtDnldFolder
            // 
            this.txtDnldFolder.Enabled = false;
            this.txtDnldFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDnldFolder.Location = new System.Drawing.Point(51, 420);
            this.txtDnldFolder.Name = "txtDnldFolder";
            this.txtDnldFolder.Size = new System.Drawing.Size(366, 26);
            this.txtDnldFolder.TabIndex = 7;
            // 
            // btBrowse
            // 
            this.btBrowse.Location = new System.Drawing.Point(424, 420);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(75, 26);
            this.btBrowse.TabIndex = 8;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(647, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = " selected  ";
            // 
            // labSelectNum
            // 
            this.labSelectNum.AutoSize = true;
            this.labSelectNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labSelectNum.ForeColor = System.Drawing.Color.Red;
            this.labSelectNum.Location = new System.Drawing.Point(746, 55);
            this.labSelectNum.Name = "labSelectNum";
            this.labSelectNum.Size = new System.Drawing.Size(19, 20);
            this.labSelectNum.TabIndex = 10;
            this.labSelectNum.Text = "0";
            // 
            // FmDbgFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 490);
            this.Controls.Add(this.labSelectNum);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.txtDnldFolder);
            this.Controls.Add(this.rdoNotStrict);
            this.Controls.Add(this.rdoStrict);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.picboxDnld);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFilter);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FmDbgFiles";
            this.Text = "Files Download";
            ((System.ComponentModel.ISupportInitialize)(this.picboxDnld)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox picboxDnld;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.RadioButton rdoStrict;
        private System.Windows.Forms.RadioButton rdoNotStrict;
        private System.Windows.Forms.TextBox txtDnldFolder;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labSelectNum;
    }
}