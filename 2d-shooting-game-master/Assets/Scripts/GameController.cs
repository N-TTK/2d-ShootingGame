using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject gameOverText;
    public GameObject gameClearText;
    public PlayerShip player;
    public Guard guard;
    public AudioClip SE;
    public AudioClip GOSE;
    public AudioClip GCSE;

    Savedata data;
    AudioSource audioSource;

    public Text ScoreText;
    public Text HpText;
    public Text GuardHpText;
    public int score = 0;
    public int stage = 0;

    void Start()
    {
        //ゲームオーバーテキストを非表示
        gameOverText.SetActive(false);
        //ゲームクリアテキストを非表示
        gameClearText.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        data = LoadPlayerData();
        stage = data.stage;

        //スコアテキストを表示
        ScoreText.text = "SCORE:" + score;
        HpText.text = "HP: " + player.hp;

        if (stage == 3)
        {
            GuardHpText.text = "GuardHP: " + guard.hp;
        }
    }
    void Update()
    {
        HpText.text = "HP: " + player.hp;

        if (stage == 3)
        {
            GuardHpText.text = "GuardHP: " + guard.hp;

            if (guard.hp <= 0)
            {
                GameOver();
            }
        }

        if (gameOverText.activeSelf == true)
        {
            //もしスペースボタンが押されたらシーンの再読み込み
            if (Input.GetKeyDown("z"))
            {
                audioSource.PlayOneShot(SE);
                Initiate.Fade("Main", Color.black, 1.0f);
            }
        }

        if (gameClearText.activeSelf == true)
        {
            //もしスペースボタンが押されたらシーンの再読み込み
            if (Input.GetKeyDown("z"))
            {
                audioSource.PlayOneShot(SE);
                Initiate.Fade("Main", Color.black, 1.0f);
                SavePlayerData(data);
            }
        }
    }

    //スコア加算メソッド
    public void AddScore(int x)
    {
        score += x;
        ScoreText.text = "SCORE:" + score;
    }

    public void AddBossScore()
    {
        score += 1000;
        ScoreText.text = "SCORE:" + score;
    }

    public void GameClear()
    {
        audioSource.PlayOneShot(GCSE);
        gameClearText.SetActive(true);
    }

    public void GameOver()
    {
        audioSource.PlayOneShot(GOSE);
        gameOverText.SetActive(true);
    }

    //軽量化のため設置
    public void SavePlayerData(Savedata data)
    {
        StreamWriter writer;

        string jsonstr = JsonUtility.ToJson(data);

        writer = new StreamWriter(Application.dataPath + "/savedata.json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }

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
