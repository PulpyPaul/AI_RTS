﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Unit {
    // Author: Ben Fairlamb
    // Author: Paul Galatic
    // Purpose: Infantry unit

    // Public constants
    public const string IDENTITY = "Infantry";

    // Public fields
    public Rigidbody m_HoverPull;

    // Private constants
    private const ArmorType ARMOR_TYPE = ArmorType.M_ARMOR;
    private const DamageType DMG_TYPE = DamageType.BULLET;
    // Default values
    private const int MAXHEALTH = 100;
    private const int DAMAGE = 10;
    private const int RANGE = 25;


    // Methods
    // Use this for initialization
    void Start () {
        base.Init();
        // Handle constants
        armorType = ARMOR_TYPE;
        dmgType = DMG_TYPE;
        // Handle default values
        // team = "NULL";
        unitName = IDENTITY;
        maxHealth = MAXHEALTH;
        dmg = DAMAGE;
        range = RANGE;
        // Handle fields
        health = Random.Range(10f, 90f);
    }

    /// <summary>
    /// Handles general physics properties of units.
    /// </summary>
    public void FixedUpdate()
    {
        // Units will hover.
        m_HoverPull.AddRelativeForce(Vector3.up * (m_HoverPull.mass * 100f * Mathf.Abs(Physics.gravity.y)));
    }

    /// <summary>
    /// Returns identity of the unit, for disambiguation purposes.
    /// </summary>
    public override string Identity()
    {
        return IDENTITY;
    }

    /// <summary>
    /// Attack the specified target.
    /// </summary>
    /// <param name="target">Target to attack.</param>
    public override void Attack(Unit target)
	{
	}

	/// <summary>
	/// Take specified damage.
	/// </summary>
	/// <param name="dmg">Damage to Take.</param>
	/// <param name="amount">Amount.</param>
	public override void TakeDmg(int amount)
	{
	}

	/// <summary>
	/// Kill this instance.
	/// </summary>
	public override void Kill()
	{
	}
}
