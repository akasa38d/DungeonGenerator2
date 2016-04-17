using UnityEngine;
using System.Collections;
using MyUtility;

public class Cell
{
	#region CELL_FIELD
	
	Floor floor;

	//位置
	MyVector2 pos;
	public MyVector2 Pos
	{
		get{ return pos; }
	}

	//乗っているオブジェクト
	DgObject obj;
	public DgObject Obj
	{
		set { obj = value; }
	}

	//初期の種類
	CellType defaultType;
	public CellType DefaultType
	{
		get { return defaultType; }
	}

	//侵入可能かどうか
	public bool Enterable
	{
		get	{ return (obj == DgObject.NullObject); }
	}

	//属する部屋の番号
	public MyVector2 RoomNum
	{
		get	{ return new MyVector2(Pos.x / floor.SectionSize, Pos.y / floor.SectionSize); }
	}

	public DgCharacterBase Character { get; set; }
	public DgItem Item { get; set; }
	public DgTrap Trap { get; set; }

	#endregion

	//コンストラクタ
	public Cell(Floor floor, CellType cellType, MyVector2 pos)
	{
		this.floor = floor;
		defaultType = cellType;
		if (cellType == CellType.Room || cellType == CellType.Room)
			obj = DgObject.NullObject;
		if (cellType == CellType.Wall)
			obj = DgObject.Wall;
		this.pos = pos;
	}

	//キャラクターの進入時のイベント
	public void Accept(DgCharacterBase character)
	{

	}

	//キャラクターの設置時のイベント
	public void AcceptFirst(DgCharacterBase character)
	{
		obj = DgObject.Character;
		Character = character;
		Debug.Log (obj);
	}

	//キャラクターが離れた時のイベント
	public void Remove()
	{
		obj = DgObject.NullObject;
		Character = null;
	}
}

//ダンジョン内のオブジェクトの列挙
public enum DgObject
{
	NullObject,
	Wall,
	Character
}

//セルのタイプの列挙
public enum CellType
{
	Room,
	Path,
	Wall
}
