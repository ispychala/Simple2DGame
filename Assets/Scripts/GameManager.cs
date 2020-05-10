using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public SceneBuilder builder;
    public AudioSource audioSource;
    public AudioClip gameWonSound, gameLostSound;

    public GameObject startPanel, endPanel, endPanelText;

    public GameObject characterContainer;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public AnimatorOverrideController[] enemyAnimations;

    GameObject player;
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
        playerAlive = true;
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
        player = Instantiate(playerPrefab, new Vector3(0, -2, 0), Quaternion.identity, characterContainer.transform);
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
            player.GetComponent<PlayerController>().moveSpeed = 0f;
            endPanelText.GetComponent<Text>().text = "You Won!";
            audioSource.PlayOneShot(gameWonSound, 1f);
        }
        else
        {
            endPanelText.GetComponent<Text>().text = "You Lost.";
            audioSource.PlayOneShot(gameLostSound, 1f);
        }
        endPanel.SetActive(true);
    }

    public void RestartGame()
    {
        foreach (Transform character in characterContainer.transform)
        {
            Destroy(character.gameObject);
        }
        endPanel.SetActive(false);
        startPanel.SetActive(true);
    }
}
