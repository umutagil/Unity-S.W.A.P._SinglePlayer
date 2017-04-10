using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Works like a connection between ai state machine and enemy scripts
   Holds the ai data to be used in states, decisions and actions */

public class StateController : MonoBehaviour {

    public State currentState;
    public Transform eyes;
    public State remainState;    //dummy state for staying in the same state
    public EnemyStats enemyStats;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public List<Transform> wayPointList;
    [HideInInspector] public int nextWayPoint;
    [HideInInspector] public Transform chaseTarget;
    [HideInInspector] public float stateTimeElapsed;
    [HideInInspector] public EnemyAttack enemyAttack;
    [HideInInspector] public EnemyMovement enemyMovement;    

    private bool aiActive;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAttack = GetComponent<EnemyAttack>();
        enemyMovement = GetComponent<EnemyMovement>();
    }

    public void SetupAI(bool aiActivationFromGameManager, List<Transform> wayPointsFromGameManager)
    {
        wayPointList = wayPointsFromGameManager;
        aiActive = aiActivationFromGameManager;
        navMeshAgent.enabled = aiActive;
    }
	
	void Update () {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
        float moveAnimSpeed = Vector3.Project(navMeshAgent.desiredVelocity, transform.forward).magnitude / enemyStats.maxSpeed;        
        enemyMovement.SetMoveAnimSpeed(moveAnimSpeed);
	}

    void OnDrawGizmos()
    {
        if(currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawSphere(eyes.position, 0.2f);
        }
    }

    // Sets the current state
    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

}
