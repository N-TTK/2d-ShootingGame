using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ItemManager : MonoBehaviour
{

	//　アイテムデータベース
	[SerializeField]
	private ItemDataBase itemDataBase;

	Savedata data = new Savedata();

	/* デバッグ用
	public void Awake()
	{
		string filepath = Application.dataPath + "/savedata.json";

		if (!File.Exists(filepath))
		{
			Zerodata();
		}

		data = LoadPlayerData();
	}
	*/

	// アイテムを追加する処理（A~E）
	public void AddA(int a)
	{
		GetItem("残骸A").SetNum(a);
		Debug.Log(GetItem("残骸A").GetNum());
	}

	public void AddB(int b)
	{
		GetItem("残骸B").SetNum(b);
		Debug.Log(GetItem("残骸B").GetNum());
	}

	public void AddC(int c)
	{
		GetItem("残骸C").SetNum(c);
		Debug.Log(GetItem("残骸C").GetNum());
	}

	public void AddD(int d)
	{
		GetItem("残骸D").SetNum(d);
		Debug.Log(GetItem("残骸D").GetNum());
	}

	public void AddE(int e)
	{
		GetItem("残骸E").SetNum(e);
		Debug.Log(GetItem("残骸E").GetNum());
	}

	//　セーブ・ロード関係
	public void Zerodata()
    {
		data.hp = 10;
		data.damage = 1;
		data.counts += 0;
		data.countd += 0;
		data.countb += 0;
		data.stagelevel = 0;
		data.stage = 0;
		data.guardhp = 10;

		GetItem("残骸A").ResetNum();
		GetItem("残骸B").ResetNum();
		GetItem("残骸C").ResetNum();
		GetItem("残骸D").ResetNum();
		GetItem("残骸E").ResetNum();

		SavePlayerData(data);
	}

	//  セーブ
	public void SavePlayerData(Savedata data)
	{
		StreamWriter writer;

		string jsonstr = JsonUtility.ToJson(data);

		writer = new StreamWriter(Application.dataPath + "/savedata.json", false);
		writer.Write(jsonstr);
		writer.Flush();
		writer.Close();
	}

	//  ロード
	public Savedata LoadPlayerData()
	{
		string datastr = "";
		StreamReader reader;
		reader = new StreamReader(Application.dataPath + "/savedata.json");
		datastr = reader.ReadToEnd();
		reader.Close();

		return JsonUtility.FromJson<Savedata>(datastr);
	}

	//　名前でアイテムを取得
	public Item GetItem(string searchName)
	{
		return itemDataBase.GetItemLists().Find(itemName => itemName.GetItemName() == searchName);
	}
}
