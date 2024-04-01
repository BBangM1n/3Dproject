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
    public Text ConditionText;
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
        player = GameObject.Find("Player").GetComponent<Player>();
        Qvalue = Random.Range(0, 10); // 랜덤 Quest값 부여
    }

    private void Start()
    {
        UseQuestList(Qvalue); 
    }

    private void Update()
    {
        if (isclear == false && isyes == true)
        {
            ClearQuest(Qvalue);
            QconditionText(Qvalue);
        }
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
        Qcondition(Qvalue);
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
            gi.Clear(); // 조건 보상 물품 지급
            Debug.Log("조건이 충족됐습니다");
            Clear(Qvalue); // 퀘스트 클리어 함수
            quest.transform.GetChild(2).gameObject.SetActive(false);


            Qvalue = Random.Range(0, 10); // 다시 초기화
            UseQuestList(Qvalue);
            Qcondition(Qvalue);
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

    void Qcondition(int value)
    {
        switch(value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                player.Qenemy = 5;
                break;
            case 3:
                player.Qgrenade = 1;
                break;
            case 4:
                player.enemyAcount = 5;
                break;
            case 5:
                player.enemyBcount = 3;
                break;
            case 6:
                player.enemyCcount = 2;
                break;
            case 7:
                player.enemyDcount = 1;
                break;
            case 8:
                break;
            case 9:
                break;
        }
    }

    void QconditionText(int value)
    {
        switch (value)
        {
            case 0:
                ConditionText.text = "조건 : 스코어 100점 달성";
                break;
            case 1:
                ConditionText.text = "조건 : 총알 100개 기부";
                break;
            case 2:
                ConditionText.text = "조건 : 몬스터 5마리 처치 " + "0 / " + player.Qenemy;
                break;
            case 3:
                ConditionText.text = "조건 : 수류탄 사용 " + "0 / " + player.Qgrenade;
                break;
            case 4:
                ConditionText.text = "조건 : 초록색 공룡 처치 " + "0 / " + player.enemyAcount;
                break;
            case 5:
                ConditionText.text = "조건 : 보라색 공룡 처치 " + "0 / " + player.enemyBcount;
                break;
            case 6:
                ConditionText.text = "조건 : 노란색 공룡 처치 " + "0 / " + player.enemyCcount;
                break;
            case 7:
                ConditionText.text = "조건 : 빨간 공룡 처치" + "0 / " + player.enemyDcount;
                break;
            case 8:
                ConditionText.text = "조건 : 체력 나눠주기" + "30 / " + player.health;
                break;
            case 9:
                ConditionText.text = "조건 : 돈 나눠주기" + "3000 / " + player.coin;
                break;
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
            case 8:
                QuestData nineQuest = qmgr.questDataList.questDataList[8];
                QuestTitleText.text = nineQuest.QuestTitle;
                ContentText.text = nineQuest.ContentText;
                gi.value = nineQuest.Giftvalue;
                ClearGiftText.text = nineQuest.Giftvalue.ToString();
                gi.itemcode = nineQuest.Giftitem;
                break;
            case 9:
                QuestData tenQuest = qmgr.questDataList.questDataList[9];
                QuestTitleText.text = tenQuest.QuestTitle;
                ContentText.text = tenQuest.ContentText;
                gi.value = tenQuest.Giftvalue;
                ClearGiftText.text = tenQuest.Giftvalue.ToString();
                gi.itemcode = tenQuest.Giftitem;
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
            case 8:
                player.health -= 30;
                break;
            case 9:
                player.coin -= 3000;
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
                if(player.Qenemy <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 3:
                if(player.Qgrenade <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 4:
                if (player.enemyAcount <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 5:
                if (player.enemyBcount <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 6:
                if (player.enemyCcount <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 7:
                if (player.enemyDcount <= 0)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 8:
                if (player.health >= 40)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
            case 9:
                if (player.coin >= 3000)
                {
                    isclear = true;
                }
                else
                    isclear = false;
                break;
        }
    }
}
