using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAbstract : MonoBehaviour
{
    // protected --> only this classes and classes that inherits this class can access these variables
    // SerializeField --> can manipulate variables from the inspector in unity
    [SerializeField] protected int field_int_hitpoints;
    [SerializeField] protected float field_float_velocity;
    [SerializeField] protected int field_int_numberOfItems;

    // waypoints for our enemies
    [SerializeField] protected Transform field_transform_waypoint1;
    [SerializeField] protected Transform field_transform_waypoint2;

    // variables for making our enemy move between waypoints
    protected Animator field_animator;
    protected SpriteRenderer field_spriteRenderer;
    protected Vector3 field_vector3;

    //variable for tracking player distance from enemy
    protected Player1 field_player1;

    protected bool field_bool_enemyIsHit = false;

    protected bool field_bool_enemyIsDead = false;

    private void Start()
    {
        Initialize();
    }

    public virtual void Initialize()
    {
        field_animator = GetComponentInChildren<Animator>();
        field_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        field_player1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>();
    }

    public virtual void Update()
    {
        if (field_animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && field_animator.GetBool("InCombat") == false)
        {
            return;
        }

        if (field_bool_enemyIsDead == false)
        {
            EnemyMovement();
        }

        
    }

    public virtual void EnemyMovement()
    {
        flipSprite();

        if (transform.position == field_transform_waypoint1.position)
        {
            field_vector3 = field_transform_waypoint2.position;
            field_animator.SetTrigger("Idle");
        }
        else if (transform.position == field_transform_waypoint2.position)
        {
            field_vector3 = field_transform_waypoint1.position;
            field_animator.SetTrigger("Idle");
        }

        if (field_bool_enemyIsHit == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, field_vector3, field_float_velocity * Time.deltaTime);
        }

        float float_distance = Vector3.Distance(transform.localPosition, field_player1.transform.localPosition);
        if (float_distance > 2.1f)
        {
            field_bool_enemyIsHit = false;
            field_animator.SetBool("InCombat", false);
        }

        Vector3 vector3_direction = field_player1.transform.localPosition - transform.localPosition;
        //Debug.Log("vector3_direction : " + vector3_direction); Positive on the right, negative on the left

        if (vector3_direction.x > 0 && field_animator.GetBool("InCombat") == true)
        {
            field_spriteRenderer.flipX = false;
        }
        else if (vector3_direction.x < 0 && field_animator.GetBool("InCombat") == true)
        {
            field_spriteRenderer.flipX = true;
        }

    }

    public virtual void flipSprite()
    {
        if (field_vector3 == field_transform_waypoint1.position)
        {
            field_spriteRenderer.flipX = true;
        }
        else
        {
            field_spriteRenderer.flipX = false;
        }
    }
}
