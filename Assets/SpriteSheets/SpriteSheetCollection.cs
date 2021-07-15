using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSheetCollection
{
    Dictionary<string, SpriteInfo> sprites = new System.Collections.Generic.Dictionary<string, SpriteInfo>();
    List<SpriteSheet> spriteSheets = new List<SpriteSheet>();
    internal List<SpriteSheet> SpriteSheets
    {
        get
        {
            return spriteSheets;
        }
    }

    internal void Add(SpriteSheet spriteSheet)
    {
        spriteSheets.Add(spriteSheet);
        foreach (var sprite in spriteSheet.Sprites)
        {
            //try to prevent the name clashing with an existing sprite (eg in another sheet)
            //by appending _1, _2 etc to the name
            var name = GetUniqueSpriteName(sprite.name);
            sprites[name] = new SpriteInfo(sprite, spriteSheets.Count-1);
        }
    }

    string GetUniqueSpriteName(string name)
    {
        string ret = name;
        for (int i = 0; i < 10; i++)
        {
            if (!sprites.ContainsKey(name))
            {
                break;
            }
            ret = name + "" + (i + 1);
        }
        //after enough retries give up trying to find a unique name
        return ret;
    }

    internal bool GetSprite(string name, out SpriteInfo spriteInfo)
    {
        if (!sprites.TryGetValue(name, out spriteInfo))
            return false;
        return true;
    }
}

struct SpriteInfo
{
    internal Sprite Sprite;
    internal int SheetIndex;

    public SpriteInfo(Sprite sprite, int sheetIndex)
    {
        Sprite = sprite;
        SheetIndex = sheetIndex;
    }
}