﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * @author Paul Galatic
 * 
 * A struct that contains all relevant information denoting to which team a
 * unit or city belongs.
 * **/
public class Team
{
    // **         //
    // * FIELDS * //
    //         ** //

    // The name of the team.
    public readonly string title;
    // The color, or "uniform," of the team.
    public readonly Color color;
    // The Players that make up the team.
    public List<Player> members;
    // The mobile units owned by the team.
    public List<MobileUnit> mobiles;
    // The cities owned by the team.
    public List<City> cities;
    // This team's enemies.
    public List<Team> enemies;

    private bool active = true;

    // **              //
    // * CONSTRUCTOR * //
    //              ** //

    /// <summary>
    /// Constructs a new Team.
    /// </summary>
    public Team (string title = "NULL", Color color = default(Color))
    {
        this.title = title;
        this.color = color;
        members = new List<Player>();
        mobiles = new List<MobileUnit>();
        cities = new List<City>();
        enemies = new List<Team>();
    }

    // **          //
    // * METHODS * //
    //          ** //

    /// <summary>
    /// Activates the Team and "turns on" all of its members.
    /// </summary>
    public void Activate()
    {
        foreach (Player p in members)
        {
            p.Activate();
        }

        active = true;
    }

    /// <summary>
    /// Kills all units in a team, returns all its cities, and deactivates
    /// all its members.
    /// </summary>
    public void Deactivate()
    {
        // Using while loops because Deactivate iteratively shrinks the list
        while (mobiles.Count > 0)
        {
            mobiles[0].Deactivate();
        }
        while (cities.Count > 0)
        {
            cities[0].Deactivate();
        }
        foreach (Player p in members)
        {
            p.Deactivate();
        }

        active = false;

    }

    public bool IsActive
    {
        get { return active; }
    }

    public static bool operator ==(Team t1, Team t2)
    {
        return t1.Equals(t2);
    }

    public static bool operator !=(Team t1, Team t2)
    {
        return !(t1.Equals(t2));
    }

    public override bool Equals(object other) {
        if (!(other is Team)) { return false; }
        return (title == ((Team)other).title) && (color == ((Team)other).color);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
