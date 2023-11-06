using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exam : MonoBehaviour
{
    QuestManager qmgr;

    private void Awake()
    {
        qmgr = GetComponent<QuestManager>();
    }
    private void Start()
    {
        qmgr.LoadData(); // 앱이 시작될 때 저장된 데이터를 불러옵니다.
        UseQuestList();
    }

    // 퀘스트 목록을 불러온 후에 사용하는 예시
    void UseQuestList()
    {
        foreach (QuestData quest in qmgr.questDataList.questDataList)
        {
            Debug.Log("퀘스트 ID: " + quest.QuestID);
            Debug.Log("퀘스트 제목: " + quest.QuestTitle);
            Debug.Log("퀘스트 내용: " + quest.ContentText);
            Debug.Log("클리어 여부: " + quest.Clear);
            Debug.Log("보상 아이템: " + quest.Giftitem);
            Debug.Log("보상 값: " + quest.Giftvalue);
        }
    }

}
