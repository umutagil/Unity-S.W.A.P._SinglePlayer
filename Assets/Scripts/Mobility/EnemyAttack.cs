using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    
    public float speed;
    private Transform player;    
    private WeaponScript weapon;
    private GameManager gameController;
    private WeaponProperties weaponProperties;

    void Awake()
    {
        weapon = GetComponent<WeaponScript>();
        weapon.shootingRate = Random.Range(1, 30) * 0.1f + 1f;
        weapon.shootCooldown = Random.Range(1f, 3f);
    }

    // get the gameController and set mapBoundary
    void Start()
    {
        
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameManager>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameManager' script");
        }

        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        if (playerObject == null)
        {
            Debug.Log("Cannot find 'Player' object");
        }        
    }    

    void Update()
    {        
    }

    public void Attack()
    {        
        if (player == null)
            return;        

        if (weapon == null || !weapon.CanAttack)
            return;        

        // Auto-fire
        Vector3 transformCenterPos = transform.position + new Vector3(0, 1f, 0);
        Ray ray = new Ray(transformCenterPos, transform.forward);
        RaycastHit hit;

        Debug.DrawRay(transformCenterPos, transform.forward, Color.red);
        if (Physics.Raycast(ray, out hit, 100))
        {

            if (hit.collider.gameObject.tag != "Player")
                return;

            Vector3 shotDirection3D = (player.transform.position - transform.position).normalized;
            weapon.Attack(true, shotDirection3D);
        }
    }

    void FixedUpdate()
    {
        return;                 
    }
}
