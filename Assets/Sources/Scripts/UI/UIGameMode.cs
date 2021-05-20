using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMode : MonoBehaviour
{

    public GameObject HealPointScale;
    public Text BulletTextInMagazine;
     public Text BulletTextTotal;


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


    public void SetHealSlider(int value, int maxHP){

      HealSlider.value = (float) value / maxHP;
    }



    public void ShowBullet(int bullet, int maxBullet, int totalBullet)
    {
        BulletTextInMagazine.text = bullet.ToString() + "/" + maxBullet.ToString();
        BulletTextTotal.text = "Total " + totalBullet.ToString();

    }

    public void ShowHealPointLevel(int val)
    {
        HealPointScale.transform.position = new Vector3(0, val * 3.5f - 350, 0);
    }


}
