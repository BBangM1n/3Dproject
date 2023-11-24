using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public Player player;
    public Boss boss;
    public GameObject itemShop;
    public GameObject weaponShop;
    public GameObject potionShop;
    public GameObject QuestShop;
    public GameObject startZone;
    public int stage;
    public float playTime;
    public bool isBattle;
    public int enemyCntA;
    public int enemyCntB;
    public int enemyCntC;
    public int enemyCntD;

    public bool isitembool1 = false;
    public bool isitembool2 = false;
    public bool isitembool3 = false;

    public Transform[] enemyZones;
    public GameObject[] enemies;
    public List<int> enemyList;

    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject overPanel;
    public GameObject pausePanel;
    public Text maxScoreText;
    public Text scoreText;
    public Text stageText;
    public Text playTimeText;
    public Text playerHealthText;
    public Text playerAmmoText;
    public Text playerCoinText;
    public Image weapon1Img;
    public Image weapon2Img;
    public Image weapon3Img;
    public Image weaponRImg;
    public Image potion1Img;
    public Image potion2Img;
    public Image potion3Img;
    public Text enemyAText;
    public Text enemyBText;
    public Text enemyCText;
    public Text QCoinText;
    public Text QAmmoText;
    public Text QScoreText;
    public RectTransform bossHealthGroup;
    public RectTransform bossHealthBar;
    public Text curScoreText;
    public Text bestScoreText;

    public Sprite[] potiomimage;
    public GameObject[] tutorials;
    

    private void Awake()
    {
        enemyList = new List<int>(); // 몬스터 갯수를 위한 리스트 선언
        maxScoreText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore")); // 컴퓨터에 저장된 맥스스코어 가져오기

        if (PlayerPrefs.HasKey("MaxScore")) // 만약 없다면 0
            PlayerPrefs.SetInt("MaxScore", 0);
    }

    public void GameStart() // 게임 스타트 버튼을 누를 시
    {
        menuCam.SetActive(false);
        pausePanel.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }

    public void TutorialStart() // 튜토 리얼 버튼을 누를 시
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
        StartCoroutine(turorial()); // 튜토리얼 코루틴 시작
    }

    IEnumerator turorial() // 튜토리얼 코루틴
    {
        // 일정 시간초마다 설정해둔 Ui들이 켜지고 꺼졌다 반복.
        yield return new WaitForSeconds(1f);
        tutorials[0].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[0].gameObject.SetActive(false);
        tutorials[1].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[1].gameObject.SetActive(false);
        tutorials[2].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[2].gameObject.SetActive(false);
        tutorials[3].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[3].gameObject.SetActive(false);
        tutorials[4].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[4].gameObject.SetActive(false);
        tutorials[5].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[5].gameObject.SetActive(false);
        tutorials[6].gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        tutorials[6].gameObject.SetActive(false);
        StopCoroutine(turorial());
    }

    public void GameOver() // 게임 오버가 되면
    {
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        curScoreText.text = scoreText.text;

        int maxScore = PlayerPrefs.GetInt("MaxScore"); // 맥스 스코어로 저장
        if(player.score > maxScore)
        {
            bestScoreText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", player.score);
        }
    }

    public void Restart() // 다시 시작버튼
    {
        SceneManager.LoadScene(0);
    }

    public void Pause() // 일시정지 버튼
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            pausePanel.SetActive(true);
            gamePanel.SetActive(false);
            menuPanel.SetActive(false);

            gameCam.SetActive(false);
            menuCam.SetActive(true);

            Time.timeScale = 0f;
        }
    }
    public void Exit() // Exit버튼
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isBattle) // 스테이지 진입시 플레이 타임 증가
            playTime += Time.deltaTime;

        QuestInfo();
        Pause();
    }

    private void LateUpdate()
    {
        //상단 UI
        scoreText.text = string.Format("{0:n0}", player.score);
        stageText.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        playTimeText.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        //플레이어 UI
        playerHealthText.text = player.health + " / " + player.maxhealth;
        playerCoinText.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
            playerAmmoText.text = "- / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoText.text = "- / " + player.ammo;
        else
            playerAmmoText.text = player.equipWeapon.curammo + " / " + player.ammo;

        //무기 UI
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponRImg.color = new Color(1, 1, 1, player.hasGrenade > 0 ? 1 : 0);

        //포션 UI
        potion1Img.color = new Color(1, 1, 1, isitembool1 ? 1 : 0);
        potion2Img.color = new Color(1, 1, 1, isitembool2 ? 1 : 0);
        potion3Img.color = new Color(1, 1, 1, isitembool3 ? 1 : 0);

        //몬스터 UI
        enemyAText.text = enemyCntA.ToString();
        enemyBText.text = enemyCntB.ToString();
        enemyCText.text = enemyCntC.ToString();

        if(boss != null)
        {
            bossHealthGroup.anchoredPosition = Vector3.down * 30;
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
        }
        else
        {
            bossHealthGroup.anchoredPosition = Vector3.up * 200;
        }

    }

    public void StageStart() // 스테이지 시작할 때
    {
        itemShop.SetActive(false); // 상점들 비활성화
        weaponShop.SetActive(false);
        potionShop.SetActive(false);
        startZone.SetActive(false);
        QuestShop.SetActive(false);

        foreach (Transform zone in enemyZones) // 적 스폰 존 활성화
            zone.gameObject.SetActive(true);

        isBattle = true; // 배틀 상태 시작
        StartCoroutine(InBattle()); // 배틀 코루틴 시작
    }

    public void StageEnd() // 스테이지 끝나면
    {
        player.transform.position = Vector3.up * 0.8f;

        itemShop.SetActive(true);  // 상점들 활성화
        weaponShop.SetActive(true);
        potionShop.SetActive(true);
        startZone.SetActive(true);
        QuestShop.SetActive(true);

        foreach (Transform zone in enemyZones) // 스폰 존 비활성화
            zone.gameObject.SetActive(false);

        isBattle = false; // 배틀 상태 꺼짐
        stage++;
    }

    IEnumerator InBattle() // 배틀 코루틴
    {
        if (stage % 5 == 0) // 5번째 스테이지마다 보스 소환
        {
            enemyCntD++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.Target = player.transform;
            enemy.manager = this;
            boss = instantEnemy.GetComponent<Boss>();
        }
        else // 아닐 시 초록, 보라, 노랑 공룡 소환
        {
            for (int index = 0; index < stage; index++)
            {
                int ran = Random.Range(0, 3);
                enemyList.Add(ran);

                switch (ran)
                {
                    case 0:
                        enemyCntA++;
                        break;
                    case 1:
                        enemyCntB++;
                        break;
                    case 2:
                        enemyCntC++;
                        break;
                }
            }

            while (enemyList.Count > 0) // 적들의 갯수 상황 0 이되면 빠져나감
            {
                int ranZone = Random.Range(0, 4);
                GameObject instantEnemy = Instantiate(enemies[enemyList[0]], enemyZones[ranZone].position, enemyZones[ranZone].rotation);
                Enemy enemy = instantEnemy.GetComponent<Enemy>();
                enemy.Target = player.transform;
                enemy.manager = this;
                enemyList.RemoveAt(0);
                yield return new WaitForSeconds(5f);
            }
        }

        while(enemyCntA + enemyCntB + enemyCntC + enemyCntD > 0)
        {
            yield return null;
        }

        yield return new WaitForSeconds(4f);
        boss = null;
        StageEnd(); // 몬스터를 다 잡았다면 스테이지 엔드
    }

    public void potioncontrol(int value) // 포션 컨트롤 함수
    {
        if (!isitembool1)
        {
            potion1Img.sprite = potiomimage[value]; // 포션이미지 배열과 아이템창 소지 판별
            isitembool1 = true;
            
        }
        else if (isitembool1 && !isitembool2)
        {
            potion2Img.sprite = potiomimage[value];
            isitembool2 = true;
        }
        else if (isitembool1 && isitembool2 && !isitembool3)
        {
            potion3Img.sprite = potiomimage[value];
            isitembool3 = true;
        }
    }

    void QuestInfo() // 퀘스트창에서의 플레이어 소지 및 현황판 표시 
    {
        QAmmoText.text = player.ammo.ToString();
        QCoinText.text = player.coin.ToString();
        QScoreText.text = player.score.ToString();
    }
}
