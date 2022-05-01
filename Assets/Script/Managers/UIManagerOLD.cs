using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManagerOLD : MonoBehaviour
{
    /*
    #region Variables
    public static UIManager Instance { get; private set; }//allow other classes to access UI

    #region Audio
    [SerializeField, Tooltip("Main audio control for everything")]
    private AudioMixer mixer;
    [Header("Audio components")]
    public AudioSource musicSource,
        fxSource;

    public AudioClip buttonClick,
        buttonHover,
        menu,
        scene1,
        back,
        quitGame,
        load,
        newGame;
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
    public Canvas mainMenu,
        pauseMenu,
        gameOverMenu,
        optionsCanvas,
        BackgroundImage,
        HUD;
    public GameObject optionsBKG;
    public GameObject optionsBKGMain;

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
    public List<Resolution> resolutions;
    #endregion
    private int cameFrom;
    private Scene currentScene;
    private bool loaded = false;
    private string mainMenuName = "MainMenu"; //name of the main menu scene as a string

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
    private void Start()
    {
        LoadPlayerPrefs();
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
    
   
    #region Menu Controls
    #region Options
    //show settings menu
    public void EnableOptionsMain()
    {
        optionsCanvas.gameObject.SetActive(true);
        optionsBKGMain.gameObject.SetActive(true);
        optionsBKG.SetActive(false);
        cameFrom = 1;
    }
    public void EnableOptionsPlay()
    {
        optionsCanvas.gameObject.SetActive(true);
        optionsBKGMain.SetActive(false);
        optionsBKG.SetActive(true);
        cameFrom = 2;
    }
    //hides Optons menu
    public void ExitOptions()
    {
        switch (cameFrom)
        {
            case 1://Main menu
                mainMenu.gameObject.SetActive(true);
                optionsCanvas.gameObject.SetActive(false);
                optionsBKGMain.gameObject.SetActive(false);
                cameFrom = 0;
                break;

            case 2://Pause Menu
                pauseMenu.gameObject.SetActive(true);
                optionsCanvas.gameObject.SetActive(false);
                cameFrom = 0;
                break;

        }

    }
    public void DisableOptions()
    {
        optionsCanvas.gameObject.SetActive(false);
    }

    #endregion
    #region Main Menu
    public void BackToMain()
    {
        SceneManager.LoadScene(mainMenuName);//load scene main
        EnableMain();
        EnableBackground();
        DisablePauseMenu();
        DisableGameOver();
        Time.timeScale = 1.0f;//resume time  
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
        GameManager.Instance.Player.Lives = 4;//give some lives back
        GameManager.Instance.lives = 4;//match gm lives
        GameManager.Instance.ResetSpawn();
        GameManager.Instance.SpawnPlayer();
        GameManager.Instance.gameOver = false;//not game over any more

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
        PlayerPrefs.SetInt("QualityLevel", index);//save in playerprefs
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
        PlayerPrefs.SetInt("FullScreen", BoolToInt(toggle));
        PlayerPrefs.Save();
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
    #region Misc Functions
    public void ButtonHighlight()
    {
        fxSource.PlayOneShot(buttonHover);
    }
    public void ButtonClick()
    {
        fxSource.PlayOneShot(buttonClick);
    }
    public int BoolToInt(bool value)
    {
        if (value == true)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
    public bool IntToBool(int value)
    {
        if (value == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<Resolution> GetResolutions()
    {
        //This should stop duplicate resolutions from showing up, and leave us with only max refresh rates
        Resolution[] resolutions = Screen.resolutions;
        HashSet<System.Tuple<int, int>> uniqueResolutions = new HashSet<System.Tuple<int, int>>();
        Dictionary<System.Tuple<int, int>, int> maxRefreshRates = new Dictionary<System.Tuple<int, int>, int>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            //add resolutions if they dont exist
            System.Tuple<int, int> resolution = new System.Tuple<int, int>(resolutions[i].width, resolutions[i].height);
            uniqueResolutions.Add(resolution);
            //get highest framerate
            if (!maxRefreshRates.ContainsKey(resolution))
            {
                maxRefreshRates.Add(resolution, resolutions[i].refreshRate);
            }
            else
            {
                maxRefreshRates[resolution] = resolutions[i].refreshRate;
            }

        }
        //Build the list
        List<Resolution> uniqueResolutionList = new List<Resolution>(uniqueResolutions.Count);
        foreach (System.Tuple<int, int> resolution in uniqueResolutions)
        {
            Resolution newResolution = new Resolution
            {
                width = resolution.Item1,
                height = resolution.Item2
            };
            if (maxRefreshRates.TryGetValue(resolution, out int refreshRate))
            {
                newResolution.refreshRate = refreshRate;
            }
            uniqueResolutionList.Add(newResolution);
        }
        return uniqueResolutionList;
    }
    void SetUpOptions()
    {
        masterVolumeSlider = GameObject.FindGameObjectWithTag("MasterSlider").GetComponent<Slider>();
        musicVolumeSlider = GameObject.FindGameObjectWithTag("MusicSlider").GetComponent<Slider>();
        healthSlider = GameObject.FindGameObjectWithTag("HealthSlider").GetComponent<Slider>();
        effectsVolumeSlider = GameObject.FindGameObjectWithTag("EffectsSlider").GetComponent<Slider>();
        fullScreenToggle = GameObject.FindGameObjectWithTag("FSToggle").GetComponent<Toggle>();
        resolutionDropDown = GameObject.FindGameObjectWithTag("ResolutionDrop").GetComponent<TMPro.TMP_Dropdown>();
        qualityDropDown = GameObject.FindGameObjectWithTag("QualityDrop").GetComponent<TMPro.TMP_Dropdown>();
        fxSource = GameObject.FindGameObjectWithTag("MasterCanvas").GetComponent<AudioSource>();
        //canvases
        mainMenu = GameObject.Find("MainMenuCanvas").GetComponent<Canvas>();
        optionsCanvas = GameObject.FindGameObjectWithTag("OptionsCanvas").GetComponent<Canvas>();
        optionsBKG = GameObject.FindGameObjectWithTag("OptionsBKG");
        optionsBKGMain = GameObject.FindGameObjectWithTag("OptionsBKGMain");

        BackgroundImage = GameObject.FindGameObjectWithTag("BackgroundCanvas").GetComponent<Canvas>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseCanvas").GetComponent<Canvas>();
        gameOverMenu = GameObject.FindGameObjectWithTag("GameOverCanvas").GetComponent<Canvas>();
        HUD = GameObject.FindGameObjectWithTag("HUDCanvas").GetComponent<Canvas>();

        resolutionDropDown.ClearOptions();//clear any res options
        resolutions = GetResolutions();//get resolution array
        List<string> options = new List<string>();//create list to hold resolutions

        for (int index = 0; index < resolutions.Count; index++)//loop through all possible resolutions system can use
        {
            options.Add(string.Format("{0} x {1}", resolutions[index].width, resolutions[index].height));//add each to the list
        }

        resolutionDropDown.AddOptions(options);//add list to the dropdown

        // Build quality levels
        qualityDropDown.ClearOptions();//clear anything that might already exist
        qualityDropDown.AddOptions(QualitySettings.names.ToList());//add the quality levels to the dropdown


        //setup options to match what is saved in playerprefs
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.5f);//get palyerprefs value, if it doesnt exist, default to .5
        mixer.SetFloat("masterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
        mixer.SetFloat("musicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume", 0.5f);
        mixer.SetFloat("effectsVolume", Mathf.Log10(effectsVolumeSlider.value) * 20);
        resolutionDropDown.value = PlayerPrefs.GetInt("ResIndex", 0);
        SetResolution(resolutionDropDown.value);
        fullScreenToggle.isOn = IntToBool(PlayerPrefs.GetInt("FullScreen", 1));
        ScreenToggle(fullScreenToggle.isOn);
        qualityDropDown.value = PlayerPrefs.GetInt("QualityLevel", 5);//get setting from playerprefs
        SetQuality(qualityDropDown.value);
    }
    #endregion
*/
}