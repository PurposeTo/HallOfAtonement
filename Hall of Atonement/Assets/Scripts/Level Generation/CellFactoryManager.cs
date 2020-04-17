using UnityEngine.Tilemaps;
using UnityEngine;

public class CellFactoryManager : MonoBehaviour
{
    public static CellFactoryManager instance;

    public TileBase WallAndFloorTile;

    private void Awake() //делаем объект синглтоном
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }


    public TileBase GetTileProduct(CellType cellType)
    {
        if (cellType == CellType.None)
        {
            return null;
        }
        else if (cellType == CellType.Wall)
        {
            // Стены и пол мы заполняем с помощью RuleTile
            return null;
        }
        else
        {
            Debug.LogError("В CellFactory не найден Tile типа: " + cellType);
            return null;
        }
    }


    public TileBase GetWallandFloorTile()
    {
        return WallAndFloorTile;
    }

}
