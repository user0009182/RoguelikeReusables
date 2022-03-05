# RoguelikeReusables
Project to hold reusable components that can be used in different projects.

assets/SpriteSheets - A wrapper around SpriteAtlas that makes it easy to obtain sprites by name
To use in a project copy the folder to that project. 
(Note: to work with sprite sheets in a project the 2D Sprite package needs to be imported (think it's automatically imported in 2D projects, but not 3D). If it's not imported the Create > 2D > Sprite Atlas option won't be available)

Create one or more Sprite Atlases in the Resources folder. Sprites can then be accessed at runtime by name like this:
    //assumes a SpriteAtlas named SpriteAtlas1 is in the Resources folder and it contains a sprite named sprite1
    var spriteSheet = new SpriteSheet("SpriteAtlas1");
    var sprite = spriteSheet.GetSprite("sprite1");    
    //There is also a SpriteSheetCollection that allows multiple SpriteSheets to be "combined"
    
assets/TileGridRendering - A tile renderer, similar to Unity's Tilemap. Depends on assets/SpriteSheets
To use in a project copy the TileGridRendering folder to the project. Some of the prefabs might lose field values in the copy and currently need to be manually fixed:
- TileRenderLayer prefab needs TileGridLayer script added and the MeshRenderer needs the "Material" set
- TileGridRenderer needs the TileGridRenderer script added and then its Layer Prefab field set to the Layer prefab
- Material needs its shader set to Custom/UnlitColorShader

Add it to a project by adding the TileGridRenderering component to a root GameObject, such as "Main" or "Rendering". Initialize it as follows:
    //each layer depends on a SpriteSheet to obtain sprites to display
    var spriteSheets = new SpriteSheetCollection();
    spriteSheets.Add(new SpriteSheet("SpriteAtlas"));
    //initialize width/height
    tileGridRenderer.Initialise(80, 40);
    //add layers in order back to front
    floorRenderLayer = gridRenderer.AddLayer(spriteSheets);
    wallRenderLayer = gridRenderer.AddLayer(spriteSheets);
    
And use it like this:
    wallRenderLayer.SetSprite(5, 7, "sprite1");
    //A sprite can be tinted with a color:
    wallRenderLayer.SetSprite(6, 7, "sprite1", Color.red);
    //A tile can be cleared (made to contain no sprite, transparent):
    wallRenderLayer.SetSprite(7, 7, null);
    //Changes aren't made until Redraw() is called:
    tileGridRenderer.Redraw();
    
