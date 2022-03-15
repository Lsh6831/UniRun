using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool isPause = false;

    public void IsPause()
    {
        isPause = !isPause;
        if (isPause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
    public void OnClickNewGame()
    {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
          
    }

    public void OnclickQuit()
    {
        Application.Quit();
    }
     

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
