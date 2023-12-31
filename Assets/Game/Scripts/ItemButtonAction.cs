using Lean.Common;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonAction : MonoBehaviour
{
    [SerializeField] private Button button;
    private GameObject hat;
    private GameObject shield;
    private bool isPressed;
    //private List<GameObject> listHats = new List<GameObject>();
    public Outline outline;
    public Image image;
    public PantType previewPant;
    //public Pant currentPant;
    public HatType previewHat;
    public ShieldType previewShield;
    public Material material;
    public GameObject hatPrefab;
    public GameObject shieldPrefab;
    public float price;
    public GameObject imgUnlock;
    

    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SkinManager.Instance.canChoosePant == false )
            {
                ChangePant();
            }
            if (SkinManager.Instance.canChooseHat == false)
            {
                ChangeHat();
            }

            if (SkinManager.Instance.canChooseShield == false)
            {
                ChangeShield();
            }
            outline.enabled = true;

        });
    }

    private void ChangePant()
    {
        //LevelManager.Instance.player.pantSkin = previewPant;
        if (!SkinManager.Instance.canChoosePant)
        {
            LevelManager.Instance.player.pant.material = material;
            SkinManager.Instance.currentPant = this.previewPant;
        }
        //if (!SkinManager.Instance.canChooseHat)
        //{
        //    //LeanPool.Despawn(hat);
        //    SkinManager.Instance.currentHat = this.previewHat;
        //    SkinManager.Instance.DespawnHat();
        //    hat = LeanPool.Spawn(hatPrefab, LevelManager.Instance.player.hatHolder);
        //    SkinManager.Instance.hats.Add(hat);
        //}
        if (GameManager.Instance.PlayerData.listPantUnlock.Contains((int)SkinManager.Instance.currentPant)             )
        {
            SkinManager.Instance.SetTextBtnBuy();
        }
        else
        {
            SkinManager.Instance.skinPrice.text = this.price.ToString();
            SkinManager.Instance.goldImg.SetActive(true);
        }
        SkinManager.Instance.currentPrice = this.price;
        //if (GameManager.Instance.PlayerData.listHatUnlock.Contains((int)SkinManager.Instance.currentHat))
        //{
        //    SkinManager.Instance.SetTextBtnBuy();
        //    Debug.Log("hat");
        //}
        //else
        //{
        //    SkinManager.Instance.skinPrice.text = this.price.ToString();
        //}

    }

    private void ChangeHat()
    {
        if (!SkinManager.Instance.canChooseHat)
        {
            //LeanPool.Despawn(hat);
            SkinManager.Instance.currentHat = this.previewHat;
            LevelManager.Instance.player.DespawnHat();
            SkinManager.Instance.DespawnHat();
            hat = LeanPool.Spawn(hatPrefab, LevelManager.Instance.player.hatHolder);
            SkinManager.Instance.hats.Add(hat);
        }
        if (GameManager.Instance.PlayerData.listHatUnlock.Contains((int)SkinManager.Instance.currentHat))
        {
            SkinManager.Instance.SetTextBtnBuy();
            Debug.Log("hat");
        }
        else
        {
            SkinManager.Instance.skinPrice.text = this.price.ToString();
            SkinManager.Instance.goldImg.SetActive(true);

        }
        SkinManager.Instance.currentPrice = this.price;

    }

    private void ChangeShield()
    {
        if (!SkinManager.Instance.canChooseShield)
        {
            SkinManager.Instance.currentShield = this.previewShield;
            LevelManager.Instance.player.DespawnShield();
            SkinManager.Instance.DespawnShield();
            shield = LeanPool.Spawn(shieldPrefab, LevelManager.Instance.player.shieldHolder);
            SkinManager.Instance.shields.Add(shield);
        }
        if (GameManager.Instance.PlayerData.listShieldUnlock.Contains((int)SkinManager.Instance.currentShield))
        {
            SkinManager.Instance.SetTextBtnBuy();
        }
        else
        {
            SkinManager.Instance.skinPrice.text = this.price.ToString();
            SkinManager.Instance.goldImg.SetActive(true);

        }
        SkinManager.Instance.currentPrice = this.price;

    }

    //private void DespawnHat()
    //{
    //    for (int i = listHats.Count - 1; i >= 0; i--)
    //    {
    //        LeanPool.Despawn(listHats[i].gameObject);
    //    }
    //    listHats.Clear();
    //}

}
