using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Enemies;
using UnityEngine;

public class FireBallHomingObject : MonoBehaviour
{
    //public Transform enemy;
    private float damage = 20;
    private float speed = 1;

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    private void Start()
    {
    }
    public void Update()
    {
        //transform.position = Vector3.Lerp(transform.position, enemy.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        //
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            other.GetComponent<Enemy>().TakeDamage(damage);
        }
        
    }
}

