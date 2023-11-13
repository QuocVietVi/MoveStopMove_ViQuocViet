using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform spawnPoint;
    public Player _player;
    public Enemy _enemy;

    private void Start()
    {
        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        this.gameObject.SetActive(false);
        Instantiate(_player, startPoint.position, Quaternion.identity);
        Instantiate(_enemy, spawnPoint.position, Quaternion.identity);
        CameraFollow.Instance.FindPlayer();
    }

}
