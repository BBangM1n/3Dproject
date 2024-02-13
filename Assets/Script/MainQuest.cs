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

        MainQuestData newQuest3 = new MainQuestData();
        newQuest3.QuestID = 2;
        QuestList.Add(newQuest3);

        MainQuestData newQuest4 = new MainQuestData();
        newQuest4.QuestID = 3;
        QuestList.Add(newQuest4);

        MainQuestData newQuest5 = new MainQuestData();
        newQuest5.QuestID = 4;
        QuestList.Add(newQuest5);

        MainQuestData newQuest6 = new MainQuestData();
        newQuest6.QuestID = 5;
        QuestList.Add(newQuest6);
    }

    void MainQuestContent(int id)
    {
        switch (id)
        {
            case 0:
                text[0] = "Q. ������ �°� ȯ����.";
                text[1] = "���� Ludo �ʸ� ������ ����̾� �ʺ����� ������ �°� ȯ����!";
                text[2] = "�̾������� ���� ���� ������ ���� �� ���� ���¾�";
                text[3] = "���� ���� �ۿ��� ������� ��Ÿ���� ������";
                text[4] = "���� ������ ����ʹٸ� ���� �����ٰ�";
                text[5] = "������ �غ� �Ǹ� ������ �ٽ� ���� �ɾ���";
                endtext = "�غ�� �� ����?";
                break;
            case 1:
                text[0] = "Q. ���⸦ ���ؾ���.";
                text[1] = "���� ������ ������ �ص� ������� ���� ĥ ���Ⱑ ����";
                text[2] = "���� ���� ���⸦ �Ĵ� ���� �ִ°�?";
                text[3] = "���⼭ �ظ� �ϳ��� ������ ����.";
                endtext = "���߾�! �ظӴ� �������������� �������� ����!";
                break;
            case 2:
                text[0] = "Q. ��ȭ�� �����غ���.";
                text[1] = "���� ���� ���⵵ �������� ��ȭ�� �غ���?";
                text[2] = "��ȭ�� �ϸ� �ۿ��ִ� ���͸� ���� ���� �� �־�!";
                text[3] = "���⼭ �ƹ��ų� ��ȭ�� �ѹ� �������Ѽ���!.";
                endtext = "����. ���� �ο� �غ� �� �Ȱ� ����?";
                break;
            case 3:
                text[0] = "Q. ���� �ֺ� ������� ��������.";
                text[1] = "���� ������ ������ �ص� ������� ���� ĥ ���Ⱑ ����";
                text[2] = "���� ���� ���⸦ �Ĵ� ���� �ִ°�?";
                text[3] = "���⼭ �ظ� �ϳ��� ������ ����.";
                endtext = "���߾�! �ظӴ� �������������� �������� ����!";
                break;
            case 4:
                text[0] = "Q. �Ŵ��� ���� : ���� ���� óġ.";
                text[1] = "���� ������ ������ �ص� ������� ���� ĥ ���Ⱑ ����";
                text[2] = "���� ���� ���⸦ �Ĵ� ���� �ִ°�?";
                text[3] = "���⼭ �ظ� �ϳ��� ������ ����.";
                endtext = "���߾�! �ظӴ� �������������� �������� ����!";
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
            // �迭 ��� ���� �ʱ�ȭ
            
            Story.SetActive(false);
            NextBtn.SetActive(true);
            YesBtn.SetActive(false);
            NoBtn.SetActive(false);
            ClearBtn.SetActive(false);

        }
        else
        {
            LudoText.text = "���� ������ �޼����� �����ݾ�!"; // ���� �ʿ�
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
