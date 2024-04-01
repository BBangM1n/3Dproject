using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{   public enum Type { Basic, Property }; // 기본, 속성 인지 구별하기 위한 값
    public Type type;
    public GameObject meshObj; // 수류탄 메쉬
    public GameObject effectObj; // 임펙트 오브젝트
    public Rigidbody rigid;
    public GameObject effect;

    void Start()
    {
        StartCoroutine(Explosion());
    }

    IEnumerator Explosion() // 코루틴
    {
        yield return new WaitForSeconds(3f); // 3초뒤 발동
        SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[7];
        SoundManager.instance.Effect_Sound.Play();
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true); // 임팩트 발동

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0f, LayerMask.GetMask("Enemy")); // SphereCastAll : 구체모양 레이캐스팅

        if(type == Type.Property) // 만약 타입이 있는 수류탄이라면
        {
            Instantiate(effect, transform.position, transform.rotation); // 저장돼있는 임펙트 활성화
        }
        else
        {
            foreach (RaycastHit hitObj in rayHits) // 레이히트에 접촉한 enemy들에게 적용
            {
                hitObj.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
            }
        }
        Destroy(gameObject, 5f);
    }
}
