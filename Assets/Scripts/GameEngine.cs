using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

[System.Serializable]

public class GameEngine : MonoBehaviour
{
    int temp = 0;
    bool runGame = false;

    // Start is called before the first frame update
    void Start()
    {
        m = new Map(UnitNum, mapHeight, mapWidth);
        m.GenerateBattleField();
        InitialiseMap();
        m.Populate();
        m.PlaceBuildings();
        placeObjects();
    }

    // Update is called once per frame
    void Update()
    {
        if (runGame)
        {
            if (temp == 20)
            {
                GameLogic();
                InitialiseMap();
                m.Populate();
                m.PlaceBuildings();
                placeObjects();

                temp = 0;
            }
            else
            {
                temp++;
            }
        }

       
    }

    //GameObjects
    public GameObject emptyTile;
    public GameObject meleeUnitHero;
    public GameObject meleeUnitVillain;
    public GameObject rangedUnitHero;
    public GameObject rangedUnitVillain;
    public GameObject wizardUnit;
    public GameObject resourceBuildingHero;
    public GameObject resourceBuildingVillain;
    public GameObject factoryBuildingHero;
    public GameObject factoryBuildingVillain;


    private int mapWidth = 20;
    private int mapHeight = 20;

    static int UnitNum = 8;
    public int Round = 1;

    int hero = 0;
    int villian = 0;

     int HeroResourcesLeft = 0;
     int VillainResourcesLeft = 0;
     int HeroResourcesCollected = 0;
    int VillainResourcesCollected = 0;


    private Map m;

    public Text txtWinText;
    public Text txtPausePlay;
    public Text txtRound;

    public Text txtHeroResourcesLeft;
    public Text txtHeroResourcesCollected;
    public Text txtHeroUnits;

    public Text txtVillainResourcesLeft;
    public Text txtVillainResourcesCollected;
    public Text txtVillainUnits;



