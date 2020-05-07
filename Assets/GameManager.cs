using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SceneBuilder builder;
    public AudioSource audioSource;

    public GameObject startPanel, endPanel;

    public GameObject characterContainer;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public AnimatorOverrideController[] enemyAnimations;
    

    AnimatorOverrideController playerAnimation;
    int numEnemies;

    int enemiesAlive;
    bool playerAlive;

    void Start()
    {
        builder.BuildScene();
        numEnemies = enemyAnimations.Length;
    }

    public void OnCharacterPicked(AnimatorOverrideController aoc)
    {
        playerAnimation = aoc;
        StartGame();
    }

    void StartGame()
    {
        CreateEnemies();
        CreatePlayer();
        startPanel.SetActive(false);
        enemiesAlive = numEnemies;
    }

    void CreateEnemies()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 position = new Vector3(Random.Range(-2.0f, 2.0f), Random.Range(0.0f, 3.0f), 0);
            GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity, characterContainer.transform);
            enemy.GetComponent<Animator>().runtimeAnimatorController = enemyAnimations[i];
            enemy.GetComponent<EnemyController>().audioSource = audioSource;
            enemy.GetComponent<EnemyController>().gm = this;
        }
    }

    void CreatePlayer()
    {
        GameObject player = Instantiate(playerPrefab, new Vector3(0, -2, 0), Quaternion.identity, characterContainer.transform);
        player.GetComponent<Animator>().runtimeAnimatorController = playerAnimation;
        player.GetComponent<PlayerController>().audioSource = audioSource;
        player.GetComponent<PlayerController>().gm = this;
    }

    public void OnEnemyDead()
    {
        enemiesAlive -= 1;

        if (enemiesAlive == 0)
            EndGame();
    }

    public void OnPlayerDead()
    {
        playerAlive = false;
        EndGame();
    }

    void EndGame()
    {
        if (playerAlive)
        {
            // you won
        }
        else
        {
            //you lost
        }
        endPanel.SetActive(true);

        foreach (Transform character in characterContainer.transform)
        {
            Destroy(character.gameObject);
        }
    }

    public void RestartGame()
    {
        endPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
