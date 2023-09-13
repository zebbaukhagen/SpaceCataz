using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 3f;

    void FixedUpdate()
    {
        // get our player input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        translateMovement(xInput, yInput);
    }

    private void translateMovement(float x, float y)
    {
        Vector3 movement = new(x * movementSpeed, y * movementSpeed, 0);

        transform.position += movementSpeed * Time.deltaTime * movement;

    }
}
