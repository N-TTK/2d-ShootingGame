using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Guard : MonoBehaviour
{
    public GameObject explosion;

    public int hp = 10;

    Savedata data;

    // Start is called before the first frame update
    public void Start()
    {
        data = LoadPlayerData();
        //hp = data.guardhp;
    }

    // Update is called once per frame
    public void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hp--;

            if (hp <= 0)
            {
                Destroy(gameObject);
                //破壊する時に爆破エフェクト生成（生成したいもの、場所、回転）
                Instantiate(explosion, transform.position, transform.rotation);
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
