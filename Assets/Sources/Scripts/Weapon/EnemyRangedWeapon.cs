using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedWeapon : MonoBehaviour
{
    public Enemy enemy;
    public int damage = 10;
    public float AttackCooldown = 10f;
    public GameObject bullet;
    public bool isAttackCooldown = false;
    public AudioSource Audio;
    public AudioClip ShootSound;
    // Start is called before the first frame update
    void Start()
    {
      enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
      
      Vector3 targetDirection = (enemy.Player.GetComponentInChildren<Player>().target.transform.position - transform.position).normalized;

      float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;

      transform.eulerAngles = new Vector3(0,0, angle);

    }

    public void Shoot()
    {
      if(Audio!= null){
        Audio.PlayOneShot(ShootSound);
      }
      
      GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation); 
      newBullet.transform.SetParent(null);
  
      newBullet.GetComponent<Rigidbody2D>().AddForce(newBullet.transform.up * newBullet.GetComponent<Bullet>().Speed);
      /*if(this.GetComponentInParent<ParentfromBullet>() != null)
          newBullet.transform.SetParent(this.GetComponentInParent<ParentfromBullet>().transform);*/
        
    }
    
    IEnumerator ShootCooldown(float waitTime)
    {

      yield return new WaitForSeconds(waitTime);
       isAttackCooldown = false;
    }

   
}
