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
        // 두 번째 퀘스트 가져오기 (인덱스 1)
        QuestData secondQuest = qmgr.questDataList.questDataList[1];
        Debug.Log("두 번째 퀘스트 ID: " + secondQuest.QuestID);

        // 네 번째 퀘스트 가져오기 (인덱스 3)
        QuestData fourthQuest = qmgr.questDataList.questDataList[3];
        Debug.Log("네 번째 퀘스트 Clear 여부: " + fourthQuest.Clear);

    }

}
