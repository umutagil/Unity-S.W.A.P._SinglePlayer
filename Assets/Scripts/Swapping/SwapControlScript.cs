using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SwapControlScript {

    public LightningControlScript lightningController;    
	
    public void SwapMultiple(List<SwappingCouple> couples, int damage)
    {
        foreach(SwappingCouple couple in couples)
        {            
            Swap(couple.shooter1, couple.shooter2);
            couple.shooter1.GetComponent<HealthScript>().Damage(damage);
            couple.shooter2.GetComponent<HealthScript>().Damage(damage);
        }

    }

    // Swaps two shooters -> (positions and bullets)
    // Plays the sound and rides the lightning effect
    public void Swap(GameObject shooter1, GameObject shooter2)
    {
        SwapPositions(shooter1.transform, shooter2.transform);
        SwapBullets(shooter1.GetComponent<WeaponScript>(), shooter2.GetComponent<WeaponScript>());
                        
        SoundEffectsHelper.Instance.MakeSwapSound();

        //Vector3 momentumDirection = shooter1.transform.position - shooter2.transform.position;
        //Rigidbody rigidBody1 = shooter1.GetComponent<Rigidbody>();
        //Rigidbody rigidBody2 = shooter2.GetComponent<Rigidbody>();
        //rigidBody1.AddForce(momentumDirection * 50);
        //rigidBody2.AddForce(momentumDirection * -1 * 50);        

        if (shooter2 == null || shooter1 == null)
            return;
        
        lightningController.RideTheLightning(shooter1.transform.position, shooter2.transform.position);      
    }

    // Swaps positions of shooters
    void SwapPositions(Transform shooter1, Transform shooter2)
    {
        Vector3 tempShooterPos = shooter2.position;
        shooter2.position = shooter1.position;
        shooter1.position = tempShooterPos;

        // create swapping effects on both shooters
        //SpecialEffectsHelper.Instance.Explosion(shooter1.position);
        //SpecialEffectsHelper.Instance.Explosion(shooter2.position);
    }
    
    // bullets of shooter1 becomes bullets of shooter2 and vice versa
    void SwapBullets(WeaponScript shooter1, WeaponScript shooter2)
    {
        foreach (ShotScript shot in shooter1.bulletList)
        {
            shot.isEnemyShot = !shot.isEnemyShot;
            shot.shooter = shooter2;
        }

        foreach (ShotScript shot in shooter2.bulletList)
        {
            shot.isEnemyShot = !shot.isEnemyShot;
            shot.shooter = shooter1;
        }

        List<ShotScript> tempBulletList = shooter2.bulletList;
        shooter2.bulletList = shooter1.bulletList;
        shooter1.bulletList = tempBulletList;
    }
}

public class SwappingCouple
{
    public SwappingCouple(GameObject shooter1, GameObject shooter2)
    {
        this.shooter1 = shooter1;
        this.shooter2 = shooter2;
    }

    public GameObject shooter1;
    public GameObject shooter2;
}
