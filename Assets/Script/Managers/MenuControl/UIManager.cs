using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    /*
    #region Variables
    public static UIManager Instance { get; private set; }//allow other classes to access UI

    #region HUD Icons
    [SerializeField]
    private Image unusedIcon;
    #endregion

    #region Lives
    [SerializeField]
    private TMPro.TMP_Text lifeText;
    #endregion
    public bool isPaused = false;
    #endregion



    private int cameFrom;
    private Scene currentScene;
    private bool loaded = false;
    private string mainMenuName = "MainMenu"; //name of the main menu scene as a string

    private void OnEnable()
    {
        //check what scene and run scenloaded functions.  This should load every time a scene change occurs
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void Awake()
    {
        //make sure there is always only 1 instance
        if (UIManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene;
        //only load on main menu
        if (scene.name == mainMenuName)
        {
            if (loaded == false)
            {
                SetUpOptions();
                //hide canvas
                DisableOptions();
                DisablePauseMenu();
                DisableGameOver();
                DisableHUD();

                //show canvas
                EnableMain();
                EnableBackground();

                musicSource = Camera.main.GetComponent<AudioSource>();
                musicSource.clip = menu;//load main menu audio
                musicSource.Play(0);//play aiduo

                loaded = true;
            }
        }
        else//we are not in main menu
        {

            musicSource = Camera.main.GetComponent<AudioSource>();
            musicSource.clip = scene1;
            musicSource.Play(0);
            //SetUpOptions();
        }

    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentScene.name != mainMenuName)
            {
                if (isPaused && !GameManager.Instance.gameOver)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
        if (SceneManager.GetActiveScene().name != (mainMenuName))//make sure we are not in main menu
        {
            //these should be moved out of update when possible
            if (GameManager.Instance)//we have an active game manager
            {
                //Update HUD icons
                //lifeText.text = string.Format("X {0}", gm.lives);//update lives text
                //health bar should be updated on change instead of in update
                healthSlider.value = GameManager.Instance.percent;//update health bar
            }
        }

    }
    

    
    
    }
    
    #region Menu Controls
   
    public void GameOverResume()
    {
        gameOverMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;//start time
        GameManager.Instance.Player.Lives = 4;//give some lives back
        GameManager.Instance.lives = 4;//match gm lives
        GameManager.Instance.ResetSpawn();
        GameManager.Instance.SpawnPlayer();
        GameManager.Instance.gameOver = false;//not game over any more

  
   

    #endregion
    
    
    #endregion
    
    */
}