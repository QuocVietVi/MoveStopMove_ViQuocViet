using Lean.Common;
using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonAction : MonoBehaviour
{
    [SerializeField] private Button button;
    private GameObject hat;
    //private List<GameObject> listHats = new List<GameObject>();
    public Outline outline;
    public Image image;
    public PantType previewPant;
    //public Pant currentPant;
    public HatType previewHat;
    public Material material;
    public GameObject hatPrefab;
    public float price;
    

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
        }
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
        }
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
