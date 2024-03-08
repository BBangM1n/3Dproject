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

            if(manager.menuCam.activeSelf == false)
            {
                FloorText.text = FloorName;
                StartCoroutine(FloorTextCoroutin());
            }

        }

    }
    
    public IEnumerator FloorTextCoroutin()
    {
        while (FloorText.color.a > 0)
        {
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        // 텍스트가 서서히 나타나도록 함
        while (FloorText.color.a < 1)
        {
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a + (Time.deltaTime * 0.5f));
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a + (Time.deltaTime * 0.5f));
            yield return null;
        }

        // 대기
        yield return new WaitForSeconds(0.5f);

        while (FloorText.color.a > 0)
        {
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            yield return null;
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
        yield return new WaitForSeconds(10f);
        StartCoroutine(HPUP());
    }
}
