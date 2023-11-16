using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene(GameParameters.SceneName.GAME);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
