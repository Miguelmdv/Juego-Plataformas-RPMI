using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UIController : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Dropdown resolutionSel;

    List<string> myList = new List<string>();
    Resolution[] myRes;

    [SerializeField] GameObject player;
    PlayerController pScript;
    [SerializeField] AudioMixerSnapshot[] snapshot;
    AudioSource audioSource;


    private void Start()
    {
        if(player != null)
            pScript = player.GetComponent<PlayerController>();

        if (resolutionSel != null)
        {
            myRes = Screen.resolutions;
            foreach (Resolution res in myRes)
            {
                myList.Add(res.ToString());
            }
            resolutionSel.AddOptions(myList);
        }

        audioSource = GetComponent<AudioSource>();


    }


    public void Play()
    {

        audioSource.Play();
        SceneManager.LoadScene(0);


    }

    public void OnApplicationQuit()
    {

        Application.Quit();
    }

    public void BackButtom()
    {
        if (pScript.pause)
        {
            Time.timeScale = 1;
            snapshot[0].TransitionTo(0);
            pScript.pause = !pScript.pause;
        }
        SceneManager.LoadScene(1);
    }

    public void Resume()
    {


        pScript.pause = !pScript.pause;
        pScript.pauseMenu.SetActive(pScript.pause);
        if (pScript.pause)
        {
            Time.timeScale = 0;
            snapshot[1].TransitionTo(0);
        }
        else
        {
            Time.timeScale = 1;
            snapshot[0].TransitionTo(0);
        }

    }

    //--------------------------------
    //AUDIO
    public void ControlVolumeMaster(float volume)
    {
        myMixer.SetFloat("VolumeMaster", volume);
    }
    public void ControlVolumeMusic(float volume)
    {
        myMixer.SetFloat("VolumeMusic", volume);
    }
    public void ControlVolumeSFX(float volume)
    {
        myMixer.SetFloat("VolumeSFX", volume);
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

    //--------------------------------
    //Save & Load

    public void Save()
    {
        PlayerPrefs.SetFloat("ejeX", player.transform.position.x);
        PlayerPrefs.SetFloat("ejeY", player.transform.position.y);
        Debug.Log(PlayerPrefs.GetFloat("ejeX", 0));
        Debug.Log(PlayerPrefs.GetFloat("ejeY", 0));
    }

    public void Load()
    {
        player.transform.position = new Vector2(
            PlayerPrefs.GetFloat("ejeX", 0),
            PlayerPrefs.GetFloat("ejeY", 0));

    }


}
