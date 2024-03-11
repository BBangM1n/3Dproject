using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartZone : MonoBehaviour
{
    public GameManager manager;
    public Vector3 spawnVt;

    public Text FloorText;
    // Start is called before the first frame update
    void Start()
    {
        FloorText = GameObject.Find("FloorUIText").gameObject.GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(manager.isBossbattle == false)
                other.gameObject.transform.position = spawnVt;

            StopCoroutine(FloorTextCoroutin());
            StartCoroutine(FloorTextCoroutin());
        }
    }

    public IEnumerator FloorTextCoroutin()
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

        // ´ë±â
        yield return new WaitForSeconds(0.5f);

        FloorText.gameObject.SetActive(false);

    }
}
