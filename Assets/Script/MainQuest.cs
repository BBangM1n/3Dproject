using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MainQuestData
{
    public int QuestID;
}

public class MainQuest : MonoBehaviour
{
    public static MainQuest Instance { get; private set;}

    public List<MainQuestData> QuestList = new List<MainQuestData>();
    string[] text = new string[10];
    string Endtext;
    string Falsetext;
    string Conditiontext;


    public GameObject Story;
    public string Content;
    public Text LudoText;
    public Text ClearGift;
    public GameObject NextBtn;
    public GameObject YesBtn;
    public GameObject NoBtn;
    public GameObject ClearBtn;
    public GameObject CloseBtn;

    int Count = 0;
    public int QuestValue = 0;
    public bool QuestOn = false;
    public bool isClear = false;

    Player player;

    public TMP_Text TitleText;
    public TMP_Text QText;
    public TextMeshProUGUI Qcolor;

    public GameObject[] State;

    public int Potion_Count;
    public int Grenade_Count;
    public int Enhance_Count;
    public int Enemy_Count;
    public int BossEnemy_Count;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        QuestValue = DataManager.instance.nowPlayer.MainQuestValue;

        MainQuestList();
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();

        TitleText.text = "";
        QText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        MainQuestContent(QuestList[QuestValue].QuestID);
        Conditional_state();
    }

    void Conditional_state()
    {
        if(isClear == true)
        {
            State[0].SetActive(false);
            State[1].SetActive(true);
        }
        else if(QuestOn == true)
        {
            State[0].SetActive(false);
            State[1].SetActive(false);
        }
        else
        {
            State[0].SetActive(true);
            State[1].SetActive(false);
        }

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

        MainQuestData newQuest7 = new MainQuestData();
        newQuest7.QuestID = 6;
        QuestList.Add(newQuest7);
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
                Endtext = "�غ�� �� ����?";
                Conditiontext = "Ludo�� ��ȭ�ϱ�";
                break;
            case 1:
                text[0] = "Q. ���⸦ ���ؾ���.";
                text[1] = "���� ������ ������ �ص� ������� ���� ĥ ���Ⱑ ����";
                text[2] = "���� ���� ���⸦ �Ĵ� ���� �ִ°�?";
                text[3] = "���⼭ �ظ� �ϳ��� ������ ����.";
                Endtext = "���߾�! �ظӴ� �������������� �������� ����!";
                Falsetext = "���� �ظӸ� ���� ���Ѱ� ������?";
                Conditiontext = "�ظ� �����ϱ�";
                break;
            case 2:
                text[0] = "Q. ����ź�� ������ ����غ���.";
                text[1] = "�׷��� �ظ� �ϳ��� ������ ���� ������";
                text[2] = "������ ���� �����ִ� ����ź�� ������ �־�";
                text[3] = "ȭ ����ź�� �ʴ� ��Ʈ����, �� ����ź�� �������� ȿ���� ����";
                text[4] = "����ź�� ���� �ϳ��� ������ ����� ����.";
                Endtext = "���߾�! ����ź�� ���ǵ��� ������ ȿ���� ������ �� ����غ�!";
                Falsetext = "�� ����غ�!";
                Conditiontext = "����" + Potion_Count + " / 1, ����ź " + Grenade_Count +  " / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (Potion_Count == 1 && Grenade_Count == 1)
                    isClear = true;
                break;
            case 3:
                text[0] = "Q. ��ȭ�� �����غ���.";
                text[1] = "���� ���� ���⵵ �������� ��ȭ�� �غ���?";
                text[2] = "��ȭ�� �ϸ� �ۿ��ִ� ���͸� ���� ���� �� �־�!";
                text[3] = "���⼭ �ƹ��ų� ��ȭ�� �ѹ� �������Ѽ���!.";
                Endtext = "����. ���� �ο� �غ� �� �Ȱ� ����?";
                Falsetext = "��ȭ�� �ϸ� �� �������ž� �� �غ���";
                Conditiontext = "��ȭ �����ϱ� " + Enhance_Count + " / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (Enhance_Count == 1)
                    isClear = true;
                break;
            case 4:
                text[0] = "Q. ���� �ֺ� ������� ��������.";
                text[1] = "���� �����̾� �غ�� ����?";
                text[2] = "���� ���� ������ ������ ������� �����ž�";
                text[3] = "�׳༮���� óġ����.";
                Endtext = "�����! �س� �� �˾Ҿ�";
                Falsetext = "���� ���� �ۿ��� ���� ���� �ִ°�..";
                Conditiontext = "���� ���� óġ " + Enemy_Count + " / 3";
                if(QuestOn)
                    QText.text = Conditiontext;
                if (Enemy_Count == 3)
                    isClear = true;
                break;
            case 5:
                text[0] = "Q. �Ŵ��� ���� : ���� ���� óġ.";
                text[1] = "���� �׷� �׳༮���� ��θӸ��� ��ƺ���.";
                text[2] = "�׳༮�� ���� �ִ� �༮���� ���� ������";
                text[3] = "���� ������ �̻��ϵ� ��!! ��û����?";
                text[4] = "�׳༮�� ���� �� �ְھ�?";
                Endtext = "������ �س´ٴ�! ����";
                Falsetext = "���� �ʿ��� ����������?";
                Conditiontext = "���� ���� ���� óġ " + BossEnemy_Count +" / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (BossEnemy_Count == 1)
                    isClear = true;
                break;
            case 6:
                text[0] = "Q. ���� ��������";
                text[1] = "���п� �츮 ������ ū ������ ������Ű���";
                text[2] = "�༮�� �����ִ� �Ա��� �������";
                text[3] = "���� �ٸ� ������ �̵��� �� ������ ����";
                text[4] = "�ٸ� ������ �츮�� ����� ��Ȳ�̾� �� ���� ���͵��� ���� �����־�";
                text[4] = "���� �������� �ٸ� ������ ���ͷκ��� ������";
                Endtext = "���� ���� ���Դϴ�.";
                Falsetext = "���� �������� �ٸ� ������ ���ͷκ��� ������";
                Conditiontext = "�������Դϴ�.";
                break;
        }

        if(isClear)
        {
            Qcolor.color = new Color32(0, 255, 0, 255);
        }
        else
        {
            Qcolor.color = new Color32(255, 255, 0, 255);
        }
    }

    public void NextBtnClick()
    {
        if (string.IsNullOrWhiteSpace(text[Count + 1]) == false)
        {
            Count++;
            LudoText.text = text[Count];
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
        ClearBtn.SetActive(false);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
        QuestOn = true;
        TitleText.text = text[0];
        QText.text = Conditiontext;
        player.isshop = false;
    }

    public void NoBtnClick()
    {
        Count = 0;
        Story.SetActive(false);
        NextBtn.SetActive(true);
        ClearBtn.SetActive(false);
        YesBtn.SetActive(false);
        NoBtn.SetActive(false);
        player.isshop = false;
    }
    
    public void ClearBtnClick()
    {
        if (isClear)
        {
            isClear = false;
            Cleargift(QuestList[QuestValue].QuestID);
            Count = 0;
            QuestValue++;
            DataManager.instance.nowPlayer.MainQuestValue += 1;
            // �迭 ��� ���� �ʱ�ȭ
            System.Array.Clear(text, 0, text.Length);
            QuestOn = false;
            Story.SetActive(false);
            NextBtn.SetActive(true);
            YesBtn.SetActive(false);
            NoBtn.SetActive(false);
            ClearBtn.SetActive(false);
            TitleText.text = "";
            QText.text = "";
            player.isshop = false;
        }
    }

    void Cleargift(int id)
    {
        switch (id)
        {
            case 0:
                player.coin += 500;
                DataManager.instance.nowPlayer.Gold += 500;
                break;
            case 1:
                player.coin += 5500;
                DataManager.instance.nowPlayer.Gold += 5500;
                break;
            case 2:
                player.coin += 500;
                DataManager.instance.nowPlayer.Gold += 500;
                break;
            case 3:
                player.coin += 3000;
                DataManager.instance.nowPlayer.Gold += 3000;
                break;
            case 4:
                player.coin += 5000;
                DataManager.instance.nowPlayer.Gold += 5000;
                break;
            case 5:
                break;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            Story.SetActive(true);
            player.isshop = true;
            if(QuestOn)
            {
                if(isClear)
                {
                    LudoText.text = Endtext;
                    ClearBtn.SetActive(true);
                    CloseBtn.SetActive(false);
                }
                else
                {
                    LudoText.text = Falsetext;
                    CloseBtn.SetActive(true);
                    //�ݱ� ��ư
                }
            }
            else
            {
                LudoText.text = text[Count];
            }

            if (QuestOn && QuestList[QuestValue].QuestID == 0)
            {
                isClear = true;
            }
        }

    }

}
