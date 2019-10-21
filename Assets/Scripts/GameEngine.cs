using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Color = UnityEngine.Color;

public class GameEngine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private int mapWidth = 20;
    private int mapHeight = 20;

    public Button[,] buttons; //button array that will be used for the grid

    static int UnitNum = 8;
    public int Round = 1;

    private Map m;


    private void Form1_Load(object sender, EventArgs e) // when the map loads, these buttons and method calls will take place to populate the map 
    {
        buttons = new Button[mapWidth, mapHeight];
        m = new Map(UnitNum, mapHeight, mapWidth);
        m.GenerateBattleField();
        PlaceButtons();
    }

    public void PlaceButtons() // Method to fill the Map with buttons to create the battlefield 
    {
        GbBoxMap.Controls.Clear();

        Size btnSize = new Size(30, 30);

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                Button btn = new Button();

                btn.Size = btnSize;
                btn.Location = new Point(i * 30, j * 30);

                //buttons[i, j] = btn;
                if (m.map[i, j] == "R")
                {
                    btn.Text = "->";
                    btn.Name = m.uniMap[i, j].ToString();
                    btn.Click += MyButtonCLick;

                    btn.BackColor = m.uniMap[i, j].factionType == Units.Faction.Hero ? Color.Chartreuse : Color.Crimson; // the color for the ranged and melee units
                }
                else if (m.map[i, j] == "M")
                {
                    btn.Text = "#";
                    btn.Name = m.uniMap[i, j].ToString();
                    btn.Click += MyButtonCLick;

                    btn.BackColor = m.uniMap[i, j].factionType == Units.Faction.Hero ? Color.Chartreuse : Color.Crimson; // the color for the ranged and melee units
                }
                else if (m.map[i, j] == "W")
                {
                    btn.Text = "≈";
                    btn.Name = m.uniMap[i, j].ToString();
                    btn.Click += MyButtonCLick;

                    btn.BackColor = Color.BlueViolet;
                }
                else if (m.map[i, j] == "FB")
                {
                    FactoryBuilding FB = (FactoryBuilding)m.buildingMap[i, j];
                    btn.Text = FB.Symbol;
                    btn.BackColor = FB.Faction == Units.Faction.Hero ? Color.Chartreuse : Color.Crimson;

                    btn.Name = m.buildingMap[i, j].ToString();
                    btn.Click += MyButtonCLick;
                }
                else if (m.map[i, j] == "RB")
                {
                    ResourceBuilding RB = (ResourceBuilding)m.buildingMap[i, j];
                    btn.Text = RB.Symbol;
                    btn.BackColor = RB.Faction == Units.Faction.Hero ? Color.Chartreuse : Color.Crimson;

                    btn.Name = m.buildingMap[i, j].ToString();
                    btn.Click += MyButtonCLick;
                }

                buttons[i, j] = btn;
            }
        }

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                GbBoxMap.Controls.Add(buttons[i, j]);
            }
        }
    }

    public void MyButtonCLick(object sender, EventArgs e) // when the button is clicked it will place all the values assigned to each unit
    {
        Button btn = ((Button)sender);

        foreach (Units u in m.units)
        {
            if (btn.Name == u.ToString())
            {
                txtOutput.Text = u.ToString();
            }
        }

        foreach (Building b in m.buildings)
        {
            if (btn.Name == b.ToString())
            {
                txtOutput.Text = b.ToString();
            }
        }
    }

    private void btnStart_Click(object sender, EventArgs e) // enabling the ticker to start game 
    {
        Ticker.Enabled = true;
    }

    private void btnPause_Click(object sender, EventArgs e) // disabling ticker when pause button is pressed
    {
        Ticker.Enabled = false;
    }

    private void Ticker_Tick(object sender, EventArgs e)
    {
        GameEngine();
        lblRound.Text = "Round: " + Round;
    }

    public void GameEngine() //game engine method instead of a separate class
    {
        int hero = 0;
        int villian = 0;

        foreach (ResourceBuilding u in m.diamondMines) // incrementing the hero or villian based on which faction type they belong to
        {
            if (u.Faction == Units.Faction.Hero)
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
            if (u.Faction == Units.Faction.Hero)
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
            if (u.factionType == Units.Faction.Hero)
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
            foreach (ResourceBuilding Rb in m.diamondMines)
            {
                Rb.GenerateResources();
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
            PlaceButtons();

        }
        else
        {
            m.Populate();
            m.PlaceBuildings();
            PlaceButtons();
            Ticker.Enabled = false;

            if (hero > villian)
            {
                MessageBox.Show("Hero Wins on Round: " + Round);
            }
            else
            {
                MessageBox.Show("Villain Wins on Round: " + Round);
            }
        }

        for (int i = 0; i < m.rangedUnit.Count; i++) // for loop to remove Ranged units once health has reached 0
        {
            if (m.rangedUnit[i].Death())
            {
                m.map[m.rangedUnit[i].posX, m.rangedUnit[i].posX] = "";
                m.rangedUnit.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.melleUnit.Count; i++) // for loop to remove Melee units once health has reached 0
        {
            if (m.melleUnit[i].Death())
            {
                m.map[m.melleUnit[i].posX, m.melleUnit[i].posX] = "";
                m.melleUnit.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.units.Count; i++) // for loop to remove units once health has reached 0
        {
            if (m.units[i].Death())
            {
                m.map[m.units[i].posX, m.units[i].posX] = "";
                m.units.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.diamondMines.Count; i++) // for loop to remove The diamond mine buildings once health has reached 0
        {
            if (m.diamondMines[i].Destruction())
            {
                m.map[m.diamondMines[i].PosX, m.diamondMines[i].PosX] = "";
                m.diamondMines.RemoveAt(i);
            }
        }

        for (int i = 0; i < m.barracks.Count; i++) // for loop to remove The Barrack buildings once health has reached 0
        {
            if (m.barracks[i].Destruction())
            {
                m.map[m.barracks[i].PosX, m.barracks[i].PosX] = "";
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
                    m.map[FB.PosX, FB.PosY] = "";
                }
                else if (m.buildings[i] is ResourceBuilding)
                {
                    ResourceBuilding RB = (ResourceBuilding)m.buildings[i];
                    m.map[RB.PosX, RB.PosY] = "";
                }

                m.buildings.RemoveAt(i);
            }
        }

    }

    private void btnRead_Click(object sender, EventArgs e) // Read button to implement the saved files from the Save button onto the map
    {

        try
        {
            FileStream fs = new FileStream("SaveFile.dat", FileMode.Open, FileAccess.Read, FileShare.None);
            BinaryFormatter bf = new BinaryFormatter();

            using (fs)
            {
                m = (Map)bf.Deserialize(fs);
                PlaceButtons();
                MessageBox.Show("File Loaded ");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    private void btnSave_Click(object sender, EventArgs e) // Save button to save the map, unit and building Information
    {

        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = new FileStream("SaveFile.dat", FileMode.Create, FileAccess.Write, FileShare.None);
            using (fs)
            {
                bf.Serialize(fs, m);
                MessageBox.Show("File Saved");
            }

        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    private void btnSetSize_Click(object sender, EventArgs e) // method allowing the user to choose the map size instead of a 20X20 grid
    {
        try
        {
            mapHeight = Convert.ToInt32(txtBoxHeight.Text);
            mapWidth = Convert.ToInt32(txtBoxWidth.Text);

            if (mapHeight < 10 || mapWidth < 10)
            {
                MessageBox.Show("Please enter values that are greater than 9X9");
            }
            else
            {
                m = new Map(UnitNum, mapHeight, mapWidth);

                buttons = new Button[mapWidth, mapHeight];

                m.GenerateBattleField();
                PlaceButtons();
            }
        }
        catch
        {
            MessageBox.Show("Please enter valid Numbers Only");  //catch to let the user know if they have entered a non integer variable
        }
    }
}



}
