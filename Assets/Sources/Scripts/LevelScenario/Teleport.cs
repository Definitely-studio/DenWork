using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{

    public Vector3 newPosition;
    public Player player;
    public AudioSource Sounds;
    public AudioClip door;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player" && player.GetKey() == true)
        {
            player.canMove = false;
            other.gameObject.transform.position = newPosition;
            if(other.gameObject.transform.position == newPosition)
                player.SetKey(false);

            player.canMove = true;
            Debug.Log("teleport");
            Debug.Log(other.gameObject.transform.position);
             Debug.Log(other.gameObject);
            Debug.Log(newPosition);
            player.GetActions().ChangeHP(player.GetMaxHP() - player.GetHP());
            Sounds.PlayOneShot(door);
           
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.gameObject.tag == "Player")
        {
            
        }
    }

}
