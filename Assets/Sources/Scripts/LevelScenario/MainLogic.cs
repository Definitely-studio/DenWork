using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{

    public GameObject Player;
    public int ProgressLevel = 0;
    public int SoulsRequiredToWin = 500;
    public int EnemyKill = 0;
    public GameObject Rain;
    

    public TMP_Text TextPrigress;

    public AudioSource AudioSoul;
    public AudioSource AudioMelody2;
    public AudioSource AudioMelodyMain;

    private float FlashTimer = 50;
    public GameObject Flash;
    public AudioSource AudioFlash;

    public GameObject Pawns;

    // Start is called before the first frame update
    void Start()
    {
       // RefrashProgressLevel();
      
    }

    // Update is called once per frame
    void Update()
    {
      
      
        GetComponent<CameraMovement>().Shaking();
      
    }

    void RefrashProgressLevel()
    {

        Rain.GetComponent<Rain>().SetRain(ProgressLevel);
       // Water.GetComponent<Water>().SetProgress(ProgressLevel);
       // TextPrigress.text = (SoulsRequiredToWin - ProgressLevel).ToString();
        //AllSpawn.GetComponent<PuppeteerSpawn>().SetProgress(ProgressLevel);
    }


    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Final");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
