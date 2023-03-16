using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PowerUp : MonoBehaviour
{
    private ItemManager im;
    public GameObject button;
    public AudioClip SE;

    Savedata data;
    AudioSource audioSource;

    //それぞれの強化項目を行った回数
    public int cs = 0;
    public int cd = 0;
    public int cb = 0;

    private bool shp = false;
    private bool dam = false;
    private bool bhp = false;

    public Text detail;
    public Text syozi;
    public Text TextA;
    public Text TextB;
    public Text TextC;
    public Text TextD;
    public Text TextE;
    public Text ButtonText;

    // Start is called before the first frame update
    public void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        audioSource = GetComponent<AudioSource>();
        data = im.LoadPlayerData();
        SetAll();
        button.SetActive(false);
        cs = data.counts;
        cd = data.countd;
        cb = data.countb;
    }

    public void PushS()
    {
        shp = true;
        dam = false;
        bhp = false;
        SetText();
        audioSource.PlayOneShot(SE);
        button.SetActive(true);
    }

    public void PushD()
    {
        shp = false;
        dam = true;
        bhp = false;
        SetText();
        audioSource.PlayOneShot(SE);
        button.SetActive(true);
    }

    public void PushB()
    {
        shp = false;
        dam = false;
        bhp = true;
        SetText();
        audioSource.PlayOneShot(SE);
        button.SetActive(true);
    }

    void SetText()
    {
        syozi.text = "必要数";

        if (shp == true)
        {
            detail.text = "プレイヤーが操作する機体のHPが上昇します。";
            TextA.text = "×" + Convert.ToString((5 + (cs * 30)));
            TextB.text = "×" + Convert.ToString((5 + (cs * 20)));
            TextC.text = "×" + Convert.ToString((5 + (cs * 20)));
            TextD.text = "×" + Convert.ToString(0);
            TextE.text = "×" + Convert.ToString(0);
        }
        else if (dam == true)
        {
            detail.text = "プレイヤーが発射する弾の威力が上昇します。";
            TextA.text = "×" + Convert.ToString(0);
            TextB.text = "×" + Convert.ToString((5 + (cd * 30)));
            TextC.text = "×" + Convert.ToString((5 + (cd * 20)));
            TextD.text = "×" + Convert.ToString((5 + (cd * 10)));
            TextE.text = "×" + Convert.ToString(0);
        }
        else if (bhp == true)
        {
            detail.text = "防衛作戦における防衛対象のHPが上昇します。";
            TextA.text = "×" + Convert.ToString(0);
            TextB.text = "×" + Convert.ToString(0);
            TextC.text = "×" + Convert.ToString((5 + (cb * 30)));
            TextD.text = "×" + Convert.ToString((5 + (cb * 30)));
            TextE.text = "×" + Convert.ToString((5 + (cb * 5)));
        }
    }

    public void PowUp()
    {
        audioSource.PlayOneShot(SE);

        if (shp == true)
        {
            ShpUp();
        }
        else if (dam == true)
        {
            DamegeUp();
        }
        else if (bhp == true)
        {
            BhpUp();
        }
    }

    //プレイヤーが操作する機体のHP増加
    void ShpUp()
    {
        int a = im.GetItem("残骸A").GetNum();
        int b = im.GetItem("残骸B").GetNum();
        int c = im.GetItem("残骸C").GetNum();

        if (a >= (5 + ( cs * 30 )) && b >= (5 + (cs * 20)) && c >= (5 + (cs * 20)))
        {
            im.GetItem("残骸A").SetNum(-5 - (cs * 30));
            im.GetItem("残骸B").SetNum(-5 - (cs * 20));
            im.GetItem("残骸C").SetNum(-5 - (cs * 20));
            cs++;
            data.counts++;
            data.hp += 5;
            ButtonText.text = "続けて強化";
            SetText();
            im.SavePlayerData(data);
        }
        else
        {
            detail.text = "素材が足りません。";
            SetAll();
            button.SetActive(false);
        }
    }

    //プレイヤーが操作する機体の発射する弾のダメージ増加
    void DamegeUp()
    {
        int b = im.GetItem("残骸B").GetNum();
        int c = im.GetItem("残骸C").GetNum();
        int d = im.GetItem("残骸D").GetNum();

        if (b >= (5 + (cd * 30)) && c >= (5 + (cd * 20)) && d >= (5 + (cd * 10)))
        {
            im.GetItem("残骸B").SetNum(-5 - (cd * 30));
            im.GetItem("残骸C").SetNum(-5 - (cd * 20));
            im.GetItem("残骸D").SetNum(-5 - (cd * 10));
            cd++;
            data.countd++;
            data.damage++;
            ButtonText.text = "続けて強化";
            SetText();
            im.SavePlayerData(data);
        }
        else
        {
            detail.text = "素材が足りません。";
            SetAll();
            button.SetActive(false);
        }
    }

    //防衛作戦で登場する護衛用オブジェクトのHP増加
    void BhpUp()
    {
        int c = im.GetItem("残骸C").GetNum();
        int d = im.GetItem("残骸D").GetNum();
        int e = im.GetItem("残骸E").GetNum();

        if (c >= (5 + (cb * 30)) && d >= (5 + (cb * 30)) && e >= (5 + (cb * 5)))
        {
            im.GetItem("残骸C").SetNum(-5 - (cb * 30));
            im.GetItem("残骸D").SetNum(-5 - (cb * 30));
            im.GetItem("残骸E").SetNum(-5 - (cb * 5));
            cb++;
            data.countb++;
            data.guardhp += 10;
            ButtonText.text = "続けて強化";
            SetText();
            im.SavePlayerData(data);
        }
        else
        {
            detail.text = "素材が足りません。";
            SetAll();
            button.SetActive(false);
        }
    }

    public void SetAll()
    {
        syozi.text = "所持数";
        TextA.text = "×" + Convert.ToString(im.GetItem("残骸A").GetNum());
        TextB.text = "×" + Convert.ToString(im.GetItem("残骸B").GetNum());
        TextC.text = "×" + Convert.ToString(im.GetItem("残骸C").GetNum());
        TextD.text = "×" + Convert.ToString(im.GetItem("残骸D").GetNum());
        TextE.text = "×" + Convert.ToString(im.GetItem("残骸E").GetNum());
    }

    //メイン画面に戻る
    public void ChangeMainScene()
    {
        audioSource.PlayOneShot(SE);
        SceneManager.LoadScene("Main");
    }
}
