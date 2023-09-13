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

public class EnemyClusterController : MonoBehaviour
{
    /// <summary>
    /// This class controls the cluster of enemies as a group
    /// It holds references to the total numbers of enemies in the cluster
    /// And when they reach 0, it tells the gameManager to go to the next level
    /// </summary>

    [SerializeField] private List<EnemyController> enemiesInCluster = new(); // list of enemies in cluster
    [SerializeField] private float timeBetweenFiringInSeconds = 3; // static time between firing signals

    private readonly float movementSpeed = 3; // the movement speed for the cluster to move
    private Direction currentDirection = Direction.RIGHT; // which direction it is going

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SendFireSignal()); // start sending firing signals to the ships in the cluster
    }

    private void FixedUpdate()
    {
        MoveCluster(); // move the cluster each frame
    }

    private IEnumerator SendFireSignal()
    {
        // send firing signals to each ship in the cluster
        // with some chosen at random, not every ship fires every signal
        while (enemiesInCluster.Count > 0)
        {
            yield return new WaitForSeconds(timeBetweenFiringInSeconds); // wait for seconds before firing
            foreach (EnemyController ship in enemiesInCluster) // go through the entire cluster
            {
                if (Random.Range(0, 2) == 0) // flip a coin to see if each ship should fire
                {
                    ship.FireWeapons();
                }

            }
        }
        // when there are no more enemies in the cluster, stop the coroutine
        StopCoroutine(SendFireSignal()); 
    }

    public void ChangeDirection()
    {
        // simply toggle cluster direction
        if (currentDirection == Direction.RIGHT)
        {
            currentDirection = Direction.LEFT;
        }
        else
        {
            currentDirection = Direction.RIGHT;
        }
    }

    private void MoveCluster()
    {
        // apply movement to the cluster based on which direction it should be moving
        float xMovement = movementSpeed;
        if (currentDirection == Direction.RIGHT)
        {
            xMovement = movementSpeed;
        }
        else if (currentDirection == Direction.LEFT)
        {
            xMovement = -movementSpeed;
        }

        Vector3 movement = new(xMovement, 0f, 0f);

        transform.position = transform.position += movement * Time.deltaTime;
    }

    public void RemoveFromClusterList(GameObject gameObject)
    {
        // called when an enemy dies
        enemiesInCluster.Remove(gameObject.GetComponent<EnemyController>());
        if (enemiesInCluster.Count == 0)
        {
            // when the enemies in the cluster reach 0, go to the next sequence
            GameManager.Instance.LoadNextSequence();
        }
    }
}

public enum Direction
{
    LEFT,
    RIGHT,
}
