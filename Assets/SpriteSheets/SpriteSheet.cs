using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

class SpriteSheet
{
    Dictionary<string, Sprite> spritesByName = new Dictionary<string, Sprite>();
    internal Sprite[] Sprites { get; private set; }
    internal Texture2D Texture { get; private set; }

    internal SpriteSheet(string resourceName)
    {
        var atlas = Resources.Load<SpriteAtlas>(resourceName);
        Sprites = new Sprite[atlas.spriteCount];
        atlas.GetSprites(Sprites);
        foreach (var sprite in Sprites)
        {
            if (Texture == null)
                Texture = sprite.texture;
            var name = sprite.name.Substring(0, sprite.name.Length - 7); //remove (clone) suffix
            sprite.name = name;
            spritesByName[name] = sprite;
        }
    }

    internal Sprite GetSprite(string name)
    {
        Sprite ret;
        if (!spritesByName.TryGetValue(name, out ret))
            return null;
        return ret;
    }
}
