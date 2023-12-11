using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private float maxEnemiesOnGround;
    private float posX, posZ;
    public int characterLevel;
    public float maxEnemies;
    public List<Enemy> listEnemies = new List<Enemy>();
    public Player playerPrefab;
    public Enemy enemyPrefab;
    public Player player;
    

    private void Start()
    {
        SpawnPlayer();

    }

    private void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
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
        
    }

    public void OnInit()
    {
        maxEnemiesOnGround = 15;
        maxEnemies = 50;
    }
    public void SpawnPlayer()
    {
        //this.gameObject.SetActive(false);
        //Instantiate(playerPrefab, startPoint.position, Quaternion.identity);
        player = LeanPool.Spawn(playerPrefab, startPoint.position, playerPrefab.transform.rotation);
        player.weaponType = UIManager.Instance.currentWeapon;
        player.OnInit();
        CameraFollow.Instance.FindPlayer();
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
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        //Enemy enemy = Instantiate(enemyPrefab, new Vector3(posX, 0, posZ), Quaternion.identity);
        Enemy enemy = LeanPool.Spawn(enemyPrefab);
        enemy.transform.position = new Vector3(posX, 0, posZ);
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
