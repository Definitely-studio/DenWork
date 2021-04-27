using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetKey : MonoBehaviour
{
        public Player player;
    public AudioSource Sounds;
    public AudioClip pickUp;
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
        
        if (other.gameObject.tag == "Player")
        {
            player.SetKey(true);
            Sounds.PlayOneShot(pickUp);
            Destroy(transform.gameObject);
        }
    }

}
