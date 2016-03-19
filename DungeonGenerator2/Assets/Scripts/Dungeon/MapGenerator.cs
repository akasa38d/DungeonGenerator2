using UnityEngine;
using System.Collections;

public class MapGenerator : SingletonMonoBehaviour<MapGenerator>
{
	[SerializeField]
	GameObject Square;

	[SerializeField]
	GameObject Wall;

	[SerializeField]
	[Range(5, 9)]
	int minRoomSize = 5;	//部屋の一辺のマス目の数の最小値

	[SerializeField]
	[Range(10,15)]
	int maxRoomSize = 10;	//部屋の一辺のマス目の数の最大値

	[SerializeField]
	[Range(2,5)]
	public int mapRow = 3;			//部屋の数（横）

	[SerializeField]
	[Range(2,5)]
	public int mapColumn = 4;		//部屋の数（縦）

	public class Floor
	{
		public int _id;		//階層

		public int _minRoomSize;

		public int _maxRoomSize;

		public MyVector2 _sequenceSize = new MyVector2 ();	//部屋の数（横、縦）

		public int Size
		{
			get { return _sequenceSize.x * _sequenceSize.y ; }
		}

		public void GetRoomSize(int min, int max)
		{
			_minRoomSize = min;
			_maxRoomSize = max;
		}

		public void GetSequenceSize(MyVector2 sequenceSize)
		{
			_sequenceSize = sequenceSize;
		}
	}

	public class Room
	{
		public MyVector2 _size;				//部屋のサイズ
		public MyVector2 _position;			//部屋のシークエンス
		public MyVector2 _startingPoint;	//始点（左上の座標）
		MapGenerator _mapGenerator;

		Path _path;
		public class Path
		{
			public int up, down, left, right;
			protected Path(int u, int d, int l, int r)
			{
				up = u;
				down = d;
				left = l;
				right = r;
			}
			public static Path RandomCreatePath(MyVector2 size)
			{
				return new Path(Random.Range(0, size.x), Random.Range(0, size.x),
				                Random.Range(0, size.y), Random.Range(0, size.y));
			}
		}

		public Room(MyVector2 size, MyVector2 position)
		{
			_mapGenerator = MapGenerator.Instance;
			_size = new MyVector2(size);
			_position = new MyVector2(position);

			//始点（左上の座標）の決定
			int startX = Random.Range(0, _mapGenerator.maxRoomSize - size.x + 1);
			int startY = Random.Range(0, _mapGenerator.maxRoomSize - size.y + 1);
			_startingPoint = new MyVector2(startX, startY);
		}
	}



	#region UNITY_CALLBACK

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	#endregion
}
