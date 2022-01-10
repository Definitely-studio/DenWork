using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private Input input;
    public GameObject PanelOptions;
    public GameObject PanelPosthumous;
    public GameObject Inventory;
    public GameObject Crosshair;
    private bool isPause = false;

    // Start is called before the first frame update
    void Start()
    {
      Cursor.visible = false;
     
    }
    void Awake()
    {
     
        input = new Input();
        input.Player.Pause.performed += context => Pause();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Pause(){
      
        if (isPause == true) OffOptions();
        else OnOptions();
    }

    public void ToStartGame()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene("StartMenu"));
    }
   

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        StartCoroutine(LoadYourAsyncScene("MAIN LEVEL"));
    }

    public void LoadFinalScene()
    {
        
        StartCoroutine(LoadYourAsyncScene("FINAL LEVEL"));
    }


    public void ActivatePostPortus()
    {
        Time.timeScale = 0f;
        PanelPosthumous.SetActive(true);
        Crosshair.SetActive(false);
        Cursor.visible = true;

    }

    public void OnOptions()
    {

        Time.timeScale = 0;
        isPause = true;
        PanelOptions.SetActive(true);
        Cursor.visible = true;
        Crosshair.SetActive(false);
    }


    public void OffOptions()
    {
        isPause = false;
        Time.timeScale = 1;
        PanelOptions.SetActive(false);
        Cursor.visible = false;
        Crosshair.SetActive(true);
    }
}
