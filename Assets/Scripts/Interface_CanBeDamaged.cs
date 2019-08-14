using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Interface_CanBeDamaged
{
    /*
     * This works as a binding contract for those classes inheriting this interface, forcing them
     * to use the included properties and methods. 
     */ 

    int property_int_hitpoints { get; set; }
    void Damage();
}

