using UnityEngine;

public class ShooterAnimationController
{
    public ShooterAnimationController(Animator animator)
    {
        anim = animator;
    }

    private float speed = 0f;
    public float Speed
    {
        get { return speed; }
        set
        {
            speed = value;
            anim.SetFloat("speed", speed);
        }
    }

    private Animator anim;    

    public void SetSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetBool(int hash, bool value)
    {
        anim.SetBool(hash, value);
    }
}
