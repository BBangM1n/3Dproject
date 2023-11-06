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
        
        QuestTitleText.text = "녹색 공룡 몬스터 처치";
        ContentText.text = "녹색 공룡 몬스터가 우리 마을에 피해를\n입히고 있어 도와줘!";
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
        nobtnText.text = "닫기";
    }

    public void Clearbtnclick() //클리어 여부함수
    {
        if(isclear)
        {
            Gift gi= gift.GetComponent<Gift>();
            gi.Clear();
            Debug.Log("조건이 충족됐습니다");
            quest.transform.GetChild(2).gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("조건이 충족되지 않았습니다");
        }
    }
}
