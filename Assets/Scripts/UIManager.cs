using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
    public GameObject Main;
[SerializeField] Dropdown resDropDown;
    Resolution[] myRes;
    List<string> myList = new List<string>();
    // Use this for initialization
    void Start () 
    {
        myRes = Screen.resolutions;

        foreach (Resolution res in myRes)
        {
            myList.Add(res.width + "x" + res.height);
        }
        resDropDown.ClearOptions();
        resDropDown.AddOptions(myList);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Resolution(int index)
    {

        Screen.SetResolution(myRes[index].width, myRes[index].height, Screen.fullScreen);
        resDropDown.value = index;
        resDropDown.RefreshShownValue();
    }

    public void PLAY()
    {
        SceneManager.LoadScene(2);

    }

    public void SetFullScreen(bool screen)
    {
        Screen.fullScreen = screen;

    }
}
