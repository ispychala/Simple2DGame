using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneBuilder builder;
    public AnimatorOverrideController[] enemies;

    AnimatorOverrideController player;


    void Start()
    {
        builder.BuildScene();
    }

    public void OnCharacterPicked(AnimatorOverrideController aoc)
    {
        player = aoc;
        StartGame();
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
        // show start panel
        //Start game
    }
}
