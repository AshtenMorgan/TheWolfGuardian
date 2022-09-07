using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public enum SceneIndexes
{
    Main_Menu = 0,
    Prologue = 1, 
    Zone_1 = 2,

}
public enum ActiveMenu
{
    Game,
    Options,
    Pause,
    GameOver,
    Main
}

public class GameSettings : MonoBehaviour
{
    #region Variables
    [Header("Menus")]
    public List<GameObject> menuNames = new List<GameObject>();
    public static GameSettings Instance { get; private set; }
    #region Audio
    [Header("Audio"), Tooltip("Main audio control for everything")]
    public AudioMixer mixer;
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
    #region HUD
    public Slider healthBarSlider;
    #endregion
    public bool isPaused;
    public ActiveMenu activeMenu;
    #endregion
    #region Canvas Components
    [Header("Elements")]
    public Slider masterVolumeSlider,
        musicVolumeSlider,
        effectsVolumeSlider;
    public Toggle fullScreenToggle;
    public TMPro.TMP_Dropdown resolutionDropDown,
    qualityDropDown;
    public List<Resolution> resolutions;
    public List<GameObject> background = new List<GameObject>();
    #endregion
    #region Quality
    private void Awake()
    {
        //make sure there is always only 1 instance
        if (GameSettings.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    private void Start()
    {
        SelectMenu("MainMenuCanvas");
        activeMenu = ActiveMenu.Main;
        SetUpOptions();
    }

    #region Button Presses
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
        //load whatever scene our zone1 is
        SceneManager.LoadSceneAsync((int)SceneIndexes.Prologue);
        //display HUD canvas
        SelectMenu("HUD Canvas");
        //Set enum to Game (Used to tell what screen is active)
        activeMenu = ActiveMenu.Game;
        Camera.main.GetComponent<AudioSource>().clip = scene1;
        Time.timeScale = 1;
    }
    #endregion
    #region Misc Functions
    public void ButtonHighlight()
    {
        fxSource.PlayOneShot(buttonHover);
    }
    public void ButtonClick()
    {
        if (!fxSource.isPlaying)
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
    
    #endregion
    #region Menu Navigation
    public void SetMain()
    {
        activeMenu = ActiveMenu.Main;
        SelectMenu("MainMenuCanvas");
    }
    public void OptionsBack()
    {
        if (activeMenu == ActiveMenu.Main)        
            MainBack();        
        else
            GameBack();
    }
    public void MainBack()
    {
        SelectMenu("MainMenuCanvas");
    }
    public void GameBack()
    {
        SelectMenu("PauseCanvas");
    }
    public void BKSelect(string name)
    {
        for (int i = 0; i < background.Count; i++)
        {
            background[i].SetActive(background[i].name.Equals(name));
        }
    }
    public void SelectMenu(string name)
    {
        for (int i = 0; i < menuNames.Count; i++)
        {
            menuNames[i].SetActive(menuNames[i].name.Equals(name));
        }
    }
    public void PauseUnpause()
    {
        if (isPaused)
        {
            SelectMenu("HUD Canvas");
            activeMenu = ActiveMenu.Game;
            Time.timeScale = 1.0f;
            isPaused = false;
        }
        else
        {
            SelectMenu("PauseCanvas");
            activeMenu = ActiveMenu.Pause;
            Time.timeScale = 0.0f;//stop time
            isPaused = true;
        }
    }
    public void ResumeGame()
    {
        GameManager.Instance.ResetSpawn();
        SelectMenu("HUD Canvas");
        Time.timeScale = 1.0f;
        activeMenu = ActiveMenu.Game;
    }
    #endregion
    #region Volume Bars
    public void UpdateMaster()
    {
        //this works better than using an animation curve, min value must be above 0
        mixer.SetFloat("masterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);

        //save playerprefs
        PlayerPrefs.SetFloat("Volume_Master", masterVolumeSlider.value);
        PlayerPrefs.Save();//save playerprefs
    }
    public void UpdateMusic()
    {
        //this works better than using an animation curve, min value must be above 0       
        mixer.SetFloat("musicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);

        //save playerprefs
        PlayerPrefs.SetFloat("Volume_Music", musicVolumeSlider.value);
        PlayerPrefs.Save();//save playerprefs
    }
    public void UpdateEffects()
    {
        //this works better than using an animation curve, min value must be above 0
        mixer.SetFloat("effectsVolume", Mathf.Log10(effectsVolumeSlider.value) * 20);

        //save playerprefs
        PlayerPrefs.SetFloat("Volume_Effects", effectsVolumeSlider.value);
        PlayerPrefs.Save();//save playerprefs
    }
    #endregion
    protected void SetUpOptions()
    {
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
        //create values if they are not present
        if (!PlayerPrefs.HasKey("Volume_Master"))
            PlayerPrefs.SetFloat("Volume_Master", 0.5f);
        if (!PlayerPrefs.HasKey("Volume_Music"))
            PlayerPrefs.SetFloat("Volume_Music", 0.5f);
        if (!PlayerPrefs.HasKey("Volume_Effects"))
            PlayerPrefs.SetFloat("Volume_Effects", 0.5f);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("Volume_Master");//get palyerprefs value, if it doesnt exist, default to .5
        musicVolumeSlider.value = PlayerPrefs.GetFloat("Volume_Music");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("Volume_Effects");

        mixer.SetFloat("masterVolume", Mathf.Log10(masterVolumeSlider.value) * 20);
        mixer.SetFloat("musicVolume", Mathf.Log10(musicVolumeSlider.value) * 20);
        mixer.SetFloat("effectsVolume", Mathf.Log10(effectsVolumeSlider.value) * 20);

        resolutionDropDown.value = PlayerPrefs.GetInt("ResIndex", 0);
        SetResolution(resolutionDropDown.value);
        fullScreenToggle.isOn = IntToBool(PlayerPrefs.GetInt("FullScreen", 1));
        ScreenToggle(fullScreenToggle.isOn);
        qualityDropDown.value = PlayerPrefs.GetInt("QualityLevel", 5);//get setting from playerprefs
        SetQuality(qualityDropDown.value);
    }
    public void SetQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);//pass the dropdown selection to qualitylevel
        PlayerPrefs.SetInt("QualityLevel", index);//save in playerprefs
        PlayerPrefs.Save();//save playerprefs
    }
    #region Screen
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
    [ContextMenu("Clear Player Prefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
    private void OnDestroy()
    {
        Destroy(Instance);
        Instance = null;
    }
}
