using System;
using UnityEngine;


public class WeaponProperties : MonoBehaviour
{
    public WeaponProperties(float bulletSpeed, float scale, float shootingRate, int damage)
    {
        this.bulletSpeed = bulletSpeed;
        this.scale = scale;
        this.shootingRate = shootingRate;
        this.damage = damage;
    }

    public void Copy(WeaponProperties wp)
    {
        this.bulletSpeed = wp.bulletSpeed;
        this.scale = wp.scale;
        this.shootingRate = wp.shootingRate;
        this.damage = wp.damage;        
        this.maxAmmunition = wp.maxAmmunition;
        this.reloadTime = wp.reloadTime;
        this.shotPrefab = wp.shotPrefab;
    }

    public int damage;
    public float shootingRate;
    public float bulletSpeed;
    public float scale;    
    public int maxAmmunition;
    public float reloadTime;
    public Transform shotPrefab;    
    
}
