using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MyUtility;

namespace MyExtensionMethods
{
	public static class IEExtensions
	{
		static Random _Rand = new Random ();

		public static T RandomElementAt<T> (this IEnumerable<T> ie)
		{
			return ie.ElementAt(_Rand.Next(ie.Count()));
		}

		public static List<T> RandomElementAt<T> (this IEnumerable<T> ie, int num)
		{
			var numbers = new List<int>();
			for (int i = 0; i < ie.Count(); i++)
				numbers.Add (i);
			var list = new List<T> ();
			for (int i = 0; i < num; i++)
			{
				var index = numbers.ElementAt(_Rand.Next (numbers.Count()));
				list.Add (ie.ElementAt(index));
				numbers.Remove(index);
			}
			return list;
		}
	}

	public static class ArrayExtensions
	{
		static Random _Rand = new Random();

		public static List<T> ToList<T>(this T[,] array)
		{
			var list = new List<T> ();
			foreach (T n in array)
				list.Add (n);
			return list;
		}

		public static T RandomElementAt<T>(this T[,] array)
		{
			var list = array.ToList ();
			return list.RandomElementAt ();
		}

		public static List<T> RandomElementAt<T> (this T[,] array, int num)
		{
			var list = array.ToList ();
			return list.RandomElementAt (num);
		}
	}
}