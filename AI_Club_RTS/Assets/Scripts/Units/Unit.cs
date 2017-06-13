﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour {

    // Author: Ben Fairlamb
    // Author: Paul Galatic
    // Purpose: Base class for all Units
    // Limitations: Meh

    // public component fields
    public Canvas m_Canvas;

    // public fields, can easily be changed for balancing or by gameplay
    public string team;
    public float maxHealth;
    public float health;
    public float dmg;
    public float range;
    public int cost;

    // protected fields, more related to fundamentals of unit type
    protected ArmorType armorType;
    protected DamageType dmgType;

    // protected fields related to physics
    protected Rigidbody body;
    protected Collider collision;

	// Properties
	/// <summary>
	/// Gets the Team of the unit.
	/// </summary>
	public string Team {
		get { return team; }
	}

    /// <summary>
    /// Gets the type of the unit.
    /// </summary>
    public ArmorType ArmorType
    {
        get { return armorType; }
    }

    /// <summary>
    /// Gets the unit's Type of Damage. (Possible use for Rock Paper Scissors effect: Temp)
    /// </summary>
    public DamageType DmgType
    {
        get { return dmgType; }
    }

    /// <summary>
    /// Gets the unit's Maximum Health.
    /// </summary>
    public float MaxHealth {
		get { return maxHealth; }
	}

	/// <summary>
	/// Gets the unit's current Health.
	/// </summary>
	public float Health {
		get { return health; }
	}

	/// <summary>
	/// Gets the Damage dealt by the unit.
	/// </summary>
	public float Dmg {
		get { return dmg; }
	}

	/// <summary>
	/// Gets the unit's Range.
	/// </summary>
	public float Range {
		get { return range; }
	}

	/// <summary>
	/// Gets the Cost of the unit.
	/// </summary>
	public int Cost {
		get { return cost; }
	}

    /// <summary>
    /// Selects the unit.
    /// </summary>
    public void Select()
    {
        m_Canvas.enabled = true; // highlight unit
    }

    /// <summary>
    /// Deselects the unit.
    /// </summary>
    public void Deselect()
    {
        m_Canvas.enabled = false; // remove highlight
    }

	/// <summary>
	/// Attack the specified target.
	/// </summary>
	/// <param name="target">Target to attack.</param>
	public abstract void Attack(Unit target);

	/// <summary>
	/// Take specified damage.
	/// </summary>
	/// <param name="dmg">Damage to Take.</param>
	public abstract void TakeDmg(int dmg);

	/// <summary>
	/// Kill this instance.
	/// </summary>
	public abstract void Kill();

}

/// <summary>
/// Type of armor. Armor type affects unit speed and damage resistance.
/// </summary>
public enum ArmorType
{
    H_ARMOR, M_ARMOR, L_ARMOR
}

/// <summary>
/// Type of damage. Explosive damage triggers knockback effects.
/// </summary>
public enum DamageType
{
    EXPLOSIVE, BULLET
}