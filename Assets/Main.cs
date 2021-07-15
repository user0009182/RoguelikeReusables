using System.Collections;
using UnityEngine;

public class Main : MonoBehaviour
{
    TileGridLayer floorRenderLayer;
    TileGridLayer wallRenderLayer;
    public TileGridRenderer gridRenderer;

    void Start()
    {
        InitialiseRenderer();

        RedrawLevel();

        Camera.main.GetComponent<CameraController>().SetViewWidth(80);
        gridRenderer.transform.position = new Vector3(-40, -20);
    }

    void RedrawLevel()
    {
        for (int x = 0; x < gridRenderer.Size.x; x++)
            for (int y = 0; y < gridRenderer.Size.y; y++)
                floorRenderLayer.SetSprite(x, y, "floor");
        wallRenderLayer.SetSprite(3, 3, "wall");
        gridRenderer.Redraw();
    }

    void InitialiseRenderer()
    {
        var spriteSheets = new SpriteSheetCollection();
        var spriteSheet = new SpriteSheet("SpriteAtlas");
        spriteSheets.Add(spriteSheet);
        gridRenderer.Initialise(80, 40);
        floorRenderLayer = gridRenderer.AddLayer(spriteSheets);
        wallRenderLayer = gridRenderer.AddLayer(spriteSheets);
    }

    Vector2Int playerPos = new Vector2Int(10, 10);
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            wallRenderLayer.SetSprite(playerPos.x, playerPos.y, null);
            playerPos.x++;
            for (int x = 0; x < gridRenderer.Size.x; x++)
                for (int y = 0; y < gridRenderer.Size.y; y++)
                    floorRenderLayer.SetSprite(x, y, "floor");
            wallRenderLayer.SetSprite(playerPos.x, playerPos.y, "sprite1", Color.red);
            gridRenderer.Redraw();
        }
    }
}
