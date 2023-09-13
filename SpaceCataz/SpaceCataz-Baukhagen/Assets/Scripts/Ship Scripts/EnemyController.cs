//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// this class controls each individual enemy ship
    /// </summary>
    
    [SerializeField] private EnemyClusterController clusterController; // reference to the cluster controller
    [SerializeField] private List<GameObject> projectileSpawns = new(); // list of possible firing spawns
    [SerializeField] private GameObject projectilePrefab; // what projectile to fire
    [SerializeField] private int upgradeLevel; // what upgrade level to start at

    private readonly List<GameObject> activeTurrets = new(); // which turrets are active
    private ShipVitals shipVitals; //  reference to an instance of the ship's vitals 

    // Start is called before the first frame update
    void Start()
    {
        shipVitals = new(upgradeLevel); // make a new shipVitals
        clusterController = GetComponentInParent<EnemyClusterController>(); // get the cluster controller
        UpgradeShip(); // set the appropriate values by upgrading to the base level
    }

    public void FireWeapons()
    {
        // goes through the list of active turrets and gets a pooled projectile to fire
        foreach (GameObject projectileSpawn in activeTurrets)
        {
            GameObject projectile = ObjectPool.sharedInstance.GetPooledObject(projectilePrefab.tag);
            if (projectile != null)
            {
                projectile.transform.SetPositionAndRotation(projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                projectile.SetActive(true);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall")) 
        {
            // if any enemy hits a wall, send the signal to the cluster controller to change directions
            clusterController.ChangeDirection();
        }
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            // if enemy hits a player projectile, take damage and check for death
            shipVitals.TakeDamage(collision.gameObject.GetComponent<Projectile>());
            CheckForDeath();
        }
    }

    public void UpgradeShip()
    {
        // set appropriate upgrade values
        if (shipVitals.UpgradeLevel == 1)
        {
            activeTurrets.Add(projectileSpawns[0]);
        }
        else if (shipVitals.UpgradeLevel == 2)
        {
            activeTurrets.Add(projectileSpawns[0]);
            activeTurrets.Add(projectileSpawns[1]);
        }
        else if (shipVitals.UpgradeLevel == 3)
        {
            activeTurrets.Add(projectileSpawns[0]);
            activeTurrets.Add(projectileSpawns[1]);
            activeTurrets.Add(projectileSpawns[2]);
        }
        else
        {
            return;
        }
    }

    private void CheckForDeath()
    {
        // check for enemy death, if dead remove self from cluster, give the player score relative to upgrade level and set self inactive
        if (shipVitals.Health == 0)
        {
            clusterController.RemoveFromClusterList(gameObject);
            GameManager.Instance.UpdateScore(upgradeLevel * 20);
            gameObject.SetActive(false);
        }
    }
}
