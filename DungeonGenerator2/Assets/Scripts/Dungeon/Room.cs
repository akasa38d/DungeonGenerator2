using UnityEngine;
using System.Collections;
using MyUtility;

public class Room
{
	#region ROOM_FIELD

	//部屋のサイズ
	protected MyVector2 size;

	//始点（左上の座標）
	protected MyVector2 startingPoint;
	public MyVector2 StartingPoint
	{
		get { return startingPoint; }
	}

	//終点（右下の座標＋１）
	public MyVector2 EndingPoint
	{
		get{ return new MyVector2 (startingPoint.x + size.x, startingPoint.y + size.y); }
	}

	//数値化したマップ
	public int[,] TempCells { get; set; }

	//通路を作るためのクラス
	public Path TempPath { get; set; }

	#endregion
	
	//通路の最初の座標を返す
	public MyVector2 PathStartingPoint (Direction dir)
	{
		if (dir == Direction.up)
			return new MyVector2 (TempPath.up, EndingPoint.y);
		if (dir == Direction.down)
			return new MyVector2 (TempPath.down, startingPoint.y - 1);
		if (dir == Direction.left)
			return new MyVector2 (startingPoint.x - 1, TempPath.left);
		if (dir == Direction.right)
			return new MyVector2 (EndingPoint.x, TempPath.right);
		return null;
	}
	
	public class Path
	{
		public int up, down, left, right;

		public Path (MyVector2 startingPoint)
		{
			up = startingPoint.x;
			down = startingPoint.x;
			left = startingPoint.y;
			right = startingPoint.y;
		}

		public Path (MyVector2 startingPoint, MyVector2 endingPoint)
		{
			up = Random.Range (startingPoint.x, endingPoint.x);
			down = Random.Range (startingPoint.x, endingPoint.x);
			left = Random.Range (startingPoint.y, endingPoint.y);
			right = Random.Range (startingPoint.y, endingPoint.y);
		}
	}
	
	MyVector2 setSize (Floor floor)
	{
		var min = floor.MinRoomSize;
		var max = floor.MaxRoomSize;
		var sizeX = Random.Range (min, max + 1);
		var sizeY = Random.Range (min, max + 1);
		return new MyVector2 (sizeX, sizeY);
	}
	
	protected MyVector2 setStartingPoint (Floor floor, MyVector2 size)
	{
		var sectionSize = floor.SectionSize;
		var space = Floor.FREE_SPACE;
		var startX = Random.Range (space, sectionSize - space - size.x + 1);
		var startY = Random.Range (space, sectionSize - space - size.y + 1);
		return new MyVector2 (startX, startY);
	}

	protected virtual int[,] createMapData (Floor floor, MyVector2 startingPoint, MyVector2 endingPoint)
	{
		var sectionSize = floor.SectionSize;
		var mapData = new int[sectionSize, sectionSize];
		for (int n = 0; n < sectionSize; n++) {
			for (int m = 0; m < sectionSize; m++) {
				if (m >= startingPoint.x && m < endingPoint.x
					&& n >= startingPoint.y && n < endingPoint.y)
					mapData [m, n] = 0;		//空間
				else
					mapData [m, n] = 9;		//壁
			}
		}
		return mapData;
	}
	
	//部屋の最小サイズ、部屋の最大サイズ、マップ全体で見た部屋の位置
	public Room (Floor floor, MyVector2 position)
	{
		size = setSize (floor);
		
		startingPoint = setStartingPoint (floor, size);

		TempPath = new Path (startingPoint, EndingPoint);
		
		//デバッグ
		#region DEBUG_ROOM
		
		bool debugRoom = true;
		var pos = new MyVector2 (position);
		if (debugRoom) {
			Debug.Log ((pos.x + 0) + "の" + (pos.y + 0) + "のサイズは" + size.x + "の" + size.y
				+ "で、始点は" + startingPoint.x + "の" + startingPoint.y + "、"
				+ "終点は" + EndingPoint.x + "の" + EndingPoint .y + "です");
		}
		
		#endregion
		
		//数字にしてマップデータを管理
		TempCells = createMapData (floor, startingPoint, EndingPoint);		
	}
}

public class NoRoom : Room
{
	public NoRoom (Floor floor, MyVector2 position) : base(floor, position)
	{
		size = new MyVector2 (1, 1);
		startingPoint = setStartingPoint (floor, size);
		TempPath = new Path (startingPoint);
		TempCells = createMapData (floor, startingPoint, startingPoint);
	}

	protected override int[,] createMapData (Floor floor, MyVector2 startingPoint, MyVector2 endingPoint)
	{
		var sectionSize = floor.SectionSize;
		var mapData = new int[sectionSize, sectionSize];
		for (int n = 0; n < sectionSize; n++) {
			for (int m = 0; m < sectionSize; m++) {
				if (m == startingPoint.x && n == startingPoint.y)
					mapData [m, n] = 1;		//道
				else
					mapData [m, n] = 9;		//壁
			}
		}
		return mapData;
	}
}
