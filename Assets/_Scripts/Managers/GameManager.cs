using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public float spawnDelay = 2f;
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;
    public float restartDelay = 3f;

    private bool gameIsOver = false;
    private GameObject enemyPrefab;

    #region Singleton
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        spawnPoint1 = GameObject.Find("Spawn1").transform;
        spawnPoint2 = GameObject.Find("Spawn2").transform;
        StartCoroutine(SpawnEnemies());
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    IEnumerator SpawnEnemies()
    {
        while (!gameIsOver)
        {
            // Choose a random spawn point
            Transform spawnPoint = Random.value < 0.5f ? spawnPoint1 : spawnPoint2;

            // Chose a random enemy
            float rand = Random.value;
            if (rand < 0.5)
            { enemyPrefab = enemyPrefab1; }
            else if (rand < 0.8)
            { enemyPrefab = enemyPrefab2; }
            else
            { enemyPrefab = enemyPrefab3; }

            // Spawn teh chosen enemy at the chosen spawn point
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // Wait for a delay before spawning the next enemy
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void GameOver()
    {
        if (!gameIsOver)
        {
            gameIsOver = true;
            Debug.Log("Game over!");

            // Restart the game after a delay
            Invoke("RestartGame", restartDelay);
        }
    }

    void RestartGame()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
