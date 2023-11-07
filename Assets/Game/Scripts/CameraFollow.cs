using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private float speed;
    [SerializeField] private Vector3 offset;

    private Transform camera;


    private void Awake()
    {
        camera = this.transform;
        //target = GameObject.FindGameObjectWithTag("Player").transform;

    }

    private void Update()
    {
        Follow();

    }

    private void Follow()
    {
        camera.DOMove(player.transform.position + offset, speed * Time.deltaTime);

    }
}
