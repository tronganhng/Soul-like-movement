using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 12;
    float currentSpeed = 0;

    private void OnEnable()
    {
        PlayerEvents.Shoot += StartMove;
    }

    private void OnDisable()
    {
        PlayerEvents.Shoot -= StartMove;
    }

    void Update()
    {
        if (currentSpeed == 0) return;
        transform.Translate(-Vector3.up * currentSpeed * Time.deltaTime);
    }

    void StartMove()
    {
        transform.SetParent(null);
        currentSpeed = speed;
    }
}
