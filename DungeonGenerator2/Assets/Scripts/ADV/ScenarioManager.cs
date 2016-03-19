using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

[RequireComponent(typeof(TextController))]
public class ScenarioManager : SingletonMonoBehaviour<ScenarioManager>
{
    public string LoadFileName;

    string[] m_scenarios;
    int m_currentLine = 0;
    bool m_isCallPreload = false;

    TextController m_textController;
    CommandController m_commandController;

    void RequestNextLine()
    {
        var currentText = m_scenarios[m_currentLine];

        m_textController.SetNextLine(CommandProcess(currentText));
        m_currentLine++;
        m_isCallPreload = false;
    }

    public void UpdateLines(string fileName)
    {
        var scenarioText = Resources.Load<TextAsset>("Scenario/" + fileName);

        if (scenarioText == null)
        {
            Debug.LogError("シナリオファイルが見つかりませんでした");
            Debug.LogError("ScenarioManagerを無効化します");
            enabled = false;
            return;
        }

        m_scenarios = scenarioText.text.Split(new string[] { "@br" }, System.StringSplitOptions.None);
        m_currentLine = 0;

		foreach (var t in m_scenarios) {
			Debug.Log (t);
		}


        Resources.UnloadAsset(scenarioText);
    }

    string CommandProcess(string line)
    {
        var lineReader = new StringReader(line);
        var lineBuilder = new StringBuilder();
        var text = string.Empty;

        while ((text = lineReader.ReadLine()) != null)
        {
            var commentCharacterCount = text.IndexOf("//");
            if (commentCharacterCount != -1)
            {
                text = text.Substring(0, commentCharacterCount);
            }

            if (!string.IsNullOrEmpty(text))
            {
                if (text[0] == '@' && m_commandController.LoadCommand(text))
                    continue;
                lineBuilder.AppendLine(text);
            }
        }

        return lineBuilder.ToString();
    }

	public void RequestPlaceName(string placeName)
	{
		m_textController.SetPlaceName (placeName);
	}

	public void RequestSelectButton(string select1, string select2)
	{

	}

    #region UNITY_CALLBACK

    // Use this for initialization
    void Start()
    {
        m_textController = GetComponent<TextController>();
        m_commandController = GetComponent<CommandController>();

        UpdateLines(LoadFileName);
        RequestNextLine();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_textController.IsCompleteDisplayText)
        {
            if (m_currentLine < m_scenarios.Length)
            {
                if (!m_isCallPreload)
                {
                    m_commandController.PreLoadCommand(m_scenarios[m_currentLine]);
                    m_isCallPreload = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    RequestNextLine();
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_textController.ForceCompleteDisplayText();
            }
        }
    }

    #endregion
}
