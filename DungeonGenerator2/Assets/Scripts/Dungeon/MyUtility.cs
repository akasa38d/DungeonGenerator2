using UnityEngine;
using System.Collections;

namespace MyUtility
{
	public class MyVector2
	{
		public int x;
		public int y;

		public MyVector2 (int x = 0, int y = 0)
		{
			this.x = x;
			this.y = y;
		}

		public MyVector2 (MyVector2 myVector2)
		{
			this.x = myVector2.x;
			this.y = myVector2.y;
		}

		public bool IsEqual (MyVector2 myVector2)
		{
			if (this.x == myVector2.x && this.y == myVector2.y) {
				return true;
			}
			return false;
		}

		public Vector3 ToVector3(float height)
		{
			return new Vector3 ((float)this.x, height, (float)this.y);
		}
		public Vector3 ToVector3(int height)
		{
			return new Vector3 ((float)this.x, (float)height, (float)this.y);
		}
		public Vector3 ToVector3(double height)
		{
			return new Vector3 ((float)this.x, (float)height, (float)this.y);
		}
	}



//方向の列挙
	public enum Direction
	{
		up,
		down,
		left,
		right,
		noDir
	}
}
