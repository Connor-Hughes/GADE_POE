using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map
{
    private int mapWidth = 20;
    private int mapHeight = 20;
    public string[,] map;
    Random Rd = new Random();


    public List<Building> buildings = new List<Building>();
    public List<ResourceBuilding> diamondMines = new List<ResourceBuilding>();
    public List<FactoryBuilding> barracks = new List<FactoryBuilding>();
    public Building[,] buildingMap;

    public List<Units> units = new List<Units>();
    public List<Units> rangedUnit = new List<Units>();
    public List<Units> melleUnit = new List<Units>();
    public List<WizardUnit> wizardUnits = new List<WizardUnit>();
    public Units[,] uniMap;


    int BuildingNum;

    public Map(int UnitN, int MapH, int MapW)
    {
        mapHeight = MapH;
        mapWidth = MapW;

        buildingMap = new Building[mapWidth, mapHeight];
        uniMap = new Units[mapWidth, mapHeight];
        map = new string[mapWidth, mapHeight];

        BuildingNum = UnitN;
    }

    public void GenerateBattleField() // method to allow the random number of units, including the ranged and the melee units
    {
        for (int i = 0; i < BuildingNum; i++)
        {
            int UnitNum = Random.Range(0, 2);
            string UnitName;
            if (UnitNum == 0)
            {
                UnitName = "Melee";
            }
            else
            {
                UnitName = "Ranged";
            }

            ResourceBuilding DiamondMine = new ResourceBuilding(0, 0, 100, Units.Faction.Hero, "◘", 10); // setting the values and symbols for the diamond mines
            diamondMines.Add(DiamondMine);

            FactoryBuilding barrack = new FactoryBuilding(0, 0, 100, Units.Faction.Hero, "┬", Random.Range(3, 10), UnitName); // setting the values and symbols for the barracks
            barracks.Add(barrack);

        }

        for (int i = 0; i < BuildingNum; i++)
        {
            int UnitNum = Random.Range(0, 2);
            string UnitName;
            if (UnitNum == 0)
            {
                UnitName = "Melee";
            }
            else
            {
                UnitName = "Ranged";
            }

            ResourceBuilding DiamondMine = new ResourceBuilding(0, 0, 100, Units.Faction.Villain, "◘", 10);
            diamondMines.Add(DiamondMine);

            FactoryBuilding barrack =
                new FactoryBuilding(0, 0, 100, Units.Faction.Villain, "┬", Random.Range(3, 10), UnitName);
            barracks.Add(barrack);
        }

        for (int i = 0; i < BuildingNum; i++)
        {
            WizardUnit wizard = new WizardUnit("Wizard", 0, 0, 15, 1, 3, 1, Units.Faction.Neutral, "≈", false); //setting values for the new wizard unit
            wizardUnits.Add(wizard);
        }

        foreach (ResourceBuilding u in diamondMines)
        {
            for (int i = 0; i < diamondMines.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while (xPos == diamondMines[i].PosX && yPos == diamondMines[i].PosY && xPos == barracks[i].PosX && yPos == barracks[i].PosY)
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
                buildingMap[u.PosX, u.PosX] = (Building)u;
            }
            buildings.Add(u);
        }

        foreach (FactoryBuilding u in barracks)
        {
            for (int i = 0; i < barracks.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while (xPos == barracks[i].PosX && yPos == barracks[i].PosY && xPos == diamondMines[i].PosX && yPos == diamondMines[i].PosY)
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
                buildingMap[u.PosY, u.PosX] = (Building)u;
            }
            buildings.Add(u);

            u.SpawnPointY = u.PosY;
            if (u.PosX < 19)
            {
                u.SpawnPointX = u.PosX + 1;
            }
            else
            {
                u.SpawnPointX = u.PosX - 1;
            }
        }

        foreach (WizardUnit u in wizardUnits) // placing the wizard units randomly throughout the map
        {
            for (int i = 0; i < wizardUnits.Count; i++)
            {
                int xPos = Random.Range(0, mapHeight);
                int yPos = Random.Range(0, mapWidth);

                while (xPos == diamondMines[i].PosX && yPos == diamondMines[i].PosY && xPos == barracks[i].PosX && yPos == barracks[i].PosY && xPos == wizardUnits[i].PosX && yPos == wizardUnits[i].PosY)
                {
                    xPos = Random.Range(0, mapHeight);
                    yPos = Random.Range(0, mapWidth);
                }

                u.PosX = xPos;
                u.PosY = yPos;
                uniMap[u.PosX, u.PosX] = (Units)u;
            }
            units.Add(u);
        }

        Populate();
        PlaceBuildings();
    }

    public void Populate() // method used to populate the map full of units
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                map[i, j] = " ";
            }
        }

        foreach (Units u in units) ////
        {
            if (u is RangedUnit)
            {
                RangedUnit r = (RangedUnit)u;
                uniMap[r.PosY, r.PosX] = u;
            }
            else if (u is MeleeUnit)
            {
                MeleeUnit m = (MeleeUnit)u;
                uniMap[m.PosY, m.PosX] = u;
            }
            else if (u is WizardUnit)
            {
                WizardUnit w = (WizardUnit)u;
                uniMap[w.PosY, w.PosX] = u;
            }
        }

        foreach (Units u in units)
        {
            uniMap[u.posY, u.posX] = u; //////
        }

        foreach (Units u in rangedUnit)
        {
            map[u.posY, u.posX] = "R";
        }
        foreach (Units u in melleUnit)
        {
            map[u.posY, u.posX] = "M";
        }
        foreach (WizardUnit u in wizardUnits)
        {
            map[u.posY, u.posX] = "W";
        }

    }

    public void PlaceBuildings() // method to place buildings randomly throughout the map and spawn the random units 
    {

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                buildingMap[i, j] = null;
            }
        }

        foreach (Building u in buildings)
        {
            if (u is FactoryBuilding)
            {
                FactoryBuilding Factory = (FactoryBuilding)u;
                buildingMap[Factory.PosY, Factory.PosX] = u;
            }
            else if (u is ResourceBuilding)
            {
                ResourceBuilding factory = (ResourceBuilding)u;
                buildingMap[factory.PosY, factory.PosX] = u;
            }
        }

        foreach (ResourceBuilding u in diamondMines)
        {
            map[u.PosY, u.PosX] = "RB";
        }
        foreach (FactoryBuilding u in barracks)
        {
            map[u.PosY, u.PosX] = "FB";
        }
    }

    public void SpawnUnits(int x, int y, Units.Faction fac, string unitType) // Spawning the Units from the Buildings including the melee and ranged Units
    {
        if (unitType == "Ranged")
        {
            RangedUnit Musketeer = new RangedUnit("Musketeer", x, y, 30, 1, 5, 3, fac, "->", false);
            rangedUnit.Add(Musketeer);
            units.Add(Musketeer);
        }
        else if (unitType == "Melee")
        {
            MeleeUnit Pekka = new MeleeUnit("Pekka", x, y, 50, 1, 10, 1, fac, "#", false);
            melleUnit.Add(Pekka);
            units.Add(Pekka);
        }
    }
}
