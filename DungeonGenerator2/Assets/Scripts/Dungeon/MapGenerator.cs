using MyUtility;

public class MapGenerator
{
	public static Cell[,] CreateCells(Floor floor)
	{
		var sectionSize = floor.SectionSize;
		var sequenceSize = floor.SequenceSize;
		var cells = new Cell[sectionSize * sequenceSize.x, sectionSize * sequenceSize.y];
		var cellsNum = floor.CreateCellsNum ();
		for (int n = 0; n < sectionSize * sequenceSize.y; n++)
			for (int m = 0; m < sectionSize * sequenceSize.x; m++)
		{
			if(cellsNum[m, n] == 0)
				cells[m, n] = new Cell(floor, CellType.Room, new MyVector2(m, n));
			if(cellsNum[m, n] == 1)
				cells[m, n] = new Cell(floor, CellType.Path, new MyVector2(m, n));
			if(cellsNum[m, n] == 9)
				cells[m, n] = new Cell(floor, CellType.Wall, new MyVector2(m, n));
		}
		return cells;
	}
}

