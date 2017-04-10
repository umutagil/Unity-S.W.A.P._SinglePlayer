using UnityEngine;
using System.Collections;

// Hold properties of the bullets
// Destroys object on collision trigger
public class ShotScript : MonoBehaviour {

    public WeaponScript shooter;
    public int damage = 1;
    public bool isEnemyShot = false;
	
	void Start () {
        Destroy(gameObject, 10);
	}
		
	void Update () {	
	}

    void OnTriggerEnter(Collider otherCollider)
    {
        WeaponScript weapon = otherCollider.gameObject.GetComponent<WeaponScript>();
        if (weapon == null) // TODO: this part should be computed in another script
        {            
            Destroy(gameObject);
        }
    }
    
}
