using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject WeaponPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.gameObject.GetComponentInChildren<Player>())
        {
            Player player = other.gameObject.GetComponentInChildren<Player>();
            GameObject Weapon = Instantiate(WeaponPrefab, player.socket.transform);
            if(Weapon.TryGetComponent(out Gun gun))
                player.SetGun(gun);
            if(!player.WeaponList.Contains(this.gameObject))
                player.WeaponList.Add(this.gameObject);
        }
    }
}
