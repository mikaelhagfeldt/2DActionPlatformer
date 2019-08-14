using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : EnemyAbstract, Interface_CanBeDamaged
{
    public GameObject field_gameObject_acidBallPrefab;

    public int property_int_hitpoints { get; set ; }

    public override void Initialize()
    {
        base.Initialize();
        property_int_hitpoints = base.field_int_hitpoints;
    }

    public override void EnemyMovement()
    {
        // Removed the base implementation because the spider is not suppose to move
    }

    public void Damage()
    {
        Debug.Log("EnemySpider.Damage() called");
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

    public void Attack()
    {
        // Instantiating the acid ball
        // Quaternion.identity means ignore rotation of the object
        Instantiate(field_gameObject_acidBallPrefab, transform.position, Quaternion.identity);
    }

    public override void Update()
    {

    }
}
