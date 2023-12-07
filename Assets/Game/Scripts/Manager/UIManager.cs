using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text enemyAlive;
    [SerializeField] private float numberEnemiesAlive;
    [SerializeField] private Button play,openWeaponShop, closeWeaponShop, prevWeapon, nextWeapon, buyBtn, openSkinShop;
    [SerializeField] private Text weaponName, weaponPrice;
    [SerializeField] private GameObject mainMenu, alive, weaponShop, skinShop;
    [SerializeField] private GameObject weaponInShop;
    [SerializeField] private GameObject imageGold;
    [SerializeField] private Vector3 camOffset;

    public WeaponType currentWeapon;
    public GameObject subMenu;
    private List<WeaponData> listWeapon;
    private int index;
    private Weapon weapon;
    private PlayerData playerData;
    
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
    }

    private void Update()
    {
        enemyAlive.text = "Alive : " + LevelManager.Instance.maxEnemies.ToString();

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
        currentWeapon = listWeapon[index].weaponType;
        Debug.Log(currentWeapon.ToString());
        if (weaponPrice.text != "Eqipped" && weaponPrice.text != "Select")
        {
            playerData.listWeaponUnlock.Add((int)currentWeapon);
        }
        playerData.weaponEquipped = (int)currentWeapon;
        DataManager.Instance.SaveData(playerData);
        SetWeaponInfo();

        //CloseWShop();
    }

    private void OpenSkinShop()
    {
        subMenu.SetActive(false);
        skinShop.SetActive(true);
        CameraFollow.Instance.offset = camOffset;
        LevelManager.Instance.DanceAnim();
    }



}
