using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    Tile[,] _tiles;


    public int GetWidth()
    {
        return _tiles.GetLength(0);
    }
    public int GetHeigth()
    {
        return _tiles.GetLength(1);
    }
    public Tile GetTile(int x_coord, int y_coord)
    {
        return _tiles[x_coord,y_coord];
    }
    public void SetTiles(Tile[,] newTiles)
    {
        _tiles = newTiles;
    }

}
