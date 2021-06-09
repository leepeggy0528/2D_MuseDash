using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musicpoint : MonoBehaviour
{
    [Header("移動速度")]
    public float speed;
    private void Move()
    {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
    }

    private void Update()
    {
        Move();
    }
}
