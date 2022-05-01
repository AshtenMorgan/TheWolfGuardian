using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HUD : MenuSelector
{
    [SerializeField]
    private GameManager gameManager;
    public Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        SelectMenu("HUD");
    }
    private void Update()
    {
 
    }
    public void ReturnToMain()
    {
        Time.timeScale = 0;
        gameManager.lives = 3;
        gameManager.ResetSpawn();
        SelectMenu("MainMenuScreen");
    }
    public void HUDPlay()
    {
        Time.timeScale = 1;
        gameManager.SpawnPlayer();
        SelectMenu("HUD");
    }
}
