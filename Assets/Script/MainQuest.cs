using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MainQuestData
{
    public int QuestID;
}

public class MainQuest : MonoBehaviour
{
    public List<MainQuestData> QuestList = new List<MainQuestData>();
    string[] text = new string[10];
    string endtext;


    public GameObject Story;
    public string Content;
    public Text LudoText;
    public Text ClearGift;
    public GameObject NextBtn;
    public GameObject YesBtn;
    public GameObject NoBtn;
    public GameObject ClearBtn;

    int Count = 0;
    int QuestValue = 0;
    bool QuestOn;
    bool isClear;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
        MainQuestList();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        MainQuestContent(QuestList[QuestValue].QuestID);
    }

    private void LateUpdate()
    {

    }

    void MainQuestList()
    {
        MainQuestData newQuest1 = new MainQuestData();
        newQuest1.QuestID = 0;
        QuestList.Add(newQuest1);

        MainQuestData newQuest2 = new MainQuestData();
        newQuest2.QuestID = 1;
        QuestList.Add(newQuest2);
    }

    void MainQuestContent(int id)
    {
        switch (id)
        {
            case 0:
                text[0] = "Q. 마을에 온걸 환영해.";
                text[1] = "나는 Ludo 너를 도와줄 사람이야 초보자의 마을에 온걸 환영해!";
                text[2] = "미안하지만 지금 마을 밖으로 나갈 수 없는 상태야";
                text[3] = "지금 마을 밖에는 공룡들이 나타나서 위험해";
                text[4] = "마을 밖으로 가고싶다면 내가 도와줄게";
                text[5] = "마음의 준비가 되면 나에게 다시 말을 걸어줘";
                endtext = "준비는 다 됐지?";
                break;
            case 1:
                text[0] = "Q. 무기를 구해야해.";
                text[1] = "지금 밖으로 나간다 해도 공룡들을 물리 칠 무기가 없어";
                text[2] = "저기 좋은 무기를 파는 곳이 있는걸?";
                text[3] = "저기서 해머 하나를 구입해 보자.";
                endtext = "잘했어! 해머는 근접공격이지만 데미지가 강해!";
                break;
        }

        if(isClear)
        {
            LudoText.text = endtext;
        }
        else
        {
            LudoText.text = text[Count];
        }

    }

    public void NextBtnClick()
    {
        if (string.IsNullOrWhiteSpace(text[Count + 1]) == false)
        {
            Count++;
        }
        if(string.IsNullOrWhiteSpace(text[Count + 1]) == true)
        {
            NextBtn.SetActive(false);
            YesBtn.SetActive(true);
            NoBtn.SetActive(true);
        }
    }

    public void YesBtnClick()
    {
        Story.SetActive(false);
        ClearBtn.SetActive(true);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
        QuestOn = true;
    }

    public void NoBtnClick()
    {
        Count = 0;
        Story.SetActive(false);
        NextBtn.SetActive(true);
        ClearBtn.SetActive(false);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
    }
    
    public void ClearBtnClick()
    {
        if (isClear)
        {
            isClear = false;
            Cleargift(QuestList[QuestValue].QuestID);
            Count = 0;
            QuestValue++;

            Story.SetActive(false);
            NextBtn.SetActive(true);
            YesBtn.SetActive(false);
            NoBtn.SetActive(false);
            ClearBtn.SetActive(false);

        }
        else
        {
            LudoText.text = "아직 조건을 달성하지 못했잖아!";
        }
    }

    void Cleargift(int id)
    {
        switch (id)
        {
            case 0:
                player.coin += 500;
                break;
            case 1:

                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Story.SetActive(true);

            if (QuestOn && QuestList[QuestValue].QuestID == 0)
            {
                isClear = true;
            }
        }

    }

}
