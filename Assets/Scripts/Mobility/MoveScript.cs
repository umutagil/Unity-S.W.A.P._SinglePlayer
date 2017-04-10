using UnityEngine;
using System.Collections;

// Script for moving objects with constant velocity like bullets
public class MoveScript : MonoBehaviour {

    public float speed = 10;

    public Vector3 direction = new Vector3(-1, 0, 0);

    private Vector3 movement;
    private Rigidbody objectRigidBody;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        movement = new Vector3(
          speed * direction.x,
          0,
          speed * direction.z);
    }

    void FixedUpdate()
    {        
        objectRigidBody.velocity = movement / Time.deltaTime;             
    }
}
