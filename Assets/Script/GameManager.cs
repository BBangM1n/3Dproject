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
    public Text stageNameText;
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

    public Text QCoinText;
    public Text QAmmoText;
    public Text QScoreText;
    public RectTransform bossHealthGroup;
    public RectTransform bossHealthBar;
    public Text curScoreText;
    public Text RespawnText;
    public Button RestartBtn;


    public Sprite[] potiomimage;

    public GameObject tutorialsPanel;
    public Sprite[] TutorialImageGroup;
    public string[] TutorialTextGroup;
    public Image TutorialImage;
    public Text TutorialText;
    public int TutorialCount = 0;
    public Button EndBtn;

    //���� �����¸��
    public GameObject BossComing;
    public Text bosscomingText;
    public Image bosscomingImage;
    public int BossCounting;
    public bool isBossbattle;

    public int Clearpoint = 0;
    public GameObject NextWolrdZone;


    private void Awake()
    {
        bosscomingText = bosscomingText.GetComponent<Text>();
        bosscomingImage = bosscomingImage.GetComponent<Image>();

        enemyList = new List<int>(); // ���� ������ ���� ����Ʈ ����
 
    }

    private void Start()
    {
        playTime = DataManager.instance.nowPlayer.PlayTime;
        TutorialCount = 0;
        if (DataManager.instance.Tutorial == true)
        {
            menuCam.SetActive(true);
            menuPanel.SetActive(true);
        }
        else
        {
            gameCam.SetActive(true);
            gamePanel.SetActive(true);
        }
    }

    private void Update()
    {
        playTime += Time.deltaTime;
        DataManager.instance.nowPlayer.PlayTime = playTime;

        QuestInfo();
        WorldClear();

        if (tutorialsPanel.activeSelf == true)
        {
            TutorialImage.sprite = TutorialImageGroup[TutorialCount];
            TutorialText.text = TutorialTextGroup[TutorialCount];

            if (TutorialImageGroup.Length == TutorialCount + 1)
            {
                EndBtn.gameObject.SetActive(true);
            }
            else
                EndBtn.gameObject.SetActive(false);

            Debug.Log(TutorialImageGroup.Length);
        }
        else
            return;
    }

    public void WorldClear()
    {
        if(Clearpoint == 1)
        {
            NextWolrdZone.SetActive(true);
        }
    }

    private void LateUpdate()
    {
        //��� UI

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

        if (boss != null)
        {   
            bossHealthGroup.anchoredPosition = Vector3.down * 30;
            bossHealthBar.localScale = new Vector3((float)boss.curHealth / boss.maxHealth, 1, 1);
        }
        else
        {
            bossHealthGroup.anchoredPosition = Vector3.up * 200;
        }

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

        //Ʃ�丮�� �ǳ� Ȱ��ȭ
        tutorialsPanel.SetActive(true);
        SoundManager.instance.SoundChange(0);
        DataManager.instance.Tutorial = false;
        player.isstop = true; 

    }

    public void TutorialLeftBtnClick()
    {
        if(TutorialCount > 0)
            TutorialCount--;
    }

    public void TutorialRightBtnClick()
    {
        if(TutorialImageGroup.Length > TutorialCount + 1)
            TutorialCount++;
    }

    public void TutorialEndBtnClick()
    {
        tutorialsPanel.SetActive(false);
        player.isstop = false;
    }

    public void GameOver() // ���� ������ �Ǹ�
    {
        gamePanel.SetActive(false);
        overPanel.SetActive(true);
        RestartBtn.interactable = false;
        StartCoroutine(RespawnTexton());

    }

    IEnumerator RespawnTexton()
    {
        yield return new WaitForSeconds(0.3f);
        RespawnText.text = "5�� �� ��Ȱ ����...";
        yield return new WaitForSeconds(1f);
        RespawnText.text = "4�� �� ��Ȱ ����...";
        yield return new WaitForSeconds(1f);
        RespawnText.text = "3�� �� ��Ȱ ����...";
        yield return new WaitForSeconds(1f);
        RespawnText.text = "2�� �� ��Ȱ ����...";
        yield return new WaitForSeconds(1f);
        RespawnText.text = "1�� �� ��Ȱ ����...";
        yield return new WaitForSeconds(1f);
        RespawnText.text = "��Ȱ ����!";
        RestartBtn.interactable = true;

        StopCoroutine(RespawnTexton());
    }

    public void Restart() // �ٽ� ���۹�ư
    {
        player.anim.SetBool("Respawn", true);
        gamePanel.SetActive(true);
        overPanel.SetActive(false);
        if (isBossbattle)
            player.transform.position = new Vector3(227, 1, -35);
        else
            player.transform.position = new Vector3(0, 1, 0);
        player.isDead = false;
        player.health = 100;
        player.coin = player.coin / 2;
        StartCoroutine(Resetanim());
    }

    IEnumerator Resetanim()
    {
        yield return new WaitForSeconds(1f);
        player.anim.SetBool("Respawn", false);
        StopCoroutine(Resetanim());
    }

    public void Pause() // �Ͻ����� ��ư
    {
            pausePanel.SetActive(true);
            gamePanel.SetActive(false);
            menuPanel.SetActive(false);

            gameCam.SetActive(false);
            menuCam.SetActive(true);
    }
    public void Exit() // Exit��ư
    {
        DataManager.instance.SaveData();
        Application.Quit();
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

    public IEnumerator BossCreateText() // UI ���� ���� ������ȿ��
    {
        while (bosscomingText.color.a > 0) // ���� �� ����
        {
            bosscomingText.color = new Color(bosscomingText.color.r, bosscomingText.color.g, bosscomingText.color.b, bosscomingText.color.a - (Time.deltaTime * 1f));
            bosscomingImage.color = new Color(bosscomingImage.color.r, bosscomingImage.color.g, bosscomingImage.color.b, bosscomingImage.color.a - (Time.deltaTime * 1f));
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        // �ؽ�Ʈ�� ������ ��Ÿ������ ��
        while (bosscomingText.color.a < 1) // ���� �� �ٽ� ����
        {
            bosscomingText.color = new Color(bosscomingText.color.r, bosscomingText.color.g, bosscomingText.color.b, bosscomingText.color.a + (Time.deltaTime * 1f));
            bosscomingImage.color = new Color(bosscomingImage.color.r, bosscomingImage.color.g, bosscomingImage.color.b, bosscomingImage.color.a + (Time.deltaTime * 1f));
            yield return null;
        }

        // ���
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(BossCreateText());
    }
}