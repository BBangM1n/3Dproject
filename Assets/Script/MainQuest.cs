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
                text[0] = "Q. 마을에 온걸 환영해.";
                text[1] = "나는 Ludo 너를 도와줄 사람이야 초보자의 마을에 온걸 환영해!";
                text[2] = "미안하지만 지금 마을 밖으로 나갈 수 없는 상태야";
                text[3] = "지금 마을 밖에는 공룡들이 나타나서 위험해";
                text[4] = "마을 밖으로 가고싶다면 내가 도와줄게";
                text[5] = "마음의 준비가 되면 나에게 다시 말을 걸어줘";
                Endtext = "준비는 다 됐지?";
                Conditiontext = "Ludo와 대화하기";
                break;
            case 1:
                text[0] = "Q. 무기를 구해야해.";
                text[1] = "지금 밖으로 나간다 해도 공룡들을 물리 칠 무기가 없어";
                text[2] = "저기 좋은 무기를 파는 곳이 있는걸?";
                text[3] = "저기서 해머 하나를 구입해 보자.";
                Endtext = "잘했어! 해머는 근접공격이지만 데미지가 강해!";
                Falsetext = "아직 해머를 구입 안한거 같은데?";
                Conditiontext = "해머 구입하기";
                break;
            case 2:
                text[0] = "Q. 수류탄과 물약을 사용해보자.";
                text[1] = "그래도 해머 하나만 가지고 가기 위험해";
                text[2] = "전투를 쉽게 도와주는 수류탄과 포션이 있어";
                text[3] = "화 수류탄은 초당 도트뎀을, 수 수류탄은 느려짐의 효과를 내지";
                text[4] = "수류탄과 포션 하나를 구입해 사용해 보자.";
                Endtext = "잘했어! 수류탄과 포션들은 각각의 효과가 있으니 잘 사용해봐!";
                Falsetext = "얼른 사용해봐!";
                Conditiontext = "포션" + Potion_Count + " / 1, 수류탄 " + Grenade_Count +  " / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (Potion_Count == 1 && Grenade_Count == 1)
                    isClear = true;
                break;
            case 3:
                text[0] = "Q. 강화를 시작해보자.";
                text[1] = "좋아 이제 무기도 구했으니 강화를 해볼까?";
                text[2] = "강화를 하면 밖에있는 몬스터를 쉽게 잡을 수 있어!";
                text[3] = "저기서 아무거나 강화를 한번 성공시켜서와!.";
                Endtext = "좋아. 이제 싸울 준비가 다 된거 같지?";
                Falsetext = "강화를 하면 더 강해질거야 얼른 해보자";
                Conditiontext = "강화 성공하기 " + Enhance_Count + " / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (Enhance_Count == 1)
                    isClear = true;
                break;
            case 4:
                text[0] = "Q. 마을 주변 공룡들을 정리해줘.";
                text[1] = "이제 실전이야 준비는 됐지?";
                text[2] = "저기 마을 밖으로 나가면 공룡들이 있을거야";
                text[3] = "그녀석들을 처치해줘.";
                Endtext = "대단해! 해낼 줄 알았어";
                Falsetext = "아직 마을 밖에는 많이 남아 있는걸..";
                Conditiontext = "공룡 몬스터 처치 " + Enemy_Count + " / 3";
                if(QuestOn)
                    QText.text = Conditiontext;
                if (Enemy_Count == 3)
                    isClear = true;
                break;
            case 5:
                text[0] = "Q. 거대한 공룡 : 보스 몬스터 처치.";
                text[1] = "좋아 그럼 그녀석들의 우두머리를 잡아보자.";
                text[2] = "그녀석은 숲에 있는 녀석들중 가장 강력해";
                text[3] = "돌도 굴리고 미사일도 쏴!! 엄청나지?";
                text[4] = "그녀석을 잡을 수 있겠어?";
                Endtext = "정말로 해냈다니! 놀라워";
                Falsetext = "역시 너에겐 무리였을까?";
                Conditiontext = "공룡 보스 몬스터 처치 " + BossEnemy_Count +" / 1";
                if (QuestOn)
                    QText.text = Conditiontext;
                if (BossEnemy_Count == 1)
                    isClear = true;
                break;
            case 6:
                text[0] = "Q. 다음 여행으로";
                text[1] = "덕분에 우리 마을에 큰 위협은 사라진거같아";
                text[2] = "녀석이 막고있던 입구도 사라졌고";
                text[3] = "이제 다른 마을로 이동할 수 있을거 같아";
                text[4] = "다른 마을도 우리랑 비슷한 상황이야 더 강한 몬스터들이 길을 막고있어";
                text[4] = "더욱 강해져서 다른 마을도 몬스터로부터 구해줘";
                Endtext = "아직 개발 중입니다.";
                Falsetext = "더욱 강해져서 다른 마을도 몬스터로부터 구해줘";
                Conditiontext = "개발중입니다.";
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
            // 배열 대사 내용 초기화
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
                    //닫기 버튼
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
