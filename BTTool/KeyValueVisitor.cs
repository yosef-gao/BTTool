using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BTTool
{
	/// <summary>
	/// 用于修改特定键值对节点的值
	/// </summary>
	class KeyValueVisitor : IVisitor
	{
		public static StringBuilder NameTable = new StringBuilder();
		static int fileNo = 0;
		static int keyNo = 0;

		private string[] tabooString = { "name", "name.utf-8", "path", "path.utf-8", "comment", "comment.utf-8",
									   "publisher", "publisher-url", "publisher-url.utf-8", "publisher.utf-8"};
		public void Visit(KeyValueNode keyValueNode)
		{
			string key = keyValueNode.Key;
			foreach (string name in tabooString)
			{
				if (key.Equals(name))
				{
					// 普通键值对
					if (keyValueNode.Child.Count == 0)
					{
						var keyOldValue = keyValueNode.Value;
						var keyNewValue = $"K{++keyNo}";
						keyValueNode.SetValue(Encoding.UTF8.GetBytes(keyNewValue));
						NameTable.AppendLine($"{keyNewValue} \t {keyOldValue}");
					}
					else // 列表项，通常是文件名
					{
						foreach (ListItemNode node in keyValueNode.Child)
						{
							string value = node.Value;
							string newFileName = $"F{++fileNo}";
							string oldFileName = value;
							int startIndex = value.LastIndexOf(".");
							if (startIndex < 0)
								value = newFileName;
							else
								value = String.Format("{0}.{1}", newFileName, value.Substring(startIndex + 1));
							NameTable.AppendLine($"{newFileName} \t {oldFileName}");
							node.SetValue(Encoding.UTF8.GetBytes(value));
						}
					}
					break;
				}
			}
		}
	}
}
