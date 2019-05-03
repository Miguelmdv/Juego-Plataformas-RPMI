using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [SerializeField] GameObject main, options;
    [SerializeField] AudioMixer master;
    [SerializeField] Dropdown resolutionSel;
    List<string> myList = new List<string>();

    Resolution[] myRes;

    private void Start()
    {
        myRes = Screen.resolutions;

        foreach (Resolution res in myRes)
        {
            myList.Add(res.ToString());
        }
        resolutionSel.AddOptions(myList);
    }

    public void Options()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void BackMain()
    {
        main.SetActive(true);
        options.SetActive(false);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OnApplicationQuit()
    {
        Application.Quit();
    }

    //--------------------------------
    //AUDIO
    public void ControlVolume(float volume)
    {
        master.SetFloat("Volume", volume);
    }

    //--------------------------------
    //Graphics

    public void CheckScreen(bool check)
    {
        Screen.fullScreen = check;

    }
    public void SetResolution(int index)
    {
        Screen.SetResolution(myRes[index].width, myRes[index].height, Screen.fullScreen);
        resolutionSel.value = index;
        resolutionSel.RefreshShownValue();
    }


}
