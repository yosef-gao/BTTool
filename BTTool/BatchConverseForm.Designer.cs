namespace BTTool
{
    partial class BatchConverseForm
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
            this.tbSourceFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDestFolder = new System.Windows.Forms.TextBox();
            this.btSourceBorwse = new System.Windows.Forms.Button();
            this.btDestBorwse = new System.Windows.Forms.Button();
            this.btApply = new System.Windows.Forms.Button();
            this.btCancel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tbSourceFolder
            // 
            this.tbSourceFolder.Location = new System.Drawing.Point(95, 9);
            this.tbSourceFolder.Name = "tbSourceFolder";
            this.tbSourceFolder.Size = new System.Drawing.Size(202, 21);
            this.tbSourceFolder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "源文件夹：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "目标文件夹：";
            // 
            // tbDestFolder
            // 
            this.tbDestFolder.Location = new System.Drawing.Point(95, 47);
            this.tbDestFolder.Name = "tbDestFolder";
            this.tbDestFolder.Size = new System.Drawing.Size(202, 21);
            this.tbDestFolder.TabIndex = 3;
            // 
            // btSourceBorwse
            // 
            this.btSourceBorwse.Location = new System.Drawing.Point(317, 7);
            this.btSourceBorwse.Name = "btSourceBorwse";
            this.btSourceBorwse.Size = new System.Drawing.Size(75, 23);
            this.btSourceBorwse.TabIndex = 4;
            this.btSourceBorwse.Text = "浏览...";
            this.btSourceBorwse.UseVisualStyleBackColor = true;
            this.btSourceBorwse.Click += new System.EventHandler(this.btSourceBorwse_Click);
            // 
            // btDestBorwse
            // 
            this.btDestBorwse.Location = new System.Drawing.Point(317, 45);
            this.btDestBorwse.Name = "btDestBorwse";
            this.btDestBorwse.Size = new System.Drawing.Size(75, 23);
            this.btDestBorwse.TabIndex = 5;
            this.btDestBorwse.Text = "浏览...";
            this.btDestBorwse.UseVisualStyleBackColor = true;
            this.btDestBorwse.Click += new System.EventHandler(this.btSourceBorwse_Click);
            // 
            // btApply
            // 
            this.btApply.Location = new System.Drawing.Point(222, 96);
            this.btApply.Name = "btApply";
            this.btApply.Size = new System.Drawing.Size(75, 23);
            this.btApply.TabIndex = 6;
            this.btApply.Text = "确定";
            this.btApply.UseVisualStyleBackColor = true;
            this.btApply.Click += new System.EventHandler(this.btApply_Click);
            // 
            // btCancel
            // 
            this.btCancel.Location = new System.Drawing.Point(316, 96);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(75, 23);
            this.btCancel.TabIndex = 7;
            this.btCancel.Text = "取消";
            this.btCancel.UseVisualStyleBackColor = true;
            this.btCancel.Click += new System.EventHandler(this.btCancel_Click);
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Location = new System.Drawing.Point(2, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(400, 1);
            this.label3.TabIndex = 8;
            // 
            // BatchConverseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(403, 131);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btCancel);
            this.Controls.Add(this.btApply);
            this.Controls.Add(this.btDestBorwse);
            this.Controls.Add(this.btSourceBorwse);
            this.Controls.Add(this.tbDestFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbSourceFolder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "BatchConverseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "批量处理";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbSourceFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDestFolder;
        private System.Windows.Forms.Button btSourceBorwse;
        private System.Windows.Forms.Button btDestBorwse;
        private System.Windows.Forms.Button btApply;
        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Label label3;
    }
}