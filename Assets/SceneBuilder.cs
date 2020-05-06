﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBuilder : MonoBehaviour
{
    public GameObject buildingBlock;
    public int blockSize = 32;
    private Camera cam;

    int screenHeight;
    int screenWidth;
    int halfBlock;

    void Start()
    {
        cam = Camera.main;

        screenHeight = Screen.height;
        screenWidth = Screen.width;
        halfBlock = blockSize / 2;

        BuildVerticalBoundaries();
        BuildHorizontalBoundaries();

        StartGame();
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
            Instantiate(buildingBlock, pos, Quaternion.identity);
            pos.x *= -1;
            Instantiate(buildingBlock, pos, Quaternion.identity);

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
            Instantiate(buildingBlock, pos, Quaternion.identity);
            pos.y *= -1;
            Instantiate(buildingBlock, pos, Quaternion.identity);

            xPos -= blockSize;
        }
    }

    void CreateEnemies()
    {

    }

    void CreatePlayer()
    {

    }

    void StartGame()
    {
        CreateEnemies();
        CreatePlayer();
        // hide start panel
    }

    void EndGame()
    {
        // show end panel
        // destroy player and enemies
    }

    void RestartGame()
    {
        // hide end panel
        //Start game
    }
}
