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
    }

    private void Start()
    {
        
        QuestTitleText.text = "��� ���� ���� óġ";
        ContentText.text = "��� ���� ���Ͱ� �츮 ������ ���ظ�\n������ �־� ������!";
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
}
