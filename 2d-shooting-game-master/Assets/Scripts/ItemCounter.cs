using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCounter : MonoBehaviour
{
    private ItemManager im;
    //  アイテムの個数などを表記するテキスト
    public Text TextA;
    public Text TextB;

    private void Start()
    {
        im = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    public void CountA()
    {
        TextA.text = im.GetItem("残骸A").GetItemName() + ": 所持数" + im.GetItem("残骸A").GetNum();
        TextB.text = im.GetItem("残骸A").GetInformation();
    }

    public void CountB()
    {
        TextA.text = im.GetItem("残骸B").GetItemName() + ": 所持数" + im.GetItem("残骸B").GetNum();
        TextB.text = im.GetItem("残骸B").GetInformation();
    }

    public void CountC()
    {
        TextA.text = im.GetItem("残骸C").GetItemName() + ": 所持数" + im.GetItem("残骸C").GetNum();
        TextB.text = im.GetItem("残骸C").GetInformation();
    }

    public void CountD()
    {
        TextA.text = im.GetItem("残骸D").GetItemName() + ": 所持数" + im.GetItem("残骸D").GetNum();
        TextB.text = im.GetItem("残骸D").GetInformation();
    }

    public void CountE()
    {
        TextA.text = im.GetItem("残骸E").GetItemName() + ": 所持数" + im.GetItem("残骸E").GetNum();
        TextB.text = im.GetItem("残骸E").GetInformation();
    }
}
