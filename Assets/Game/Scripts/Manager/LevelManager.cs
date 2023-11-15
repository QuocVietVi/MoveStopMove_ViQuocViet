using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private float maxEnemiesOnGround;
    public int characterLevel;
    public float maxEnemies;
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
        if (listEnemies.Count < maxEnemiesOnGround)
        {
            posX = Random.Range(-42, 42);
            posZ = Random.Range(-38, 38);
            SpawnEnemy();
        }
        if (listEnemies.Count > maxEnemies - 2)
        {
            this.enabled = false;
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
        for (int i = 0; i <= maxEnemiesOnGround; i++)
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
