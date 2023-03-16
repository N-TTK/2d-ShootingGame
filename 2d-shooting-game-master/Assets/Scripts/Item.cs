using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{

	//　アイテムのアイコン
	[SerializeField]
	private Sprite icon;
	//  アイテムの個数
	[SerializeField]
	private int num;
	//　アイテムの名前
	[SerializeField]
	private string itemName;
	//　アイテムの情報
	[SerializeField]
	private string information;


	public Sprite GetIcon()
	{
		return icon;
	}

	public int GetNum()
	{
		return num;
	}

	public void SetNum(int x)
	{
		num += x;
	}

	public void ResetNum()
	{
		num = 0;
	}

	public string GetItemName()
	{
		return itemName;
	}

	public string GetInformation()
	{
		return information;
	}
}
