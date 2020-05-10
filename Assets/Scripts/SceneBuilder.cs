using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    public GameObject buildingBlock;
    public int blockSize = 32;
    public Transform wallContainer;

    private Camera cam;

    int screenHeight;
    int screenWidth;
    int halfBlock;

    public void BuildScene()
    {
        cam = Camera.main;

        screenHeight = Screen.height;
        screenWidth = Screen.width;
        halfBlock = blockSize / 2;

        BuildVerticalBoundaries();
        BuildHorizontalBoundaries();
        AddColliders();
    }

    void BuildVerticalBoundaries()
    {
        int xPos = screenWidth - halfBlock;
        int yPos = screenHeight - halfBlock;
        Vector3 pos;

        while (yPos >= 0)
        {
            pos = cam.ScreenToWorldPoint(new Vector3(xPos, yPos, 0));
            pos.z = 0;
            Instantiate(buildingBlock, pos, Quaternion.identity, wallContainer);
            pos.x *= -1;
            Instantiate(buildingBlock, pos, Quaternion.identity, wallContainer);

            yPos -= blockSize;
        }
    }

    void BuildHorizontalBoundaries()
    {
        int xPos = screenWidth - halfBlock;
        int yPos = screenHeight - halfBlock;
        Vector3 pos;

        while (xPos >= 0)
        {
            pos = cam.ScreenToWorldPoint(new Vector3(xPos, yPos, 0));
            pos.z = 0;
            Instantiate(buildingBlock, pos, Quaternion.identity, wallContainer);
            pos.y *= -1;
            Instantiate(buildingBlock, pos, Quaternion.identity, wallContainer);

            xPos -= blockSize;
        }
    }

    void AddColliders()
    {
        BoxCollider2D wall;
        Vector3 screen = cam.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight, 0));
        float width = screen.x;
        float height = screen.y;
        float block = 0.009f * blockSize;
 
        //west
        wall = wallContainer.gameObject.AddComponent<BoxCollider2D>();
        wall.size = new Vector2(block, 2 * height);
        wall.offset = new Vector2(block/2 - width, 0);

        //east
        wall = wallContainer.gameObject.AddComponent<BoxCollider2D>();
        wall.size = new Vector2(block, 2 * height);
        wall.offset = new Vector2(width - block/2, 0);

        //north
        wall = wallContainer.gameObject.AddComponent<BoxCollider2D>();
        wall.size = new Vector2(2 * width, block);
        wall.offset = new Vector2(0, height - block/2);

        //south
        wall = wallContainer.gameObject.AddComponent<BoxCollider2D>();
        wall.size = new Vector2(2 * width, block);
        wall.offset = new Vector2(0, block/2 - height);
    }
}
