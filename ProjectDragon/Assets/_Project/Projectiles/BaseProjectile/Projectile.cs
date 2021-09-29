using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [SerializeField] protected float speed = 10;
    [SerializeField] protected float damage = 100;
    
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
    protected abstract void Move();
}
