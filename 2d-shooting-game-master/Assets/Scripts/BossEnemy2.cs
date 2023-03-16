using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy2 : MonoBehaviour
{
    GameObject player;
    public BossEnemyBullet bulletPrefab;
    public GameObject EnemyPrefab0;
    public GameObject EnemyPrefab1;
    public GameObject EnemyPrefab2;
    public GameObject EnemyPrefab3;
    public GameObject EnemyPrefab4;

    ItemManager im;
    Savedata data;

    //爆破エフェクト
    public GameObject explosion;
    //GameControllerのAddScoreメソッドを使用するため入れ物を用意
    GameController gameController;

    public int hp = 50;
    public int stagelevel = 1;
    float offset;

    void Start()
    {
        player = GameObject.Find("Spaceship_Player");
        StartCoroutine(CPU());

        offset = Random.Range(0, 2f * Mathf.PI);
        //GameObject.Find("")でカッコ内のオブジェクトを取得し、GetComponentでそのオブジェクトの指定した部品を取得してくる
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        data = im.LoadPlayerData();
        stagelevel = data.stagelevel;

        switch (stagelevel)
        {
            case 1:
                hp = 100;
                break;
            case 2:
                hp = 3000;
                break;
            case 3:
                hp = 10000;
                break;
            default:
                break;

        }
    }

    void Update()
    {
        //左右に揺れる
        transform.position -= new Vector3(
            Mathf.Sin(Time.frameCount * 0.05f + offset) * 0.01f, 0, 0);
    }

    void Shot(float angle, float speed)
    {
        BossEnemyBullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Setting(angle, speed); // Mathf.PI/4fは45°
    }

    IEnumerator CPU()
    {
        // 特定の位置より上だったら
        while (transform.position.y > 1f)
        {
            transform.position -= new Vector3(0, 1, 0) * Time.deltaTime;
            yield return null; //1フレーム(0.02秒)待つ 
        }

        // while(カッコの中がtrueの間繰り返し処理をする)
        while (true)
        {
            yield return WaveNShotM(4, 8);
            yield return new WaitForSeconds(1f);
            yield return SummonEnemy(4);
            yield return new WaitForSeconds(1f);
            yield return WaveNPlayerAimShot(3, 5);
            yield return new WaitForSeconds(1f);
            yield return SummonEnemy(8);
            yield return new WaitForSeconds(1f);
            yield return WaveNShotMCurve(4, 16);
            yield return new WaitForSeconds(1f);
            yield return SummonEnemy(4);
            yield return new WaitForSeconds(1f);
            yield return WaveNPlayerAimShot(5, 7);
            yield return new WaitForSeconds(1f);
            yield return SummonEnemy(8);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator WaveNShotM(int n, int m)
    {
        // 4回8方向に撃ちたい
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(0.3f);
            ShotN(m, 2);
        }
    }

    IEnumerator WaveNShotMCurve(int n, int m)
    {
        // 4回8方向に撃ちたい
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(0.3f);
            yield return ShotNCurve(m, 2);
        }
    }

    IEnumerator WaveNPlayerAimShot(int n, int m)
    {
        // 4回8方向に撃ちたい
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(1f);
            PlayerAimShot(m, 3);
        }
    }

    IEnumerator SummonEnemy(int count)
    {
        for (int w = 0; w < count; w++)
        {
            yield return new WaitForSeconds(1f);
            Spawn();
        }
    }

    void ShotN(int count, float speed)
    {
        int bulletCount = count;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (2 * Mathf.PI / bulletCount); // 2PI：360
            Shot(angle, speed);
        }
    }

    IEnumerator ShotNCurve(int count, float speed)
    {
        int bulletCount = count;
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (2 * Mathf.PI / bulletCount); // 2PI：360
            Shot(angle - Mathf.PI / 2f, speed);
            Shot(-angle - Mathf.PI / 2f, speed);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Playerを狙う
    // ・Playerの位置取得
    // ・どの角度に撃てばいいのかを計算
    void PlayerAimShot(int count, float speed)
    {
        //この弾幕前にplayerが倒されていたら何もしない
        if (player != null)
        {
            // 自分からみたPlayerの位置を計算する
            Vector3 diffPosition = player.transform.position - transform.position;
            // 自分から見たPlayerの角度を出す：傾きから角度を出す：アークタンジェントを使う
            float angleP = Mathf.Atan2(diffPosition.y, diffPosition.x);

            int bulletCount = count;
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = (i - bulletCount / 2f) * ((Mathf.PI / 2f) / bulletCount); // PI/2f：90


                Shot(angleP + angle, speed);
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

        if (rnd >= 81)
        {
            Instantiate(EnemyPrefab4, spawnPositon, transform.rotation);
        }
        else if (rnd < 81 && rnd >= 61)
        {
            Instantiate(EnemyPrefab3, spawnPositon, transform.rotation);
        }
        else if (rnd < 61 && rnd >= 41)
        {
            Instantiate(EnemyPrefab2, spawnPositon, transform.rotation);
        }
        else if (rnd < 41 && rnd >= 21)
        {
            Instantiate(EnemyPrefab1, spawnPositon, transform.rotation);
        }
        else
        {
            Instantiate(EnemyPrefab0, spawnPositon, transform.rotation);
        }

    }

    //Bossの当たり判定
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //playerとBossが接触した時
        if (collision.CompareTag("Player") == true)
        {
            //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
            Instantiate(explosion, collision.transform.position, transform.rotation);
            Destroy(collision.gameObject);

            gameController.GameOver();
        }
        else if (collision.tag == "Bullet")
        {
            //Bossのダメージ判定
            hp -= collision.GetComponent<Bullet>().damage;

            //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
            Instantiate(explosion, collision.transform.position, transform.rotation);
            Destroy(collision.gameObject);

            if (hp <= 0)
            {
                //enemyの機体を破壊
                Destroy(gameObject);
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, transform.rotation);

                gameController.AddBossScore();
                dropitem();
                dropitem();
                dropitem();
                dropitem();
                dropitem();
                gameController.GameClear();
            }
        }
    }

    void dropitem()
    {
        int rnd = Random.Range(1, 100);
        int rndi = Random.Range(0, 4);
        if (rnd >= 81)
        {
            im.AddE(rndi);
        }
        else if (rnd < 81 && rnd >= 61)
        {
            im.AddD(rndi);
        }
        else if (rnd < 61 && rnd >= 41)
        {
            im.AddC(rndi);
        }
        else if (rnd < 41 && rnd >= 21)
        {
            im.AddB(rndi);
        }
        else
        {
            im.AddA(rndi);
        }
    }
}
