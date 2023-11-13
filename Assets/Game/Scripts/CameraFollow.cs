using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : Singleton<CameraFollow> 
{
    private Player player;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;

    private Transform camera;


    private void Awake()
    {
        camera = this.transform;

    }

    private void Update()
    {
        Follow();

    }

    private void Follow()
    {
        if (player != null)
        {
            camera.DOMove(player.transform.position + offset, speed * Time.deltaTime);

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
