using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Gernerater : MonoBehaviour
{
    public Transform Container;
    public GraphicRaycaster Raycaster;
    public bool CanWalkSideling;

    public int xCount = 50;
    public int yCount = 50;

    public int padding = 2;
    public int tileSize = 8;

    public float Rate = 10;

    private AStarPath _astarPath;
    public AStarPath AStarPath
    {
        get
        {
            if(_astarPath == null)
                _astarPath = new AStarPath();
            return _astarPath;
        }
    }

    void Start ()
    {
        AStarPath.Tiles = new Tile[xCount][];
        GenerateTileMap();
    }
    
    private void GenerateTileMap()
    {
        for (int x = 0; x < xCount; x++)
        {
            AStarPath.Tiles[x] = new Tile[yCount];
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
                AStarPath.Tiles[x][y] = tile;
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
                if (AStarPath.EndTile) AStarPath.EndTile.SetColor(false, false);
                AStarPath.EndTile = tile;
                AStarPath.EndTile.SetColor(false, true);
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
                if (AStarPath.StartTile) AStarPath.StartTile.SetColor(false, false);
                AStarPath.StartTile = tile;
                AStarPath.StartTile.SetColor(true, false);
            }
        }
    }

    public void StartFindPath()
    {
        AStarPath.DoFindPath(xCount, yCount, CanWalkSideling);
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