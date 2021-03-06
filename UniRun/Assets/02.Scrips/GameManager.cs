using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 게임오버 상태를 표현하고, 게임 점수와 UI를 관리하는 매니저
// 씬에는 단 하나의 게임 매니저만 존재할수 있다
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글턴을 할당항 전역 변수

    public bool isGameover = false; // 게임 오버 상태

    public Text scoreText; // 점수를 출력할 UI 텍스트

    public GameObject gameoverUi; // 게임오버시 활성화할 UI 오브젝트

    private int score = 0; //게임 점수

    public GameObject menuPanel; //메뉴 페널

    public Text lifeText;

    public Text hpText;

    public int hpCount = 2;

    private int life = 2; //라이프

    private void Awake()
    // 보이스 스타트 보다 먼저 실행
    // 게임 시작과 동시에 싱글턴을 구성
    {
        
        // 싱글턴 변수 instance가 비어 있나요?
        if (instance == null)
        {
            //instance 가 비어 있다면 그곳에 내 자신을 할당
            instance = this; //this는 내 자신을 가르킨
        }

        else
        {
            // instance에 이미 다른 GameManager 오브젝트가 할당되어 있다면

            // 씬에 두 개 이상의 GameManager 오브젝트가 존재한다는 의미
            // 싱글턴 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.Log("씬에 두 개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //                    ToString으로 형변환
        hpText.text = ": "+hpCount.ToString();  
    }

    // 게임 오버 상태에서 게임을 재시작 할수 있게 하는 처리
    void Update()
    {
        // 게임 오버 상태에서 사용자가 마우스 왼쪽 버튼(점프 버튼)을 클릭 한다면
        if(isGameover&&Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //현재 활성화 되어 있는 씬의 이름을 가져온다
            //↑ 같음 SceneManager.LoadScene(0);
        }

    }

    
        



    //점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        //게임오버가 아니라면
        
        //if (isGameover) return;
        //score += newScore;
        //scoreText.text = "Score : " + score;
        
        //똑같음
        if(!isGameover)
        {
            //점수를 증가
            score += newScore;
            scoreText.text = "Score : " + score;
        }
    }

    public void PlayerLife(int newlife)
    {
        if (!isGameover)
        {
            hpCount += newlife;
            hpText.text = ": " + hpCount;
        }
    }


    // 플레이어 캐릭터가 사망 시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        // 현재 상태를 게임 오버 상태로 변경
        isGameover = true;
        // 게임오버 UI를 활성화
        gameoverUi.SetActive(true);
    }

    public void OnMenu()
    {

         menuPanel.SetActive(true);

        Time.timeScale = 0f;

        

    }

    public void OffMenu()
    {
        menuPanel.SetActive(false);

        Time.timeScale = 1f;
    }

  
    public void Exit()
    {
        Application.Quit();
    }

    public void Restart()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;

    }
    ////선생님의 경우(하는법)
    //퍼블릭 불 사용시 아래처럼 표시
    public bool Crash()
    {
        hpCount--;
        hpText.text = hpCount.ToString();
        //= hpText.text = ""+ --hpCount;
        ////                      Tostring 으로 형변황 안됨
        if (hpCount <= 0) return true;
        //리턴이 조건문(if)안에 있으면 안됨
        return false;
        //반대값(트루면 펄스) 이 있어야 한다
    }


    ////간략화
    ////                           bool isActive 해놓으면 선택지가 생김 
    //public void MenuPanelControl(bool isActive)
    //{
    //    menuPanel.SetActive(isActive);
    //}

    //// 클릭시 버튼(글자를) 보낼수 있는 방법
    //public void UiContol(string type)
    //{
    //    switch (type)
    //    {
    //        case "menuon":
    //            break;
    //            menuPanel.SetActive(true);

    //            Time.timeScale = 0f;
    //        case "menuoff":
    //            break;
    //            menuPanel.SetActive(false);

    //            Time.timeScale = 1f;

    //        case "restart":
    //            break;
    //            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //            Time.timeScale = 1f;
    //        case "exit":
    //            break;
    //            Application.Quit();
    //    }
    //}

}
