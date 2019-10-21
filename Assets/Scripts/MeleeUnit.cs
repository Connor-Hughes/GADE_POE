using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeUnit : Units
{
    public int PosX
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
    Building ClosestBuilding;

    public MelleUnit(string N, int x, int y, int hp, int spd, int atk, int attRange, Faction fac, string sym,
        bool iatk)
        : base(N, x, y, hp, spd, atk, attRange, fac, sym, iatk)
    {

    }


    public override void Move(int type) // all the move functions for the units once they have spawned and the the Buildings themselves
    {
        if (Health > MaxHealth * 0.25) // if the units health is below 25% then he will try to run away
        {
            if (type == 0)
            {
                if (ClosestUnit is MelleUnit)
                {
                    MelleUnit closestUnitM = (MelleUnit)ClosestUnit;

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
                    RangedUnit closestUnitR = (RangedUnit)ClosestUnit;

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
                else if (ClosestUnit is WizardUnit) // new wizard unit and telling it when and who he can attack
                {
                    WizardUnit closestUnitW = (WizardUnit)ClosestUnit;

                    if (closestUnitW.PosX > posX && PosX < 20)
                    {
                        posX++;
                    }
                    else if (closestUnitW.PosX < posX && posX > 0)
                    {
                        posX--;
                    }

                    if (closestUnitW.PosY > posY && PosY < 20)
                    {
                        posY++;
                    }
                    else if (closestUnitW.PosY < posY && posY > 0)
                    {
                        posY--;
                    }
                }

            }
            else
            {
                if (ClosestBuilding is FactoryBuilding)
                {
                    FactoryBuilding closestBuildingFB = (FactoryBuilding)ClosestBuilding;

                    if (closestBuildingFB.PosX > posX && PosX < 20)
                    {
                        posX++;
                    }
                    else if (closestBuildingFB.PosX < posX && posX > 0)
                    {
                        posX--;
                    }

                    if (closestBuildingFB.PosY > posY && PosY < 20)
                    {
                        posY++;
                    }
                    else if (closestBuildingFB.PosY < posY && posY > 0)
                    {
                        posY--;
                    }
                }
                else if (ClosestBuilding is ResourceBuilding)
                {
                    ResourceBuilding closestBuildingRB = (ResourceBuilding)ClosestBuilding;

                    if (closestBuildingRB.PosX > posX && PosX < 19)//
                    {
                        posX++;
                    }
                    else if (closestBuildingRB.PosX < posX && posX > 0)
                    {
                        posX--;
                    }

                    if (closestBuildingRB.PosY > posY && PosY < 19) //
                    {
                        posY++;
                    }
                    else if (closestBuildingRB.PosY < posY && posY > 0)
                    {
                        posY--;
                    }
                }
            }

        }
        else
        {
            int direction = Random.Range(0, 4);

            if (direction == 0 && PosX < 19)
            {
                posX++;
            }
            else if (direction == 1 && posX > 0)
            {
                posX--;
            }
            else if (direction == 2 && posY > 19)
            {
                posY++;
            }
            else if (direction == 3 && posY > 0)
            {
                posY--;
            }
        }

    }

    public override void Combat(int type) // the method to get the units to fight their enemies and shows them who to fight
    {
        if (type == 0)
        {
            if (ClosestUnit is MelleUnit)
            {
                MelleUnit M = (MelleUnit)ClosestUnit;
                M.Health -= Attack;       // checking the health = health + attack 
            }
            else if (ClosestUnit is RangedUnit)
            {
                RangedUnit R = (RangedUnit)ClosestUnit;
                R.Health -= Attack; // checking the health = health + attack 
            }
            else if (ClosestUnit is WizardUnit)
            {
                WizardUnit Wu = (WizardUnit)ClosestUnit;
                Wu.Health -= Attack; // checking the health = health + attack 
            }
        }
        else if (type == 1)
        {
            if (ClosestBuilding is FactoryBuilding)
            {
                FactoryBuilding Fb = (FactoryBuilding)ClosestBuilding;
                Fb.Health -= Attack; // checking the health = health + attack 
            }
            else if (ClosestBuilding is ResourceBuilding)
            {
                ResourceBuilding RB = (ResourceBuilding)ClosestBuilding;
                RB.Health -= Attack; // checking the health = health + attack 

            }
        }

    }

    public override void AttRange(List<Units> uni, List<Building> builds) // checking the range of the building and the units they spawn
    {
        units = uni;
        buildings = builds;

        ClosestUnit = Position();
        ClosestBuilding = BuildingPosition();

        int enemyType;

        int xDis = 0, yDis = 0;

        int uDistance = 10000, bDistance = 10000;
        int distance;

        if (ClosestUnit is MelleUnit)
        {
            MelleUnit M = (MelleUnit)ClosestUnit;
            xDis = Mathf.Abs((PosX - M.PosX) * (PosX - M.PosX));
            yDis = Mathf.Abs((PosY - M.PosY) * (PosY - M.PosY));

            uDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }
        else if (ClosestUnit is RangedUnit)
        {
            RangedUnit R = (RangedUnit)ClosestUnit;
            xDis = Mathf.Abs((PosX - R.PosX) * (PosX - R.PosX));
            yDis = Mathf.Abs((PosY - R.PosY) * (PosY - R.PosY));

            uDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }

        if (ClosestBuilding is FactoryBuilding)
        {
            FactoryBuilding FB = (FactoryBuilding)ClosestBuilding;
            xDis = Mathf.Abs((PosX - FB.PosX) * (PosX - FB.PosX));
            yDis = Mathf.Abs((PosY - FB.PosY) * (PosY - FB.PosY));

            bDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
        }
        else if (ClosestBuilding is ResourceBuilding)
        {
            ResourceBuilding RB = (ResourceBuilding)ClosestBuilding;
            xDis = Mathf.Abs((PosX - RB.PosX) * (PosX - RB.PosX));
            yDis = Mathf.Abs((PosY - RB.PosY) * (PosY - RB.PosY));

            bDistance = (int)Mathf.Round(Mathf.Sqrt(xDis + yDis));
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

        //Checks to see if they are below 25% health so they move rather than attacking
        if (Health > MaxHealth * 0.25)
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
                RangedUnit Fb = (RangedUnit)b;

                if (FactionType != b.factionType)
                {
                    Xdis = Mathf.Abs(PosX - Fb.PosX) * (PosX - Fb.PosX);
                    Ydis = Mathf.Abs(PosY - Fb.PosY) * (PosY - Fb.PosY);

                    Distance = Mathf.Round(Mathf.Sqrt(Xdis + Ydis));
                }

            }
            else if (b is MelleUnit)
            {
                MelleUnit Rb = (MelleUnit)b;
                if (FactionType != b.factionType)
                {
                    Xdis = Mathf.Abs(PosX - Rb.PosX) * (PosX - Rb.PosX);
                    Ydis = Mathf.Abs(PosY - Rb.PosY) * (PosY - Rb.PosY);

                    Distance = Mathf.Round(Mathf.Sqrt(Xdis + Ydis));
                }
            }
            else if (b is WizardUnit)
            {
                WizardUnit Wu = (WizardUnit)b;
                if (FactionType != b.factionType)
                {
                    Xdis = Mathf.Abs(PosX - Wu.PosX) * (PosX - Wu.PosX);
                    Ydis = Mathf.Abs(PosY - Wu.PosY) * (PosY - Wu.PosY);

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

    public Building BuildingPosition() //Method to check the positions of the buildings
    {
        int Xdis = 0, Ydis = 0;
        double Distance = 1000;
        double temp = 1000;
        Building Target = null;

        foreach (Building b in buildings)
        {


            if (b is FactoryBuilding)
            {
                FactoryBuilding Fb = (FactoryBuilding)b;

                if (FactionType != b.faction)
                {
                    Xdis = Mathf.Abs(PosX - Fb.PosX) * (PosX - Fb.PosX);
                    Ydis = Mathf.Abs(PosY - Fb.PosY) * (PosY - Fb.PosY);

                    Distance = Mathf.Round(Mathf.Sqrt(Xdis + Ydis));
                }

            }
            else
            {
                ResourceBuilding Rb = (ResourceBuilding)b;
                if (FactionType != b.faction)
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

