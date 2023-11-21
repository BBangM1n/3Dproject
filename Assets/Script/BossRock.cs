using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRock : Bullet // 보스 돌 굴리는 패턴
{
    Rigidbody rigid;
    float angularPower = 2; // 굴러가는 힘
    float scaleValue = 0.1f; // 커지는 힘
    bool isShoot;
    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        StartCoroutine(GainPower());
        StartCoroutine(GainPowerTimer());
    }

    IEnumerator GainPowerTimer() // 쿨타임
    {
        yield return new WaitForSeconds(2.2f);
        isShoot = true;
    }

    IEnumerator GainPower()
    {
        while (!isShoot)
        {
            angularPower += 0.02f;
            scaleValue += 0.005f;
            transform.localScale = Vector3.one * scaleValue;
            rigid.AddTorque(transform.right * angularPower, ForceMode.Acceleration); // Accleration 점차증가하기위해 사용, 돌을 굴리는 함수
            yield return null;
        }
    }
}
