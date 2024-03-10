using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public enum RangeType { nothing ,Hand, Sub };
    public RangeType rangetype;
    public int damage;
    public float rate;
    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    public Transform bulletPos;
    public GameObject bullet;
    public Transform bulletCasePos;
    public GameObject bulletCase;
    public int maxammo;
    public int curammo;

    private void Update()
    {
        Bulletdamage();
    }
    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }

        if (type == Type.Range && curammo > 0)
        {
            curammo--;
            StartCoroutine("Shot");
        }
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.45f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;
        SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[1];
        SoundManager.instance.Effect_Sound.Play();
        Debug.Log("½ºÀ®»ç¿îµå");
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletrigid = instantBullet.GetComponent<Rigidbody>();
        bulletrigid.velocity = bulletPos.forward * 50;
        if(RangeType.Hand == rangetype)
        {
            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[2];
            SoundManager.instance.Effect_Sound.Play();
        }
        else
        {
            SoundManager.instance.Effect_Sound.clip = SoundManager.instance.EffectGroup[3];
            SoundManager.instance.Effect_Sound.Play();
        }
        yield return null;

        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caserigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caserigid.AddForce(caseVec, ForceMode.Impulse);
        caserigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    void Bulletdamage()
    {
        if (type == Type.Melee)
            return;
        else
        {
            Bullet bul = bullet.GetComponent<Bullet>();
            bul.damage = damage;
        }

    }
}
