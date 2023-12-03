using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    [SerializeField] private Player player;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 menuState;
    public Vector3 offset;

    private Transform camera;


    private void Awake()
    {

        OnInit();
    }

    private void FixedUpdate()
    {
        Follow();
        if (player.level == 8)
        {
            offset = new Vector3(0, 17f, -21f);
        }else if (player.level == 16)
        {
            offset = new Vector3(0, 19f, -23f);
        }
        else if (player.level == 24)
        {
            offset = new Vector3(0, 21f, -25f);
        }
    }

    public void OnInit()
    {
        camera = this.transform;
        offset = menuState;
    }

    private void Follow()
    {
        if (player != null)
        {
            camera.DOMove(player.transform.position + offset, speed * Time.fixedDeltaTime);
        }


    }

    public void FindPlayer()
    {
        player = FindObjectOfType<Player>();
        offset = camera.position - player.transform.position;

    }
}
