using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;
    private float posX, posZ;
    private int level;
    public GameObject currentMap;
    public float maxEnemiesOnGround;
    public float maxEnemies;
    public int characterLevel;
    public List<Enemy> listEnemies = new List<Enemy>();
    public Player playerPrefab;
    public Enemy enemyPrefab;
    public Player player;
    public Enemy enemy;

    

    private void Start()
    {
        SpawnPlayer();
        level = 1;
        OnInit();
    }

    private void Update()
    {
        //if (GameManager.Instance.IsState(GameState.GamePlay))
        //{
        //    if (listEnemies.Count < maxEnemiesOnGround)
        //    {
        //        posX = Random.Range(-42, 42);
        //        posZ = Random.Range(-38, 38);
        //        SpawnEnemy();
        //    }
        //    if (listEnemies.Count > maxEnemies - 2)
        //    {
        //        this.enabled = false;
        //    }
        //}
        CheckNumberEnemies();
        
    }

    public void OnInit()
    {
        maxEnemiesOnGround = 15;
        maxEnemies = 20;
    }
    public void SpawnPlayer()
    {
        //this.gameObject.SetActive(false);
        //Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        player = LeanPool.Spawn(playerPrefab, startPoint.position, playerPrefab.transform.rotation);
        player.weaponType = UIManager.Instance.currentWeapon;
        player.OnInit();
        CameraFollow.Instance.FindPlayer();
        CameraFollow.Instance.OnInit();
    }

    public void DeSpawnPlayer()
    {
        player.Despawn();
    }

    public void SpawnEnemies()
    {
        //this.gameObject.SetActive(false);
        for (int i = 0; i <= maxEnemiesOnGround; i++)
        {
            posX = Random.Range(-50, 50);
            posZ = Random.Range(-38, 38);
            SpawnEnemy(posX,posZ);
        }
    }

    public void SpawnEnemy(float x, float z)
    {
        //Enemy enemy = Instantiate(enemyPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);
        enemy = LeanPool.Spawn(enemyPrefab);
        enemy.transform.position = new Vector3(x, 0, z);
        enemy.OnInit();
        listEnemies.Add(enemy);
    }

    public void DespawnAllEnemy()
    {
        for (int i = 0; i < listEnemies.Count; i++)
        {
            LeanPool.Despawn(listEnemies[i].gameObject);
        }
        listEnemies.Clear();
    }

    public void DanceAnim()
    {
        player.ChangeAnim(ConstantAnim.DANCE);
    }

    public void CheckNumberEnemies()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (listEnemies.Count < maxEnemiesOnGround)
            {
                posX = Random.Range(-42, 42);
                posZ = Random.Range(-38, 38);
                SpawnEnemy(posX, posZ);
            }
            if (listEnemies.Count > maxEnemies - 2)
            {
                this.enabled = false;
            }
        }
    }

    public void LoadMap(int map)
    {
        currentMap = Instantiate(Resources.Load<GameObject>(ConstantName.MAP + map));
        DeSpawnPlayer();
        Invoke(nameof(SpawnPlayer), 0.5f);
    }

    public void NextLevel()
    {
        if (currentMap != null)
        {
            Destroy(currentMap);
        }
        level++;
        LoadMap(level);
    }

    //public void ChangePant()
    //{

    //    player.pantData = GameManager.Instance.GetPantData(player.pantSkin);
    //}


    //public void UnlockPant()
    //{
    //    player.pantSkin = SkinManager.Instance.currentPant;
    //    player.OnInit();
    //    if (player.pantSkin == SkinManager.Instance.currentPant)
    //    {
    //        player.pantData.unlock = true;
    //        player.pantData.isEqipped = true;
    //    }
    //}

}

