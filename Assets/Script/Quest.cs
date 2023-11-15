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
    public bool isyes;

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
        Qvalue = Random.Range(0, 8);
    }

    private void Start()
    {
        //qmgr.LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
        UseQuestList(Qvalue);
        
    }

    private void Update()
    {
        if (isclear == false && isyes == true)
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
        isyes = true;
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
            Clear(Qvalue);
            quest.transform.GetChild(2).gameObject.SetActive(false);


            Qvalue = Random.Range(0, 8);
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
            case 4:
                QuestData fiveQuest = qmgr.questDataList.questDataList[4];
                QuestTitleText.text = fiveQuest.QuestTitle;
                ContentText.text = fiveQuest.ContentText;
                gi.value = fiveQuest.Giftvalue;
                ClearGiftText.text = fiveQuest.Giftvalue.ToString();
                gi.itemcode = fiveQuest.Giftitem;
                break;
            case 5:
                QuestData sixQuest = qmgr.questDataList.questDataList[5];
                QuestTitleText.text = sixQuest.QuestTitle;
                ContentText.text = sixQuest.ContentText;
                gi.value = sixQuest.Giftvalue;
                ClearGiftText.text = sixQuest.Giftvalue.ToString();
                gi.itemcode = sixQuest.Giftitem;
                break;
            case 6:
                QuestData sevenQuest = qmgr.questDataList.questDataList[6];
                QuestTitleText.text = sevenQuest.QuestTitle;
                ContentText.text = sevenQuest.ContentText;
                gi.value = sevenQuest.Giftvalue;
                ClearGiftText.text = sevenQuest.Giftvalue.ToString();
                gi.itemcode = sevenQuest.Giftitem;
                break;
            case 7:
                QuestData eightQuest = qmgr.questDataList.questDataList[7];
                QuestTitleText.text = eightQuest.QuestTitle;
                ContentText.text = eightQuest.ContentText;
                gi.value = eightQuest.Giftvalue;
                ClearGiftText.text = eightQuest.Giftvalue.ToString();
                gi.itemcode = eightQuest.Giftitem;
                break;
        }
    }
    void Clear(int value)
    {
        switch(value)
        {
            case 0:
                break;
            case 1:
                player.ammo -= 100;
                break;
            case 2:
                player.Qenemy = 0;
                break;
            case 3:
                player.Qgrenade = 0;
                break;
            case 4:
                player.enemyAcount = 0;
                break;
            case 5:
                player.enemyBcount = 0;
                break;
            case 6:
                player.enemyCcount = 0;
                break;
            case 7:
                player.enemyDcount = 0;
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
                    isclear = false;
                break;
            case 1:
                if(player.ammo >= 100)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 2:
                if(player.Qenemy >= 1)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 3:
                if(player.Qgrenade >= 1)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 4:
                if (player.enemyAcount >= 3)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 5:
                if (player.enemyBcount >= 5)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 6:
                if (player.enemyCcount >= 2)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 7:
                if (player.enemyDcount >= 1)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
        }
    }
}
