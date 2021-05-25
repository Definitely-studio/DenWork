using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject PanelCredits;
    public GameObject PanelOptions;
    public Text Version;

    // Start is called before the first frame update
    void Start()
    {
        ShowVersion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGame()
    {
        StartCoroutine(LoadYourAsyncScene());
    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MAIN LEVEL");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void ShowVersion(){
        Version.text = "Version: " + Application.version.ToString();
    }
    public void OnExit()
    {
        Application.Quit();
    }

    
    public void OnCredits()
    {
        PanelCredits.SetActive(true);
    }

    
    public void OffCredits()
    {
        PanelCredits.SetActive(false);
    }
    
    public void OnOptions()
    {
        PanelOptions.SetActive(true);
    }

    
    public void OffOptions()
    {
        PanelOptions.SetActive(false);
    }

    public void OpenLink(string link)
    {
        Application.OpenURL(link);
    }

}
