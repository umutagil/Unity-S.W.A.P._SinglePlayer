using UnityEngine;

public class PlayerAnimation
{
    public PlayerAnimation(Animator animator)
    {
        anim = animator;
    }

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
    private float speed = 0f;    
       
    public void SetSpeed(float speed)
    {
        this.Speed = speed;
    }

    public void SetBool(int hash, bool value)
    {
        anim.SetBool(hash, value);
    }


}