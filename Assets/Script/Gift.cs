using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public enum GiftType { Coin, Ammo, Grenade, Potion };
    public GiftType Gifttype;
    public enum GrenadeType { no, Basic, Fire, Ice };
    public GrenadeType grenadetype;
    public enum PotionType { no, Speed, Health, Pawer, AS, Coin };
    public PotionType Potiontype;

    public int value;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clear() // 클리어 보상함수
    {
        switch (Gifttype)
        {
            case GiftType.Coin:
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.coin += value;
                break;
            case GiftType.Ammo:
                break;
            case GiftType.Grenade:
                break;
            case GiftType.Potion:
                break;
        }
    }
}
