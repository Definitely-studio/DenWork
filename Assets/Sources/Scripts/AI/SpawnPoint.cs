using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    public MeleeEnemy meleeEnemy;
    public RangedEnemy rangedEnemy;
    public float spawnFreq = 5.0f;
    //public EnemyTypes enemyType;
    public SpawnTypes spawnType;

    private bool isCanSpawn = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
      if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" )
        isCanSpawn = false;
      else
        isCanSpawn = true;
    }

    public bool SpawnEnemy (EnemyTypes enemyType)
    {
      if(isCanSpawn)
        if(enemyType == EnemyTypes.ranged)
        {
          Instantiate (rangedEnemy, transform.position, transform.rotation);
          return true;
        }
        else
        {
          Instantiate (meleeEnemy, transform.position, transform.rotation);
          return true;
        }
      else
        return false;
    }

}

public enum SpawnTypes {
  home,
  street
}
