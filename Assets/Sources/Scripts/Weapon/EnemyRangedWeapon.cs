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
    // Start is called before the first frame update
    void Start()
    {
      enemy = GetComponentInParent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
      
      Vector3 targetDirection = (enemy.Player.transform.position - transform.position).normalized;

      float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

      transform.eulerAngles = new Vector3(0,0, angle);

    }

    public void Shoot()
    {
      if(bullet != null)
      Instantiate(bullet, transform.position, transform.rotation);
    }
    
    IEnumerator ShootCooldown(float waitTime)
    {

      yield return new WaitForSeconds(waitTime);
       isAttackCooldown = false;
    }

   
}
