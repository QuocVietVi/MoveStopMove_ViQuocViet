using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtonAction : MonoBehaviour
{
    [SerializeField] private Button button;
    public Outline outline;
    public Image image;
    public Pant currentPant;
    public Material material;


    private void Start()
    {
        button.onClick.AddListener(ChangePant);
    }

    private void ChangePant()
    {
        LevelManager.Instance.player.pantSkin = currentPant;
        LevelManager.Instance.player.pant.material = material;
        //LevelManager.Instance.ChangePant();
    }

}
