using UnityEngine;
using System.Collections;

public class MyVector2
{
	public int x;
	public int y;

	public MyVector2(int x = 0, int y = 0)
	{
		this.x = x;
		this.y = y;
	}
	public MyVector2(MyVector2 myVector2)
	{
		this.x = myVector2.x;
		this.y = myVector2.y;
	}
	public bool IsEqual(MyVector2 myVector2)
	{
		if (this.x == myVector2.x && this.y == myVector2.y)
		{
			return true;
		}
		return false;
	}
}
