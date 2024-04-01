using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public Text[] slotText;		// ���Թ�ư �Ʒ��� �����ϴ� Text��

    bool[] savefile = new bool[3];	// ���̺����� �������� ����

    public GameObject MessagePanel;

    public int BtnValue;



    void Start()
    {
        // ���Ժ��� ����� �����Ͱ� �����ϴ��� �Ǵ�.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// �����Ͱ� �ִ� ���
            {
                savefile[i] = true;			// �ش� ���� ��ȣ�� bool�迭 true�� ��ȯ
                DataManager.instance.nowSlot = i;	// ������ ���� ��ȣ ����
                DataManager.instance.LoadData();	// �ش� ���� ������ �ҷ���
                int hour = (int)(DataManager.instance.nowPlayer.PlayTime / 3600);
                int min = (int)((DataManager.instance.nowPlayer.PlayTime - hour * 3600) / 60);
                int sec = (int)(DataManager.instance.nowPlayer.PlayTime % 60);
                slotText[i].text = "�÷��� Ÿ�� - " + string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);
            }
            else	// �����Ͱ� ���� ���
            {
                slotText[i].text = "�������";
            }
        }
        // �ҷ��� �����͸� �ʱ�ȭ��Ŵ.(��ư�� �г����� ǥ���ϱ������̾��� ����)
        DataManager.instance.DataClear();
    }

    public void Slot(int number)	// ������ ��� ����
    {
        DataManager.instance.nowSlot = number;	// ������ ��ȣ�� ���Թ�ȣ�� �Է���.

        if (savefile[number])	// bool �迭���� ���� ���Թ�ȣ�� true��� = ������ �����Ѵٴ� ��
        {
            DataManager.instance.LoadData();	// �����͸� �ε��ϰ�
            GoGame();	// ���Ӿ����� �̵�
        }
        else	// bool �迭���� ���� ���Թ�ȣ�� false��� �����Ͱ� ���ٴ� ��
        {
            GoGame();
        }
    }


    public void GoGame()	// ���Ӿ����� �̵�
    {
        if (!savefile[DataManager.instance.nowSlot])	// ���� ���Թ�ȣ�� �����Ͱ� ���ٸ�
        {
            DataManager.instance.Tutorial = true;
            DataManager.instance.SaveData(); // ���� ������ ������.
        }
        else
        {
            DataManager.instance.Tutorial = false;
        }
        SceneManager.LoadScene("GameScene"); // ���Ӿ����� �̵�
    }

    public void DeleteBtnClick(int value) // ���� ��ư Ŭ��
    {
        MessagePanel.SetActive(true);
        BtnValue = value;
    }
    public void DeleteData() // ���� ��� �Լ�
    {
        File.Delete(DataManager.instance.path + BtnValue);
        slotText[BtnValue].text = "�������";
        savefile[BtnValue] = false;
    }
}

