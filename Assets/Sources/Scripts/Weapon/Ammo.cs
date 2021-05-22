using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : Item
{
    public int ammoCount = 5;
    public Player player;
    public AudioSource Sounds;
    public AudioClip pickUp;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Ammo(ItemType type, string label, int amount, int id, string name, Sprite icon) : base(type, label, amount, id, name, icon)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
      /*  if (other.gameObject.tag == "Player")
        {
            Debug.Log("pickup ammo");
            other.gameObject.GetComponent<Player>().SetAmmo(player.GetAmmo() + ammoCount);
            Sounds.PlayOneShot(pickUp);
            Destroy(transform.gameObject);
        }*/
    }

}
