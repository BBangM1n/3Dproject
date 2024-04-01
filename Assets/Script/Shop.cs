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
    bool FindPlayer = false;

    private void Start()
    {

    }
    private void Update()
    {
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 3, Vector3.up, 0f, LayerMask.GetMask("Player")); // 플레이어 감지
        foreach (RaycastHit hit in rayHits) // 플레이어가 감지됐다면
        {
            enterPlayer = hit.transform.gameObject.GetComponent<Player>();
            enterPlayer.nearObject = this.gameObject;
            FindPlayer = true;
        }

        if(FindPlayer == true && rayHits.Length == 0) // 감지된 플레이어 배열이 0개라면,
        {
            FindPlayer = false;
            enterPlayer.nearObject = null;
            Exit();
        }
    }
    public void Enter(Player player) // 플레이어가 상점을 열때
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }
    
    public void Exit() // 플레이어가 떠날 때
    {
        if(!isQuest)
            anim.SetTrigger("Hello");
        if (enterPlayer.isshop == true)
            enterPlayer.isshop = false;
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index) // 구입
    {
        int price = itemPrice[index]; // 아이템의 가격
        if(price > enterPlayer.coin) // 가격보다 코인이 적다면
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        SoundManager.instance.Effect_Sound_2.clip = SoundManager.instance.EffectGroup[0];
        SoundManager.instance.Effect_Sound_2.Play();
        enterPlayer.coin -= price; // 값 지불
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
