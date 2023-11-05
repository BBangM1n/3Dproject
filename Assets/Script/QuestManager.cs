using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    List<Text> QuestTitle = new List<Text>();
    List<Text> ContentText = new List<Text>();
    List<Text> GiftText = new List<Text>();
    List<Image> GiftImage = new List<Image>();
    List<bool> Clear = new List<bool>();

    Text questtitle;
    Text contenttext;
    Text gifttext;
    Image giftimage;
    Image[] imagelist;
    bool clear;

    void QuestList(int value)
    {
        switch (value)
        {
            case 0:
                questtitle.text = "1번퀘스트";
                contenttext.text = "이건 첫번째 퀘스트여";
                gifttext.text = "2000";
                giftimage = imagelist[0];
                clear = false;
                break;
            case 1:
                break;
        }
    }
}
