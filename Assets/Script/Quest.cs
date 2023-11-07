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
        qmgr.LoadData(); // ���� ���۵� �� ����� �����͸� �ҷ��ɴϴ�.
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
        nobtnText.text = "�ݱ�";
    }

    public void Clearbtnclick() //Ŭ���� �����Լ�
    {
        if(isclear)
        {
            Gift gi= gift.GetComponent<Gift>();
            gi.Clear();
            Debug.Log("������ �����ƽ��ϴ�");
            quest.transform.GetChild(2).gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("������ �������� �ʾҽ��ϴ�");
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
                    //����
                    Debug.Log("Ŭ����");
                }
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}
