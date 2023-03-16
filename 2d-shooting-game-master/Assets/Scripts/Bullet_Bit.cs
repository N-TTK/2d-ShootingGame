using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Bit : MonoBehaviour
{
    GameObject player;
    public BossEnemyBullet bulletPrefab;
    //爆破エフェクト
    public GameObject explosion;

    public int hp = 5;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Spaceship_Player");
        StartCoroutine(CPU());
    }

    void Shot(float angle, float speed)
    {
        BossEnemyBullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.Setting(angle, speed); // Mathf.PI/4fは45°
    }

    IEnumerator CPU()
    {
        // while(カッコの中がtrueの間繰り返し処理をする)
        while (true)
        {
            yield return new WaitForSeconds(1f);
            yield return WaveNPlayerAimShot(4, 6);
            yield return new WaitForSeconds(1f);
            yield return AssaultBit(3, 5);
            yield return new WaitForSeconds(1f);
            yield return WaveNPlayerAimShot(4, 6);
            yield return new WaitForSeconds(3f);
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

    IEnumerator AssaultBit(int n, int m)
    {
        // 4回8方向に撃ちたい
        for (int w = 0; w < n; w++)
        {
            yield return new WaitForSeconds(1f);
            BitAttack(m, 5);
        }
    }

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
                Shot(angleP, speed);
            }
        }
    }

    void BitAttack(int count, float speed)
    {
        //この弾幕前にplayerが倒されていたら何もしない
        if (player != null)
        {
            Vector3 px = player.transform.position;

            for (int i = 0; i < count; i++)
            {
                transform.LookAt(player.transform);
                transform.position = Vector3.MoveTowards(transform.position, px, speed * Time.deltaTime);
            }
        }
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            hp -= collision.GetComponent<Bullet>().damage;
            Debug.Log(collision.GetComponent<Bullet>().damage);
            Instantiate(explosion, transform.position, transform.rotation);

            if (hp <= 0)
            {
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
                //enemyの機体を破壊
                Destroy(gameObject);
            }
        }
    }
}
