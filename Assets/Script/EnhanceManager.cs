using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceManager : MonoBehaviour
{
    public enum Type {Head, Armor, Hammer, Gun }
    public Type type;
    //플레이어 강화 상태 불러오기
    public int Level;
    public int gold;
    public int PlusStat; // 강화된 스탯수치
    public int PlusHand;
    public int PlusSub;
    public int Probability; // 확률
    public bool isGun;

    public Text LvText;
    public Text Stat;
    public Text Consumgold;
    public Text TalkText;

    Player player;

    void Start()
    {
        player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        if (type == Type.Head)
        {
            Level = DataManager.instance.nowPlayer.EnhanceHead;
            PlusStat = 10 * Level;
            player.maxhealth += PlusStat;
        }
        else if (type == Type.Armor)
        {
            Level = DataManager.instance.nowPlayer.EnhanceArmor;
            PlusStat = 1 * Level;
            player.defens += PlusStat;
        }
        else if (type == Type.Hammer)
        {
            Level = DataManager.instance.nowPlayer.EnhanceHammer;
            PlusStat = 3 * Level;
            GameObject.Find("Weapon Point").transform.GetChild(0).gameObject.GetComponent<Weapon>().damage += PlusStat;
        }
        else if (type == Type.Gun)
        {
            Level = DataManager.instance.nowPlayer.EnhanceGun;
            PlusHand = 2 * Level;
            PlusSub = 1 * Level;
            GameObject.Find("Weapon Point").transform.GetChild(1).gameObject.GetComponent<Weapon>().damage += PlusHand;
            GameObject.Find("Weapon Point").transform.GetChild(2).gameObject.GetComponent<Weapon>().damage += PlusSub;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Enhance(Level);
        LvText.text = "Lv." + Level;
        if (isGun)
            Stat.text = "+ " + PlusHand + " & + " + PlusSub;
        else
            Stat.text = "+ " + PlusStat;
        Consumgold.text = gold.ToString();
    }

    public void EnhanceBtnClick()
    {
        if(player.coin < gold)
        {
            TalkText.text = "지금 가진 돈이 부족해!";
        }
        else
        {
            player.coin -= gold;
            DataManager.instance.nowPlayer.Gold -= gold;
            int random = Random.Range(0, 101);
            if (Probability > random)
            {
                TalkText.text = "축하해! 강화에 성공했어!";
                Level++;
                SoundManager.instance.Effect_Sound_2.clip = SoundManager.instance.EffectGroup[9];
                SoundManager.instance.Effect_Sound_2.Play();

                if (MainQuest.Instance.QuestOn && MainQuest.Instance.QuestList[MainQuest.Instance.QuestValue].QuestID == 3)
                {
                    MainQuest.Instance.Enhance_Count++;
                }

                switch (type)
                {
                    case Type.Head:
                        player.maxhealth += 10;
                        PlusStat += 10;
                        DataManager.instance.nowPlayer.EnhanceHead += 1;
                        break;
                    case Type.Armor:
                        player.defens += 1;
                        PlusStat += 1;
                        DataManager.instance.nowPlayer.EnhanceArmor += 1;
                        break;
                    case Type.Hammer:
                        GameObject.Find("Weapon Point").transform.GetChild(0).gameObject.GetComponent<Weapon>().damage += 3;
                        PlusStat += 3;
                        DataManager.instance.nowPlayer.EnhanceHammer += 1;
                        break;
                    case Type.Gun:
                        GameObject.Find("Weapon Point").transform.GetChild(1).gameObject.GetComponent<Weapon>().damage += 2;
                        PlusHand += 2;
                        GameObject.Find("Weapon Point").transform.GetChild(2).gameObject.GetComponent<Weapon>().damage += 1;
                        PlusSub += 1;
                        DataManager.instance.nowPlayer.EnhanceGun += 1;
                        break;
                }
            }
            else
            {
                TalkText.text = "아쉽게 강화에 실패했어...";
                SoundManager.instance.Effect_Sound_2.clip = SoundManager.instance.EffectGroup[10];
                SoundManager.instance.Effect_Sound_2.Play();
            }
        }
    }

    public void Enhance(int level)
    {
        switch(level)
        {
            case 0:
                gold = 500;
                Probability = 100;
                break;
            case 1:
                gold = 1000;
                Probability = 75;
                break;
            case 2:
                gold = 1500;
                Probability = 60;
                break;
            case 3:
                gold = 2000;
                Probability = 50;
                break;
            case 4:
                gold = 2500;
                Probability = 40;
                break;
            case 5:
                gold = 3000;
                Probability = 30;
                break;
            case 6:
                gold = 3500;
                Probability = 25;
                break;
            case 7:
                gold = 4000;
                Probability = 20;
                break;
            case 8:
                gold = 4500;
                Probability = 15;
                break;
            case 9:
                gold = 5000;
                Probability = 10;
                break;
            case 10:
                gold = 5500;
                Probability = 5;
                break;
        }
    }
}
