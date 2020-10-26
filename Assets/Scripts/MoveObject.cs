using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public float speed;
    [Header("Направление движения")]
    public Vector2 dir;
    void Update()
    {
        transform.Translate(dir*speed*Time.deltaTime,Space.World); //движение обьектов
    }
}
