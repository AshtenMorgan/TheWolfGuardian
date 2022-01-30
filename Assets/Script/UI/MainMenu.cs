using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        //GameManager.Instance.player.transform.position = GameManager.Instance.playerSpawn.transform.position;
    }

    public void QuitGame ()
    {
        Debug.Log("QUITTING");
        Application.Quit();
    }
}
