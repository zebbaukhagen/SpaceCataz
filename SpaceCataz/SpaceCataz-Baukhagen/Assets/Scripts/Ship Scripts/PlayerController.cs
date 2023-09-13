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

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// PlayerController controls the player ship, provides movement and tracks the shipVitals
    /// </summary>
    
    [SerializeField] private SpriteRenderer playerDamage; // tracks the players dynamic damage indicators
    [SerializeField] private List<Sprite> damageSprites = new(); // holds the potential damage sprites
    [SerializeField] private List<GameObject> projectileSpawns = new(); // holds the potential projectile spawns
    [SerializeField] private GameObject projectilePrefab; // holds the projectile to fire

    private readonly List<GameObject> activeProjectileSpawn = new(); // holds the active turrets

    [SerializeField] Transform playerSpawn; // holds the spawn location
    ShipVitals shipVitals; //  reference to an instance of the ship's vitals 

    private bool invincible = false;

    public bool Invincible { get => invincible; set => invincible = value; }

    // Start is called before the first frame update
    void Start()
    {
        shipVitals = new(1); // make a new shipVitals
        UpgradeShip(); // set the upgrade values upon start
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireWeapons();
        }
    }



    private void UpdateVisualShipDamage()
    {
        // normalizes the damage and then assigns the appropriate damage sprite
        float damagePercentile = Mathf.InverseLerp(0, shipVitals.MaxHealth, shipVitals.Health);

        if (damagePercentile >= .8f)
        {
            playerDamage.sprite = null;
        }
        else if (damagePercentile < .8f && damagePercentile >= .6f)
        {
            playerDamage.sprite = damageSprites[0];
        }
        else if (damagePercentile < .6f && damagePercentile >= .3f)
        {
            playerDamage.sprite = damageSprites[1];
        }
        else if (damagePercentile < .3f)
        {
            playerDamage.sprite = damageSprites[2];
        }
    }

    private void FireWeapons()
    {
        // goes through each active projectile spawn and get a projectile from the pool
        foreach (GameObject projectileSpawn in activeProjectileSpawn)
        {
            GameObject projectile = ObjectPool.sharedInstance.GetPooledObject(projectilePrefab.tag);
            if (projectile != null)
            {
                projectile.transform.SetPositionAndRotation(projectileSpawn.transform.position, projectileSpawn.transform.rotation);
                projectile.SetActive(true);
            }
        }
    }

    public void UpgradeShip()
    {
        // this method isn't really used because I didn't implement leveling up or upgrades yet
        if (shipVitals.UpgradeLevel == 1)
        {
            activeProjectileSpawn.Add(projectileSpawns[0]);
        }
        else if (shipVitals.UpgradeLevel == 2)
        {
            activeProjectileSpawn.Remove(projectileSpawns[0]);
            activeProjectileSpawn.Add(projectileSpawns[1]);
            activeProjectileSpawn.Add(projectileSpawns[2]);
        }
        else if (shipVitals.UpgradeLevel == 3)
        {
            activeProjectileSpawn.Add(projectileSpawns[0]);
        }
        else
        {
            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile") || collision.gameObject.CompareTag("EnemyProjectile_1"))
        {
            // if the player collides with any enemy projectiles and isn't invincible, take damage and update damage sprites
            if (!invincible)
            {
                shipVitals.TakeDamage(collision.gameObject.GetComponent<Projectile>());
                UpdateVisualShipDamage();
                GameManager.Instance.UpdateHealthbar(shipVitals.Health, shipVitals.MaxHealth);
            }
            // finally, check for death
            CheckForDeath();
        }
    }

    private void CheckForDeath()
    {
        // if players shipVitals are 0, update the players lives, start respawn sequence and get a new shipVitals
        if (shipVitals.Health == 0)
        {
            GameManager.Instance.UpdatePlayerLives();
            if (GameManager.Instance.PlayerLives >= 0)
            {
                GameManager.Instance.Respawn();
                shipVitals = new(1);
                UpdateVisualShipDamage();
                GameManager.Instance.UpdateHealthbar(shipVitals.Health, shipVitals.MaxHealth);
            }
        }
    }
}
