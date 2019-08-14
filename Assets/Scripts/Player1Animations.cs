using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Animations : MonoBehaviour
{
    private Animator field_animator;
    private Animator field_animator_fireSwordAnimation;


    // Start is called before the first frame update
    void Start()
    {
        field_animator = GetComponentInChildren<Animator>();
        field_animator_fireSwordAnimation = transform.GetChild(1).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
     * The Animator has a parameter called "Move", that when greater than 0 will activate the Run Animation.
     * When the Move value is 0, the Idle animation will activate. This method affects the value of Move so
     * that the correct animation can be used. 
     */ 

    public void MovePlayer(float p_move)
    {
        field_animator.SetFloat("Move", Mathf.Abs(p_move));
    }

    /*
     * Affecting the bool value of CanJump parameter in the Animator. 
     */ 

    public void PlayerJump(bool p_canJump)
    {
        field_animator.SetBool("isJumping", p_canJump);
    }

    public void Attack()
    {
        field_animator.SetTrigger("Attack");
        field_animator_fireSwordAnimation.SetTrigger("RegAttackFire");
    }

    public void Death()
    {
        field_animator.SetTrigger("Death");
    }
}
