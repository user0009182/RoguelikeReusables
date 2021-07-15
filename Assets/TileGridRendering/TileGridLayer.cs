using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGridLayer : MonoBehaviour
{
    bool isModified;

    SpriteSheetCollection spriteSheet;
    TileGridMesh mesh;
    internal void Initialise(int width, int height, SpriteSheetCollection spriteSheet)
    {
        this.spriteSheet = spriteSheet;
        GetComponent<MeshRenderer>().sharedMaterial.mainTexture = spriteSheet.SpriteSheets[0].Texture;
        mesh = new TileGridMesh(width, height);
        GetComponent<MeshFilter>().mesh = mesh.Mesh;
    }

    internal void SetSprite(int x, int y, string name)
    {
        SetSprite(x, y, name, Color.white);
    }

    internal void SetSprite(int x, int y, string name, Color color)
    {
        Sprite sprite = null;
        if (name != null)
        {
            SpriteInfo spriteInfo;
            if (!spriteSheet.GetSprite(name, out spriteInfo))
            {
                Debug.Log("Could not find sprite " + name);
                return;
            }
            sprite = spriteInfo.Sprite;
        }
        
        //TODO use spriteInfo.SheetIndex to indicate index into different texture
        mesh.SetSprite(x, y, sprite, color);
        isModified = true;
    }

    internal void CommitAnyChanges()
    {
        if (isModified)
        {
            mesh.CommitChanges();
            isModified = false;
        }
    }
}
