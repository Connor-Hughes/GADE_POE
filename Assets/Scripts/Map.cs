using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Tiles
{
    emptyTile,
    meleeUnitHero,
    rangedUnitHero,
    meleeUnitVillain,
    rangedUnitVillain,
    resourceBuildingHero,
    resourceBuildingVillain,
    factoryBuildingHero,
    factoryBuildingVillain,
    wizardUnit
}

public enum Faction //factions for both the Melee and the Ranged unit and new wizard unit
{
    Hero,
    Villain,
    Neutral
}

public enum ResourceType //enumeration for the diffarent type of resources
{
    Diamonds,
    Coal
}
public class Map
{
    private int mapWidth = 20;
    private int mapHeight = 20;
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

    public Tiles[,] tileMap; 

    
    int BuildingNum;

    public Map(int UnitN, int MapH, int MapW)
    {
        mapHeight = MapH;
        mapWidth = MapW;

        buildingMap = new Building[mapWidth, mapHeight];
        uniMap = new Units[mapWidth, mapHeight];

        BuildingNum = UnitN;

        tileMap = new Tiles[mapWidth,mapHeight];
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

            ResourceBuilding DiamondMine = new ResourceBuilding(0, 0, 100, Faction.Hero, "◘", 10); // setting the values and symbols for the diamond mines
            diamondMines.Add(DiamondMine);

            FactoryBuilding barrack = new FactoryBuilding(0, 0, 100, Faction.Hero, "┬", Random.Range(3, 10), UnitName); // setting the values and symbols for the barracks
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

            ResourceBuilding DiamondMine = new ResourceBuilding(0, 0, 100, Faction.Villain, "◘", 10);
            diamondMines.Add(DiamondMine);

            FactoryBuilding barrack =
                new FactoryBuilding(0, 0, 100, Faction.Villain, "┬", Random.Range(3, 10), UnitName);
            barracks.Add(barrack);
        }

        for (int i = 0; i < BuildingNum; i++)
        {
            WizardUnit wizard = new WizardUnit("Wizard", 0, 0, 15, 1, 3, 1, Faction.Neutral, "≈", false); //setting values for the new wizard unit
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
        foreach (RangedUnit u in rangedUnit)
        {
            if (u.factionType == Faction.Hero)
            {
                tileMap[u.posY, u.posX] = Tiles.rangedUnitHero;
            } 
            else
            {
                tileMap[u.posY, u.posX] = Tiles.rangedUnitVillain;
            }
        }

        foreach (MeleeUnit u in melleUnit)
        {
            if (u.factionType == Faction.Hero)
            {
                tileMap[u.posY, u.posX] = Tiles.meleeUnitHero;
            }
            else
            {
                tileMap[u.posY, u.posX] = Tiles.meleeUnitVillain;
            }
        }
        foreach (WizardUnit u in wizardUnits)
        {
            tileMap[u.posY, u.posX] = Tiles.wizardUnit;
        }
    }

    public void PlaceBuildings() // method to place buildings randomly throughout the map and spawn the random units 
    {
        foreach (FactoryBuilding u in barracks)
        {
            if (u.Faction == Faction.Hero)
            {
                tileMap[u.PosY, u.PosX] = Tiles.factoryBuildingHero;
            }
            else
            {
                tileMap[u.PosY, u.PosX] = Tiles.factoryBuildingVillain;
            }
        }
        foreach (ResourceBuilding u in diamondMines)
        {
            if (u.Faction == Faction.Hero)
            {
                tileMap[u.PosY, u.PosX] = Tiles.resourceBuildingHero;
            }
            else
            {
                tileMap[u.PosY, u.PosX] = Tiles.resourceBuildingVillain;
            }
        }

    }

    public void SpawnUnits(int x, int y, Faction fac, string unitType) // Spawning the Units from the Buildings including the melee and ranged Units
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
