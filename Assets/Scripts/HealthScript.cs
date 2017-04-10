using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void UnitSwapped(GameObject unit1, GameObject unit2);

public class HealthScript : MonoBehaviour {
   
    public int hp = 1;
    public bool isEnemy;
    public Transform bloodEffect;
    private GameManager gameController;

    public event UnitSwapped OnUnitSwapped;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameManager>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void Start() {}

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            Transform bloodTransform = Instantiate(bloodEffect) as Transform;
            bloodTransform.position = transform.position + new Vector3(0, 0.01f, 0);
            Destroy(gameObject);                                    
        }
    }

    void OnTriggerEnter(Collider otherCollider)
    {        
        ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();        
        if (shot != null)
        {
            if (shot.shooter == null)
                return;

            // Avoid friendly fire
            //if (shot.isEnemyShot != isEnemy)
            if(shot.shooter.gameObject != gameObject)
            {
                if (shot.shooter.tag == "Player")
                    gameController.IncrementScore();

                Damage(shot.damage);   

                // Destroy the shot
                Destroy(shot.gameObject); // Target the game object, otherwise it will just remove the script
                
                Transform killingShooter = shot.shooter.transform;                                              
                Swap(killingShooter.gameObject, gameObject);                
            }
        }
        
    }

    // invokes OnUnitSwapped -> GameManager swaps the shooters
    private void Swap(GameObject shooter1, GameObject shooter2)
    {
        if (OnUnitSwapped != null)
        {
            OnUnitSwapped(shooter1, shooter2);
        } 
    }

    
}
