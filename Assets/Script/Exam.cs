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
        foreach (QuestData quest in qmgr.questDataList.questDataList)
        {
            Debug.Log("����Ʈ ID: " + quest.QuestID);
            Debug.Log("����Ʈ ����: " + quest.QuestTitle);
            Debug.Log("����Ʈ ����: " + quest.ContentText);
            Debug.Log("Ŭ���� ����: " + quest.Clear);
            Debug.Log("���� ������: " + quest.Giftitem);
            Debug.Log("���� ��: " + quest.Giftvalue);
        }
    }

}
