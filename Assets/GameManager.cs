using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneBuilder builder;

    void Start()
    {
        builder.BuildScene();
    }


    void StartGame()
    {
        builder.CreateEnemies();
        builder.CreatePlayer();
        // hide start panel
    }

    void EndGame()
    {
        // show end panel
        // destroy player and enemies
    }

    public void RestartGame()
    {
        // hide end panel
        //Start game
    }
}
