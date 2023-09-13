//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using UnityEngine;

public class EnemyProjectile : Projectile
{
    // same as projectile but moves downward
    public readonly new int damageValue = 10;
    public override void ApplyMovement()
    {
        Vector3 movement = new(0, -speed, 0);
        transform.position += movement * Time.deltaTime;
    }
}

