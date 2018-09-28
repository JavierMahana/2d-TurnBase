using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    public TileTerrain terrain;
    public bool isOcupied;
    public bool isPassable = true;
    public Unit unitAbove;


    //devuelve true si es que es pasable
    //false si no
    public bool Evaluate(Unit movingUnit)
    {
        if (isPassable == false)
        {
            return false;
        }

        switch (movingUnit.state)
        {
            case Unit.UnitState.Friendly:
                if (isOcupied)
                {
                    switch (unitAbove.state)
                    {
                        case Unit.UnitState.Friendly:
                            return true;
                        case Unit.UnitState.Enemy:
                            return false;
                        case Unit.UnitState.Neutral:
                            return true;
                    }
                }
                break;
            case Unit.UnitState.Enemy:
                if (isOcupied)
                {
                    switch (unitAbove.state)
                    {
                        case Unit.UnitState.Friendly:
                            return false;
                        case Unit.UnitState.Enemy:
                            return true;
                        case Unit.UnitState.Neutral:
                            return false;
                    }
                }
                break;
            case Unit.UnitState.Neutral:
                if (isOcupied)
                {
                    switch (unitAbove.state)
                    {
                        case Unit.UnitState.Friendly:
                            return true;
                        case Unit.UnitState.Enemy:
                            return false;
                        case Unit.UnitState.Neutral:
                            return true;
                    }
                }
                break;
        }

        //no hay unidad encima
        return true;
    }
    public void TileUpdate()
    {
        SpriteRenderer r = transform.GetComponent<SpriteRenderer>();
        r.sprite = terrain.sprite;
    }
}
