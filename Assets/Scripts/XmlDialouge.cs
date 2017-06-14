using System.Collections.Generic;

using UnityEngine;
using System.Xml.Linq;
namespace DialougeTree
{
    public class XmlDialouge {

        public List<DialougeNode> Nodes;
        public List<DialougeOption> AllOptions;

        public XmlDialouge()
        {
            Nodes = new List<DialougeNode>();
            AllOptions = new List<DialougeOption>();
            ConnectData();
        }

        public void ConnectData()
        {
            ConnectOptionData();
            ConnectDialougeData();
        }


        private void ConnectOptionData()
        {
            var OptionTree = XDocument.Load("Assets/OptionList_1.xml").Root.Elements("OptionID");
            foreach (var _option in OptionTree)
            {
                var id = int.Parse(_option.Attribute("id").Value.Trim());
                var _text = _option.Element("Text").Value.Trim(); //也是內容
                var _nextNode = int.Parse(_option.Element("NextNode").Value.Trim()); //也是內容

                AddAllOption(id, _text, _nextNode);
            }
        }
        private void ConnectDialougeData()
        {
            var NodeTree = XDocument.Load("Assets/DialougeContent_1.xml").Root.Elements("NodeID");

            foreach (var node in NodeTree)
            {
                var _type = node.Attribute("type").Value.Trim();// 屬性 nodeID
                var id = int.Parse(node.Attribute("id").Value.Trim());
                var _character = node.Element("Character").Value.Trim(); //也是內容
                var _text = node.Element("Text").Value.Trim(); //也是內容
                if (_type == "NullOption")
                {

                    var _nextNode = int.Parse(node.Element("NextNode").Value.Trim()); //也是內容
                    AddNode(id, _nextNode, _character, _text);
                }
                else
                {
                    var _optionList = node.Element("OptionList").Value.Trim().Split(','); //也是內容
                    List<DialougeOption> tempOptionArray = new List<DialougeOption>();
                    foreach (string temp in _optionList)
                    {
                        int oid = int.Parse(temp);
                        tempOptionArray.Add(FindOption(oid));
                    }


                    AddNode(id, -1, _character, _text, tempOptionArray);
                }


            }
        }

        public void AddNode(int self, int next, string speaker, string text, List<DialougeOption> options = null) //這個是加節點的 所以不用在外部新增節點了
        {
            //提供外部新增節點，分為兩種，第一為有選項，第二為無選項，有選項的使用選項來跳轉節點，無選項的自行附帶跳轉
            if (!Nodes.Exists(delegate (DialougeNode tempNode) { return tempNode.NodeID == self; }))
            {
                if (next >= 0)
                {
                    DialougeNode temp = new DialougeNode(speaker, self, next, text);
                    Nodes.Add(temp);
                }
                else
                {
                    DialougeNode temp = new DialougeNode(speaker, self, text, options);
                    Nodes.Add(temp);
                }
            }
        }
        public void AddOptionsWithNode(int OptionID, DialougeNode inNode) //把選項加入node中用的
        {
            //如果指定ID存在於所有選項中，把值都進去node的選項。

            // DialougeOption temp = allOptions[OptionID];
            //if (allOptions.Contains(temp)) 
            //    inNode.Options.Add(allOptions[OptionID]);
            if (!AllOptions.Exists(delegate (DialougeOption temp) { return temp.OID == OptionID; }))
            {
                inNode.Options.Add(AllOptions[OptionID]);
            }
        }

        public void AddAllOption(int oID, string txt, int dest) //一開始初始化所有選項用的
        {
            if (!AllOptions.Exists(delegate (DialougeOption temp) { return temp.OID == oID; }))
            {
                DialougeOption t_option = new DialougeOption(oID, txt, dest);
                AllOptions.Add(t_option);
            }

        }

        public DialougeOption FindOption(int OID)
        {
            foreach (DialougeOption tempOption in AllOptions)
            {
                if (tempOption.OID == OID)
                {
                    return tempOption;
                }
            }

            return null;
        }

        public DialougeNode FindNode(int UID)
        {
            foreach (DialougeNode tempNode in Nodes)
            {
                if (tempNode.NodeID == UID)
                {
                    return tempNode;
                }
            }

            return null;
        }
        public List<DialougeNode> ConversationData(int UID)
        {
            int current = UID;
            List<DialougeNode> tempConNode = new List<DialougeNode>();
            while (current >= 0)
            {
                int next = FindNode(current).NextNode;

                tempConNode.Add(FindNode(current));
                current = next;
            }

            return tempConNode;
        }
    }
}