    public void GameLogic() //game engine method instead of a separate class
    {
        txtRound.text = "Round: " + Round;
        hero = 0;
        villian = 0;

        foreach (ResourceBuilding u in m.diamondMines) // incrementing the hero or villain based on which faction type they belong to
        {
            if (u.Faction == Faction.Hero)
            {
                hero++;
            }
            else
            {
                villian++;
            }
        }

        foreach (FactoryBuilding u in m.barracks) // incrementing the hero or villian based on which faction type they belong to
        {
            if (u.Faction == Faction.Hero)
            {
                hero++;
            }
            else
            {
                villian++;
            }
        }

        foreach (Units u in m.units) // incrementing the hero or villian based on which faction type they belong to
        {
            if (u.factionType == Faction.Hero)
            {
                hero++;
            }
            else
            {
                villian++;
            }
        }


        if (hero > 0 && villian > 0) // telling the game when there is only 1 type of unit left then that team is the victor
        {
            int ResourcesRemainingVillain;
            int ResourcesRemainingHero;

            foreach (ResourceBuilding Rb in m.diamondMines)
            {
                Rb.GenerateResources();
                if (Rb.faction == Faction.Hero)
                {
                    
                }
                else if (Rb.faction == Faction.Villain)
                {

                }
            }


            foreach (FactoryBuilding Fb in m.barracks)
            {
                if (Round % Fb.ProductionSPeed == 0)
                {
                    m.SpawnUnits(Fb.SpawnPointX, Fb.SpawnPointY, Fb.Faction, Fb.UnitType);
                }
            }

            foreach (Units u in m.units)
            {
                u.AttRange(m.units, m.buildings);
            }

            m.Populate();
            m.PlaceBuildings();
            Round++;


        }
        else
        {
            m.Populate();
            m.PlaceBuildings();
            if (hero > villian)
            {
                txtWinText.text = "Hero Wins on round:" + Round;
            }
            else
            {
                txtWinText.text = "Villain Wins on round:" + Round;
            }
        }

        for (int i = 0; i < m.rangedUnit.Count; i++) // for loop to remove Ranged units once health has reached 0
        {
            if (m.rangedUnit[i].Death())
            {
                m.rangedUnit.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.melleUnit.Count; i++) // for loop to remove Melee units once health has reached 0
        {
            if (m.melleUnit[i].Death())
            {
                m.melleUnit.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.units.Count; i++) // for loop to remove units once health has reached 0
        {
            if (m.units[i].Death())
            {
                m.units.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.diamondMines.Count; i++) // for loop to remove The diamond mine buildings once health has reached 0
        {
            if (m.diamondMines[i].Destruction())
            {
                m.diamondMines.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.barracks.Count; i++) // for loop to remove The Barrack buildings once health has reached 0
        {
            if (m.barracks[i].Destruction())
            {
                m.barracks.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.buildings.Count; i++) // for loop to remove The buildings once health has reached 0
        {
            if (m.buildings[i].Destruction())
            {
                if (m.buildings[i] is FactoryBuilding)
                {
                    FactoryBuilding FB = (FactoryBuilding)m.buildings[i];
                }
                else if (m.buildings[i] is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)m.buildings[i];
                }

                m.buildings.RemoveAt(i);
            }
        }

    }

    public void placeObjects()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");

        foreach (GameObject g in tiles)
        {
            Destroy(g);
        }

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                if (m.tileMap[x, z] == Tiles.emptyTile)
                {
                    Instantiate(emptyTile, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.meleeUnitHero)
                {
                    Instantiate(meleeUnitHero, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.meleeUnitVillain)
                {
                    Instantiate(meleeUnitVillain, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.rangedUnitHero)
                {
                    Instantiate(rangedUnitHero, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.rangedUnitVillain)
                {
                    Instantiate(rangedUnitVillain, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.wizardUnit)
                {
                    Instantiate(wizardUnit, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.factoryBuildingHero)
                {
                    Instantiate(factoryBuildingHero, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.factoryBuildingVillain)
                {
                    Instantiate(factoryBuildingVillain, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.resourceBuildingHero)
                {
                    Instantiate(resourceBuildingHero, new Vector3(x, 0f, z), Quaternion.identity);
                }
                else if (m.tileMap[x, z] == Tiles.resourceBuildingVillain)
                {
                    Instantiate(resourceBuildingVillain, new Vector3(x, 0f, z), Quaternion.identity);
                }
            }
        }
    }

    public void InitialiseMap()
    {
        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                m.tileMap[i, j] = Tiles.emptyTile;
            }
        }
    }

    public void Display()
    {
        txtRound.text = "Round: " + Round;

        txtHeroUnits.text = "Units Remaining: " + hero;
        txtVillainUnits.text = "Units Remaining: " + villian;

        txtHeroResourcesCollected.text = "Resources Collected: \n\t" + HeroResourcesCollected;
        txtVillainResourcesCollected.text = "Resources Collected: \n\t" + VillainResourcesCollected;

        txtHeroResourcesLeft.text = "Resources Left: \n\t" + HeroResourcesLeft;
        txtVillainResourcesLeft.text = "Resources Left: \n\t" + VillainResourcesLeft;


    }

    public void PlayPause()
    {
        if (runGame == false)
        {
            runGame = true;
            txtPausePlay.text = "Pause";
        }
        else
        {
            runGame = false;
            txtPausePlay.text = "Play";
        }
    }

    public void Read() // Read button to implement the saved files from the Save button onto the map
    {
        try
        {
            FileStream fs = new FileStream("SaveFile.dat", FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryFormatter bf = new BinaryFormatter();

            using (fs)
            {
                m = (Map)bf.Deserialize(fs);
                placeObjects();
                //MessageBox.Show("File Loaded ");
            }

        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }

    }

    public  void Save()// Save button to save the map, unit and building Information
    {

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("SaveFile.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            using (fs)
            {
                bf.Serialize(fs, m);
                //MessageBox.Show("File Saved");
            }

        }
        catch (Exception ex)
        {
            //MessageBox.Show(ex.Message);
        }

    }

}




