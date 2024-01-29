using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MainQuestData
{
    public int QuestID;
    public string QuestTitle;
    public int Giftitem;
    public int Giftvalue;
}

public class MainQuest : MonoBehaviour
{
    public List<MainQuestData> QuestList = new List<MainQuestData>();

    // Start is called before the first frame update
    void Start()
    {
        MainQuestList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MainQuestList()
    {
        MainQuestData newQuest1 = new MainQuestData();
        newQuest1.QuestID = 1111;
        newQuest1.QuestTitle = "> 마을에 온걸 환영해";
        newQuest1.Giftitem = 0;
        newQuest1.Giftvalue = 1000;
        QuestList.Add(newQuest1);
    }
}
