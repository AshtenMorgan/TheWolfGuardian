using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    #region Variables
    public static UIManager Instance { get; private set; }//allow other classes to access UI
    bool loaded = false;
    #region GM
    private GameManager gm;
    #endregion

    #region Audio
    [SerializeField, Tooltip("Main audio control for everything")]
    private AudioMixer mixer;
    [SerializeField, Tooltip("The slider value vs decibel volume curve")]
    private AnimationCurve volumeVsDecibels;
    public AudioSource musicSource;
    public AudioSource fxSource;
    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip menu;
    public AudioClip scene1;
    #endregion

    #region HUD Icons
    [SerializeField]
    private Image unusedIcon;
    #endregion

    #region Lives
    [SerializeField]
    private TMPro.TMP_Text lifeText;
    #endregion

    #region Canvases
    [Header("Prefab Canvas")]
    public Canvas mainMenu;
    public Canvas pauseMenu;
    public Canvas gameOverMenu;
    public Canvas optionsMenu;
    public Canvas BackgroundImage;
    public Canvas HUD;
    public string cameFrom;
    public bool isPaused = false;
    #endregion

    #region Canvas Components
    [Header("Elements")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider effectsVolumeSlider;
    public Toggle fullScreenToggle;
    public TMPro.TMP_Dropdown resolutionDropDown;
    public TMPro.TMP_Dropdown qualityDropDown;
    public Slider healthSlider;
    public Resolution[] resolutions;
    #endregion


    #endregion
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

        gm = GameManager.Instance;//reference to game manager
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //only load on main menu
        if (scene.name == "MainMenu")
        {
            //and only the first time
            if (loaded == false)
            {
                //setup components (Not scene specific)
                //runs before they are disabled
                masterVolumeSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
                musicVolumeSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
                healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
                effectsVolumeSlider = GameObject.FindGameObjectWithTag("EffectsSlider").GetComponent<Slider>();
                fullScreenToggle = GameObject.FindGameObjectWithTag("FSToggle").GetComponent<Toggle>();
                resolutionDropDown = GameObject.FindGameObjectWithTag("ResolutionDrop").GetComponent<TMPro.TMP_Dropdown>();
                qualityDropDown = GameObject.FindGameObjectWithTag("QualityDrop").GetComponent<TMPro.TMP_Dropdown>();
                fxSource = GameObject.FindGameObjectWithTag("MasterCanvas").GetComponent<AudioSource>();
                //canvases
                mainMenu = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<Canvas>();
                optionsMenu = GameObject.FindGameObjectWithTag("OptionsCanvas").GetComponent<Canvas>();
                BackgroundImage = GameObject.FindGameObjectWithTag("BackgroundCanvas").GetComponent<Canvas>();
                pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
                gameOverMenu = GameObject.FindGameObjectWithTag("GameOverCanvas").GetComponent<Canvas>();
                optionsMenu = GameObject.FindGameObjectWithTag("OptionsCanvas").GetComponent<Canvas>();
                HUD = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Canvas>();

                




                //hide canvas
                DisableOptions();
                DisablePauseMenu();
                DisableGameOver();
                DisableHUD();

                loaded = true;
            }
            musicSource = Camera.main.GetComponent<AudioSource>();
            musicSource.clip = menu;//load main menu audio
            musicSource.Play(0);//play aiduo
        }
        else//we are not in main menu
        {
            musicSource = Camera.main.GetComponent<AudioSource>();
            musicSource.clip = scene1;
            musicSource.Play(0);
        }



    }

    // Start is called before the first frame update
    private void Start()
    {
        gm = GameManager.Instance;//reference game manager

        //set up screen resolutions for dropdown
        resolutions = Screen.resolutions;//get resolution array

        //build dropdown for screen resolutions and quality levels
        List<string> options = new List<string>();//create list to hold resolutions

        resolutionDropDown.ClearOptions();//clear anything that might be there

        for (int index = 0; index < resolutions.Length; index++)//loop through all possible resolutions system can use
        {
            options.Add(string.Format("{0} x {1}", resolutions[index].width, resolutions[index].height));//add each to the list
        }

        resolutionDropDown.AddOptions(options);//add list to the dropdown

        // Build quality levels
        qualityDropDown.ClearOptions();//clear anything that might already exist
        qualityDropDown.AddOptions(QualitySettings.names.ToList());//add the quality levels to the dropdown

        //set values to match what will be seen on options screen
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);//get palyerprefs value, if it doesnt exist, default to .5
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        resolutionDropDown.value = PlayerPrefs.GetInt("ResIndex");

    }

    // Update is called once per frame
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused && !gm.gameOver)
            {

                ResumeGame();

            }
            else
            {
                PauseGame();
            }
        }
        if (SceneManager.GetActiveScene().name == ("AnimationTestScene"))//make sure we are in scene "Main"
        {
            //lifeText.text = string.Format("X {0}", gm.lives);//update lives text

            /* Update HUD icons
            */
            if (gm)//we have an active game manager
            {
                healthSlider.value = gm.percent;//update health bar
            }



        }

    }
    //show pause menu
    public void EnablePauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
    }
    //hide pause menu
    public void DisablePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
    }
    //show game over
    public void EnableGameOverMenu()
    {
        gameOverMenu.gameObject.SetActive(true);
    }
    //exit game
    public void QuitGame()
    {
        Application.Quit();//quit game
                           //#if is preprossor code, if unity editor is running, this statement is true
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//stop editor
#endif
    }
    //start the game
    public void StartGame()
    {
        if (SceneManager.GetActiveScene().name == ("MainMenu"))
        {
            SceneManager.LoadScene("AnimationTestScene");
        }
        else
        {
            
            /*
            gm.player.gameObject.SetActive(false);
            Time.timeScale = 1.0f;//restart time
            DisableBackground();
            DisableMain();
            */
        }
               
        

    }

    //resume from game over screen


    #region Menu Controls
    #region Options
    //show settings menue
    public void EnableOptions()
    {
        
        optionsMenu.gameObject.SetActive(true);

        /*
        //set volume sliders to whatever they are in player prefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", masterVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", musicVolumeSlider.value);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", effectsVolumeSlider.value);
        */

        //add this to playerprefs as well
        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state

    }
    public void EnableOptionsPause()
    {
        SceneManager.LoadScene("MainMenu");
        /*
        cameFrom = "Pause";
        optionsMenu.gameObject.SetActive(true);

        //set volume sliders to whatever they are in player prefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.value);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Effects Volume", effectsVolumeSlider.value);

        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state
        */
    }
    public void EnableOptionsGameOver()
    {
        cameFrom = "GameOver";
        optionsMenu.gameObject.SetActive(true);

        //set volume sliders to whatever they are in player prefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.value);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Effects Volume", effectsVolumeSlider.value);

        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state

    }

    //hides settings menu
    public void DisableOptions()
    {
        optionsMenu.gameObject.SetActive(false);
    }

    public void OptionsBack()
    {
        EnableMain();
    }
    #endregion
    #region Main Menu
    public void BackToMain()
    {
        //maybe just pause and show main menu
        //SceneManager.LoadScene("MainMenu");
        EnableMain();
        EnableBackground();
        DisablePauseMenu();
        DisableHUD();
        DisableOptions();
        DisableGameOver(); 
    }
    public void EnableMain()
    {
        mainMenu.gameObject.SetActive(true);
    }
    public void DisableMain()
    {
        mainMenu.gameObject.SetActive(false);
    }
    #endregion
    #region GameOver
    public void GameOverResume()
    {
        gameOverMenu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;//start time
        gm.Player.Lives = 4;//give some lives back
        gm.lives = 4;//match gm lives
        gm.gameOver = false;//not game over any more
    }
    public void DisableGameOver()
    {
        gameOverMenu.gameObject.SetActive(false);
    }

    #endregion
    #region BackgroundImage
    public void EnableBackground()
    {
        BackgroundImage.gameObject.SetActive(true);
    }
    public void DisableBackground()
    {
        BackgroundImage.gameObject.SetActive(false);
    }
    #endregion
    #region HUD
    public void EnableHUD()
    {
        HUD.gameObject.SetActive(true);
    }
    public void DisableHUD()
    {
        HUD.gameObject.SetActive(false);
    }

    #endregion
    #endregion
    #region Settings
    //apply settings
    //be sure to use dynamic functions in inspector
    #region Quality
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);//pass the dropdown selection to qualitylevel
        PlayerPrefs.SetFloat("QualityLevel", index);//save in playerprefs
        PlayerPrefs.Save();//save playerprefs
    }
    #endregion

    #region Screen
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];//pass index to resolution array
        Screen.SetResolution(resolution.width, resolution.height, fullScreenToggle);//set resolution and fullscreen
        PlayerPrefs.SetInt("ResIndex", index);
        PlayerPrefs.Save();//save playerprefs
    }
    public void ScreenToggle(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    #endregion
    #region Volume Bars
    public void BarUpdate()
    {
        //this works better than using an animation curve, min value must be above 0
        mixer.SetFloat("masterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);
        mixer.SetFloat("musicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
        mixer.SetFloat("effectsVolume", Mathf.Log10(effectsVolumeSlider.value) * 20);

        //save playerprefs
        //set volume sliders to whatever they are in player prefs, if non existant, set to max value
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
        PlayerPrefs.Save();//save playerprefs
        
    }
    #endregion
    #endregion
    #region Pause
    public void PauseGame()
    {
        EnablePauseMenu();
        Time.timeScale = 0.0f;//stop time
        isPaused = true;
    }
    public void ResumeGame()
    {
        DisablePauseMenu();
        DisableOptions();
        Time.timeScale = 1.0f;//resume time
        isPaused = false;
    }
    #endregion
    public void ButtonHighlight()
    {
        fxSource.PlayOneShot(buttonHover);
    }
    public void ButtonClick()
    {
        fxSource.PlayOneShot(buttonClick);
    }
}