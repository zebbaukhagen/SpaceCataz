using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer shipDamage;
    [SerializeField] private List<Sprite> damageSprites = new();

    [SerializeField] private List<GameObject> projectileSpawns = new();
    [SerializeField] private GameObject projectilePrefab;

    ShipVitals shipVitals; //  reference to an instance of the ship's vitals 

    // Start is called before the first frame update
    void Start()
    {
        shipVitals = new(1);
    }

    public void UpdateVisualShipDamage()
    {
        float damagePercentile = Mathf.InverseLerp(0, shipVitals.MaxHealth, shipVitals.Health);

        if (damagePercentile >= .8f)
        {
            shipDamage.sprite = null;
        }
        else if (damagePercentile < .8f && damagePercentile >= .6f)
        {
            shipDamage.sprite = damageSprites[0];
        }
        else if (damagePercentile < .6f && damagePercentile >= .3f)
        {
            shipDamage.sprite = damageSprites[1];
        }
        else if (damagePercentile < .3f)
        {
            shipDamage.sprite = damageSprites[2];
        }
    }

    public void FireWeapons(int upgradeLevel)
    {
        if (upgradeLevel == 1)
        {
            Instantiate(projectilePrefab, projectileSpawns[0].transform.position, Quaternion.identity);
        }
        else if (upgradeLevel == 2)
        {
            Instantiate(projectilePrefab, projectileSpawns[1].transform.position, Quaternion.identity);
            Instantiate(projectilePrefab, projectileSpawns[2].transform.position, Quaternion.identity);
        }
        else if (upgradeLevel == 3)
        {
            Instantiate(projectilePrefab, projectileSpawns[0].transform.position, Quaternion.identity);
            Instantiate(projectilePrefab, projectileSpawns[1].transform.position, Quaternion.identity);
            Instantiate(projectilePrefab, projectileSpawns[2].transform.position, Quaternion.identity);
        }

    }
}
