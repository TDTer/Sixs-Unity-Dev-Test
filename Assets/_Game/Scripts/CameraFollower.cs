using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : Singleton<CameraFollower>
{
    [SerializeField] Transform player;
    [SerializeField] Transform offset;
    [SerializeField] Transform tf;
    [SerializeField] Transform target;

    private Vector3 targetOffset;
    private float moveSpeed = 5f;

    private void Awake()
    {
        ChangeCameraToPlayer();
        targetOffset = offset.localPosition - target.position;
    }

    private void LateUpdate()
    {
        tf.position = Vector3.Lerp(tf.position, target.position + targetOffset, Time.deltaTime * moveSpeed);
    }

    public void ChangeTarget(Transform target)
    {
        this.target = target;
    }

    public void ChangeCameraToPlayer()
    {
        target = player;
    }
}
