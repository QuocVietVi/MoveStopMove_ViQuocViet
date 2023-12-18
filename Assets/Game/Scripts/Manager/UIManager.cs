using Lean.Pool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text enemyAlive;
    [SerializeField] private float numberEnemiesAlive;
    //mainmenu
    [SerializeField] private Button play,openWeaponShop, closeWeaponShop, prevWeapon, nextWeapon, buyBtn, openSkinShop;
    [SerializeField] private Text weaponName, weaponPrice;
    [SerializeField] private GameObject mainMenu, alive, weaponShop, skinShop;
    [SerializeField] private GameObject weaponInShop;
    [SerializeField] private GameObject imageGold;
    [SerializeField] private Vector3 camOffset;
    [SerializeField] private Text golds;
    //revive panel
    [SerializeField] private GameObject revivePanel,gameOverPanel;
    [SerializeField] private Button closeRevivePanel, reviveBtn;
    [SerializeField] private Text reviveText, revivePriceTxt;
    [SerializeField] private Image reviveCicle;
    //Gameover panel
    [SerializeField] private Button continueBtn;
    [SerializeField] private Text rankTxt;
    public Text killerName;
    private List<WeaponData> listWeapon;
    private int index;
    private Weapon weapon;
    private PlayerData playerData;
    private float timeCountDown, revivePrice;
    public WeaponType currentWeapon;
    public GameObject subMenu;
    
    private void Start()
    {
        index = (int)currentWeapon;
        listWeapon = GameManager.Instance.weaponSO.weapons;
        playerData = GameManager.Instance.PlayerData;
        //currentWeapon = (WeaponType)playerData.weaponEquppied;
        play.onClick.AddListener(StartGamePlay);
        openWeaponShop.onClick.AddListener(OpenWShop);
        closeWeaponShop.onClick.AddListener(CloseWShop);
        nextWeapon.onClick.AddListener(NextWeapon);
        prevWeapon.onClick.AddListener(PrevWeapon);
        buyBtn.onClick.AddListener(BuyWeapon);
        openSkinShop.onClick.AddListener(OpenSkinShop);
        continueBtn.onClick.AddListener(Continue);
        closeRevivePanel.onClick.AddListener(GameOverPopup);
        SetTextGold(0f);
        timeCountDown = 5;
        revivePrice = 150f;
        LevelManager.Instance.player.Dead -= GameOverPopup;
        LevelManager.Instance.player.Dead += GameOverPopup;
        //GameManager.Instance.ChangeState(GameState.GameOver);
    }

    private void Update()
    {
        enemyAlive.text = "Alive : " + LevelManager.Instance.maxEnemies.ToString();
        //if (GameManager.Instance.IsState(GameState.Revive))
        //{
        //    timeCountDown -= 1 * Time.deltaTime;
        //    reviveText.text = timeCountDown.ToString("0");
        //    reviveCicle.transform.Rotate(0,0,-360 * Time.deltaTime);
        //    if (timeCountDown <= 0)
        //    {
        //        GameOverPopup();
        //    }
        //}
    }
    //private void OnInit()
    //{

    //}
    private void StartGamePlay()
    {
        mainMenu.SetActive(false);
        alive.SetActive(true);
        CameraFollow.Instance.offset = new Vector3(0f, 15f, -19f);
        LevelManager.Instance.SpawnEnemies();
        GameManager.Instance.ChangeState(GameState.GamePlay);
        
    }

    private void OpenWShop()
    {
        subMenu.SetActive(false);
        weaponShop.SetActive(true);
        LevelManager.Instance.DeSpawnPlayer();
        GameManager.Instance.GetWeponData(currentWeapon);
        weapon = LeanPool.Spawn(listWeapon[index].weapon, weaponInShop.transform.position, Quaternion.identity, weaponInShop.transform);
        //OnInit();
        //DataManager.Instance.LoadData<PlayerData>();
        SetWeaponInfo();
    }

    private void CloseWShop()
    {
        subMenu.SetActive(true);
        weaponShop.SetActive(false);
        LevelManager.Instance.SpawnPlayer();
        LeanPool.Despawn(weapon);
        CameraFollow.Instance.OnInit();
    }

    private void NextWeapon()
    {
        if (index < listWeapon.Count-1)
        {
            index++;
            LeanPool.Despawn(weapon);
            weapon = LeanPool.Spawn(listWeapon[index].weapon, weaponInShop.transform.position, Quaternion.identity, weaponInShop.transform);
            currentWeapon = listWeapon[index].weaponType;
            SetWeaponInfo();
        }
        //weapon = listWeapon[index].weapon;
        //LeanPool.Spawn(weapon, weaponInShop.transform.position, Quaternion.identity, weaponInShop.transform);
    }

    private void PrevWeapon()
    {
        if (index > 0)
        {
            index--;
            LeanPool.Despawn(weapon);
            weapon = LeanPool.Spawn(listWeapon[index].weapon, weaponInShop.transform.position, Quaternion.identity, weaponInShop.transform);
            currentWeapon = listWeapon[index].weaponType;
            SetWeaponInfo();
        }

    }

    private void SetWeaponInfo()
    {
        weaponName.text = weapon.name;
        if (playerData.listWeaponUnlock.Contains((int)currentWeapon))
        {
            weaponPrice.text = "Eqipped";
            imageGold.SetActive(false);
            if ((int)currentWeapon != playerData.weaponEquipped)
            {
                weaponPrice.text = "Select";
            }
        }
        else
        {
            weaponPrice.text = listWeapon[index].price.ToString();
            imageGold.SetActive(true);

        }
    }

    private void BuyWeapon()
    {
        if (listWeapon[index].price < GameManager.Instance.PlayerData.golds)
        {
            currentWeapon = listWeapon[index].weaponType;
            Debug.Log(currentWeapon.ToString());
            if (weaponPrice.text != "Eqipped" && weaponPrice.text != "Select")
            {
                playerData.listWeaponUnlock.Add((int)currentWeapon);
            }
            playerData.weaponEquipped = (int)currentWeapon;
            DataManager.Instance.SaveData(playerData);
            SetWeaponInfo();
            SetTextGold(listWeapon[index].price);
        }
        
        //CloseWShop();
    }

    private void OpenSkinShop()
    {
        subMenu.SetActive(false);
        skinShop.SetActive(true);
        skinShop.GetComponent<SkinManager>().enabled = true;
        CameraFollow.Instance.offset = camOffset;
        LevelManager.Instance.DanceAnim();
        
    }

    public void SetTextGold(float price)
    {
        GameManager.Instance.PlayerData.golds -= price;
        golds.text = GameManager.Instance.PlayerData.golds.ToString();
    }

    public void RevivePopup()
    {
        GameManager.Instance.ChangeState(GameState.Revive);
        revivePanel.SetActive(true);

    }

    private void GameOverPopup()
    {
        gameOverPanel.SetActive(true);
        revivePanel.SetActive(false);
        rankTxt.text = "#" + LevelManager.Instance.maxEnemies.ToString();
        GameManager.Instance.ChangeState(GameState.GameOver);
    }

    private void Continue()
    {
        gameOverPanel.SetActive(false);
        mainMenu.SetActive(true);
        GameManager.Instance.ChangeState(GameState.MainMenu);
        LevelManager.Instance.DeSpawnPlayer();
        LevelManager.Instance.SpawnPlayer();
        LevelManager.Instance.DespawnAllEnemy();
        LevelManager.Instance.OnInit();
        CameraFollow.Instance.OnInit();
    }





}
