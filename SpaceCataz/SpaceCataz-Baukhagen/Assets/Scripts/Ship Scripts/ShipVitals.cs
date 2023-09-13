//////////////////////////////////////////////
//Assignment/Lab/Project: Final Project
//Name: Zebulun Baukhagen
//Section: 2023SP.SGD.213.2172
//Instructor: Brian Sowers
//Date: 05/01/2023
/////////////////////////////////////////////

using UnityEngine;

public class ShipVitals
{
    /// <summary>
    /// ShipVitals tracks the various stats that a given ship can have
    /// </summary>
    private int upgradeLevel;
    private int health;
    private readonly int maxHealth;

    #region Properties
    public int UpgradeLevel { get => upgradeLevel; set => upgradeLevel = value; }

    public int Health { get => health; }

    public int MaxHealth { get => maxHealth; }
    #endregion

    public ShipVitals(int initialLevel)
    {
        // constructor
        upgradeLevel = initialLevel;
        maxHealth = initialLevel * 100;
        health = initialLevel * 100;
    }

    public void TakeDamage(Projectile projectile)
    {
        // take damage and clamp to range
        health -= projectile.damageValue;
        health = Mathf.Clamp(health, 0, maxHealth);
    }
}
