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

    public TMP_Text TextPrigress;

    public AudioSource AudioMelody2;
    public AudioSource AudioMelodyMain;


  
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

       
    }


    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("FINAL LEVEL");

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
