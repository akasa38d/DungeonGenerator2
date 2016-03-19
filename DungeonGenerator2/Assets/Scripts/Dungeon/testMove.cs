using UnityEngine;
using System.Collections;

public class testMove : MonoBehaviour {

	public float speed = 5;

	#region UNITY_CALLBACK

	// Update is called once per frame
	void Update () {	
		// 右・左
		float x = Input.GetAxisRaw ("Horizontal");
		
		// 上・下
		float y = 0;

		float z = -Input.GetAxisRaw ("Vertical");
		
		// 移動する向きを求める
		Vector3 direction = new Vector3 (x, y, z).normalized;
		
		// 移動する向きとスピードを代入する
		GetComponent<Rigidbody>().velocity = direction * speed;
	}

	#endregion

}
