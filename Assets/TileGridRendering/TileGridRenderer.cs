using System;
using System.Collections.Generic;
using UnityEngine;

public class TileGridRenderer : MonoBehaviour
{
    public TileGridLayer layerPrefab;

    List<TileGridLayer> layers = new List<TileGridLayer>();
    internal Vector2Int Size { get; private set; }
    internal void Initialise(int width, int height)
    {
        Size = new Vector2Int(width, height);
    }

    internal TileGridLayer AddLayer(SpriteSheetCollection spriteSheet)
    {
        var layer = Instantiate<TileGridLayer>(layerPrefab);
        layer.transform.SetParent(transform);
        layer.transform.position = new Vector3(0, 0, -layers.Count);
        layer.Initialise(Size.x, Size.y, spriteSheet);
        layers.Add(layer);
        return layer;
    }

    /// <summary>
    /// Redraws the grid with any changes made
    /// </summary>
    internal void Redraw()
    {
        foreach (var layer in layers)
        {
            layer.CommitAnyChanges();
        }
    }
}
