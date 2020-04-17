using System.Collections.Generic;
using UnityEngine;

public class MapData
{
    public MapData(int colums, int rows)
    {
        // Стоит тут указать минимальный размер?

        this.colums = colums;
        this.rows = rows;

        InitializeMap();
    }


    private readonly int colums;
    private readonly int rows;
    private Cell[,] mapCells;
    private List<Cell> freeCells = new List<Cell>();


    public int GetColums() { return colums; }
    public int GetRows() { return rows; }
    public Cell[,] GetAllMapCells() { return mapCells; }
    public Cell GetMapCell(int x, int y) { return mapCells[x, y]; }


    public bool IsCellFree(int x, int y)
    {
        return (mapCells[x, y].GetCellType() != CellType.None);
    }


    public List<Cell> GetFreeCells()
    {
        return freeCells;
    }


    private void RemoveCellFromFreeCellsList(Cell cell)
    {
        freeCells.Remove(cell);
        cell.OnOccupyCell -= RemoveCellFromFreeCellsList;
    }


    private void InitializeMap()
    {
        mapCells = new Cell[colums, rows]; // Задать размер массива

        // Проинициализировать массив
        for (int x = 0; x < colums; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Cell cell = new Cell(new Vector3Int(x, y, 0));
                mapCells[x, y] = cell;

                freeCells.Add(cell);
                cell.OnOccupyCell += RemoveCellFromFreeCellsList;
            }
        }
    }
}


public delegate void OccupyCell(Cell cell);
public class Cell
{
    public event OccupyCell OnOccupyCell;

    public Cell(Vector3Int position) : this(CellType.None, position) { }

    public Cell(CellType cellType, Vector3Int position)
    {
        this.cellType = cellType;
        this.position = position;
    }

    private CellType cellType;
    private Vector3Int position;


    public Vector3Int GetCellPosition() { return position; }
    public CellType GetCellType() { return cellType; }


    public void SetCellType(CellType newCellType)
    {
        if (cellType == CellType.None)
        {
            cellType = newCellType;
            OnOccupyCell?.Invoke(this);
        }
        else
        {
            Debug.LogError("Внимание! Данная ячейка уже занята: " + cellType);
        }
    }
}


public enum CellType
{
    None,
    Wall,
    Floor
}
