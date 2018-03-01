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
            ((System.ComponentModel.ISupportInitialize)(this.picboxDnld)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFilter.Location = new System.Drawing.Point(119, 30);
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
            // 
            // picboxDnld
            // 
            this.picboxDnld.Image = global::DataSyncSystem.Properties.Resources.dnldlev;
            this.picboxDnld.Location = new System.Drawing.Point(363, 428);
            this.picboxDnld.Name = "picboxDnld";
            this.picboxDnld.Size = new System.Drawing.Size(100, 50);
            this.picboxDnld.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picboxDnld.TabIndex = 3;
            this.picboxDnld.TabStop = false;
            this.picboxDnld.Click += new System.EventHandler(this.picboxDnld_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataSyncSystem.Properties.Resources.filter;
            this.pictureBox1.Location = new System.Drawing.Point(51, 30);
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
            this.rdoStrict.Location = new System.Drawing.Point(405, 30);
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
            this.rdoNotStrict.Location = new System.Drawing.Point(499, 30);
            this.rdoNotStrict.Name = "rdoNotStrict";
            this.rdoNotStrict.Size = new System.Drawing.Size(96, 24);
            this.rdoNotStrict.TabIndex = 6;
            this.rdoNotStrict.TabStop = true;
            this.rdoNotStrict.Text = "not strict";
            this.rdoNotStrict.UseVisualStyleBackColor = true;
            // 
            // FmDbgFiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(836, 490);
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
    }
}