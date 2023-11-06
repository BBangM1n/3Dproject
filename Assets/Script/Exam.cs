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
        qmgr.LoadData(); // ���� ���۵� �� ����� �����͸� �ҷ��ɴϴ�.
        UseQuestList();
    }

    // ����Ʈ ����� �ҷ��� �Ŀ� ����ϴ� ����
    void UseQuestList()
    { 
        // �� ��° ����Ʈ �������� (�ε��� 1)
        QuestData secondQuest = qmgr.questDataList.questDataList[1];
        Debug.Log("�� ��° ����Ʈ ID: " + secondQuest.QuestID);

        // �� ��° ����Ʈ �������� (�ε��� 3)
        QuestData fourthQuest = qmgr.questDataList.questDataList[3];
        Debug.Log("�� ��° ����Ʈ Clear ����: " + fourthQuest.Clear);

    }

}
