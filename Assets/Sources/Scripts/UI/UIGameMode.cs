using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMode : MonoBehaviour
{

    public GameObject HealPointScale;
    public Text BulletText;
     public Text BulletText2;

    public Image M1Img;
    public Image M2Img;
    public Image M3Img;

    public int machineGunSoulsDemand =  25;
    public int shootGunSoulsDemand = 35;
    public int healSoulsDemand = 40;


    public Slider HealSlider;


    // Start is called before the first frame update
    void Start()
    {
        //GameObject textInfo = GameObject.FindWithTag("TextInformation");
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void SetHealSlider(int value){

      HealSlider.value = (float) value / 100;
    }



    public void ShowBullet(int bullet, int maxBullet, int totalBullet)
    {
        BulletText.text = bullet.ToString() + "/" + maxBullet.ToString();
        BulletText2.text = "Total " + totalBullet.ToString();

    }

    public void ShowHealPointLevel(int val)
    {
        HealPointScale.transform.position = new Vector3(0, val * 3.5f - 350, 0);
    }


}
