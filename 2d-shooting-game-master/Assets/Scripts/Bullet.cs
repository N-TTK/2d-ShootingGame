using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Savedata data;
    //爆破エフェクト
    public GameObject explosion;

    public int damage = 500;

    private void Start()
    {
        data = LoadPlayerData();
        damage = data.damage;
    }

    void Update()
    {
        //上に動かす
        transform.position += new Vector3(0, 3f, 0) * Time.deltaTime;

        //撃った弾のy軸が3以上の位置にいったら破壊＊＊＊＊＊＊＊＊＊＊＊＊今回追加
        if (transform.position.y > 3)
        {
            Destroy(gameObject);
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
