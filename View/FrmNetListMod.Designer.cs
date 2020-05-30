namespace View
{
    partial class FrmNetListMod
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
            this.groupBoxLog = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSet = new System.Windows.Forms.Button();
            this.btnExec = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMsg1 = new System.Windows.Forms.Label();
            this.txtBoxNetList = new System.Windows.Forms.TextBox();
            this.groupBoxLog.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLog
            // 
            this.groupBoxLog.Controls.Add(this.textBox1);
            this.groupBoxLog.Location = new System.Drawing.Point(12, 72);
            this.groupBoxLog.Name = "groupBoxLog";
            this.groupBoxLog.Size = new System.Drawing.Size(614, 383);
            this.groupBoxLog.TabIndex = 0;
            this.groupBoxLog.TabStop = false;
            this.groupBoxLog.Text = "Log";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 39);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(583, 327);
            this.textBox1.TabIndex = 0;
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(538, 12);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 42);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnSet
            // 
            this.btnSet.Location = new System.Drawing.Point(433, 12);
            this.btnSet.Name = "btnSet";
            this.btnSet.Size = new System.Drawing.Size(75, 42);
            this.btnSet.TabIndex = 2;
            this.btnSet.Text = "Set";
            this.btnSet.UseVisualStyleBackColor = true;
            this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
            // 
            // btnExec
            // 
            this.btnExec.Location = new System.Drawing.Point(328, 12);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(75, 42);
            this.btnExec.TabIndex = 3;
            this.btnExec.Text = "Exec";
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 473);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 25);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(81, 20);
            this.StatusLabel.Text = "Version 1.0";
            // 
            // lblMsg1
            // 
            this.lblMsg1.AutoSize = true;
            this.lblMsg1.Location = new System.Drawing.Point(12, 25);
            this.lblMsg1.Name = "lblMsg1";
            this.lblMsg1.Size = new System.Drawing.Size(52, 17);
            this.lblMsg1.TabIndex = 5;
            this.lblMsg1.Text = "Status:";
            // 
            // txtBoxNetList
            // 
            this.txtBoxNetList.Location = new System.Drawing.Point(70, 25);
            this.txtBoxNetList.Name = "txtBoxNetList";
            this.txtBoxNetList.ReadOnly = true;
            this.txtBoxNetList.Size = new System.Drawing.Size(176, 22);
            this.txtBoxNetList.TabIndex = 7;
            // 
            // FrmNetListMod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 498);
            this.Controls.Add(this.txtBoxNetList);
            this.Controls.Add(this.lblMsg1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnExec);
            this.Controls.Add(this.btnSet);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBoxLog);
            this.Name = "FrmNetListMod";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetListMod";
            this.groupBoxLog.ResumeLayout(false);
            this.groupBoxLog.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLog;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnSet;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Label lblMsg1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox txtBoxNetList;
    }
}

