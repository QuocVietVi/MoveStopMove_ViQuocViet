using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text enemyAlive;
    [SerializeField] private float numberEnemiesAlive;
    [SerializeField] private Button play;
    [SerializeField] private GameObject mainMenu, alive;

    private void Start()
    {
        play.onClick.AddListener(StartGamePlay);
    }

    private void Update()
    {
        enemyAlive.text = "Alive : " + LevelManager.Instance.maxEnemies.ToString();

    }

    private void StartGamePlay()
    {
        mainMenu.SetActive(false);
        alive.SetActive(true);
        CameraFollow.Instance.offset = new Vector3(0f, 15f, -19f);
        LevelManager.Instance.SpawnEnemies();
        GameManager.Instance.ChangeState(GameState.GamePlay);
    }
}
