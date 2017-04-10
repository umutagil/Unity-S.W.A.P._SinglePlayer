using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WeaponScript : MonoBehaviour {
    
    [SerializeField] 
    private WeaponProperties weaponProperties;
    public WeaponProperties WeaponProperties
    {
        get { return weaponProperties; }            
        set 
        {
            weaponProperties = value;
            shootingRate = weaponProperties.shootingRate; 
        }
    }

    public List<ShotScript> bulletList = new List<ShotScript>();        
    public float shootingRate = 0.25f;
    public float shootCooldown;
    private float bulletExistanceTime = 1f;
    private bool isReloading = false;
    private int ammunition = 1;

	// Use this for initialization
	void Start () 
    {
        shootingRate = WeaponProperties.shootingRate;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }        

        bulletExistanceTime -= Time.deltaTime;
        if(bulletExistanceTime <= 0)
        {
            bulletList = bulletList.Where(item => item != null).ToList();
            bulletExistanceTime = 1f;
        }
	}

    public bool Attack(bool isEnemy, Vector3 direction)
    {        
        if (CanAttack)
        {
            shootCooldown = shootingRate;
            ammunition -= 1;

            // Create a new shot
            var shotTransform = Instantiate(weaponProperties.shotPrefab) as Transform;
            shotTransform.GetComponent<MoveScript>().speed = weaponProperties.bulletSpeed;
            shotTransform.localScale *= weaponProperties.scale;

            // Assign position
            shotTransform.position = transform.position + new Vector3(0, 1f, 0);

            // The is enemy property
            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
                shot.damage = WeaponProperties.damage;
                shot.shooter = this;
                bulletList.Add(shot);
            }

            // Rotate the shot object to make its front look through target
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = direction;

                // shot rotation (z = z- 90 for fire looking upwards)
                float y = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg - 90;
                shotTransform.eulerAngles = new Vector3(0, y, 0);
            }

            // reload weapon if ammunition runs out
            if (ammunition <= 0)
                Reload();            

            return true;
        }                    
        return false;
    }

    // Check if shotCooldown is completed and shooter has ammunition
    public bool CanAttack
    {
        get
        {
            return (shootCooldown <= 0f && ammunition > 0);
        }
    }    

    // Reloads the weapon in reload time
    public void Reload()
    {
        if (isReloading)
            return;

        isReloading = true;
        Invoke("ReloadAmmunition", weaponProperties.reloadTime);
    }

    private void ReloadAmmunition()
    {
        ammunition = weaponProperties.maxAmmunition;
        isReloading = false;
    }
}


