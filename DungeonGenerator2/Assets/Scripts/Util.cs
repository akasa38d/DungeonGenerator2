using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// 様々なユーティリティ
public class Util
{
    /// Mathf.Cosの角度指定版
    public static float CosEx(float Deg)
    {
        return Mathf.Cos(Mathf.Deg2Rad * Deg);
    }
    /// Mathf.Sinの角度指定版
    public static float SinEx(float Deg)
    {
        return Mathf.Sin(Mathf.Deg2Rad * Deg);
    }

    /// 入力方向を取得する
    public static Vector2 GetInputVector()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }

    /// トークンを動的生成する
    public static MapToken CreateMapToken(float x, float y, string SpriteFile, string SpriteName, string objName = "MapToken")
    {
		GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
		GameObject token = DgGenerator.Instance.mapToken;
		GameObject obj = GameObject.Instantiate(token, new Vector3(x, 0, y), Quaternion.identity) as GameObject;
		obj.transform.SetParent(canvas.transform);
		obj.AddComponent<MapToken> ();

        MapToken t = obj.GetComponent<MapToken>();
        // スプライト設定
		t.setSprite ("wall1");


        // 座標を設定
        t.X = x;
        t.Y = y;

        return t;
    }

    /// スプライトをリソースから取得する
    /// ※スプライトは「Resources/Sprites」以下に配置していなければなりません
    /// ※fileNameに空文字（""）を指定するとシングルスプライトから取得します
    public static Sprite GetSprite(string fileName, string spriteName)
    {
        if (spriteName == "")
        {
            // シングルスプライト
            return Resources.Load<Sprite>(fileName);
        }
        else
        {
            // マルチスプライト
            Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
            return System.Array.Find<Sprite>(sprites, (sprite) => sprite.name.Equals(spriteName));
        }
    }

    private static Rect _guiRect = new Rect();
    static Rect GetGUIRect()
    {
        return _guiRect;
    }
    private static GUIStyle _guiStyle = null;
    static GUIStyle GetGUIStyle()
    {
        return _guiStyle ?? (_guiStyle = new GUIStyle());
    }
    /// フォントサイズを設定
    public static void SetFontSize(int size)
    {
        GetGUIStyle().fontSize = size;
    }
    /// フォントカラーを設定
    public static void SetFontColor(Color color)
    {
        GetGUIStyle().normal.textColor = color;
    }
    /// ラベルの描画
    public static void GUILabel(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        GUI.Label(rect, text, GetGUIStyle());
    }
    // ボタンの描画
    public static bool GUIButton(float x, float y, float w, float h, string text)
    {
        Rect rect = GetGUIRect();
        rect.x = x;
        rect.y = y;
        rect.width = w;
        rect.height = h;

        return GUI.Button(rect, text, GetGUIStyle());
    }
}
