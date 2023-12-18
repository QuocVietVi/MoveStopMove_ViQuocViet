using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfoManager : Singleton<PlayerInfoManager>
{
    public Text levelTxt;
    public Text nameTxt;
    public CharName charName;
    [SerializeField] Character character;

    private void Start()
    {
        SetCharName((CharName)Random.Range(0,10));
    }
    private void Update()
    {
        this.transform.LookAt(CameraFollow.Instance.transform.position);
        levelTxt.text = character.level.ToString();

    }
    private void SetCharName(CharName name) 
    {
        this.charName = name;
        nameTxt.text = charName.ToString();
    }
}
