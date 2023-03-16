using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyGenerator_T : MonoBehaviour
{
    public GameObject EnemyPrefab0;
    public GameObject EnemyPrefab1;
    public GameObject EnemyPrefab2;
    public GameObject EnemyPrefab3;
    public GameObject EnemyPrefab4;
    public GameObject BossEnemyPrefab;
    private GameController gc;

    Savedata data;

    public int Bs = 2000;
    public int p0 = 98;
    public int p1 = 93;
    public int p2 = 85;
    public int p3 = 75;
    private bool boss = false;

    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();
        data = LoadPlayerData();

        switch (data.stagelevel)
        {
            case 1:
                Bs = 10000;
                break;
            case 2:
                Bs = 30000;
                break;
            case 3:
                Bs = 50000;
                break;
            default:
                break;

        }

        InvokeRepeating("Spawn", 2f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gc.score >= Bs)
        {
            if (boss == false)
            {
                Invoke("BossSpawn", 1f);
                boss = true;
            }
        }
    }

    void Spawn()
    {
        Vector3 spawnPositon = new Vector3(
            Random.Range(-2.9f, 2.9f),
            transform.position.y,
            transform.position.z);

        int rnd = Random.Range(1, 100);

        if (rnd >= p0)
        {
            Instantiate(EnemyPrefab4, spawnPositon, transform.rotation);
        }
        else if (rnd < p0 && rnd >= p1)
        {
            Instantiate(EnemyPrefab3, spawnPositon, transform.rotation);
        }
        else if (rnd < p1 && rnd >= p2)
        {
            Instantiate(EnemyPrefab2, spawnPositon, transform.rotation);
        }
        else if (rnd < p2 && rnd >= p3)
        {
            Instantiate(EnemyPrefab1, spawnPositon, transform.rotation);
        }
        else
        {
            Instantiate(EnemyPrefab0, spawnPositon, transform.rotation);
        }

    }

    void BossSpawn()
    {
        Instantiate(BossEnemyPrefab, transform.position, transform.rotation);

        if (data.stagelevel < 2)
        {
            CancelInvoke();
        }
    }

    //軽量化のため設置
    public Savedata LoadPlayerData()
    {
        string datastr = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/savedata.json");
        datastr = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<Savedata>(datastr);
    }

}