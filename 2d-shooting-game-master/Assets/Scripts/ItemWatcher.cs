using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemWatcher : MonoBehaviour
{
    private ItemManager im;

    public Text TextA;
    public Text TextB;
    public Text TextC;
    public Text TextD;
    public Text TextE;

    // Start is called before the first frame update
    public void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SetAll();
    }

    public void SetAll()
    {
        TextA.text = "×" + Convert.ToString(im.GetItem("残骸A").GetNum());
        TextB.text = "×" + Convert.ToString(im.GetItem("残骸B").GetNum());
        TextC.text = "×" + Convert.ToString(im.GetItem("残骸C").GetNum());
        TextD.text = "×" + Convert.ToString(im.GetItem("残骸D").GetNum());
        TextE.text = "×" + Convert.ToString(im.GetItem("残骸E").GetNum());
    }
}
