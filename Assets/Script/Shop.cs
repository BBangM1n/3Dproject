  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Shop : MonoBehaviour
{

    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemBtn;
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;
    public Text talkText;

    public bool isQuest = false;

    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    
    public void Exit()
    {
        if(!isQuest)
            anim.SetTrigger("Hello");
        if (enterPlayer.isshop == true)
            enterPlayer.isshop = false;
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }
        SoundManager.instance.Effect_Sound_2.clip = SoundManager.instance.EffectGroup[0];
        SoundManager.instance.Effect_Sound_2.Play();
        enterPlayer.coin -= price;
        DataManager.instance.nowPlayer.Gold -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);

    }

    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }

    public void Next()
    {
        if(itemBtn[0].activeSelf && itemBtn[1].activeSelf && itemBtn[2].activeSelf)
        {
            itemBtn[0].SetActive(false);
            itemBtn[1].SetActive(false);
            itemBtn[2].SetActive(false);
            itemBtn[3].SetActive(true);
            itemBtn[4].SetActive(true);
            itemBtn[5].SetActive(true);
        }
        else
        {
            itemBtn[0].SetActive(true);
            itemBtn[1].SetActive(true);
            itemBtn[2].SetActive(true);
            itemBtn[3].SetActive(false);
            itemBtn[4].SetActive(false);
            itemBtn[5].SetActive(false);
        }
    }

}
