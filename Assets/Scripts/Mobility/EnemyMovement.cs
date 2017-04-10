using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
    
    private ShooterAnimationController shooterAnimationController;
    		
    void Awake()
    {
        shooterAnimationController = new ShooterAnimationController(GetComponent<Animator>());
    }
		    
    public void SetMoveAnimSpeed(float speed)
    {
        shooterAnimationController.SetSpeed(speed);
    }
}
