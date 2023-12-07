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
    

    private void Start()
    {
        button.onClick.AddListener(ChangeSkin);
    }

    private void ChangeSkin()
    {
        //LevelManager.Instance.player.pantSkin = previewPant;
        if (!SkinManager.Instance.canChoosePant)
        {
            LevelManager.Instance.player.pant.material = material;
            SkinManager.Instance.currentPant = this.previewPant;
        }
        if (!SkinManager.Instance.canChooseHat)
        {
            //LeanPool.Despawn(hat);
            SkinManager.Instance.DespawnHat();
            hat = LeanPool.Spawn(hatPrefab, LevelManager.Instance.player.hatHolder);
            SkinManager.Instance.hats.Add(hat);
        }

        //LevelManager.Instance.ChangePant();
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
