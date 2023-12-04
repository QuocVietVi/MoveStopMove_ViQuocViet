using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : Singleton<SkinManager>
{
    [SerializeField] private Button hairBtn, pantBtn, shieldBtn, setFullBtn, closeBtn, buyBtn;
    [SerializeField] private Image pantImg;
    [SerializeField] private ItemButtonAction itemBtn;
    [SerializeField] private GameObject btnHolder;

    private List<SkinData> listPants;
    private int index;
    private bool canChoosePant, buttonPressed;

    private void Start()
    {
        //index = (int)currentPant;
        listPants = GameManager.Instance.skinSO.pants;
        canChoosePant = true;
        pantBtn.onClick.AddListener(SpawnPantItem);
        closeBtn.onClick.AddListener(CloseSkinShop);
    }

    private void SpawnPantItem()
    {
        if (canChoosePant == true)
        {
            for (int i = 1; i < listPants.Count; i++)
            {
                ItemButtonAction itemBtnAction = LeanPool.Spawn(itemBtn, btnHolder.transform);
                itemBtnAction.image.sprite = listPants[i].image;
                itemBtnAction.currentPant = itemBtnAction.previewPant = listPants[i].pant;
                itemBtnAction.material = listPants[i].material;
                
            }
        }
        canChoosePant = false;

    }

    private void CloseSkinShop()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.subMenu.SetActive(true);
        LevelManager.Instance.player.OnInit();
        CameraFollow.Instance.OnInit();

    }

    private void BuyPant()
    {
        LevelManager.Instance.UnlockPant();
    }

}
