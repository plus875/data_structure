using System;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IComparable<Tile>
{
    public bool IsBlock;
    public Color OriginColor;
    public Image Image;
    public RectTransform RectTransform;
    public Tile Parent;

    public int CoordinateX;
    public int CoordinateY;

    public int F
    {
        get { return G + H; }
    }
    public int G;
    public int H;

    public void Awake()
    {
        Image = gameObject.AddComponent<Image>();
        OriginColor = Image.color;
        RectTransform = Image.rectTransform;
    }

    public void SetColor(bool isStart, bool isEnd)
    {
        if (isStart)
            Image.color = Color.yellow;
        else if(isEnd)
            Image.color = Color.green;
        else
            Image.color = IsBlock ? Color.red : OriginColor;
    }

    public int CompareTo(Tile tile)
    {
        if (this.F < tile.F)
        {
            //升序
            return -1;
        }

        if (this.F > tile.F)
        {
            //降序
            return 1;
        }

        return 0;
    }
}