using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Quest : MonoBehaviour
{
    public GameObject quest;
    public GameObject gift;
    public GameObject[] questsobj;
    public Text QuestTitleText;
    public Text ContentText;
    public Text ClearGiftText;
    public Text nobtnText;
    public Button Yesbtn;
    public Button Nobtn;
    public Button Clearbtn;
    public Image Questicon;
    public Sprite ClearGiftImg;
    public bool isclear;
    public int Qvalue;

    public int QenemyCnt;
    public int QgrenadeCnt;

    public QuestManager qmgr;

    public Player player;

    public float SetTransform;


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
        player = GameObject.Find("Player").GetComponent<Player>();
        Qvalue = Random.Range(0, 4);
    }

    private void Start()
    {
        qmgr.LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
        UseQuestList(Qvalue);
        
    }

    private void Update()
    {
        if (isclear == false)
            ClearQuest(Qvalue);
    }

    public void QuestClickBtn()
    {
        quest.transform.GetChild(2).gameObject.SetActive(true);
        quest.transform.GetChild(2).localPosition= new Vector3(0, SetTransform, 0);
        questsobj[0].gameObject.SetActive(false);
        questsobj[1].gameObject.SetActive(false);
        questsobj[2].gameObject.SetActive(false);
        questsobj[3].gameObject.SetActive(false);
    }

    public void Yesbtnclick()
    {
        Yesbtn.gameObject.SetActive(false);
        Clearbtn.gameObject.SetActive(true);
        Questicon.color = Color.red;
        nobtnText.text = "닫기";
    }

    public void Nobtnclick()
    {
        questsobj[0].gameObject.SetActive(true);
        questsobj[1].gameObject.SetActive(true);
        questsobj[2].gameObject.SetActive(true);
        questsobj[3].gameObject.SetActive(true);
    }

    public void Clearbtnclick() //클리어 여부함수
    {
        if(isclear)
        {
            Gift gi= gift.GetComponent<Gift>();
            gi.Clear();
            Debug.Log("조건이 충족됐습니다");
            quest.transform.GetChild(2).gameObject.SetActive(false);

            Qvalue = Random.Range(0, 4);
            UseQuestList(Qvalue);
            isclear = false;
            Questicon.color = Color.white;
            Yesbtn.gameObject.SetActive(true);
            Clearbtn.gameObject.SetActive(false);


            questsobj[0].gameObject.SetActive(true);
            questsobj[1].gameObject.SetActive(true);
            questsobj[2].gameObject.SetActive(true);
            questsobj[3].gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("조건이 충족되지 않았습니다");
        }
    }

    void UseQuestList(int value)
    {
        Gift gi = gift.GetComponent<Gift>();
        switch (value)
        {
            case 0:
                QuestData firstQuest = qmgr.questDataList.questDataList[0];
                QuestTitleText.text = firstQuest.QuestTitle;
                ContentText.text = firstQuest.ContentText;
                gi.value = firstQuest.Giftvalue;
                ClearGiftText.text = firstQuest.Giftvalue.ToString();
                gi.itemcode = firstQuest.Giftitem;
                break;
            case 1:
                QuestData secondQuest = qmgr.questDataList.questDataList[1];
                QuestTitleText.text = secondQuest.QuestTitle;
                ContentText.text = secondQuest.ContentText;
                gi.value = secondQuest.Giftvalue;
                ClearGiftText.text = secondQuest.Giftvalue.ToString();
                gi.itemcode = secondQuest.Giftitem;
                break;
            case 2:
                QuestData thirdQuest = qmgr.questDataList.questDataList[2];
                QuestTitleText.text = thirdQuest.QuestTitle;
                ContentText.text = thirdQuest.ContentText;
                gi.value = thirdQuest.Giftvalue;
                ClearGiftText.text = thirdQuest.Giftvalue.ToString();
                gi.itemcode = thirdQuest.Giftitem;
                break;
            case 3:
                QuestData fourQuest = qmgr.questDataList.questDataList[3];
                QuestTitleText.text = fourQuest.QuestTitle;
                ContentText.text = fourQuest.ContentText;
                gi.value = fourQuest.Giftvalue;
                ClearGiftText.text = fourQuest.Giftvalue.ToString();
                gi.itemcode = fourQuest.Giftitem;
                break;
        }
    }

    void ClearQuest(int value)
    {
        switch (value)
        {
            case 0:
                if (player.score >= 100)
                {
                    //보상
                    isclear = true;
                    Debug.Log("클리어");
                }
                else
                    return;
                break;
            case 1:
                if(player.ammo >= 100)
                {
                    isclear = true;
                }
                else
                    return;
                break;
            case 2:
                if(player.Qenemy >= 1)
                {
                    isclear = true;
                }
                else
                    return;
                break;
            case 3:
                if(player.Qgrenade >= 1)
                {
                    isclear = true;
                }
                else
                    return;
                break;
        }
    }
}
