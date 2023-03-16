using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    void Update()
    {
        //Updateが呼び出される度に弾が動く
        transform.position += new Vector3(0, -3f, 0) * Time.deltaTime;
        //指定範囲外に出たら弾(object)を壊す
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}