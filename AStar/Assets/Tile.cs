using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public bool IsBlock;
    public Color OriginColor;
    public Image Image;
    public RectTransform RectTransform;

    public void Awake()
    {
        Image = gameObject.AddComponent<Image>();
        OriginColor = Color.white;
        RectTransform = Image.rectTransform;
    }

    public void SetColor()
    {
        Image.color = IsBlock ? Color.red : OriginColor;
    }
}