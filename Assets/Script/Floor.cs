using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Floor : MonoBehaviour
{
    public bool isVillage;
    public string FloorName;

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
        if(collision.gameObject.CompareTag("Player"))
        {
            if (isVillage)
                StartCoroutine(HPUP());

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
        Debug.Log("코루틴끝");
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
        Debug.Log("피업");
        yield return new WaitForSeconds(5f);
        StartCoroutine(HPUP());
    }
}
