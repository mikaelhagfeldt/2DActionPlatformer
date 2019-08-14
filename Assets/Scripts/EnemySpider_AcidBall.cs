using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider_AcidBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroys the acid ball after a given time
        Destroy(gameObject, 4.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the acid ball constantly to the right
        transform.Translate(Vector3.right * 2 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
         * If the acid ball detects tag "Player", then the Damage() function from the interface will be called.
         */ 

        if (collision.tag == "Player")
        {
            Interface_CanBeDamaged interface_CanBeDamaged = collision.GetComponent<Interface_CanBeDamaged>();
            if (interface_CanBeDamaged != null)
            {
                interface_CanBeDamaged.Damage();
                Destroy(gameObject);
            }
        }
    }
}
