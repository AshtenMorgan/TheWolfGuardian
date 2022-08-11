using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;


public class OptionsScreen : GameSettings
{
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

    // Start is called before the first frame update
    void Start()
    {
        SetUpOptions();
    }

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
    public void BKSelect(string name)
    {
        for (int i = 0; i < background.Count; i++)
        {
            background[i].SetActive(background[i].name.Equals(name));
        }
    }
}
