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
        private IAnalyser _btAnalyser;
        private IBNode rootNode;

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
                    AnalysisBt(_filename);
                    //Console.WriteLine(filename);
                    return;
                }
            }
        }

        private void AnalysisBt(string filename)
        {
            SetLogger(BTToolLogger.Start('f', filename));

            // 清理上一次的工作
            treeView.Nodes.Clear();
            _btAnalyser = new CommonAnalyser();
            rootNode = null;

            // 读入BT文件
            byte[] buffer = null;
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            {
                buffer = new byte[stream.Length];
                stream.Read(buffer, 0, (int)stream.Length);
            }

            // 解析
            IBNode rootBNode = null;
            try
            {
                rootBNode = this._btAnalyser.Analysis(buffer);
                rootNode = rootBNode;
            }
            catch
            {
                SetLogger(BTToolLogger.Start('e', filename));
                tabControl.SelectedIndex = 1;
                filename = null;
                return;
            }
            SetLogger(BTToolLogger.End('f'));

            SetLogger(BTToolLogger.Start('s'));
            // 构建树
            TreeNode rootTNode = new TreeNode();
            ConstructTree(rootTNode, rootBNode);
            rootTNode.Expand();
            treeView.Nodes.Add(rootTNode);
            SetLogger(BTToolLogger.End('s'));
        }

        private void ConstructTree(TreeNode tParent, IBNode bParent)
        {
            tParent.Tag = bParent;
            //tParent.ToolTipText = bParent as 
            
            tParent.Text = bParent.ToString();
            if (bParent is KeyValueNode)
            {
                tParent.Text += "KeyValueNode";
            }
            else if (bParent is ListItemNode)
            {
                tParent.Text += "ListItemNode";
            }
            bParent.Child.ForEach(bNode =>
            {
                TreeNode tNode = new TreeNode();
                tNode.Text = bNode.ToString();
                tNode.Tag = bNode;
                tParent.Nodes.Add(tNode);
                ConstructTree(tNode, bNode);
            });
        }

        private void RefreshTree(TreeNode parent)
        {
            parent.Text = ((IBNode)parent.Tag).ToString();
            foreach (TreeNode node in parent.Nodes)
            {
                node.Text = ((IBNode)node.Tag).ToString();
                RefreshTree(node);
            }
        }

        private void openOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "种子文件(*.torrent)|*.torrent|所有文件|(*.*)";
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _filename = ofd.FileName;
                AnalysisBt(ofd.FileName);
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
                SetLogger(BTToolLogger.Start('r'));
                BatchConverser.Iterate(_btAnalyser.BNodeList);
                RefreshTree(treeView.Nodes[0]);
                treeView.Refresh();
                SetLogger(BTToolLogger.End('r'));
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "种子文件(*.torrent)|*.torrent";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveBt(sfd.FileName);
            }
        }

        private void SaveBt(string filename)
        {
            if (rootNode == null)
                return;
            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                byte[] buffer = rootNode.ToBytes();
                stream.Write(buffer, 0, buffer.Length);
            }
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
                // Console.WriteLine(bcf.SourceFolder + " " + bcf.DestFolder);
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
                AnalysisBt(args[0]);
            }
        }
    }
}
