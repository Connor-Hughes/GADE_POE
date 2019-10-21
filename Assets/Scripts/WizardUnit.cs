using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WizardUnit : Units
{
    public int PosX //properties for the New Wizard unit 
    {
        get { return posX; }
        set { base.posX = value; }
    }

    public int PosY
    {
        get { return posY; }
        set { base.posY = value; }
    }

    public int Health
    {
        get { return health; }
        set { base.health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }

    }

    public int Speed
    {
        get { return speed; }
    }

    public int Attack
    {
        get { return attack; }
    }

    public int AtkRange
    {
        get { return atkRange; }
    }

    public Faction FactionType
    {
        get { return factionType; }
    }

    public string Symbol
    {
        get { return symbol; }
    }


    public bool IsAtk
    {
        get { return isAtk; }
    }

    List<Units> units = new List<Units>();
    List<Building> buildings = new List<Building>();
    private int speedCounter = 1;
    Random r = new Random();
    Units ClosestUnit;

    public WizardUnit(string N, int x, int y, int hp, int spd, int atk, int attRange, Faction fac,
        string sym, // constructor
        bool iatk)
        : base(N, x, y, hp, spd, atk, attRange, fac, sym, iatk)
    {

    }

    public override void
        Move(int type) // all the move functions for the units once they have spawned and the the Buildings themselves
    {
        if (Health > MaxHealth * 0.5)
        {
            if (ClosestUnit is MeleeUnit)
            {
                MeleeUnit closestUnitM = (MeleeUnit) ClosestUnit;

                if (closestUnitM.posX > posX && posX < 20)
                {
                    posX++;
                }
                else if (closestUnitM.posX < posX && posX > 0)
                {
                    posX--;
                }

                if (closestUnitM.posY > posY && posY < 20)
                {
                    PosY++;
                }
                else if (closestUnitM.posY < posY && posY > 0)
                {
                    posY--;
                }
            }
            else if (ClosestUnit is RangedUnit)
            {
                RangedUnit closestUnitR = (RangedUnit) ClosestUnit;

                if (closestUnitR.PosX > posX && PosX < 20)
                {
                    posX++;
                }
                else if (closestUnitR.PosX < posX && posX > 0)
                {
                    posX--;
                }

                if (closestUnitR.PosY > posY && PosY < 20)
                {
                    posY++;
                }
                else if (closestUnitR.PosY < posY && posY > 0)
                {
                    posY--;
                }
            }
        }
    }

    public override void
        Combat(int type) // the method to get the units to fight their enemies and shows them who to fight
    {
        foreach (Units u in units)
        {
            if (u is MeleeUnit) //setting the attack variables if the oposition is a melee unit
            {
                MeleeUnit m = (MeleeUnit) u;

                if (m.posX == posX - 1 && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posY && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX - 1 && m.posY == posY)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX - 1 && m.posY == posY + 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX && m.posY == posY + 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY + 1)
                {
                    m.health -= 1;
                }


            }

            if (u is RangedUnit) //setting the attack variables if the oposition is a Ranged unit
            {
                RangedUnit m = (RangedUnit) u;

                if (m.posX == posX - 1 && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posY && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY - 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX - 1 && m.posY == posY)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX - 1 && m.posY == posY + 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX && m.posY == posY + 1)
                {
                    m.health -= 1;
                }
                else if (m.posX == posX + 1 && m.posY == posY + 1)
                {
                    m.health -= 1;
                }


            }
        }
    }

    public override void
        AttRange(List<Units> uni, List<Building> builds) // checking the range of the building and the units they spawn
    {

        units = uni;
        buildings = builds;

        ClosestUnit = Position();

        int enemyType;

        int xDis = 0, yDis = 0;

        int uDistance = 10000, bDistance = 10000;
        int distance;

        if (ClosestUnit is MeleeUnit)
        {
            MeleeUnit M = (MeleeUnit) ClosestUnit;
            xDis = Mathf.Abs((PosX - M.PosX) * (PosX - M.PosX));
            yDis = Mathf.Abs((PosY - M.PosY) * (PosY - M.PosY));

            uDistance = (int) Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }
        else if (ClosestUnit is RangedUnit)
        {
            RangedUnit R = (RangedUnit) ClosestUnit;
            xDis = Mathf.Abs((PosX - R.PosX) * (PosX - R.PosX));
            yDis = Mathf.Abs((PosY - R.PosY) * (PosY - R.PosY));

            uDistance = (int) Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }

        if (units[0] != null)
        {
            if (uDistance < bDistance)
            {
                distance = uDistance;
                enemyType = 0;
            }
            else
            {
                distance = bDistance;
                enemyType = 1;
            }
        }
        else
        {
            distance = bDistance;
            enemyType = 1;
        }

        //Checks to see if they are below 50% health so they move rather than attacking
        if (Health > MaxHealth * 0.5)
        {
            if (distance <= atkRange)
            {
                isAtk = true;
                Combat(enemyType);
            }
            else
            {
                isAtk = false;
                Move(enemyType);
            }
        }
        else
        {
            Move(enemyType);
        }
    }

    public override bool Death() // Checks if the Units health is at or below 0
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

    public override Units Position() //checks the Position of the Buildings and which positions to spawn units
    {

        int Xdis = 0, Ydis = 0;
        double Distance = 1000;
        double temp = 1000;
        Units Target = null;

        foreach (Units b in units)
        {
            if (b is RangedUnit)
            {
                RangedUnit Fb = (RangedUnit) b;

                if (FactionType != b.factionType)
                {
                    Xdis = Mathf.Abs(PosX - Fb.PosX) * (PosX - Fb.PosX);
                    Ydis = Mathf.Abs(PosY - Fb.PosY) * (PosY - Fb.PosY);

                    Distance = Mathf.Round(Mathf.Sqrt(Xdis + Ydis));
                }

            }
            else if (b is MeleeUnit)
            {
                MeleeUnit Rb = (MeleeUnit) b;
                if (FactionType != b.factionType)
                {
                    Xdis = Mathf.Abs(PosX - Rb.PosX) * (PosX - Rb.PosX);
                    Ydis = Mathf.Abs(PosY - Rb.PosY) * (PosY - Rb.PosY);

                    Distance = Mathf.Round(Mathf.Sqrt(Xdis + Ydis));
                }
            }

            if (Distance < temp)
            {
                temp = Distance;
                Target = b;
            }

        }

        return Target;
    }

    public override string ToString() //Shares the unit and Building information with the player
    {
        return name + " X: " + posX
               + " Y: " + posY
               + "\nMaxHealth: " + MaxHealth
               + "\nHealth: " + Health
               + "\nSpeed: " + Speed
               + "\nAttackDamage: " + Attack
               + "AttackRange: " + AtkRange
               + "\nFaction: " + FactionType;

    }
}

