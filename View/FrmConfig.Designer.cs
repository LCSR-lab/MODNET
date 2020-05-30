namespace View
{
    partial class FrmConfig
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
            this.lblPath = new System.Windows.Forms.Label();
            this.lblOutput = new System.Windows.Forms.Label();
            this.lblSrc = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.textBoxSrc = new System.Windows.Forms.TextBox();
            this.textBoxDate = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnPath = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.folderBrowserDialogPath = new System.Windows.Forms.FolderBrowserDialog();
            this.btnDefault = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Location = new System.Drawing.Point(26, 55);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(37, 17);
            this.lblPath.TabIndex = 10;
            this.lblPath.Text = "Path";
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(26, 102);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(51, 17);
            this.lblOutput.TabIndex = 11;
            this.lblOutput.Text = "Output";
            // 
            // lblSrc
            // 
            this.lblSrc.AutoSize = true;
            this.lblSrc.Location = new System.Drawing.Point(26, 149);
            this.lblSrc.Name = "lblSrc";
            this.lblSrc.Size = new System.Drawing.Size(29, 17);
            this.lblSrc.TabIndex = 12;
            this.lblSrc.Text = "Src";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(26, 195);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(38, 17);
            this.lblDate.TabIndex = 13;
            this.lblDate.Text = "Date";
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(93, 52);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.ReadOnly = true;
            this.textBoxPath.Size = new System.Drawing.Size(447, 22);
            this.textBoxPath.TabIndex = 6;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(95, 98);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.ReadOnly = true;
            this.textBoxOutput.Size = new System.Drawing.Size(447, 22);
            this.textBoxOutput.TabIndex = 7;
            // 
            // textBoxSrc
            // 
            this.textBoxSrc.Location = new System.Drawing.Point(95, 144);
            this.textBoxSrc.Name = "textBoxSrc";
            this.textBoxSrc.ReadOnly = true;
            this.textBoxSrc.Size = new System.Drawing.Size(447, 22);
            this.textBoxSrc.TabIndex = 8;
            // 
            // textBoxDate
            // 
            this.textBoxDate.Location = new System.Drawing.Point(92, 190);
            this.textBoxDate.Name = "textBoxDate";
            this.textBoxDate.ReadOnly = true;
            this.textBoxDate.Size = new System.Drawing.Size(447, 22);
            this.textBoxDate.TabIndex = 9;
            // 
            // btnPath
            // 
            this.btnPath.Location = new System.Drawing.Point(546, 49);
            this.btnPath.Name = "btnPath";
            this.btnPath.Size = new System.Drawing.Size(28, 23);
            this.btnPath.TabIndex = 0;
            this.btnPath.Text = "...";
            this.btnPath.UseVisualStyleBackColor = true;
            this.btnPath.Click += new System.EventHandler(this.btnPath_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(499, 235);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(408, 235);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // folderBrowserDialogPath
            // 
            this.folderBrowserDialogPath.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // btnDefault
            // 
            this.btnDefault.Location = new System.Drawing.Point(26, 235);
            this.btnDefault.Name = "btnDefault";
            this.btnDefault.Size = new System.Drawing.Size(75, 23);
            this.btnDefault.TabIndex = 14;
            this.btnDefault.Text = "Default";
            this.btnDefault.UseVisualStyleBackColor = true;
            this.btnDefault.Click += new System.EventHandler(this.btnDefault_Click);
            // 
            // FrmConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 278);
            this.Controls.Add(this.btnDefault);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnPath);
            this.Controls.Add(this.textBoxDate);
            this.Controls.Add(this.textBoxSrc);
            this.Controls.Add(this.textBoxOutput);
            this.Controls.Add(this.textBoxPath);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblSrc);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lblPath);
            this.Name = "FrmConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration of the paths";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label lblSrc;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.TextBox textBoxSrc;
        private System.Windows.Forms.TextBox textBoxDate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnPath;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogPath;
        private System.Windows.Forms.Button btnDefault;
    }
}