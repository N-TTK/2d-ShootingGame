using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour
{
    public AudioClip shotSE;
    public GameObject BulletPrefab;
    public GameObject FirePoint;
    public GameObject explosion;
    //public Slider EnergySlider;


    public static float px = 0;//自機位置ｘ外部ファイル参照用
    public static float py = 0;//自機位置ｙ外部ファイル参照用

    public int hp = 10;

    AudioSource audioSource;
    GameController gameController;

    Savedata data;

    // Start is called before the first frame update
    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        data = LoadPlayerData();
        hp = data.hp;
    }

    // Update is called once per frame
    public void Update()
    {
        Move();
        Shot();

        //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊ここから
        //実機の位置に0.7を×ことでラグを発生させている
        px = transform.position.x * 0.7f;//自機狙い弾用
        py = transform.position.y * 0.7f;//自機狙い弾用
        //＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊＊ここまで追加
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 nextPoint = transform.position + new Vector3(x, y, 0) * Time.deltaTime * 4;

        nextPoint = new Vector3(
            Mathf.Clamp(nextPoint.x, -2.9f, 2.9f),
            Mathf.Clamp(nextPoint.y, -2f, 2f),
            0
            );

        transform.position = nextPoint;
    }

    public void Shot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.PlayOneShot(shotSE);
            Instantiate(BulletPrefab, FirePoint.transform.position, transform.rotation);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            hp -= 5;

            if (hp <= 0)
            {
                Destroy(gameObject);
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.GameOver();
            }
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            hp--;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(collision.gameObject);

            if (hp <= 0)
            {
                Destroy(gameObject);
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                gameController.GameOver();
            }
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