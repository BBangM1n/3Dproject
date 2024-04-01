using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class TitleManager : MonoBehaviour
{
    public Text[] slotText;		// 슬롯버튼 아래에 존재하는 Text들

    bool[] savefile = new bool[3];	// 세이브파일 존재유무 저장

    public GameObject MessagePanel;

    public int BtnValue;



    void Start()
    {
        // 슬롯별로 저장된 데이터가 존재하는지 판단.
        for (int i = 0; i < 3; i++)
        {
            if (File.Exists(DataManager.instance.path + $"{i}"))	// 데이터가 있는 경우
            {
                savefile[i] = true;			// 해당 슬롯 번호의 bool배열 true로 변환
                DataManager.instance.nowSlot = i;	// 선택한 슬롯 번호 저장
                DataManager.instance.LoadData();	// 해당 슬롯 데이터 불러옴
                int hour = (int)(DataManager.instance.nowPlayer.PlayTime / 3600);
                int min = (int)((DataManager.instance.nowPlayer.PlayTime - hour * 3600) / 60);
                int sec = (int)(DataManager.instance.nowPlayer.PlayTime % 60);
                slotText[i].text = "플레이 타임 - " + string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", min) + ":" + string.Format("{0:00}", sec);
            }
            else	// 데이터가 없는 경우
            {
                slotText[i].text = "비어있음";
            }
        }
        // 불러온 데이터를 초기화시킴.(버튼에 닉네임을 표현하기위함이었기 때문)
        DataManager.instance.DataClear();
    }

    public void Slot(int number)	// 슬롯의 기능 구현
    {
        DataManager.instance.nowSlot = number;	// 슬롯의 번호를 슬롯번호로 입력함.

        if (savefile[number])	// bool 배열에서 현재 슬롯번호가 true라면 = 데이터 존재한다는 뜻
        {
            DataManager.instance.LoadData();	// 데이터를 로드하고
            GoGame();	// 게임씬으로 이동
        }
        else	// bool 배열에서 현재 슬롯번호가 false라면 데이터가 없다는 뜻
        {
            GoGame();
        }
    }


    public void GoGame()	// 게임씬으로 이동
    {
        if (!savefile[DataManager.instance.nowSlot])	// 현재 슬롯번호의 데이터가 없다면
        {
            DataManager.instance.Tutorial = true;
            DataManager.instance.SaveData(); // 현재 정보를 저장함.
        }
        else
        {
            DataManager.instance.Tutorial = false;
        }
        SceneManager.LoadScene("GameScene"); // 게임씬으로 이동
    }

    public void DeleteBtnClick(int value) // 삭제 버튼 클릭
    {
        MessagePanel.SetActive(true);
        BtnValue = value;
    }
    public void DeleteData() // 삭제 기능 함수
    {
        File.Delete(DataManager.instance.path + BtnValue);
        slotText[BtnValue].text = "비어있음";
        savefile[BtnValue] = false;
    }
}

