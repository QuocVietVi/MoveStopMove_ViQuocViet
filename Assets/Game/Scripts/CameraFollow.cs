using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    [SerializeField] private Player player;
    [SerializeField] private float speed;
    public Vector3 offset;

    private Transform camera;


    private void Awake()
    {
        camera = this.transform;
        offset = new Vector3(0f, 6f, -8.8f);

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

    private void Follow()
    {
        if (player != null)
        {
            camera.DOMove(player.transform.position + offset, speed * Time.fixedDeltaTime);

        }
        else
        {
            return;
        }

    }

    public void FindPlayer()
    {
        player = FindObjectOfType<Player>();
        offset = camera.position - player.transform.position;

    }
}
