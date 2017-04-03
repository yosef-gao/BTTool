using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace BTTool
{
    public partial class MainForm : Form
    {
        private string[] args;
        private string _filename;
        private TorrentFile _torrentFile = null;

        public MainForm(string[] args)
        {
            InitializeComponent();
            this.args = args;
        }

        private void treeView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void treeView_DragDrop(object sender, DragEventArgs e)
        {
            foreach (string filename in ((System.Array)e.Data.GetData(DataFormats.FileDrop)))
            {
                if (new FileInfo(filename).Extension.Equals(".torrent"))
                {
                    _filename = filename;
                    AnalyseBt(_filename);
                    return;
                }
            }
        }

        private void AnalyseBt(string filename)
        {
            SetLogger(BTToolLogger.Start('f', filename));
            _torrentFile = new TorrentFile();
            try
            {
                _torrentFile.OpenFile(filename);
            }
            catch
            {
                SetLogger(BTToolLogger.Start('e', filename));
            }
            SetLogger(BTToolLogger.End('f'));
            SetLogger(BTToolLogger.Start('s'));
            TreeNode rootNode = _torrentFile.RootNode;
            rootNode.Expand();
            treeView.Nodes.Clear();
            treeView.Nodes.Add(rootNode);
            SetLogger(BTToolLogger.End('s'));
        }

        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "种子文件(*.torrent)|*.torrent|所有文件|(*.*)";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filename = ofd.FileName;
                AnalyseBt(ofd.FileName);
            }
        }

        private void exitEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void intellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_filename != null)
            {
                _torrentFile.Modify();
                treeView.Nodes.Clear();
                var rootNode = _torrentFile.RootNode;
                rootNode.Expand();
                treeView.Nodes.Add(rootNode);
            }
        }

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			using (var sfd = new SaveFileDialog())
			{
				sfd.Filter = "种子文件(*.torrent)|*.torrent";
				if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					var fn = sfd.FileName;
					SaveBt(fn);
					SaveNamesText(fn);
				}
			}
		}

		/// <summary>
		/// 写入同名的名称对照列表。
		/// </summary>
		/// <param name="fn"></param>
		private void SaveNamesText(string torrentFileName)
		{
			if (String.IsNullOrEmpty(torrentFileName))
			{
				//.
			}
			else
			{
				var ext = Path.GetExtension(torrentFileName);
				var newName = torrentFileName.Substring(0, torrentFileName.Length - ext.Length) + ".txt";
				var nameList = KeyValueVisitor.NameTable.ToString();
				File.WriteAllText(newName, nameList);
			}
		}

		private void SaveBt(string filename)
        {
            _torrentFile.SaveFile(filename);
        }

        private void SetLogger(string message)
        {
            this.Invoke(new Action(() => {
                tbLogger.Text += message;
                tbLogger.Text += Environment.NewLine;
            }));
        }

        public void ShowCallBackMessage(string message)
        {
            SetLogger(BTToolLogger.End('b'));
            MessageBox.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void batchReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BatchConverseForm bcf = new BatchConverseForm();
            if (bcf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // 异步处理
                SetLogger(BTToolLogger.Start('b'));
                BatchConverser bc = new BatchConverser(bcf.SourceFolder, bcf.DestFolder, ShowCallBackMessage);
                Thread thread = new Thread(bc.BacthVonverse);
                thread.Start();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (args.Length != 0)
            {
                _filename = args[0];
                AnalyseBt(args[0]);
            }
        }
    }
}
