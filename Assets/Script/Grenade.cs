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
    IEnumerator Explosion() // �ڷ�ƾ
    {
        yield return new WaitForSeconds(3f); // 3�ʵ� �ߵ�
        SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[7];
        SoundManager.instance.Effect_Sound.Play();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true); // ����Ʈ �ߵ�

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Enemy")); // SphereCastAll : ��ü��� ����ĳ����

        if(type == Type.Property) // ���� Ÿ���� �ִ� ����ź�̶��
        {
            Instantiate(effect, transform.position, transform.rotation); // ������ִ� ����Ʈ Ȱ��ȭ
        }
        else
        {
            foreach (RaycastHit hitObj in rayHits) // ������Ʈ�� ������ enemy�鿡�� ����
            {
                hitObj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
            }
        }
        Destroy(gameObject, 5f);
    }
}
