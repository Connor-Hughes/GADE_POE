using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    abstract class Building // all properties for the buildings
    {
        protected int posX;

        protected int posY;

        protected int health;

        public Faction faction;

        protected string symbol;


        public abstract bool Destruction();

        public abstract string ToString();

        public Building(int x, int y, int hp, Faction fac, string sym) //constructor for the building class
        {
            posX = x;
            posY = y;
            health = hp;
            faction = fac;
            symbol = sym;

        }

    }
}
