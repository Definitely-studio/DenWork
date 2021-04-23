using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar_Eyes : MonoBehaviour
{

    public float fade_level = 0.0f;
    public bool souls_in_progress;

    public IEnumerator ignite_eyes(bool soul_transfer)
    //public void ignite_eyes(bool soul_transfer)
    {
        
        //public float fade_level=0.0f;
        Color runeColor, ignitedColor;
        
        float duration = 1.5f;
        
        if (soul_transfer)
        {
            souls_in_progress = true;
            while (fade_level < 1.0f && souls_in_progress==true)
            {
                runeColor = GetComponent<SpriteRenderer>().color;
                ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, fade_level);
                fade_level += 0.001f;
                GetComponent<SpriteRenderer>().color = ignitedColor;
                yield return null;
            }
            yield break;
        }
        else
        {
            while (fade_level > 0.0f)
            {
                runeColor = GetComponent<SpriteRenderer>().color;
                ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, fade_level);
                GetComponent<SpriteRenderer>().color = ignitedColor;
                fade_level -= 0.002f;
                yield return null;
            }
        }
    }

    /*public void fade_eyes()
    {
        //Debug.Log(player_in_col);
        //if (player_in_col)
        //{
        runeColor = GetComponent<SpriteRenderer>().color;
        ignitedColor = new Color(runeColor.r, runeColor.g, runeColor.b, 0.000f);
        GetComponent<SpriteRenderer>().color = ignitedColor;
        //}
    }*/

    public void vizvano(bool is_soul_transfer)
    {
        Debug.Log("ZHOPA");
        //StopCoroutine(ignite_eyes(is_soul_transfer));
        souls_in_progress = false;
        StartCoroutine(ignite_eyes(is_soul_transfer));
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
