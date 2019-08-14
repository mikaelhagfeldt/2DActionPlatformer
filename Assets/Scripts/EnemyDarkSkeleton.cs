using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDarkSkeleton : EnemyAbstract, Interface_CanBeDamaged
{
    public int property_int_hitpoints { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        property_int_hitpoints = base.field_int_hitpoints;
    }

    public override void EnemyMovement()
    {
        base.EnemyMovement();
    }

    public void Damage()
    {
        Debug.Log("EnemyDarkSkeleton.Damage() called");
        property_int_hitpoints += -1;
        field_animator.SetTrigger("Hit");
        field_bool_enemyIsHit = true;
        field_animator.SetBool("InCombat", true);
        if (property_int_hitpoints < 1)
        {
            field_bool_enemyIsDead = true;
            field_animator.SetTrigger("Death");
        }
    }

    public override void Update()
    {
        base.Update();
    }

    public override void flipSprite()
    {
        if (field_vector3 == field_transform_waypoint1.position)
        {
            field_spriteRenderer.flipX = false;
        }
        else
        {
            field_spriteRenderer.flipX = true;
        }
    }

}
