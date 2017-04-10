using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/EnemyStats")]
public class EnemyStats : ScriptableObject {

    public float searchingTurnSpeed = 120f;
    public float searchDuration = 2f;
    public float attackDistance = 10f;
    public float lookDistance = 20f;
    public float lookSphereRadius = 5f;
    public float maxSpeed = 5f;

}
