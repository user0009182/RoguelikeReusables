# RoguelikeReusables
Project to hold reusable components that can be used in different projects.

assets/SpriteSheets - A wrapper around SpriteAtlas that makes it easy to obtain sprites by name

    var spriteSheet = new SpriteSheet("SpriteAtlas");
    var sprite = spriteSheet.GetSprite("sprite1");

assets/TileGridRendering - A tile renderer, similar to Unity's Tilemap. It is initialized like this:

    var spriteSheets = new SpriteSheetCollection();
    spriteSheets.Add(new SpriteSheet("SpriteAtlas"));
    //initialize width/height
    tileGridRenderer.Initialise(80, 40);
    //add layers in order back to front
    floorRenderLayer = gridRenderer.AddLayer(spriteSheets);
    wallRenderLayer = gridRenderer.AddLayer(spriteSheets);
    
And used like this:

    wallRenderLayer.SetSprite(5, 7, "sprite1");
    //A sprite can be tinted with a color:
    wallRenderLayer.SetSprite(6, 7, "sprite1", Color.red);
    //A tile can be cleared (made to contain no sprite, transparent):
    wallRenderLayer.SetSprite(7, 7, null);
    //Changes aren't made until Redraw() is called:
    tileGridRenderer.Redraw();
    
