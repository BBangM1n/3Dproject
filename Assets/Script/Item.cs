using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Type { Ammo, Coin, Grenade, Heart, Weapon, Potion} //enum : ������ Ÿ��
    public Type type;
    public int value;
    public int Grenadevalue = -1;
    Rigidbody rigid;
    SphereCollider sphereCollider;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphereCollider = GetComponent<SphereCollider>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(type != Type.Potion)
            transform.Rotate(Vector3.up * 10 * Time.deltaTime);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            rigid.isKinematic = true;
            sphereCollider.enabled = false;
        }
    }
}
