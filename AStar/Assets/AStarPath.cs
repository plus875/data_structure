using System.Collections.Generic;
using UnityEngine;

public class AStarPath
{
    public Tile[][] Tiles;
    public Tile EndTile;
    public Tile StartTile;

    public void ResetTiles()
    {
        _openList.Clear();
        _closeList.Clear();

        foreach (Tile[] tiles in Tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.G = tile.H = 0;
                tile.SetColor(tile == StartTile, tile == EndTile);
            }
        }
    }

    public void DrawResult()
    {
        var tile = EndTile.Parent;
        while (tile != null)
        {
            if (tile == StartTile) break;

            tile.Image.color = Color.blue;
            tile = tile.Parent;
        }
    }


    List<Tile> _openList = new List<Tile>();
    List<Tile> _closeList = new List<Tile>();

    public void DoFindPath(int xCount, int yCount, bool canWalkSideling)
    {
        ResetTiles();

        if (!StartTile || !EndTile)
        {
            Debug.LogWarning("no start point or no end point");
            return;
        }

        _openList.Add(StartTile);

        bool end = false;
        int count = 0;

        while (_openList.Count > 0)
        {
            if (count > 100000)
            {
                Debug.LogError("loop break");
                break;
            }

            if (end)
            {
                DrawResult();
                break;
            }

            var curTile = _openList[0];
            _openList.RemoveAt(0);

            count++;
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    //斜着走
                    if (!canWalkSideling)
                        if (i != 0 && j != 0)
                            continue;
                    int xCoor = curTile.CoordinateX + i;
                    int yCoor = curTile.CoordinateY + j;
                    if (xCoor < 0 || xCoor >= xCount || yCoor < 0 || yCoor >= yCount) continue;

                    var tile = Tiles[xCoor][yCoor];
                    if (tile.IsBlock)
                    {
                        _closeList.Add(tile);
                    }
                    else if (!_closeList.Contains(tile))
                    {
                        if (tile.G == 0)
                        {
                            _openList.Add(tile);
                            tile.Parent = curTile;
                            tile.G = curTile.G + CalcGValue(tile, curTile, canWalkSideling);
                            tile.H = CalcHValue(tile);
                        }
                        else if (_openList.Contains(tile))
                        {
                            var fromCurToG = curTile.G + CalcGValue(tile, curTile, canWalkSideling);
                            if (fromCurToG < tile.G)
                            {
                                tile.Parent = curTile;
                                tile.G = fromCurToG;
                                tile.H = CalcHValue(tile);
                            }
                        }

                        if (tile == EndTile)
                        {
                            end = true;
                            break;
                        }
                    }
                }
            }

            _openList.Sort();
            _closeList.Add(curTile);

            if (_openList.Count == 0)
            {
                Debug.LogWarning("Can not Find");
            }
        }
    }

    private int CalcGValue(Tile curTile, Tile lastTile, bool canWalkSideling)
    {
        int g;
        //if (curTile.Parent)
        {
            if (canWalkSideling)
                g = (int)(Mathf.Sqrt(Mathf.Pow(curTile.CoordinateX - lastTile.CoordinateX, 2) +
                                     Mathf.Pow(curTile.CoordinateY - lastTile.CoordinateY, 2)) * 10);
            else
                g = Mathf.Abs((curTile.CoordinateX - lastTile.CoordinateX) * 10) +
                    Mathf.Abs((curTile.CoordinateY - lastTile.CoordinateY) * 10);
        }
        return g;
    }

    private int CalcHValue(Tile tile)
    {
        var h = Mathf.Abs((EndTile.CoordinateX - tile.CoordinateX) * 10) +
                Mathf.Abs((EndTile.CoordinateY - tile.CoordinateY) * 10);
        return h;
    }

}
