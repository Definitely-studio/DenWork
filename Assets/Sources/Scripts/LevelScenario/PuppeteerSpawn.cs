using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppeteerSpawn : MonoBehaviour
{

    private List<Transform> AllSpawn = new List<Transform>();
    public GameObject AllEnemys;
    private float SpawnTimer = 10;
    public int ProgressLevel = 0;
    public float DeltaRespawn = 5;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in GetComponentInChildren<Transform>())
        {
            AllSpawn.Add(child);
        }
    }

    public void SetProgress(int progress)
    {
        ProgressLevel = progress;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0)
        {
            SpawnTimer = DeltaRespawn - ProgressLevel/100;
            int spawnNumber = Mathf.FloorToInt(UnityEngine.Random.Range(0, AllSpawn.Count));
            AllEnemys.GetComponent<PuppeteerEnemys>().SpawnEnemy(ProgressLevel, AllSpawn[spawnNumber].position, AllSpawn[spawnNumber].GetComponent<SpawnPoint>().spawnType);
        }


    }
}
