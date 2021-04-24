using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Rune : MonoBehaviour
{
    //public int Progress;
    //public int ProgressLevel;
    public PuppeteerSpawn progresslvl;

    float Delay;
    Color runeColor, unseenColor, ignitedColor;
    float rune_trancparency = 0.0f;

 
    void Start()
    {
        runeColor = GetComponent<SpriteRenderer>().color;
        unseenColor = new Color(runeColor.r, runeColor.g, runeColor.b, 0.000f);
        GetComponent<SpriteRenderer>().color = unseenColor ;


    }

    public int Progress;
    void Update()
    {
        /*
            //
            Let's break that up into a few steps. It looks like you're using C#.

    First, find a GameObject by name:

     GameObject go = GameObject.Find("mainCharacter");
            Second, get a component:

            controllerScript cs = go.GetComponent<controllerScript>();
            Third, read a public field/property from that component:

     float thisObjectMove = cs.move;
        Putting all of that together:

     GameObject go = GameObject.Find("mainCharacter");
        controllerScript cs = go.GetComponent<controllerScript>();
        float thisObjectMove = cs.move;
        //
        */

        PuppeteerSpawn spawn = GameObject.FindGameObjectWithTag("PuppeterSpawner").GetComponent<PuppeteerSpawn>();
        Debug.Log(spawn);
        //controllerScript cs = PuppeterSpawner.GetComponent<PuppeteerSpawn>();
        Progress = spawn.ProgressLevel;
        Debug.Log(Progress);

        //
        //progresslvl = GameObject.FindGameObjectWithTag("PuppeterSpawner").GetComponent<PuppeteerSpawn>();
        //Debug.Log("Player in Trigger Zone");
        //sn.ignite_eyes(true);
        //souls_transfer = true;
        //Aeyes.vizvano(souls_transfer);
        //


        //Ignite_Altar_Rune
        //Progress = ProgressLevel;
        if (Progress == 150)
        {
            runeColor = GameObject.Find("FX_Rune1").GetComponent<SpriteRenderer>().color;
            ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, 1.0f);
            GameObject.Find("FX_Rune1").GetComponent<SpriteRenderer>().color = ignitedColor;
        }
        else if (Progress == 300){
            runeColor = GameObject.Find("FX_Rune2").GetComponent<SpriteRenderer>().color;
            ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, 1.0f);
            GameObject.Find("FX_Rune2").GetComponent<SpriteRenderer>().color = ignitedColor;
        }
        else if (Progress == 450){
            runeColor = GameObject.Find("FX_Rune3").GetComponent<SpriteRenderer>().color;
            ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, 1.0f);
            GameObject.Find("FX_Rune3").GetComponent<SpriteRenderer>().color = ignitedColor;
        }

    }
       
    
}
