using UnityEngine;
using System.Collections;
using MyUtility;

public class Floor
{
	#region FLOOR_FIELD
	
	//階層
//  public int m_id = 0;
	
	//部屋が取り得る最小サイズ
	int minRoomSize;
	public int MinRoomSize
	{
		get { return minRoomSize; }
	}
	
	//部屋が取り得る最大サイズ
	int maxRoomSize;
	public int MaxRoomSize
	{
		get { return maxRoomSize; }
	}

	//部屋の数（横、縦）
	public MyVector2 sequenceSize;
	public MyVector2 SequenceSize
	{
		get { return sequenceSize; }
	}
	
	//区画の余裕
	public const int FREE_SPACE = 2;
	
	//一区画の一辺の長さ
	public int SectionSize { get { return maxRoomSize + FREE_SPACE * 2; } }
	
	//マップ全体の一辺の長さ
	public MyVector2 Overall {
		get { return new MyVector2 (SectionSize * sequenceSize.x, SectionSize * sequenceSize.y); }
	}

	#endregion
	
	Room[,] setRoom (MyVector2 seqSize)
	{
		int x = Random.Range (0, seqSize.x);
		int y = Random.Range (0, seqSize.y);
		var room = new Room[seqSize.x, seqSize.y];
		for (int n = 0; n < seqSize.y; n++)
			for (int m = 0; m < seqSize.x; m++) {
				if (m == x && n == y)
					room [m, n] = new NoRoom (this, new MyVector2 (m, n));
				else
					room [m, n] = new Room (this, new MyVector2 (m, n));
			}
		return room;
	}
	
	int[,] setOverallMapData (MyVector2 seqSize, Room[,] rooms)
	{
		var overallMapData = new int[SectionSize * seqSize.x, SectionSize * seqSize.y];
		for (int n = 0; n < seqSize.y; n++)
			for (int y = 0; y < SectionSize; y++)
				for (int m = 0; m < seqSize.x; m++)
					for (int x = 0; x < SectionSize; x++)
						overallMapData [x + m * SectionSize, y + n * SectionSize] = rooms [m, n].TempCells [x, y];
		return overallMapData;
	}
	
	void createPathFirst (Room[,] rooms)
	{
		for (int n = 0; n < sequenceSize.y - 1; n++)
			for (int m = 0; m < sequenceSize.x - 1; m++) {
				var direction = (Direction)Random.Range (0, 5);
				if (direction == Direction.up) {
					rooms [m, n + 1].TempPath.right = -1;
					rooms [m + 1, n + 1].TempPath.left = -1;
				}
				if (direction == Direction.down) {
					rooms [m, n].TempPath.right = -1;
					rooms [m + 1, n].TempPath.left = -1;
				}
				if (direction == Direction.left) {
					rooms [m, n].TempPath.up = -1;
					rooms [m, n + 1].TempPath.down = -1;
				}
				if (direction == Direction.right) {
					rooms [m + 1, n].TempPath.up = -1;
					rooms [m + 1, n + 1].TempPath.down = -1;
				}
			}
		
		foreach (var n in rooms)
			if (n.TempPath.up == -1 && n.TempPath.down == -1 && n.TempPath.left == -1 && n.TempPath.right == -1) {
				Debug.Log ("もう一回！");
				//nとランダムな隣の部屋を結ぶ
			}
	}

	void connectRoomRow (Room westRoom, Room eastRoom)
	{
		if (westRoom.TempPath.right == -1 && eastRoom.TempPath.left == -1)
			return;
		
		var wPos = westRoom.PathStartingPoint (Direction.right);
		var ePos = eastRoom.PathStartingPoint (Direction.left);

		var wRoomRength = SectionSize - wPos.x;
		var eRoomRength = ePos.x;
		var protrusion = wRoomRength - eRoomRength;
		
		if (protrusion >= 0) {
			for (int i = 0; i < wRoomRength - protrusion / 2; i++)
				westRoom.TempCells [wPos.x + i, wPos.y] = 1;
			for (int i = 0; i <= eRoomRength; i++)
				eastRoom.TempCells [ePos.x - i, ePos.y] = 1;
			for (int i = 0; i < protrusion / 2; i++)
				westRoom.TempCells [SectionSize - protrusion / 2 + i, ePos.y] = 1;
			if (ePos.y > wPos.y)
				for (int i = 0; i <= ePos.y - wPos.y; i++)
					westRoom.TempCells [SectionSize - protrusion / 2 - 1, ePos.y - i] = 1;
			if (ePos.y < wPos.y)
				for (int i = 0; i <= wPos.y - ePos.y; i++)
					westRoom.TempCells [SectionSize - protrusion / 2 - 1, wPos.y - i] = 1;
		}
		
		if (protrusion < 0) {
			for (int i = 0; i < wRoomRength; i++)
				westRoom.TempCells [wPos.x + i, wPos.y] = 1;
			for (int i = 0; i < eRoomRength + protrusion / 2; i++)
				eastRoom.TempCells [ePos.x - i, ePos.y] = 1;
			for (int i = 0; i <= -protrusion / 2; i++)
				eastRoom.TempCells [-protrusion / 2 - i, wPos.y] = 1;				
			if (ePos.y > wPos.y)
				for (int i = 0; i < ePos.y - wPos.y; i++)
					eastRoom.TempCells [-protrusion / 2, ePos.y - i] = 1;
			if (ePos.y < wPos.y)
				for (int i = 0; i <= wPos.y - ePos.y; i++)
					eastRoom.TempCells [-protrusion / 2, wPos.y - i] = 1;
		}
	}
	
