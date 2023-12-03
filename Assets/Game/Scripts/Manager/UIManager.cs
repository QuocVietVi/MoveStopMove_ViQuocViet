using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Text enemyAlive;
    [SerializeField] private float numberEnemiesAlive;
    [SerializeField] private Button play,openWeaponShop, closeWeaponShop, prevWeapon, nextWeapon, buyBtn;
    [SerializeField] private Text weaponName, weaponPrice;
    [SerializeField] private GameObject mainMenu, alive, weaponShop;
    [SerializeField] private GameObject weaponInShop;
    public WeaponType currentWeapon;
    private List<WeaponData> listWeapon;
    private int index;
    private Weapon weapon;
    
    private void Start()
    {
        index = (int)currentWeapon;
        listWeapon = GameManager.Instance.weaponSO.weapons;
        play.onClick.AddListener(StartGamePlay);
        openWeaponShop.onClick.AddListener(OpenWShop);
        closeWeaponShop.onClick.AddListener(CloseWShop);
        nextWeapon.onClick.AddListener(NextWeapon);
        prevWeapon.onClick.AddListener(PrevWeapon);
        buyBtn.onClick.AddListener(BuyWeapon);
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

    private void OpenWShop()
    {
        mainMenu.SetActive(false);
        weaponShop.SetActive(true);
        LevelManager.Instance.DeSpawnPlayer();
        GameManager.Instance.GetWeponData(currentWeapon);
        weapon = LeanPool.Spawn(listWeapon[index].weapon, weaponInShop.transform.position, Quaternion.identity, weaponInShop.transform);
        SetWeaponInfo();
    }

    private void CloseWShop()
    {
        mainMenu.SetActive(true);
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
            SetWeaponInfo();
        }

    }

    private void SetWeaponInfo()
    {
        weaponName.text = weapon.name;
        weaponPrice.text = listWeapon[index].price.ToString();
    }

    private void BuyWeapon()
    {
        currentWeapon = listWeapon[index].weaponType;
        CloseWShop();
    }

}
