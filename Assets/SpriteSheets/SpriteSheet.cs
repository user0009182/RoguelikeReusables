using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.U2D;

class SpriteSheet
{
    Dictionary<string, SpriteInfo> spritesByName = new Dictionary<string, SpriteInfo>();
    internal List<SpriteInfo> Sprites { get; private set; } = new List<SpriteInfo>();
    internal Texture2D Texture { get; private set; }

    internal SpriteSheet(string resourceName)
    {
        var atlas = Resources.Load<SpriteAtlas>(resourceName);
        Sprite[] sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(sprites);
        foreach (var sprite in sprites)
        {
            if (Texture == null)
                Texture = sprite.texture;
            var name = sprite.name.Substring(0, sprite.name.Length - 7); //remove (clone) suffix
            sprite.name = name;
            var spriteInfo = new SpriteInfo(sprite);
            Sprites.Add(spriteInfo);
            spritesByName[name] = spriteInfo;
        }
    }

    internal SpriteInfo GetSprite(string name)
    {
        SpriteInfo ret;
        if (!spritesByName.TryGetValue(name, out ret))
            return null;
        return ret;
    }
}

class SpriteInfo
{
    internal Sprite Sprite;
    internal Vector2[] Uv;

    public SpriteInfo(Sprite sprite)
    {
        Sprite = sprite;
        var minX = sprite.uv.Min(uv => uv.x);
        var minY = sprite.uv.Min(uv => uv.y);
        var maxX = sprite.uv.Max(uv => uv.x);
        var maxY = sprite.uv.Max(uv => uv.y);
        Uv = new Vector2[4];
        Uv[0] = new Vector2(minX, minY);
        Uv[1] = new Vector2(minX, maxY);
        Uv[2] = new Vector2(maxX, maxY);
        Uv[3] = new Vector2(maxX, minY);
    }
}