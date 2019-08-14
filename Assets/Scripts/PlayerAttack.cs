using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private bool field_bool_canDoDamageNow = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Collision: " + collision.name);

        Interface_CanBeDamaged interface_CanBeDamaged = collision.GetComponent<Interface_CanBeDamaged>();
        if (interface_CanBeDamaged != null)
        {
            if (field_bool_canDoDamageNow == true)
            {
                interface_CanBeDamaged.Damage();
                field_bool_canDoDamageNow = false;
                StartCoroutine(DamageCooldown());
            }
        }
    }

    /*
     * Creates a cooldown for the Damage() function so that an enemy can only take damage every 0.5f. 
     */ 

    IEnumerator DamageCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        field_bool_canDoDamageNow = true;
    }
}
