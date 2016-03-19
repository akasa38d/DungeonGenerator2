using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour
{
    [SerializeField]
    [Range(0.001f, 0.3f)]
    float textSpeed = 0.05f;			//文字送りの速度

    string currentText = string.Empty;	//現在の文字列
    float timeUntilDisplay = 0;			//表示にかかる時間
    float timeElapsed = 1;				//文字列の表示を開始した時間
    int lastUpdateCharacter = -1;		//表示中の文字数

    [SerializeField]
    Text _uiText;
	[SerializeField]
	Text _topText;

    // 文字の表示が完了しているかどうか
    public bool IsCompleteDisplayText
    {
        get { return Time.time > timeElapsed + timeUntilDisplay; }
    }

    //強制的に全文表示する
    public void ForceCompleteDisplayText()
    {
        timeUntilDisplay = 0;
    }

    //次に表示する文字列をセットする
    public void SetNextLine(string text)
    {
        currentText = text;
        timeUntilDisplay = currentText.Length * textSpeed;
        timeElapsed = Time.time;
        lastUpdateCharacter = -1;
    }

	public void SetPlaceName(string placeName)
	{
		_topText.text = placeName;
	}

    #region UNITY_CALLBACK

    public void Update()
    {
        int displayCharacterCount =
            (int)(Mathf.Clamp01((Time.time - timeElapsed) / timeUntilDisplay) * currentText.Length);
        if (displayCharacterCount != lastUpdateCharacter)
        {
            _uiText.text = currentText.Substring(0, displayCharacterCount);
            lastUpdateCharacter = displayCharacterCount;
        }
    }

    #endregion

}
