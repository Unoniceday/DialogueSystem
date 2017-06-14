using System;
using System.Collections.Generic;

/*
 * 每個對話節點，如果沒有選項可以往下個節點走，則手動設置對話節點。
 * 
 * 
*/
namespace DialougeTree
{
    public class DialougeNode
    {
        public int NodeID = -1;
        public int NextNode = -1;
        public string Text;
		public string Speaker;
        public List<DialougeOption> Options;

		public DialougeNode(string Speaker,int NodeID, int nextNode, string text) //這應該也要有對話 //這個是沒選項的節點
        {
            Options = null;

            this.NodeID = NodeID;
            NextNode = nextNode;
            Text = text;
			this.Speaker = Speaker;
        }
		public DialougeNode(string Speaker,int NodeID, string text,List<DialougeOption> SelectOptions) //有對話
        {
            Text = text;
            this.NodeID = NodeID;
            Options = SelectOptions;
			this.Speaker = Speaker;
        }
    }
}