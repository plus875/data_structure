using System.Collections;
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
    //private Dictionary<int, bool> _blockDictionary = new Dictionary<int, bool>();
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
                bool isBlock = TryRandomSetBlock(ref tile);
                //if (isBlock)
                //    _blockDictionary[x * 10 + y] = true;
                _tiles[x][y] = tile;
            }
        }
    }

    private bool TryRandomSetBlock(ref Tile tile)
    {
        int i = Random.Range(0, 100);
        bool isBlock = i < Rate;
        tile.IsBlock = isBlock;
        tile.SetColor();
        
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
                    _endTile.SetColor();
                tile.Image.color = Color.green;
                _endTile = tile;
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
                _startTile = tile;
                _startTile.Image.color = Color.gray;
            }
        }
    }

    List<Tile> _openList = new List<Tile>();
    List<Tile> _closeList = new List<Tile>();
    public void StartFindPath()
    {

    }

    private int CalcFValue()
    {
        int f = 0;

        return f;
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
            SetEndTile();
        }
        if (Input.GetMouseButtonDown(1))
        {
            SetStartTile();
        }
    }
}