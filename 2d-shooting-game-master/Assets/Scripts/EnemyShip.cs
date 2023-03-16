using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 敵が弾を発射する
 * ・弾を作る
 * ・弾の移動を実装する
 * ・発射ポイントを作る // なし
 * ・敵から弾を生成する
 * ・Playerが弾に当たったら破壊される
 * 　・弾にコライダーをつける
 * ・敵が自分の弾に当たったら破壊されるバグの修正
 */

public class EnemyShip : MonoBehaviour
{
    //爆破エフェクト
    public GameObject explosion;

    public GameObject bulletPrefab;

    ItemManager im;

    Savedata data;

    //GameControllerのAddScoreメソッドを使用するため入れ物を用意
    GameController gameController;

    public int hp = 1;
    public int stagelevel = 1;

    public int score = 100;
    public int p0 = 98;
    public int p1 = 93;
    public int p2 = 85;
    public int p3 = 75;
    public int p4 = 0;

    float offset;

    void Start()
    {
        //揺れ方をランダムにする
        offset = Random.Range(0, 2f * Mathf.PI);
        InvokeRepeating("Shot", 1f, 1f);
        //GameObject.Find("")でカッコ内のオブジェクトを取得し、GetComponentでそのオブジェクトの指定した部品を取得してくる
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        data = im.LoadPlayerData();
        stagelevel = data.stagelevel;

        switch (stagelevel)
        {
            case 1:
                hp = 1;
                break;
            case 2:
                hp = 20;
                break;
            case 3:
                hp = 50;
                break;
            default:
                break;

        }
    }

    void Shot()
    {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }


    //
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            hp -= collision.GetComponent<Bullet>().damage;
            Debug.Log(collision.GetComponent<Bullet>().damage);
            Instantiate(explosion, transform.position, transform.rotation);

            if (hp <= 0)
            {
                //スコア加算
                gameController.AddScore(score);
                //アイテムドロップ
                dropitem();
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                //enemyの機体を破壊
                Destroy(gameObject);
            }
        }
        else if(collision.tag == "Player")
        {
            //スコア加算
            gameController.AddScore(score);
            //アイテムドロップ
            dropitem();
            //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
            Instantiate(explosion, transform.position, transform.rotation);
            //enemyの機体を破壊
            Destroy(gameObject);
        }
    }

    void dropitem()
    {
        int rnd = Random.Range(1, 100);
        int rndi = Random.Range(0, 4);
        if (rnd >= p0)
        {
            im.AddE(rndi);
        }
        else if (rnd < p0 && rnd >= p1)
        {
            im.AddD(rndi);
        }
        else if (rnd < p1 && rnd >= p2)
        {
            im.AddC(rndi);
        }
        else if (rnd < p2 && rnd >= p3)
        {
            im.AddB(rndi);
        }
        else if (rnd < p3 && rnd >= p4)
        {
            im.AddA(rndi);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左右に揺れながら、下に移動する
        transform.position -= new Vector3(
            Mathf.Cos(Time.frameCount * 0.05f + offset) * 0.01f,
            Time.deltaTime,
            0);

        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }
}