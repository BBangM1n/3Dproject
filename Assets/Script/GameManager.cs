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
        enemyList = new List<int>(); // ���� ������ ���� ����Ʈ ����
        maxScoreText.text = string.Format("{0:n0}", PlayerPrefs.GetInt("MaxScore")); // ��ǻ�Ϳ� ����� �ƽ����ھ� ��������

        if (PlayerPrefs.HasKey("MaxScore")) // ���� ���ٸ� 0
            PlayerPrefs.SetInt("MaxScore", 0);
    }

    public void GameStart() // ���� ��ŸƮ ��ư�� ���� ��
    {
        menuCam.SetActive(false);
        pausePanel.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }

    public void TutorialStart() // Ʃ�� ���� ��ư�� ���� ��
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

        player.gameObject.SetActive(true);
        StartCoroutine(turorial()); // Ʃ�丮�� �ڷ�ƾ ����
    }

    IEnumerator turorial() // Ʃ�丮�� �ڷ�ƾ
    {
        // ���� �ð��ʸ��� �����ص� Ui���� ������ ������ �ݺ�.
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

    public void GameOver() // ���� ������ �Ǹ�
    {
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        curScoreText.text = scoreText.text;

        int maxScore = PlayerPrefs.GetInt("MaxScore"); // �ƽ� ���ھ�� ����
        if(player.score > maxScore)
        {
            bestScoreText.gameObject.SetActive(true);
            PlayerPrefs.SetInt("MaxScore", player.score);
        }
    }

    public void Restart() // �ٽ� ���۹�ư
    {
        SceneManager.LoadScene(0);
    }

    public void Pause() // �Ͻ����� ��ư
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
    public void Exit() // Exit��ư
    {
        Application.Quit();
    }

    private void Update()
    {
        if (isBattle) // �������� ���Խ� �÷��� Ÿ�� ����
            playTime += Time.deltaTime;

        QuestInfo();
        Pause();
    }

    private void LateUpdate()
    {
        //��� UI
        scoreText.text = string.Format("{0:n0}", player.score);
        stageText.text = "STAGE " + stage;

        int hour = (int)(playTime / 3600);
        int min = (int)((playTime - hour * 3600) / 60);
        int sec = (int)(playTime % 60);
        playTimeText.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);

        //�÷��̾� UI
        playerHealthText.text = player.health + " / " + player.maxhealth;
        playerCoinText.text = string.Format("{0:n0}", player.coin);
        if (player.equipWeapon == null)
            playerAmmoText.text = "- / " + player.ammo;
        else if (player.equipWeapon.type == Weapon.Type.Melee)
            playerAmmoText.text = "- / " + player.ammo;
        else
            playerAmmoText.text = player.equipWeapon.curammo + " / " + player.ammo;

        //���� UI
        weapon1Img.color = new Color(1, 1, 1, player.hasWeapons[0] ? 1 : 0);
        weapon2Img.color = new Color(1, 1, 1, player.hasWeapons[1] ? 1 : 0);
        weapon3Img.color = new Color(1, 1, 1, player.hasWeapons[2] ? 1 : 0);
        weaponRImg.color = new Color(1, 1, 1, player.hasGrenade > 0 ? 1 : 0);

        //���� UI
        potion1Img.color = new Color(1, 1, 1, isitembool1 ? 1 : 0);
        potion2Img.color = new Color(1, 1, 1, isitembool2 ? 1 : 0);
        potion3Img.color = new Color(1, 1, 1, isitembool3 ? 1 : 0);

        //���� UI
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

    public void StageStart() // �������� ������ ��
    {
        itemShop.SetActive(false); // ������ ��Ȱ��ȭ
        weaponShop.SetActive(false);
        potionShop.SetActive(false);
        startZone.SetActive(false);
        QuestShop.SetActive(false);

        foreach (Transform zone in enemyZones) // �� ���� �� Ȱ��ȭ
            zone.gameObject.SetActive(true);

        isBattle = true; // ��Ʋ ���� ����
        StartCoroutine(InBattle()); // ��Ʋ �ڷ�ƾ ����
    }

    public void StageEnd() // �������� ������
    {
        player.transform.position = Vector3.up * 0.8f;

        itemShop.SetActive(true);  // ������ Ȱ��ȭ
        weaponShop.SetActive(true);
        potionShop.SetActive(true);
        startZone.SetActive(true);
        QuestShop.SetActive(true);

        foreach (Transform zone in enemyZones) // ���� �� ��Ȱ��ȭ
            zone.gameObject.SetActive(false);

        isBattle = false; // ��Ʋ ���� ����
        stage++;
    }

    IEnumerator InBattle() // ��Ʋ �ڷ�ƾ
    {
        if (stage % 5 == 0) // 5��° ������������ ���� ��ȯ
        {
            enemyCntD++;
            GameObject instantEnemy = Instantiate(enemies[3], enemyZones[0].position, enemyZones[0].rotation);
            Enemy enemy = instantEnemy.GetComponent<Enemy>();
            enemy.Target = player.transform;
            enemy.manager = this;
            boss = instantEnemy.GetComponent<Boss>();
        }
        else // �ƴ� �� �ʷ�, ����, ��� ���� ��ȯ
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

            while (enemyList.Count > 0) // ������ ���� ��Ȳ 0 �̵Ǹ� ��������
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
        StageEnd(); // ���͸� �� ��Ҵٸ� �������� ����
    }

    public void potioncontrol(int value) // ���� ��Ʈ�� �Լ�
    {
        if (!isitembool1)
        {
            potion1Img.sprite = potiomimage[value]; // �����̹��� �迭�� ������â ���� �Ǻ�
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

    void QuestInfo() // ����Ʈâ������ �÷��̾� ���� �� ��Ȳ�� ǥ�� 
    {
        QAmmoText.text = player.ammo.ToString();
        QCoinText.text = player.coin.ToString();
        QScoreText.text = player.score.ToString();
    }
}
