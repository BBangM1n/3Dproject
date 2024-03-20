# 쿼드액션
## 게임 장르 : 3D 슈팅 액션 RPG
## 게임 소개 : 캐릭터 강화와 아이템을 사용하여 필드별 보스를 격파하는 챕터형식의 RPG
## 개발 목적 : RPG를 좋아하여 3D RPG 제작 기획
## 사용 엔진 : UNITY 2022.3.15f1
## 개발 기간 : 2023.10.31 ~ 2024.03.10
## 포트폴리오 빌드 파일 
-
## 유튜브 영상 링크
-
## 주요 활용 기술
- #01)(스크립트) Json 직렬화 사용한 데이터 저장 및 불러오기
<details>
<summary>적용 코드</summary>
  
```
      public void SaveData()
    {
        string data = JsonUtility.ToJson(nowPlayer); // JsonUtility.ToJson 메서드를 사용하여 JSON 포맷으로 직렬화(전환)
        File.WriteAllText(path + nowSlot.ToString(), data);
    }

    public void LoadData()
    {
        string data = File.ReadAllText(path + nowSlot.ToString());
        nowPlayer = JsonUtility.FromJson<PlayerData>(data); // JSON을 다시 오브젝트로 전환하려면 JsonUtility.FromJson을 사용
    }
```

</details>

***

- #02)(스크립트) 싱글톤 패턴 활용
<details>
<summary>적용 코드</summary>
  
```
    public static DataManager instance; // 싱글톤패턴

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
```

</details>

***

- #02)(스크립트) NavMeshAgent 활용한 자동 추적 및 복귀
<details>
<summary>적용 코드</summary>
  
```
        if (nav.enabled) // 자동으로 플레이어 추적하기
        {   
            if(Vector3.Distance(transform.position, Spawnposition.position) < 70 && !isDead) // 몬스터와 스폰장소길이가 50보다 작을때 까지 플레이어를 추적
            {
                nav.SetDestination(Target.position); // SetDestination : 도착할 목표 위치 정할 함수
                nav.isStopped = !isChase;
            }
        }

        // 몬스터와 플레이어 길이가 50보다 길거나 플레이어가 몬스터스폰장소의 길이차이가 70일때
        if (Vector3.Distance(transform.position, Target.position) > 50 && !isDead || Vector3.Distance(Target.position, Spawnposition.position) > 70)
        {
            nav.enabled = false;
            if (Vector3.Distance(transform.position, Spawnposition.position) > 5f)
            {
                // 목표 방향을 바라보는 함수 호출
                LookAtSmooth(Spawnposition.position, 0.1f);
            }
        }
        else
        {
            nav.enabled = true;
        }
```

</details>

***
