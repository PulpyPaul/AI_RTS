﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
 * @author Ben Fairlamb
 * @author Paul Galatic
 * 
 * Class designed to handle City-specific functionality and state. Like with 
 * other game objects, menus and UI elements should be handled by observers.
 * **/
public class City : Unit
{
    // **         //
    // * FIELDS * //
    //         ** //

    public Transform spawnPoint;
    public SpawnRingRig spawnRingRig;

    public const string IDENTITY = "CITY";
    public const float MAX_HEALTH = 500f;
    public const int MAX_INCOME_LEVEL = 8;

    private const string DEFAULT_NAME = "Dylanto";
    // The health a city has just after it's captured
    private const float CAPTURED_HEALTH = 50f;
    // The rate at which a city's health regenerates (per second)
    private const float REGENERATION_RATE = CAPTURED_HEALTH / 5f;
    // Regeneration is delayed after taking damage
    private const float REGENERATION_DELAY = 2f;
    // Enemy units will suffer recoil upon contact with a City
    private const float PUSH_FORCE = 400f;
    private const float PUSH_RADIUS = 50f;
    private const int COST = 500;
    private const int MIN_INCOME_LEVEL = 1;
    private const int DEFAULT_INCOME_LEVEL = 8;
    private const int PUSH_COOLDOWN = 2;

    private int incomeLevel;
    private bool delayRegen = true;

    // **          //
    // * METHODS * //
    //          ** //

    /// <summary>
    /// Returns the class name of the unit in the form of a string.
    /// </summary>
    public override string Identity()
    {
        return IDENTITY;
    }

    /// <summary>
    /// Returns how much a city would cost to place.
    /// </summary>
    public override int Cost()
    {
        return COST;
    }

    /// <summary>
    /// Activates the city.
    /// </summary>
    public override void Activate()
    {
        maxHealth = MAX_HEALTH;
        incomeLevel = DEFAULT_INCOME_LEVEL;

        base.Activate();

        spawnRingRig.Init(team.color);

        StartCoroutine(Regenerate());

    }

    /// <summary>
    /// Deactivates the city.
    /// </summary>
    public override void Deactivate()
    {
        base.Deactivate();
        team.cities.Remove(this);
        Toolbox.CityPool.Return(this);
    }

    /// <summary>
    /// Causes the city to lose health. 
    /// </summary>
    /// <param name="damage">The amount of damage to take.</param>
    /// <param name="source">The source of the damage.</param>
    public override void UpdateHealth(float damage, Unit source)
    {
        delayRegen = true;
        base.UpdateHealth(damage, source);
    }

    /// <summary>
    /// What to do when the unit collides with another unit that's not on the 
    /// same team.
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();
        if (unit != null && !(unit.Team.Equals(team)))
        {
            UpdateHealth(-UnityEngine.Random.Range(10f, 20f), unit);
        }
    }

    /// <summary>
    /// If the city's health goes to or below 
    /// zero, its team changes to the team that caused the damage, and its 
    /// health gets set to a CAPTURED_HEALTH (to prevent rapid capturing / 
    /// recapturing in the event of a major skirmish).
    /// </summary>
    /// <param name="capturer">The capturer of the city.</param>
    protected override void OnDeath(Unit capturer)
    {
        NotifyAll(Invocation.CITY_CAPTURED, capturer.Team);
        UpdateColor();
        spawnRingRig.UpdateColor(team.color);
        UpdateHealth(CAPTURED_HEALTH - health, capturer);
    }

    /// <summary>
    /// Cities will regenerate their health over time.
    /// </summary>
    private IEnumerator Regenerate()
    {
        WaitForSeconds wait = new WaitForSeconds(REGENERATION_DELAY);
        while (true)
        {
            if (delayRegen)
            {
                delayRegen = false;
                yield return wait;
            }
            base.UpdateHealth(REGENERATION_RATE * Time.deltaTime);
            yield return 0f;
        }
    }

    /// <summary>
    /// Pushes all units away from the city.
    /// 
    /// Activates every <PUSH_COOLDOWN> seconds.
    /// </summary>
    private IEnumerator Push()
    {
        Rigidbody body;
        float cooldown = PUSH_COOLDOWN;

        while (true)
        {
            if (cooldown > 0f)
            {
                cooldown -= Time.deltaTime;
                yield return null;
                continue;
            }

            Collider[] nearby = Physics.OverlapSphere(transform.position, PUSH_RADIUS, ~Toolbox.Terrain.ignoreAllButTerrain);
            foreach (Collider c in nearby)
            {
                body = c.gameObject.GetComponentInParent<Rigidbody>();
                if (body)
                {
                    body.AddForce((c.gameObject.transform.position - transform.position).normalized * PUSH_FORCE, ForceMode.Impulse);
                }
            }

            cooldown = PUSH_COOLDOWN;
        }

    }

    /// <summary>
    /// Pull up the menu when the unit is clicked.
    /// </summary>
    private void OnMouseDown()
    {
        Highlight();
        NotifyAll(Invocation.CITY_MENU);
    }

    private void Start()
    {
        spawnRingRig = GetComponentInChildren<SpawnRingRig>();
        StartCoroutine(Push());
    }

    /// <summary>
    /// Returns the location at which to spawn units.
    /// </summary>
    public Transform SpawnPoint
    {
        get { return spawnPoint; }
    }

    /// <summary>
    /// Gets the Income Level of the city.
    /// </summary>
    public int IncomeLevel
    {
        get { return incomeLevel; }
    }

}