	void connectRoomColumn (Room southRoom, Room northRoom)
	{
		if (southRoom.TempPath.up == -1 && northRoom.TempPath.down == -1)
			return;
		
		var sPos = southRoom.PathStartingPoint (Direction.up);
		var nPos = northRoom.PathStartingPoint (Direction.down);
		
		var sRoomRength = SectionSize - sPos.y;
		var nRoomRength = nPos.y;
		var protrusion = sRoomRength - nRoomRength;
		
		if (protrusion >= 0) {
			for (int i = 0; i < sRoomRength - protrusion / 2; i++)
				southRoom.TempCells [sPos.x, sPos.y + i] = 1;
			for (int i = 0; i <= nPos.y; i++)
				northRoom.TempCells [nPos.x, nPos.y - i] = 1;
			for (int i = 0; i < protrusion / 2; i++)
				southRoom.TempCells [nPos.x, SectionSize - 1 - i] = 1;
			if (sPos.x > nPos.x)
				for (int i = 1; i <= sPos.x - nPos.x; i++)
					southRoom.TempCells [sPos.x - i, SectionSize - protrusion / 2 - 1] = 1;
			if (sPos.x < nPos.x)
				for (int i = 0; i < nPos.x - sPos.x; i++)
					southRoom.TempCells [nPos.x - i, SectionSize - protrusion / 2 - 1] = 1;
		}
		
		if (protrusion < 0) {
			for (int i = 0; i < sRoomRength; i++)
				southRoom.TempCells [sPos.x, sPos.y + i] = 1;
			for (int i = 0; i < nRoomRength + protrusion / 2; i++)
				northRoom.TempCells [nPos.x, nPos.y - i] = 1;
			for (int i = 0; i <= -protrusion / 2; i++)
				northRoom.TempCells [sPos.x, -protrusion / 2 - i] = 1;
			if (sPos.x > nPos.x)
				for (int i = 1; i <= sPos.x - nPos.x; i++)
					northRoom.TempCells [sPos.x - i, -protrusion / 2] = 1;
			if (sPos.x < nPos.x)
				for (int i = 0; i < nPos.x - sPos.x; i++)
					northRoom.TempCells [nPos.x - i, -protrusion / 2] = 1;
		}
	}
	
	public Floor (int min, int max, MyVector2 seqSize)
	{
		this.minRoomSize = min;
		this.maxRoomSize = max;
		this.sequenceSize = new MyVector2(seqSize);
	}

	public int[,] CreateCellsNum()
	{
		//部屋の作成
		var rooms = setRoom (sequenceSize);
		
		//部屋を繋ぐ通路を作成（２）
		createPathFirst (rooms);
		
		//部屋を繋ぐ通路を作成（３）
		//隣り合う２つの部屋を選択
		//通路を伸ばしていく
		for (int n = 0; n < sequenceSize.y; n++)
		for (int m = 0; m < sequenceSize.x; m++) {
			if (m != sequenceSize.x - 1)
				connectRoomRow (rooms [m, n], rooms [m + 1, n]);
			if (n != sequenceSize.y - 1)
				connectRoomColumn (rooms [m, n], rooms [m, n + 1]);
		}
		
		//部屋のマップデータを結合
		var overallMapData = setOverallMapData (sequenceSize, rooms);
		
		#region DEBUG_OVERALL_MAP
		
		bool debugOverallMap = false;
		if (debugOverallMap) {
			//デバッグ用
			var testString = new string[Overall.y];
			for (int n = 0; n < Overall.y; n++)
				for (int m = 0; m < Overall.x; m++)
					testString [n] += overallMapData [m, n];
			foreach (var n in testString)
				Debug.Log (n);
		}
		
		#endregion

		return overallMapData;
	}
}
