using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help : MonoBehaviour
{
    private List<int> itemList = new List<int> {};
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            UseAnd();
            itemList.Add(3);
        }
        
    }

    void UseAnd()
    {
        if (itemList.Count > 0)
        {
            int nextItem = itemList[0]; // 다음 항목 가져오기
            itemList.RemoveAt(0); // 다음 항목 제거
            Debug.Log("Used: " + nextItem);
        }
        else
        {
            Debug.Log("No more items in the list.");
        }
    }
}
