using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CommandUpdatePlace : ICommand {

	public string Tag
	{
		get { return "place"; }
	}

	public void Command(Dictionary<string,string> command)
	{
		var Scenario = ScenarioManager.Instance;
		var pName = command ["pName"];
		Scenario.RequestPlaceName (pName);
	}
}
