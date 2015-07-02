using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTTool
{
    public partial class BatchConverseForm : Form
    {
        public string SourceFolder { get; set; }

        public string DestFolder { get; set; }

        public BatchConverseForm()
        {
            InitializeComponent();
        }

        private void btSourceBorwse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (sender.Equals(btSourceBorwse))
                {
                    this.SourceFolder = fbd.SelectedPath;
                    this.tbSourceFolder.Text = SourceFolder;
                }
                else
                {
                    this.DestFolder = fbd.SelectedPath;
                    this.tbDestFolder.Text = DestFolder;
                }
            }
        }

        private void btApply_Click(object sender, EventArgs e)
        {
            if (tbDestFolder.Text.Equals(tbSourceFolder.Text))
            {
                MessageBox.Show("原文件夹与目标文件夹请不要选择同一个", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
