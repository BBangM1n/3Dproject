using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gift : MonoBehaviour
{
    public enum GiftType { Coin, Ammo, Grenade, Potion };
    public GiftType Gifttype;
    public enum GrenadeType { Basic, Fire, Ice };
    public GrenadeType grenadetype;
    public enum PotionType { Speed, Health, Pawer, AS, Coin };
    public PotionType Potiontype;

    public GameObject[] Grenades;
    public GameObject[] Potions;

    public Image giftimg;
    public Sprite[] GiftTypeimg;

    public Transform giftzone;

    public int itemcode;
    public int value;
    // Start is called before the first frame update

    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ItemType();
    }

    void ItemType()
    {
        if(itemcode == 0) //코인
        {
            Gifttype = GiftType.Coin;
            giftimg.sprite = GiftTypeimg[0];
        }
        else if(itemcode == 1) //총알
        {
            Gifttype = GiftType.Ammo;
            giftimg.sprite = GiftTypeimg[1];
        }
        else if (itemcode == 2) //수류탄
        {
            Gifttype = GiftType.Grenade;
            giftimg.sprite = GiftTypeimg[2];
        }
        else if (itemcode == 3) //포션
        {
            Gifttype = GiftType.Potion;
            giftimg.sprite = GiftTypeimg[3];
        }
    }

    public void Clear() // 클리어 보상함수
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        switch (Gifttype)
        {
            case GiftType.Coin:
                player.coin += value;
                break;
            case GiftType.Ammo:
                player.ammo += value;
                break;
            case GiftType.Grenade:
                Instantiate(Grenades[0], giftzone.position, giftzone.rotation);
                break;
            case GiftType.Potion:
                Instantiate(Potions[0], giftzone.position, giftzone.rotation);
                break;
        }
    }
}
