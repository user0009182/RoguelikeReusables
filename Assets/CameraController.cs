using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public int pixelsPerUnit = 16;

    float currentAspect = -1;

    float gridWidth = 40;
    float displayedHeight=20;

    internal void SetViewWidth(int width)
    {
        gridWidth = width;
    }

    void Update()
    {
        if (Camera.main.aspect != currentAspect)
        {
            //set the orthographic size so the full 80 tile width fits on the screen
            //for thinner resolutions this will cause empty parts of the screen vertically
            //float gridWidth = 40;

            //calculate the height of the screen in world units given the width and current aspect ratio
            displayedHeight = gridWidth / Camera.main.aspect;

            //if the displayed height is below a minimum then widen the displayed width so the minimum will fit
            if (displayedHeight < 45)
            {
                displayedHeight = 45;
                gridWidth = displayedHeight * Camera.main.aspect;
            }

            Camera.main.orthographicSize = displayedHeight / 2;
            currentAspect = Camera.main.aspect;
        }

        //transform.position = new Vector3(gridWidth/2, displayedHeight/2, -10);

        RoundToNearestPixel();
    }

    void RoundToNearestPixel()
    {
        var n = transform.position;
        n *= pixelsPerUnit;
        n.x = Mathf.Round(n.x) / pixelsPerUnit;
        n.y = Mathf.Round(n.y) / pixelsPerUnit;
        transform.position = new Vector3(n.x, n.y, transform.position.z);
    }
}
