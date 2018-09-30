using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public enum UnitState
    {
        Friendly = 0,
        Enemy = 1,
        Neutral = 2
    }


    public int movement;
    public int movementLeft;
    public UnitState state;
    public int[] weaponRange;

    public Tile standingTile;
    Map map;
    

	
	void Start ()
    {
        map = GameObject.FindObjectOfType<Map>();
        //subscribe to selections events
        //FindObjectOfType<Selector>().selectEvent += OnSelect;
        SetOcupiedTile();
	}

    void SetOcupiedTile()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;

        standingTile = map.GetTile(x, y);
        standingTile.isOcupied = true;
        standingTile.unitAbove = this;
    }

}
