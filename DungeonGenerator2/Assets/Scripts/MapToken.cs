using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// キャラクター基底クラス
public class MapToken : MonoBehaviour {
	
	/// プレハブ取得
	/// プレハブは必ず"Resources/Prefabs/"に配置すること
	public static GameObject GetPrefab(GameObject prefab, string name) {
		return prefab ?? (prefab = Resources.Load("Prefabs/"+name) as GameObject);
	}

	/// インスタンスを生成してスクリプトを返す
	public static Type CreateInstance<Type>(GameObject prefab, Vector3 p, float direction=0.0f, float speed=0.0f) where Type : MapToken {
		GameObject g = Instantiate(prefab, p, Quaternion.identity) as GameObject;
		Type obj = g.GetComponent<Type>();
		return obj;
	}
	public static Type CreateInstance2<Type>(GameObject prefab, float x, float y, float direction=0.0f, float speed=0.0f) where Type : MapToken {
		Vector3 pos = new Vector3(x, y, 0);
		return CreateInstance<Type>(prefab, pos, direction, speed);
	}
	/// 生存フラグ
	bool _exists = false;
	public bool Exists {
		get { return _exists; }
		set { _exists = value; }
	}

	/// アクセサ
	/// レンダラー
	SpriteRenderer _renderer = null;
	public SpriteRenderer Renderer
	{
		get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
	}
	/// 描画フラグ
	public bool Visible
	{
		get { return Renderer.enabled; }
		set { Renderer.enabled = value; }
	}
	/// ソーティングレイヤー名
	public string SortingLayer
	{
		get { return Renderer.sortingLayerName; }
		set { Renderer.sortingLayerName = value; }
	}
	/// ソーティング・オーダー
	public int SortingOrder
	{
		get { return Renderer.sortingOrder; }
		set { Renderer.sortingOrder = value; }
	}
	/// 座標(X)
	public float X
	{
		set { Vector3 pos = transform.position; pos.x = value; transform.position = pos; }
		get { return transform.position.x; }
	}
	/// 座標(Y)
	public float Y
	{
		set { Vector3 pos = transform.position; pos.y = value; transform.position = pos; }
		get { return transform.position.y; }
	}
	/// 座標を足し込む
	public void AddPosition(float dx, float dy) {
		X += dx;
		Y += dy;
	}
	/// 座標を設定する
	public void SetPosition(float x, float y) {
		Vector3 pos = transform.position;
		pos.Set(x, y, 0);
		transform.position = pos;
	}
	/// スケール値(X)

	/// スケール値(Y)

	/// スケール値を設定

	/// スケール値(X/Y)


	/// 移動量(X)

	/// 移動量(Y)

	/// 方向

	/// スプライトの設定
	public void setSprite(string filename)
	{
		Sprite sprite;
		sprite = Resources.Load<Sprite> (filename) as Sprite;
		Debug.Log (sprite);
		GetComponent<Image> ().sprite = sprite;

	}

	/// サイズを設定
	float _width = 0.0f;
	float _height = 0.0f;
	public void SetSize(float width, float height) {
		_width = width;
		_height = height;
	}
	/// 消滅（メモリから削除）
	public void DestroyObj() {
		Destroy(gameObject);
	}
	/// アクティブにする
	public void Revive() {
		gameObject.SetActive(true);
		Exists = true;
		Visible = true;
	}
	/// 消滅する
	public void Vanish() {
		gameObject.SetActive(false);
		Exists = false;
	}
}
