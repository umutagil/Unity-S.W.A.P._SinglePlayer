using UnityEngine;
using System.Collections;

public class PlayerMobility : MonoBehaviour {
    
    public float speed;

    private Rigidbody rb;    
    private WeaponScript weapon;    
    private ShooterAnimationController playerAnimation;
    private Vector3 lookPos;
    private Vector3 movementDir;
    private float lastShotTime;
    private float animSpeed;    

    private Camera cam;
    private Com.LuisPedroFonseca.ProCamera2D.ProCamera2D proCam;
       
    void Awake()
    {
        cam = Camera.main;
        proCam = cam.GetComponent<Com.LuisPedroFonseca.ProCamera2D.ProCamera2D>();
        rb = GetComponent<Rigidbody>();
        playerAnimation = new ShooterAnimationController(GetComponent<Animator>());
        weapon = GetComponent<WeaponScript>();
        weapon.shootCooldown = 0;
    }

    void Start()
    {
        lastShotTime = Time.time;
    }

    void Update()
    {        
    }
    
    void FixedUpdate()
    {
        HandleInput();
        Move(movementDir, animSpeed);
        Rotate();
        Attack();
    }
    
    // Rotate w.r.t. mouse pos
    void Rotate()
    {
        Quaternion rot = Quaternion.LookRotation(lookPos - transform.position, Vector3.up);
        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }

    void Attack()
    {
        // shooting when clicked or holding down the click
        bool shoot = Input.GetButtonDown("Fire1");
        shoot = shoot || Input.GetButton("Fire1");
        if (shoot && (Time.time - lastShotTime > weapon.shootingRate))
        {
            lastShotTime = Time.time;
            if (weapon != null)
            {                
                Vector3 shotDirection = transform.forward;
                bool canAttack = weapon.Attack(false, shotDirection);
                if (canAttack)
                    SoundEffectsHelper.Instance.MakeGunShotSound();
            }
        }
    }

    void HandleInput()
    {
        //keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementDir = new Vector3(horizontal, 0, vertical);        
        animSpeed = Mathf.Max(Mathf.Abs(horizontal), Mathf.Abs(vertical));        

        //mouse
        Vector3 mousePos = Input.mousePosition;
        Ray ray = cam.ScreenPointToRay(mousePos);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100))
        {
            lookPos = hit.point;
            lookPos.z += proCam.OffsetY / cam.transform.position.y; //balancing procam offset error
        }
    }

    void Move(Vector3 movementDir, float animSpeed)
    {
        Vector3 movement = movementDir.normalized * Time.fixedDeltaTime * speed;
        rb.MovePosition(rb.transform.position + movement);
        playerAnimation.SetSpeed(animSpeed);
    }

    void OnDestroy()
    {
        // Game Over.
        // Add the script to the parent because the current game
        // object is likely going to be destroyed immediately.
        //transform.parent.gameObject.AddComponent<GameOverScript>();
    }

}
