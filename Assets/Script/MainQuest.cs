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

    // Start is called before the first frame update
    void Start()
    {
        MainQuestList();
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
    }

    void MainQuestContent(int id)
    {
        switch (id)
        {
            case 0:
                text[0] = "Q. 마을에 온걸 환영해.";
                text[1] = "나는 Ludo 너를 도와줄 사람이야 초보자의 마을에 온걸 환영해!";
                text[2] = "2번째 대사";
                text[3] = "싸울 마음의 준비가 되면 나에게 다시 말을 걸어줘.";
                endtext = "도와줘서 고마워. 자 여기 보상이야.";

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
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
    }
    
    public void ClearBtnClick()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKey(KeyCode.E))
        {
            Story.SetActive(true);

            if (QuestOn && QuestList[QuestValue].QuestID == 0)
            {
                isClear = true;
            }
        }

    }

}
