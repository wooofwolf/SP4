using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public string GameScene;
    public void LoadGame()
    {
        SceneManager.LoadScene(GameScene);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
