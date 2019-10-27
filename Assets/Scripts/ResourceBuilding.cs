using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ResourceBuilding : Building
{


    public int PosX
    {
        get { return base.posX; }
        set { posX = value; }
    }

    public int PosY
    {
        get { return base.posY; }
        set { posY = value; }
    }

    public int Health
    {
        get { return base.health; }
        set { health = value; }
    }

    public Faction Faction
    {
        get { return base.faction; }
        set { faction = value; }
    }

    public string Symbol
    {
        get { return base.symbol; }
        set { symbol = value; }
    }

    public int ResourcesLeft
    {
        get { return ResourcePool; }
    }


    public int ResourcesGathered
    {
        get { return ResourceGenerated; }
    }



    private int ResourceGenerated = 0;
    private int GeneratePerRound = 0;
    private int ResourcePool = 1000;
    private ResourceType Resource;


    public ResourceBuilding(int x, int y, int hp, Faction fac, string sym, int ResPerRound) : //constructor
        base(x, y, hp, fac, sym)
    {
        GeneratePerRound = ResPerRound;
    }

    public override bool Destruction() // same method to destroy Buildings when the health reaches 0
    {
        if (Health <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void GenerateResources() // telling ther resource buildings when they can produce resources
    {
        if (ResourcePool > 0)
        {
            ResourcePool -= GeneratePerRound;
            ResourceGenerated += GeneratePerRound;
        }
    }



    public override string ToString() //ToString method to let the user know what type of unit they are looking at, the HP, the atk damage, what resource the building is producing etc
    {
        return " Mine: X: " + posX
               + " Y: " + posY
               + "\nHealth: " + Health
               + "\nResource: " + Resource + ": " + ResourceGenerated
               + "\nResource: " + ResourcePool
               + "\nFaction: " + Faction;
    }
}
