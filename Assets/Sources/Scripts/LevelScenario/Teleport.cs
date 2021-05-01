using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Transform newPosition;
    public Player player;
    public AudioSource Sounds;
    public AudioClip door;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player" && player.GetKey() == true)
        {
            player.SetKey(false);
            other.gameObject.transform.position = newPosition.position;
            player.GetActions().ChangeHP(player.GetMaxHP());
            Sounds.PlayOneShot(door);
           
        }
    }

}
