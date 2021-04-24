using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{
    public GameObject AllSpawn;
    public GameObject AllEnemy;
    public GameObject Player;
    public int ProgressLevel = 0;
    public int SoulsRequiredToWin = 500;
    public int EnemyKill = 0;
    public GameObject Rain;
    public GameObject Water;

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
        FlashTimer -= Time.deltaTime;
        if(Flash != null){
        Flash.SetActive(false);
        if (FlashTimer < 0)
        {
            FlashTimer = UnityEngine.Random.Range(5, 50);
            if(ProgressLevel > 300)
            {
                AudioFlash.Play();
                GetComponent<CameraMovement>().Shaking();
                Flash.SetActive(true);
            }
        }
        }
    }

    public void EnemyDead()
    {
        EnemyKill++;
        AllEnemy.GetComponent<PuppeteerEnemys>().FirstAttack();
    }

    void RefrashProgressLevel()
    {

        Rain.GetComponent<Rain>().SetRain(ProgressLevel);
        Water.GetComponent<Water>().SetProgress(ProgressLevel);
       // TextPrigress.text = (SoulsRequiredToWin - ProgressLevel).ToString();
        AllSpawn.GetComponent<PuppeteerSpawn>().SetProgress(ProgressLevel);
    }

    public void AddSoul()
    {
        if(ProgressLevel < SoulsRequiredToWin)
        {
            if(!AudioSoul.isPlaying)
                AudioSoul.Play();

            AudioMelody2.volume = ProgressLevel / (5000);
            AudioMelodyMain.volume = 1 - ProgressLevel / (1000);
            ProgressLevel++;
            Pawns.GetComponent<PuppeteerSpawn>().SetProgress(ProgressLevel);
        }
        else
        {
            StartCoroutine(LoadYourAsyncScene());
        }
        RefrashProgressLevel();
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
