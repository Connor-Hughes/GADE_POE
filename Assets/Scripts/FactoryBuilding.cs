using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryBuilding : Building
{
    private string unitType; // all the properties for the factory buildings

    public string UnitType
    {
        get { return unitType; }
        set { unitType = value; }
    }

    private int spawnSpeed;

    public int SpawnSpeed
    {
        get { return spawnSpeed; }
        set { spawnSpeed = value; }
    }

    private int productionSpeed;

    public int ProductionSPeed
    {
        get { return productionSpeed; }
        set { productionSpeed = value; }
    }


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

    private int spawnPointX;

    public int SpawnPointX
    {
        get { return spawnPointX; }
        set { spawnPointX = value; }
    }

    private int spawnPointY;

    public int SpawnPointY
    {
        get { return spawnPointY; }
        set { spawnPointY = value; }
    }


    public FactoryBuilding(int x, int y, int hp, Faction fac, string sym, int Pspeed, string uType) : //constructor
        base(x, y, hp, fac, sym)
    {
        ProductionSPeed = Pspeed;
        unitType = uType;

    }

    public override bool Destruction() // method to destroy a building when the health reached 0
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

    public string SpawnUnits()
    {
        return UnitType;
    }

    public override string ToString() //ToString method to let the user know what type of unit they are looking at, the HP, the atk damage, what resource the building is producing etc
    {
        return " Pekka Hut: X: " + posX
                                 + " Y: " + posY
                                 + "\nHealth: " + Health
                                 + "\nUnitSpawn: " + UnitType
                                 + "\nProduction Speed: " + ProductionSPeed
                                 + "\nFaction: " + Faction;
    }
}
