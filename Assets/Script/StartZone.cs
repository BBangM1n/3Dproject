using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartZone : MonoBehaviour
{
    public GameManager manager;
    public Vector3 spawnVt;

    public Text FloorText;

    void Start()
    {
        FloorText = GameObject.Find("FloorUIText").gameObject.GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(manager.isBossbattle == false) // 보스 배틀중이 아니라면
                other.gameObject.transform.position = spawnVt; // 지정된 곳으로 순간이동

            StopCoroutine(FloorTextCoroutin());
            StartCoroutine(FloorTextCoroutin()); // 지정된 맵 이름 알림 코루틴
        }
    }

    public IEnumerator FloorTextCoroutin() // 맵 이름 알림 코루틴
    {
        yield return new WaitForSeconds(0.5f);
        FloorText.gameObject.SetActive(true);

        while (FloorText.color.a < 1)
        {
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a + (Time.deltaTime * 0.5f));
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a + (Time.deltaTime * 0.5f));
            yield return null;
        }

        while (FloorText.color.a > 0)
        {
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            FloorText.color = new Color(FloorText.color.r, FloorText.color.g, FloorText.color.b, FloorText.color.a - (Time.deltaTime * 0.5f));
            yield return null;
        }

        // 대기
        yield return new WaitForSeconds(0.5f);

        FloorText.gameObject.SetActive(false);

    }
}
