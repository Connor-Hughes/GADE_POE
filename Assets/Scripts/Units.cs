﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[System.Serializable]
public abstract class Units
{
    //[Serializable]

    public string name;

    public int posX;

    public int posY;

    public int health;

    public int maxHealth;

    public int speed;

    public int attack;

    public int atkRange;

    public Faction factionType;

    public string symbol;

    public bool isAtk;


    public abstract void Move(int type);

    public abstract void Combat(int type);

    public abstract void AttRange(List<Units> uni, List<Building> builds);

    public abstract bool Death();

    public abstract Units Position();


    public Units(string N, int x, int y, int hp, int spd, int atk, int attRange, Faction fac, string sym,
        bool iatk) // constructor
    {
        name = N;
        posX = x;
        posY = y;
        health = hp;
        speed = spd;
        attack = atk;
        atkRange = attRange;
        factionType = fac;
        symbol = sym;
        isAtk = iatk;

        maxHealth = hp;
    }



}