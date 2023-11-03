using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type { Melee, Range };
    public Type type;
    public enum RangeType { nothing ,Hand, Sub, Shot };
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

    public int value;
    public int afvalue;

    private void Update()
    {
        RangeBuf();
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

        /*if (type == Type.Range && curammo > 0 && rangetype == RangeType.Shot)
        {
            curammo--;
            StartCoroutine("Shotgun");
        }*/
    }

    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.45f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }

    /*IEnumerator Shotgun() //¼¦°ÇÀü¿ë ¼¦
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletrigid = instantBullet.GetComponent<Rigidbody>();
        bulletrigid.velocity = bulletPos.forward * 50;
        yield return null;

        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caserigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caserigid.AddForce(caseVec, ForceMode.Impulse);
        caserigid.AddTorque(Vector3.up * 10, ForceMode.Impulse); // È¸Àü°ª
    }*/

    IEnumerator Shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletrigid = instantBullet.GetComponent<Rigidbody>();
        bulletrigid.velocity = bulletPos.forward * 50;
        yield return null;

        GameObject instantCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody caserigid = instantCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-3, -2) + Vector3.up * Random.Range(2, 3);
        caserigid.AddForce(caseVec, ForceMode.Impulse);
        caserigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);
    }

    void RangeBuf()
    {
        Bullet bul = bullet.GetComponent<Bullet>();
        if (type == Type.Range && damage > 0)
        {
            bul.damage = value;
        }
        else
        {
            bul.damage = afvalue;
        }
    }
}
