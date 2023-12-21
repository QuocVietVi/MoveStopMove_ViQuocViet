using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : Singleton<SkinManager>
{
    [SerializeField] private Button hatBtn, pantBtn, shieldBtn, setFullBtn, closeBtn, buyBtn;
    [SerializeField] private ItemButtonAction itemBtn;
    [SerializeField] private GameObject btnHolder;

    private List<SkinData> listPants;
    private List<HatData> listHats;
    private List<ShieldData> listShields;
    private int index;
    private ItemButtonAction itemBtnAction;
    private List<ItemButtonAction> listItems = new List<ItemButtonAction>();
    private PlayerData playerData;
    private Player player;
    public GameObject goldImg;
    public bool canChoosePant, canChooseHat, canChooseShield;
    public PantType currentPant;
    public HatType currentHat;
    public ShieldType currentShield;
    public List<GameObject> hats;
    public List<GameObject> shields;
    public Text skinPrice;
    public float currentPrice;
    private void Start()
    {
        //index = (int)currentPant;
        playerData = GameManager.Instance.PlayerData;
        listPants = GameManager.Instance.skinSO.pants;
        listHats = GameManager.Instance.hatSO.hats;
        listShields = GameManager.Instance.shieldSO.shields;
        player = LevelManager.Instance.player;
        canChoosePant = true;
        canChooseHat = true;
        canChooseShield = true;
        pantBtn.onClick.AddListener(SpawnPantItem);
        closeBtn.onClick.AddListener(CloseSkinShop);
        buyBtn.onClick.AddListener(BuySkin);
        hatBtn.onClick.AddListener(SpawnHatItem);
        shieldBtn.onClick.AddListener(SpawnShieldItem);
    }
    //private void OnEnable()
    //{
    //    SpawnHatItem();
    //}
    private void SpawnPantItem()
    {
        if (canChoosePant == true)
        {
            DespawnItem();
            for (int i = 1; i < listPants.Count; i++)
            {
                itemBtnAction = LeanPool.Spawn(itemBtn, btnHolder.transform);
                itemBtnAction.image.sprite = listPants[i].image;
                itemBtnAction.previewPant = listPants[i].pant;
                itemBtnAction.material = listPants[i].material;
                itemBtnAction.price = listPants[i].price;
                listItems.Add(itemBtnAction);
            }
            DespawnHat();
            ResetSkin();
        }
        canChoosePant = false;
        canChooseHat = canChooseShield = true;

    }

    public void SpawnHatItem()
    {
        if (canChooseHat == true)
        {
            DespawnItem();
            for (int i = 1; i < listHats.Count; i++)
            {
                itemBtnAction = LeanPool.Spawn(itemBtn, btnHolder.transform);
                itemBtnAction.image.sprite = listHats[i].image;
                itemBtnAction.previewHat = listHats[i].hatType;
                itemBtnAction.hatPrefab = listHats[i].hatPrefab;
                itemBtnAction.price = listHats[i].price;
                listItems.Add(itemBtnAction);

            }
            ResetSkin();
        }
        canChoosePant = canChooseShield = true;
        canChooseHat = false;

    }

    private void SpawnShieldItem()
    {
        if (canChooseShield == true)
        {
            DespawnItem();
            for (int i = 1; i < listShields.Count; i++)
            {
                itemBtnAction = LeanPool.Spawn(itemBtn, btnHolder.transform);
                itemBtnAction.image.sprite = listShields[i].image;
                itemBtnAction.previewShield = listShields[i].shieldType;
                itemBtnAction.shieldPrefab = listShields[i].shieldPrefab;
                itemBtnAction.price = listShields[i].price;
                listItems.Add (itemBtnAction);
            }
            ResetSkin() ;
        }
        canChooseHat = canChoosePant = true;
        canChooseShield = false;
    }

    private void ResetSkin()
    {
        player.OnInit();
        player.ChangeAnim(ConstantAnim.DANCE);
    }

    private void CloseSkinShop()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.subMenu.SetActive(true);
        player.OnInit();
        CameraFollow.Instance.OnInit();
        DespawnHat();
        DespawnShield();
        this.enabled = false;
    }

    private void BuySkin()
    {
        if (currentPrice < GameManager.Instance.PlayerData.golds)
        {
            if (canChoosePant == false)
            {
                if (skinPrice.text != "Equipped" && skinPrice.text != "Select")
                {
                    playerData.listPantUnlock.Add((int)currentPant);
                }
                playerData.pantEqipped = (int)currentPant;
                DataManager.Instance.SaveData(playerData);
                SetTextBtnBuy();

            }
            if (canChooseHat == false)
            {
                if (skinPrice.text != "Equipped" && skinPrice.text != "Select")
                {
                    playerData.listHatUnlock.Add((int)currentHat);
                }
                playerData.hatEqipped = (int)currentHat;
                DataManager.Instance.SaveData(playerData);
                SetTextBtnBuy();
            }
            if (canChooseShield == false)
            {
                if (skinPrice.text != "Equipped" && skinPrice.text != "Select")
                {
                    playerData.listShieldUnlock.Add((int)currentShield);
                }
                playerData.shieldEqipped = (int)currentShield;
                DataManager.Instance.SaveData(playerData);
                SetTextBtnBuy();
            }
            UIManager.Instance.SetTextGold(currentPrice);
            
        }

    }

    public void SetTextBtnBuy()
    {
        //if (playerData.listPantUnlock.Contains((int)currentPant))
        //{
        skinPrice.text = "Equipped";
        goldImg.SetActive(false);
        if ((int)currentPant != playerData.pantEqipped && canChoosePant == false)
        {
            skinPrice.text = "Select";
        }
        if ((int)currentHat != playerData.hatEqipped && canChooseHat == false)
        {
            skinPrice.text = "Select";
        }
        if ((int)currentShield != playerData.shieldEqipped && canChooseShield == false)
        {
            skinPrice.text = "Select";
        }

        //}
    }

    private void DespawnItem() {
        if (listItems.Count > 0)
        {
            for (int i = listItems.Count - 1; i >= 0; i--)
            {
                LeanPool.Despawn(listItems[i].gameObject);
            }
            listItems.Clear();
        }
    }

    public void DespawnHat()
    {
        if (hats.Count > 0)
        {
            //for (int i = 0; i< hats.Count; i++)
            //{
            //    LeanPool.Despawn(hats[i].gameObject);
            //}
            LeanPool.Despawn(hats[0].gameObject);
            hats.Clear();
        }
    }

    public void DespawnShield()
    {
        if (shields.Count > 0)
        {
            LeanPool.Despawn(shields[0].gameObject);
            shields.Clear();
        }
    }

}
