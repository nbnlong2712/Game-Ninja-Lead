using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public Enemy[] enemies;
        public int count;
        public float timeBetweenSpawns;
    }

    [SerializeField] Wave[] waves;
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] float timeBetweenWaves;

    private Wave currentWave;
    private int currentWaveIndex;
    private Transform player;

    [Header("Boss")]
    [SerializeField] GameObject boss;
    bool isFinish;
    bool bossIsDie;
    LevelManager levelManager;

    void Start()
    {
        bossIsDie = false;
        isFinish = false;
        levelManager = FindObjectOfType<LevelManager>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(StartNextWave(currentWaveIndex));
    }

    IEnumerator StartNextWave(int index)
    {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(index));
    }

    IEnumerator SpawnWave(int index)
    {
        currentWave = waves[index];
        for (int i = 0; i < currentWave.count; i++)
        {
            if (player == null)
            {
                yield break;
            }
            Enemy randomEnemy = currentWave.enemies[UnityEngine.Random.Range(0, currentWave.enemies.Length)];
            Transform randomSpot = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomSpot.position, randomSpot.rotation);
            yield return new WaitForSeconds(currentWave.timeBetweenSpawns);
        }
        isFinish = true;
        StartCoroutine(SpawnBoss());
    }

    void Update()
    {
        BossLv1 boss1 = FindObjectOfType<BossLv1>();
        if (boss1 != null)
        {
            if (boss1.isDie)
                bossIsDie = true;
        }

        if (isFinish && bossIsDie)
        {
            if(FindObjectsOfType<Enemy>().Length == 0)
            {
                if(levelManager != null)
                {
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 2);
                    if(SceneManager.GetActiveScene().name != "Level 4")
                    {
                        string path = SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1);
                        string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);
                        PlayerPrefs.SetInt(sceneName, 1);
                    }
                    if(PlayerPrefs.GetInt("Level 4") == 2)
                    {
                        PlayerPrefs.SetInt("Level 1", 0);
                        PlayerPrefs.SetInt("Level 2", 0);
                        PlayerPrefs.SetInt("Level 3", 0);
                        PlayerPrefs.SetInt("Level 4", 0);
                    }
                    levelManager.LoadGameWin();
                }
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(5f);
        Instantiate(boss, Vector3.zero, Quaternion.identity);
    }
}
