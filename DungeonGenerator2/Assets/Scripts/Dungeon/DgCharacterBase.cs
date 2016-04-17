using UnityEngine;
using System.Collections;

/// <summary>
/// ダンジョンでのキャラクターを扱うクラス
/// </summary>
public abstract class DgCharacterBase : MonoBehaviour
{
	public Cell CurrentCell { get; set;}

	public Cell NextCell { get; set; }

	public void Visit (Cell cell)
	{
		CurrentCell = cell;
	}

	public void Leave (Cell cell)
	{
		CurrentCell.Remove ();
	}

}
