using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gernerater : MonoBehaviour
{
    public Transform Container;
    public GraphicRaycaster Raycaster;

    public int xCount = 50;
    public int yCount = 50;

    public int padding = 2;
    public int tileSize = 8;

    public float Rate = 10;

    private Tile[][] _tiles;
    private Tile _endTile;
    private Tile _startTile;

    void Start ()
    {
        _tiles = new Tile[xCount][];
        GenerateTileMap();
    }

    private void GenerateTileMap()
    {
        
        for (int x = 0; x < xCount; x++)
        {
            _tiles[x] = new Tile[yCount];
            for (int y = 0; y < yCount; y++)
            {
                GameObject go = new GameObject(string.Format("tile_{0}_{1}", x, y));
                var tile = go.AddComponent<Tile>();
                tile.RectTransform.SetParent(Container);
                tile.RectTransform.sizeDelta = new Vector2(tileSize, tileSize);
                tile.RectTransform.localPosition = new Vector3(x * (tileSize + padding), y * (tileSize + padding), 0);
                tile.CoordinateX = x;
                tile.CoordinateY = y;
                bool isBlock = TryRandomSetBlock(ref tile);
                _tiles[x][y] = tile;
            }
        }
    }

    private bool TryRandomSetBlock(ref Tile tile)
    {
        int i = Random.Range(0, 100);
        bool isBlock = i < Rate;
        tile.IsBlock = isBlock;
        tile.SetColor(false, false);
        
        return isBlock;
    }

    public void SetEndTile()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        eventData.pressPosition = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        Raycaster.Raycast(eventData, list);
        if (list.Count > 0)
        {
            var tile = list[list.Count - 1].gameObject.GetComponent<Tile>();
            if (tile && !tile.IsBlock)
            {
                if (_endTile)
                    _endTile.SetColor(false, false);
                _endTile = tile;
                _endTile.SetColor(false, true);
            }
        }
    }

    public void SetStartTile()
    {
        var eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        eventData.pressPosition = Input.mousePosition;
        List<RaycastResult> list = new List<RaycastResult>();
        Raycaster.Raycast(eventData, list);
        if (list.Count > 0)
        {
            var tile = list[list.Count - 1].gameObject.GetComponent<Tile>();
            if (tile && !tile.IsBlock)
            {
                if (_startTile)
                    _startTile.SetColor(false, false);
                _startTile = tile;
                _startTile.SetColor(true, false);
            }
        }
    }

    List<Tile> _openList = new List<Tile>();
    List<Tile> _closeList = new List<Tile>();
    public void StartFindPath()
    {
        Reset();

        if (!_startTile || !_endTile)
        {
            Debug.LogWarning("no start point or no end point");
            return;
        }

        _openList.Add(_startTile);

        bool end = false;
        int count = 0;

        while (_openList.Count > 0)
        {
            if(count > 100000)
            {
                Debug.LogError("loop break");
                break;
            }
            if (end)
            {
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
                    int xCoor = curTile.CoordinateX + i;
                    int yCoor = curTile.CoordinateY + j;
                    if (xCoor < 0 || xCoor >= xCount || yCoor < 0 || yCoor >= yCount) continue;

                    var tile = _tiles[xCoor][yCoor];
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
                            tile.G = curTile.G + CalcGValue(tile, curTile);
                            tile.H = CalcHValue(tile);
                        }
                        else if(_openList.Contains(tile))
                        {
                            var fromCurToG = curTile.G + CalcGValue(tile, curTile);
                            if (fromCurToG < tile.G)
                            {
                                tile.Parent = curTile;
                                tile.G = fromCurToG;
                                tile.H = CalcHValue(tile);
                            }   
                        }

                        if (tile == _endTile)
                        {
                            end = true;
                            break;
                        }
                    }
                }
            }

            _openList.Sort();
            _closeList.Add(curTile);
        }

        DrawResult();
    }

    private void Reset()
    {
        _openList.Clear();
        _closeList.Clear();
        foreach (Tile[] tiles in _tiles)
        {
            foreach (Tile tile in tiles)
            {
                tile.G = tile.H = 0;
                tile.SetColor(tile == _startTile, tile == _endTile);
            }
        }
    }

    private void DrawResult()
    {
        var tile = _endTile.Parent;
        while (tile != null)
        {
            if (tile == _startTile) break;

            tile.Image.color = Color.blue;
            tile = tile.Parent;
        }
    }

    private int CalcGValue(Tile curTile, Tile lastTile)
    {
        int g = 0;
        //if (curTile.Parent)
        {
            g = (int) (Mathf.Sqrt(Mathf.Pow(curTile.CoordinateX - lastTile.CoordinateX, 2) +
                           Mathf.Pow(curTile.CoordinateY - lastTile.CoordinateY, 2)) * 10);
        }
        return g;
    }

    private int CalcHValue(Tile tile)
    {
        int h = 0;
        h = Mathf.Abs((_endTile.CoordinateX - tile.CoordinateX) * 10) +
            Mathf.Abs((_endTile.CoordinateY - tile.CoordinateY) * 10);
        return h;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            foreach (var t in Container)
            {
                Destroy((t as Transform).gameObject);
            }

            GenerateTileMap();
        }

        if (Input.GetMouseButtonDown(0))
        {
            SetStartTile();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetEndTile();
        }
    }
}