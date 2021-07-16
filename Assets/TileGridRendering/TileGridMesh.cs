using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class TileGridMesh
{
    Mesh mesh;
    List<Vector3> vertices = new List<Vector3>();
    List<Vector2> uvs = new List<Vector2>();
    List<int> triangles = new List<int>();
    List<Color> colors = new List<Color>();

    internal Vector2Int Size { get; private set; }
    public Mesh Mesh
    {
        get
        {
            return mesh;
        }
    }

    internal TileGridMesh(int width, int height)
    {
        mesh = new Mesh();
        Initialize(width, height);
    }

    internal void BindToSpriteSheets(SpriteSheet[] spriteSheets)
    {

    }

    internal void SetSprite(int x, int y, SpriteInfo spriteInfo)
    {
        SetSprite(x, y, spriteInfo, Color.white);
    }

    internal void SetSprite(int x, int y, SpriteInfo spriteInfo, Color color)
    {
        int uvIndex = (x * Size.y + y) * 4;
        
        if (spriteInfo == null)
        {
            colors[uvIndex] = Color.clear;
            colors[uvIndex+1] = Color.clear;
            colors[uvIndex+2] = Color.clear;
            colors[uvIndex+3] = Color.clear;
            return;
        }

        colors[uvIndex] = color;
        colors[uvIndex + 1] = color;
        colors[uvIndex + 2] = color;
        colors[uvIndex + 3] = color;
        
        uvs[uvIndex] = spriteInfo.Uv[0];
        uvs[uvIndex+1] = spriteInfo.Uv[1];
        uvs[uvIndex+2] = spriteInfo.Uv[2];
        uvs[uvIndex+3] = spriteInfo.Uv[3];
        return;
        //var minX = sprite.uv.Min(uv => uv.x);
        //var minY = sprite.uv.Min(uv => uv.y);
        //var maxX = sprite.uv.Max(uv => uv.x);
        //var maxY = sprite.uv.Max(uv => uv.y);
        //uvs[uvIndex] = new Vector2(minX, minY);
        //uvs[uvIndex+1] = new Vector2(minX, maxY);
        //uvs[uvIndex+2] = new Vector2(maxX, maxY);
        //uvs[uvIndex+3] = new Vector2(maxX, minY);
    }

    internal void CommitChanges()
    {
        mesh.SetUVs(0, uvs);
        mesh.SetColors(colors);
    }

    internal void Initialize(int width, int height)
    {
        Size = new Vector2Int(width, height);
        vertices.Clear();
        uvs.Clear();
        triangles.Clear();
        for (int x = 0; x < Size.x; x++)
            for (int y = 0; y < Size.y; y++)
            {
                var i = vertices.Count;
                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i + 2);
                triangles.Add(i + 2);
                triangles.Add(i + 3);
                triangles.Add(i);
                vertices.Add(new Vector3(x, y, 0));
                vertices.Add(new Vector3(x, y + 1, 0));
                vertices.Add(new Vector3(x + 1, y + 1, 0));
                vertices.Add(new Vector3(x + 1, y, 0));
                uvs.Add(new Vector2(0, 0));
                uvs.Add(new Vector2(0, 1));
                uvs.Add(new Vector2(1, 1));
                uvs.Add(new Vector2(1, 0));
                colors.Add(Color.clear);
                colors.Add(Color.clear);
                colors.Add(Color.clear);
                colors.Add(Color.clear);
            }
        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles.ToArray(), 0);
        mesh.SetColors(colors);
        mesh.SetUVs(0, uvs);
    }
}