using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private float maxEnemiesSpawn;
    public List<Enemy> listEnemies = new List<Enemy>();
    public Player playerPrefab;
    public Enemy enemyPrefab;

    private Transform spawnPoint;
    private float posX, posZ;

    private void Start()
    {
        SpawnPlayer();
        SpawnEnemies();

    }

    private void Update()
    {
        if (listEnemies.Count < maxEnemiesSpawn)
        {
            posX = Random.Range(-50, 50);
            posZ = Random.Range(-38, 38);
            SpawnEnemy();
        }
    }
    private void SpawnPlayer()
    {
        //this.gameObject.SetActive(false);
        Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        CameraFollow.Instance.FindPlayer();
    }

    private void SpawnEnemies()
    {
        //this.gameObject.SetActive(false);
        for (int i = 0; i <= maxEnemiesSpawn; i++)
        {
            posX = Random.Range(-50, 50);
            posZ = Random.Range(-38, 38);
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Enemy enemy = Instantiate(enemyPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);
        listEnemies.Add(enemy);
    }

}
