using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    public bool isVillage; // 마을 여부 체크
    public string FloorName; // 땅 이름

    public Text FloorText;
    // Start is called before the first frame update
    void Start()
    {
        FloorText = GameObject.Find("FloorUIText").gameObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player")) // 플레이어가 땅을 밟을 시
        {
            if (isVillage) 
                StartCoroutine(HPUP()); // 마을이면 HP자동 회복 함수 발동

            GameManager manager = GameObject.Find("Game Manager").gameObject.GetComponent<GameManager>();
            manager.stageNameText.text = FloorName;
            FloorText.text = FloorName;

            if(DataManager.instance.Tutorial == false)
                FieldValue();
        }
    }

    public void FieldValue()
    {
        if(SoundManager.instance.isboss == false)
        {
            switch(FloorName)
            {
                case "초보자의 마을":
                    SoundManager.instance.SoundChange(0);
                    break;
                case "공룡들의 숲":
                    SoundManager.instance.SoundChange(1);
                    break;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        StopAllCoroutines();
    }

    IEnumerator HPUP()
    {
        Player player = GameObject.Find("Player").gameObject.GetComponent<Player>();
        if (player.health < player.maxhealth)
        {
            player.health += 5;
            if(player.health > player.maxhealth)
            {
                player.health = player.maxhealth;
            }
        }
        yield return new WaitForSeconds(5f);
        StartCoroutine(HPUP());
    }
}
