using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MyUtility;
using System.Linq;
using MyExtensionMethods;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
	#region MAPGENERATOR_FIELD
	
	[SerializeField]
	GameObject SquarePrefab;
	
	[SerializeField]
	GameObject WallPrefab;
	
	[SerializeField]
	GameObject TestSquarePrefab;

	[SerializeField]
	GameObject TestPlayerPrefab;

	[SerializeField]
	GameObject TestEnemyPrefab;

	//階層を管理するクラス（あとでいっぱい作る）
	Floor testFloor;

	int currentFloor = 1;

	Cell[,] m_cells;

	//ここから仮データ
	[SerializeField]
	[Range(5, 9)]
	int minRoomSize = 5;	//部屋の一辺の長さの最小値	
	[SerializeField]
	[Range(10, 15)]
	int maxRoomSize = 10;	//部屋の一辺の長さの最大値	
	[SerializeField]
	[Range(2, 10)]
	public int mapRow = 3;			//部屋の数（横）	
	[SerializeField]
	[Range(2, 10)]
	public int mapColumn = 2;		//部屋の数（縦）
		
	#endregion

	#region UNITY_CALLBACK
	
	// Use this for initialization
	void Start()
	{
		//フロアデータの読み込み
		testFloor = new Floor(minRoomSize, maxRoomSize, new MyVector2(mapRow, mapColumn));

		//マップデータの作成
		m_cells = MapGenerator.CreateCells(testFloor);

		//マップデータからゲームオブジェクトを作成
		for (int n = 0; n < testFloor.Overall.y; n++)
			for (int m = 0; m < testFloor.Overall.x; m++)
		{
			if (m_cells[m, n].DefaultType == CellType.Room)
				Instantiate(SquarePrefab, new Vector3(m, 0, n), Quaternion.identity);
			if (m_cells[m, n].DefaultType == CellType.Path)
				Instantiate(TestSquarePrefab, new Vector3(m, 0, n), Quaternion.identity);
			if (m_cells[m, n].DefaultType == CellType.Wall)
				Instantiate(WallPrefab, new Vector3(m, 0, n), Quaternion.identity);
		}

		//キャラクターの設置
		var iniCells = m_cells.ToList()
			.Where(n => n.DefaultType == CellType.Room && n.Enterable == true)
				.RandomElementAt (5);
		var player = Instantiate (TestPlayerPrefab, iniCells.ElementAt(0).Pos.ToVector3(0.5), Quaternion.identity) as GameObject;
		iniCells.ElementAt (0).AcceptFirst (player.GetComponent<DgPlayer>());
		for (int i = 1; i < 5; i++)
		{
			var enemy = Instantiate (TestEnemyPrefab, iniCells.ElementAt (i).Pos.ToVector3 (0.5), Quaternion.identity) as GameObject;
			iniCells.ElementAt(i).AcceptFirst(enemy.GetComponent<DgEnemy>());
		}
	}
	
	#endregion
}
