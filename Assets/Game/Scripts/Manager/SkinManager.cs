using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : Singleton<SkinManager>
{
    [SerializeField] private Button hatBtn, pantBtn, shieldBtn, setFullBtn, closeBtn, buyBtn;
    [SerializeField] private Image pantImg;
    [SerializeField] private ItemButtonAction itemBtn;
    [SerializeField] private GameObject btnHolder;

    private List<SkinData> listPants;
    private List<HatData> listHats;
    private int index;
    private ItemButtonAction itemBtnAction;
    private List<ItemButtonAction> listItems = new List<ItemButtonAction>();
    private PlayerData playerData;
    public bool canChoosePant, canChooseHat, canChooseShield;
    public PantType currentPant;
    public HatType currentHat;
    public List<GameObject> hats;
    public Text skinPrice;
    private void Start()
    {
        //index = (int)currentPant;
        playerData = GameManager.Instance.PlayerData;
        listPants = GameManager.Instance.skinSO.pants;
        listHats = GameManager.Instance.hatSO.hats;
        canChoosePant = true;
        canChooseHat = true;
        pantBtn.onClick.AddListener(SpawnPantItem);
        closeBtn.onClick.AddListener(CloseSkinShop);
        buyBtn.onClick.AddListener(BuySkin);
        hatBtn.onClick.AddListener(SpawnHatItem);
    }

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
        }
        canChoosePant = false;
        canChooseHat = true;

    }

    private void SpawnHatItem()
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
        }
        canChoosePant = true;
        canChooseHat = false;

    }

    private void CloseSkinShop()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.subMenu.SetActive(true);
        LevelManager.Instance.player.OnInit();
        CameraFollow.Instance.OnInit();
        DespawnHat();
    }

    private void BuySkin()
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

    }

    public void SetTextBtnBuy()
    {
        //if (playerData.listPantUnlock.Contains((int)currentPant))
        //{
        skinPrice.text = "Equipped";
        if ((int)currentPant != playerData.pantEqipped && canChoosePant == false)
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

}
