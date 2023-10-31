using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{   public enum Type { Basic, Property };
    public Type type;
    public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rigid;
    public GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Enemy")); // SphereCastAll : 구체모양 레이캐스팅

        if(type == Type.Property)
        {
            Instantiate(effect, transform.position, transform.rotation);
        }
        else
        {
            foreach (RaycastHit hitObj in rayHits)
            {
                hitObj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
            }
        }
        Destroy(gameObject, 5f);
    }
}
