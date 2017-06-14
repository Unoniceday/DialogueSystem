using System;
using System.Collections.Generic;
namespace DialougeTree
{
    //還是說，選項可以觸發事件，其中一個事件就是移動node到指定？
    public class DialougeOption
    {
        public int OID;
        public string Text;
        public int DestinationNodeID;

        public DialougeOption() { }
        public DialougeOption(int oID, string text, int dest)
        {
            this.OID = oID;
            this.Text = text;
            this.DestinationNodeID = dest;
        }
    }
}
