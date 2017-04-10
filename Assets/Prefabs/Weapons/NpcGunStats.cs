using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Weapons/NpcGunStats")]
public class NpcGunStats : ScriptableObject {

    public int damage;
    public float shootingRate;
    public float bulletSpeed;
    public float scale;
    public int ammunition;
    public int maxAmmunition;
    public float reloadTime;
    public Transform shotPrefab;

    public bool isReloading = false;

}
