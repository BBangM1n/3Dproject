using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    public float curtime = 0f;
    float coolTime = 1f;
    bool iscool = false;
    void Start()
    {
        transform.position = new Vector3(transform.position.x ,0.76f, transform.position.z);
        transform.rotation = Quaternion.identity;
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        curtime += Time.deltaTime;
        if (curtime >= coolTime)
            cool();

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if(iscool)
            {
                    enemy.curHealth -= 5;
                    StartCoroutine(enemy.OnDamage(transform.position, false));
                    iscool = false;
                    curtime = 0;
            }
        }
    }

    void cool()
    {
        iscool = true;
    }

}