using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void LoadScene(int scene)
    {
        switch(scene)
        {
            case 1: 
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;
            case 2: 
                SceneManager.LoadScene("MainMenu");
                break;
            case 3:
                ExitGame();
                break;
            case 4:
                SceneManager.LoadScene("TutorialScene");
                break;
        }

    }
    public void LoadGame()
    {
        SceneManager.LoadScene("CollectibleTestScene");
    }
    public void ExitGame()
    {
        //Application.Quit();
        SceneManager.LoadScene("MainMenu");
    }
}
