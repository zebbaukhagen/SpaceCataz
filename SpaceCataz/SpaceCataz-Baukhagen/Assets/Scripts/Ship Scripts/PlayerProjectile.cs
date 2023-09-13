//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    // same as projectile but moves upwards
    public readonly new int damageValue = 20;
    public override void ApplyMovement()
    {
        Vector3 movement = new(0, speed, 0);
        transform.position += movement * Time.deltaTime;
    }
}
