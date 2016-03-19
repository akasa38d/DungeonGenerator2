using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

public class CommandController : SingletonMonoBehaviour<CommandController>
{
    //文字を解析しながら呼び出すコマンド
    readonly List<ICommand> m_commandList = new List<ICommand>()
	{
		new CommandUpdateImage(),	// name=オブジェクト名 image=イメージ名 
		new CommandJumpNextScenario(),	// fileName=シナリオ名
		new CommandUpdatePlace(),	//pName=場所の名前
		new CommandSelectButton(),	//select1=選択肢１のメッセージ select2=選択肢２のメッセージ
	};

    //文字の表示が完了した時点で呼ばれる処理
    List<IPreCommand> m_preCommandList = new List<IPreCommand>();

    public void PreLoadCommand(string line)
    {
        var dic = CommandAnalytics(line);
        foreach (var command in m_preCommandList)
            if (command.Tag == dic["tag"])
                command.PreCommand(dic);
    }

    public bool LoadCommand(string line)
    {
        var dic = CommandAnalytics(line);
        foreach (var command in m_commandList)
        {
            if (command.Tag == dic["tag"])
            {
                command.Command(dic);
                return true;
            }
        }
        return false;
    }

    //コマンドを解析
    Dictionary<string, string> CommandAnalytics(string line)
    {
        Dictionary<string, string> command = new Dictionary<string, string>();

        //コマンド名を取得
        var tag = Regex.Match(line, "@(\\S+)\\s");
        command.Add("tag", tag.Groups[1].ToString());

        //コマンドのパラメータを取得
        var regex = new Regex("(\\S+)=(\\S+)");
        var matches = regex.Matches(line);
        foreach (Match match in matches)
            command.Add(match.Groups[1].ToString(), match.Groups[2].ToString());

        return command;
    }

    #region UNITY_CALLBACK

    new void Awake()
    {
        base.Awake();

        //PreCommandを取得
        foreach (var command in m_commandList)
        {
            if (command is IPreCommand)
                m_preCommandList.Add((IPreCommand)command);
        }
    }

    #endregion
}
