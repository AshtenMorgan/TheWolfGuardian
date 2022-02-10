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
    public AudioClip buttonClick;
    public AudioClip buttonHover;
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
    public AudioSource audioSource;
    public Slider healthSlider;
    public Resolution[] resolutions;
    #endregion


    #endregion
    private void OnEnable()
    {
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
                audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
                //canvases
                mainMenu = GameObject.FindGameObjectWithTag("MainMenuCanvas").GetComponent<Canvas>();
                optionsMenu = GameObject.FindGameObjectWithTag("OptionsCanvas").GetComponent<Canvas>();
                BackgroundImage = GameObject.FindGameObjectWithTag("BackgroundCanvas").GetComponent<Canvas>();
                pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
                gameOverMenu = GameObject.FindGameObjectWithTag("GameOverCanvas").GetComponent<Canvas>();
                optionsMenu = GameObject.FindGameObjectWithTag("OptionsCanvas").GetComponent<Canvas>();
                HUD = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Canvas>();

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




                //hide canvas
                DisableOptions();
                DisablePauseMenu();
                DisableGameOver();
                DisableHUD();

                loaded = true;
            }
        }




    }

    // Start is called before the first frame update
    private void Start()
    {
        gm = GameManager.Instance;
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

    public void OnMouseOver()
    {
        audioSource.PlayOneShot(buttonHover);//play sound when a button is hovered
    }
    public void ButtonClick()
    {
        audioSource.PlayOneShot(buttonClick);//play click sound
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
        SceneManager.LoadScene("AnimationTestScene");
    }

    //resume from game over screen


    #region Menu Controls
    #region Options
    //show settings menue
    public void EnableOptionsMain()
    {
        cameFrom = "Main";
        optionsMenu.gameObject.SetActive(true);

        //set volume sliders to whatever they are in player prefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.value);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Effects Volume", effectsVolumeSlider.value);

        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state

    }
    public void EnableOptionsPause()
    {
        cameFrom = "Pause";
        optionsMenu.gameObject.SetActive(true);

        //set volume sliders to whatever they are in player prefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Master Volume", masterVolumeSlider.value);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Music Volume", musicVolumeSlider.value);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Effects Volume", effectsVolumeSlider.value);

        fullScreenToggle.isOn = Screen.fullScreen;//check or uncheck box based on screen status
        qualityDropDown.value = QualitySettings.GetQualityLevel();//set dropdown to current state

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
        if (cameFrom == "Main")
        {
            EnableMain();
            cameFrom = "-1";
        }
        else if (cameFrom == "Pause")
        {
            EnablePauseMenu();
            cameFrom = "-1";
        }
        else if (cameFrom == "GameOver")
        {
            EnableGameOverMenu();
            cameFrom = "-1";
        }
        else
        {
            return;
        }
    }
    #endregion
    #region Main Menu
    public void BackToMain()
    {
        //maybe just pause and show main menu
        SceneManager.LoadScene("MainMenu");
        mainMenu.gameObject.SetActive(true);
        BackgroundImage.gameObject.SetActive(true);
        pauseMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        gameOverMenu.gameObject.SetActive(false);
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
        HUD?.gameObject.SetActive(false);
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
    }
    #endregion

    #region Screen
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];//pass index to resolution array
        Screen.SetResolution(resolution.width, resolution.height, fullScreenToggle);//set resolution and fullscreen
    }
    public void ScreenToggle(bool toggle)
    {
        Screen.fullScreen = toggle;
    }
    #endregion
    #region Volume Bars
    public void BarUpdate()
    {
        mixer.SetFloat("masterVolume", volumeVsDecibels.Evaluate(masterVolumeSlider.value));//use the animation curve to set volume
        mixer.SetFloat("musicVolume", volumeVsDecibels.Evaluate(musicVolumeSlider.value));
        mixer.SetFloat("effectsVolume", volumeVsDecibels.Evaluate(effectsVolumeSlider.value));
        //save playerprefs
        //set volume sliders to whatever they are in player prefs, if non existant, set to max value
        PlayerPrefs.SetFloat("Master Volume", masterVolumeSlider.value);
        PlayerPrefs.SetFloat("Music Volume", musicVolumeSlider.value);
        PlayerPrefs.SetFloat("Effects Volume", effectsVolumeSlider.value);
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
}