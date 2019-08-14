using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderAnimationHandler : MonoBehaviour
{
    EnemySpider field_enemySpider;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the EnemySpider component
        field_enemySpider = transform.parent.GetComponent<EnemySpider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FireProjectile()
    {
        Debug.Log("EnemySpiderAnimationHandler.FireProjectile() called");
        field_enemySpider.Attack();

    }
}
