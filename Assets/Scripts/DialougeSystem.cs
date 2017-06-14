using UnityEngine;
using System;
using System.Collections;
using DialougeTree;
using UnityEngine.UI;
using System.Collections.Generic;

/*
 * 傳入數據，當下一個指令則往下移動
 */
public class DialougeSystem : MonoBehaviour {

    public string[] tempRow;
    XmlDialouge Dialouges = new XmlDialouge();
    //Dialouge<CSVConnect>
    IEnumerator process;

    //List<DialougeOption> currentOptions;
    public DialougeNode currentNode;
	public string currentSpeaker;
    public string currentText;
    public int currentUID;
    public Button UIbutton;

    public delegate void DialougeQuest();
    public static event DialougeQuest OnEnd;
    public static event DialougeQuest OnStart;

    public delegate void OptionQuest(int index);
    public static event OptionQuest StartOption;

    // Use this for initialization
    void Start () {
       
        //Dialouges.connector.loadFile(Application.dataPath + "/StreamingAssets/CSVasset", "NewDialog.csv");
        //Dialouges.SetLoadPath(Application.dataPath + "/StreamingAssets/CSVasset", "NewDialog2.csv");
        //Dialouges.SetOptionLoadPath(Application.dataPath + "/StreamingAssets/CSVasset", "OptionDialog2.csv");
        //Dialouges.ConnectOptionData();
        //Dialouges.ConnectAllData();

        StartOptionQuest(0);



    }

   
    //遍歷所有node，如果有選項，則印出選項
    //因為當前node如果有下一個，就沒有選項
    //如果有選項，就沒有下一個，因此選項都會在最後一個DATA裡面
    //所以也就是說，一個選項，會有目標節點，一個節點，必包含一段文字，而選項則不依定
    //那開始對話的話，就是獲得當前節點，和

     //option需要點擊後進行某件事還有開啟某個對話的事件
     //所以就是結束目前對話，但文字不消失
     //然後如果沒選項，則再點一次消失。
     //如果有選項，則是點選項，點選項則會調用一個委派，檢查目前按到的按鈕OID
     //然後開始執行StartConveration(data)
    IEnumerator StartConvestaion(List<DialougeNode> Data)
    {

        DestoryButton();

        for (int i = 0; i < Data.Count; i++)
        {

            currentNode = Data[i];
            currentText = currentNode.Text;
			currentSpeaker = currentNode.Speaker;
            if (OnStart != null)
                OnStart();
            //第一種方案
            //if (Data[i].Options != null)
            //{

            //    SetOptionButtons(Data[i].Options);

            //}
           
            yield return null; //movenext之後會得到這個
        }
        //這樣就不用每次都判斷了
        if (Data[Data.Count - 1].Options != null)
        {
            SetOptionButtons(Data[Data.Count - 1].Options);
        }
        else
        {
            OnEnd();
        }


    }

    #region 按鈕類處理
    //創建一個選項群組，當currentnode的選項為空 則刪除群組裡面所有按鈕
    public Button[] OptionBtns;
   
    //在這邊加入每個按鈕按完後的事件
    void  SetOptionButtons(List<DialougeOption> temp)
    {
        OptionBtns = new Button[temp.Count];
        
        int index = 0;
        foreach (DialougeOption j in temp)
        {
            OptionBtns[index] = Instantiate(UIbutton, new Vector2(Screen.width / 2,(Screen.height)/3+index*50), Quaternion.identity) as Button;
            OptionBtns[index].transform.SetParent(GameObject.Find("OptionGroup").GetComponent<Transform>());
            OptionBtns[index].GetComponent<Button>().onClick.AddListener(delegate { this.StartOptionQuest(j.DestinationNodeID); });
            index += 1;

        }
        
    }

    void DestoryButton()
    {
        foreach (Button temp in OptionBtns)
        {
            Destroy(temp.gameObject);
        }
        OptionBtns = null;
    }
    
    public void StartOptionQuest(int index)
    {
        
        process = StartConvestaion(Dialouges.ConversationData(index));
        
        process.MoveNext();
        if (OnStart != null)
            OnStart();
        //如果用了就可以直接進入第一個對話
    }
    #endregion
    //我還需要做一個類別，用來處理產生按鈕，而按鈕產生後會自對應目前的對話內容產生變化，然後按下去後會開始進行對話

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
           
            process.MoveNext();
           
        }
       
    }

}
