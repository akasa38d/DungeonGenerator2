using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class CommandJumpNextScenario : ICommand
{
    public string Tag
    {
        get { return "jump"; }
    }

    public void Command(Dictionary<string, string> command)
    {
        var Scenario = ScenarioManager.Instance;
        var fileName = command["fileName"];
        Scenario.UpdateLines(fileName);
    }
}
