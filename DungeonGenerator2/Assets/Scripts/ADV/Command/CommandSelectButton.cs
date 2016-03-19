using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandSelectButton : ICommand {

	public string Tag
	{
		get { return "select"; }
	}
	
	public void Command(Dictionary<string, string> command)
	{
		var Scenario = ScenarioManager.Instance;
		var select1 = command ["select1"];
		var select2 = command["select2"];

	}

}
