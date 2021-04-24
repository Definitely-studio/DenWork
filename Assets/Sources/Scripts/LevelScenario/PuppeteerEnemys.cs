using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerEnemys : MonoBehaviour
{

    public GameObject PrefabEnemyMelee;
    public GameObject PrefabEnemyRange;
    public int[][] ProgressSpawn = new int[4][]; //[прогресс игры, вероятность спавна группы, вероятность спавна милишника, вероятность спавна ренжевика, максимум на карте]

    private List<Transform> AllEnemys = new List<Transform>();
    private StatusGame GameMode = StatusGame.WaitingFirstAttack;

    // Start is called before the first frame update
    void Start()
    {
        ProgressSpawn[0] = new int[5];
        ProgressSpawn[1] = new int[5];
        ProgressSpawn[2] = new int[5];
        ProgressSpawn[3] = new int[5];

        ProgressSpawn[0][0] = 0;        //прогресс игры
        ProgressSpawn[0][1] = 100;       //вероятность спавна группы
        ProgressSpawn[0][2] = 100;      //вероятность спавна милишника
        ProgressSpawn[0][3] = 0;        //вероятность спавна ренжевика
        ProgressSpawn[0][4] = 10;       //максимум на карте

        ProgressSpawn[1][0] = 100;
        ProgressSpawn[1][1] = 30;
        ProgressSpawn[1][2] = 90;
        ProgressSpawn[1][3] = 10;
        ProgressSpawn[1][4] = 20;

        ProgressSpawn[2][0] = 200;
        ProgressSpawn[2][1] = 20;
        ProgressSpawn[2][2] = 70;
        ProgressSpawn[2][3] = 30;
        ProgressSpawn[2][4] = 30;

        ProgressSpawn[3][0] = 300;
        ProgressSpawn[3][1] = 20;
        ProgressSpawn[3][2] = 50;
        ProgressSpawn[3][3] = 50;
        ProgressSpawn[3][4] = 50;

        GetEnemyCount();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GetEnemyCount()
    {
        AllEnemys.Clear();
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            AllEnemys.Add(child);
        }
    }

    public void FirstAttack()
    {
        if (StatusGame.WaitingFirstAttack == GameMode)
        {
            foreach (Transform child in AllEnemys)
            {
                if (child.GetComponent<MeleeEnemy>())
                {
                    child.GetComponent<MeleeEnemy>().state = States.lookingfor;
                }
                if (child.GetComponent<RangedEnemy>())
                {
                    child.GetComponent<RangedEnemy>().state = States.lookingfor;
                }
            }
            GameMode = StatusGame.GameMode;
        }
    }

    public bool SpawnEnemy(int progress, Vector3 spawn, SpawnTypes spawnType)
    {
        int[] dataValid = new int[5];
        foreach (int[] dataSpawn in ProgressSpawn)
        {
            if(dataSpawn[0] <= progress)
            {
                dataValid = dataSpawn;
            }
        }

        GetEnemyCount();
        if (dataValid[4] > AllEnemys.Count)
        {
            int enemyRandomazer = UnityEngine.Random.Range(0, 100);
            int enemyMaximum = UnityEngine.Random.Range(2, 7);
            int enemyGroup = (dataValid[1] > enemyRandomazer && spawnType == SpawnTypes.street) ? enemyMaximum : 1;

            for (int i = 0; i < enemyGroup; i++)
            {
                GameObject NewEnemy;
                //spawn melee
                if (UnityEngine.Random.Range(0, 100) <= dataValid[2])
                {
                    NewEnemy = Instantiate(PrefabEnemyMelee, transform);
                }
                //spawn range
                else
                {
                    NewEnemy = Instantiate(PrefabEnemyRange, transform);
                }

                NewEnemy.transform.position = spawn;

                AllEnemys.Add(NewEnemy.transform);
            }

            return true;
        }

        return false;
    }

    enum StatusGame
    {
        WaitingFirstAttack,
        GameMode
    }
}
