using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public RectTransform uiGroup;
    Player enterPlayer;
    public void Enter(Player player)
    {
        enterPlayer = player;
        Debug.Log("�湮�ߴ�");
        uiGroup.anchoredPosition = Vector3.zero;
    }
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }
}
