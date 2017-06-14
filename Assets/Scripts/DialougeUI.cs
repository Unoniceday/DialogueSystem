using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(DialougeSystem))]
public class DialougeUI : MonoBehaviour {

    public Text dialougeText;
	public Text Speaker;
    //設定最多四個選項或五個
    //public Button[] OptionBtns;

    DialougeSystem m_dialougeSystem;
    // Use this for initialization
    void Awake () {
        m_dialougeSystem = GetComponent<DialougeSystem>();
        DialougeSystem.OnStart += EnableUI;
        DialougeSystem.OnEnd += UnableUI;
    }

    void EnableUI()
    {
      
      
        dialougeText.enabled = true;
		Speaker.enabled = true;
        //if(m_dialougeSystem.currentNode!=null)
        // dialougeText.text = m_dialougeSystem.currentNode.Text;
        dialougeText.text = m_dialougeSystem.currentText;
		Speaker.text = m_dialougeSystem.currentSpeaker;
        //處理按鈕   以後再說
        //int maxOption = m_dialougeSystem.currentOptions.Count;
        //for (int i = 0; i < maxOption; i++)
        //{
        //    OptionBtns[i].enabled = true;


        //    OptionBtns[i].GetComponent<Button>().onClick.AddListener(delegate { m_dialougeSystem.StartOptionQuest(m_dialougeSystem.currentOptions[i].DestinationNodeID); });
        //}


    }

    void UnableUI()
    {
        dialougeText.enabled = false;
		Speaker.enabled = false;
        // dialougeText.text = null;
        Debug.Log("End");

    }
}
