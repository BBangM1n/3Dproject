using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quest : MonoBehaviour
{
    public GameObject quest;
    public GameObject gift;
    public Text QuestTitleText;
    public Text ContentText;
    public Text nobtnText;
    public Button Yesbtn;
    public Button Nobtn;
    public Button Clearbtn;
    public Image Questicon;
    public bool isclear;
    public int Qvalue;

    public QuestManager qmgr;


    private void Awake()
    {
        Button QuestClick = GetComponent<Button>();
        Text QuestTitleText = GetComponent<Text>();
        Text ContentText = GetComponent<Text>();
        Button Yesbtn = GetComponent<Button>();
        Button Nobtn = GetComponent<Button>();
        Button Clearbtn = GetComponent<Button>();
        Image Questicon = GetComponent<Image>();
        Text nobtnText = Nobtn.GetComponent<Text>();
        QuestManager qmgr = GameObject.Find("QuestMgr").GetComponent<QuestManager>();
    }

    private void Start()
    {
        Qvalue = Random.Range(0, 3);
        qmgr.LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
        UseQuestList(Qvalue);
    }

    private void Update()
    {

    }

    public void QuestClickBtn()
    {
        quest.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void Yesbtnclick()
    {
        Yesbtn.gameObject.SetActive(false);
        Clearbtn.gameObject.SetActive(true);
        Questicon.color = Color.red;
        nobtnText.text = "닫기";
    }

    public void Clearbtnclick() //클리어 여부함수
    {
        if(isclear)
        {
            Gift gi= gift.GetComponent<Gift>();
            gi.Clear();
            Debug.Log("조건이 충족됐습니다");
            quest.transform.GetChild(2).gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("조건이 충족되지 않았습니다");
        }
    }

    void UseQuestList(int value)
    {
        switch (value)
        {
            case 0:
                QuestData firstQuest = qmgr.questDataList.questDataList[0];
                QuestTitleText.text = firstQuest.QuestTitle;
                ContentText.text = firstQuest.ContentText;
                break;
            case 1:
                QuestData secondQuest = qmgr.questDataList.questDataList[1];
                QuestTitleText.text = secondQuest.QuestTitle;
                ContentText.text = secondQuest.ContentText;
                break;
            case 2:
                QuestData thirdQuest = qmgr.questDataList.questDataList[0];
                QuestTitleText.text = thirdQuest.QuestTitle;
                ContentText.text = thirdQuest.ContentText;
                break;
        }
    }

    void ClearQuest(int value)
    {
        switch (value)
        {
            case 0:
                Player player = GetComponent<Player>();
                if(player.score >= 100)
                {
                    //보상
                    Debug.Log("클리어");
                }
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
