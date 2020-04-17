using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

class SimpleLevelGeneration : MonoBehaviour
{
    public Tilemap BaseTilemap; //Базовый тайлмап, на котором располагаются пол и стены
    public Tilemap CommunityTilemap; //Тайлмап, на котором располагаются персонажи
    public Tilemap ObstacleTilemap; //Тайлмап, на котором располагаются персонажи

    private MapData mapData;

    private void Start()
    {
        mapData = new MapData(16, 16);

        SetWalls();
        BuildRoom();
        //SetObstacle();
    }


    private void BuildRoom()
    {
        // Расположить RuleTile по всей комнате (так как комната квадратная)

        int MapDataColums = mapData.GetColums();
        int MapDataRows = mapData.GetRows();

        for (int i = 0; i < MapDataColums; i++)
        {
            for (int j = 0; j < MapDataRows; j++)
            {
                Cell cell = mapData.GetMapCell(i, j);

                BaseTilemap.SetTile(cell.GetCellPosition(), CellFactoryManager.instance.WallAndFloorTile);
            }
        }
    }


    private void SetObstacle()
    {
        int MapDataColums = mapData.GetColums();
        int MapDataRows = mapData.GetRows();

        for (int i = 0; i < MapDataColums; i++)
        {
            for (int j = 0; j < MapDataRows; j++)
            {
                Cell cell = mapData.GetMapCell(i, j);
                CellType cellType = cell.GetCellType();

                ObstacleTilemap.SetTile(cell.GetCellPosition(), CellFactoryManager.instance.GetTileProduct(cellType));
            }
        }
    }


    private void SetWalls()
    {
        int MapDataColums = mapData.GetColums();
        int MapDataRows = mapData.GetRows();

        for (int i = 0; i < MapDataColums; i++)
        {
            for (int j = 0; j < MapDataRows; j++)
            {
                if (i == 0 || i == mapData.GetColums() - 1 || (j == 0 || j == mapData.GetRows() - 1))
                {
                    mapData.GetMapCell(i, j).SetCellType(CellType.Wall); // Ставим стены
                }
            }
        }
    }
}
