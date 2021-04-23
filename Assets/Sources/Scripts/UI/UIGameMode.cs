using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameMode : MonoBehaviour
{

    public GameObject SoulScale;
    public Text SoulText;
    public GameObject HealPointScale;
    public Text BulletText;

    public Image M1Img;
    public Image M2Img;
    public Image M3Img;

    public int machineGunSoulsDemand =  25;
    public int shootGunSoulsDemand = 35;
    public int healSoulsDemand = 40;

    public Slider SoulSlider;
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

      HealSlider.value = (float) value /100;
    }

    public void SetSoulSlider(int val){

      SoulSlider.value = (float) val /100;
      SoulText.text = val.ToString();

      if (machineGunSoulsDemand < val)
      {
          M1Img.color = new Color(1, 1, 1);
      }
      else
      {
          M1Img.color = new Color(0, 0, 0);
      }

      if (healSoulsDemand < val)
      {
          M2Img.color = new Color(1, 1, 1);
      }
      else
      {
          M2Img.color = new Color(0, 0, 0);
      }

      if (shootGunSoulsDemand < val)
      {
          M3Img.color = new Color(1, 1, 1);
      }
      else
      {
          M3Img.color = new Color(0, 0, 0);
      }


    }

    public void ShowBullet(int bullet, int maxBullet)
    {
        BulletText.text = bullet.ToString() + "/" + maxBullet.ToString();
    }

    public void ShowSoulLevel(int val)
    {
        if (machineGunSoulsDemand < val)
        {
            M1Img.color = new Color(1, 1, 1);
        }
        else
        {
            M1Img.color = new Color(0, 0, 0);
        }

        if (healSoulsDemand < val)
        {
            M2Img.color = new Color(1, 1, 1);
        }
        else
        {
            M2Img.color = new Color(0, 0, 0);
        }

        if (shootGunSoulsDemand < val)
        {
            M3Img.color = new Color(1, 1, 1);
        }
        else
        {
            M3Img.color = new Color(0, 0, 0);
        }

        SoulScale.transform.position = new Vector3(0, val - 100, 0);
        SoulText.text = val.ToString();
    }

    public void ShowHealPointLevel(int val)
    {
        HealPointScale.transform.position = new Vector3(0, val * 3.5f - 350, 0);
    }


}
