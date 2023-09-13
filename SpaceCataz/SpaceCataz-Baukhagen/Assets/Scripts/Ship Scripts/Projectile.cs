//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 3.5f;
    public int damageValue;

    // Update is called once per frame
    void FixedUpdate()
    {
        ApplyMovement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // set self inactive and back into the pool if it hits any ships
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnBecameInvisible()
    {
        // if projectile exits the camera view, set inactive
        gameObject.SetActive(false);
    }

    public virtual void ApplyMovement()
    {
        // move projectile, must be overridden
        Vector3 movement = new(0, speed, 0);
        transform.position += movement * Time.deltaTime;
    }
}
