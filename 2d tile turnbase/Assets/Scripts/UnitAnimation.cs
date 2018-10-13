using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimation : MonoBehaviour {

    Selector selector;
    Hoover hoover;
    

    Unit previouslySelected;

	void Start ()
    {
        selector = GameObject.FindObjectOfType<Selector>();
        hoover = GameObject.FindObjectOfType<Hoover>();
        

        hoover.hooverEvent += OnHoover;
        hoover.outOfHooverEvent += OutOfHoover;

        selector.selectionEvent += OnSelect;
        selector.deselectEvent += Deselect;
	}
    void Deselect()
    {
        if (previouslySelected == null)
        {
            return;
        }
        previouslySelected.animator.SetBool("IsSelected", false);
    }
    void OnSelect(Tile other)
    {
        if (other.isOcupied == false)
        {
            return;
        }
        Unit u = other.unitAbove;
        u.animator.SetBool("IsSelected",true);
        previouslySelected = u;
    }

    //starts the onHover method of the unit
    void OnHoover(Tile hooveredTile)
    {
        if (hooveredTile.isOcupied)
        {
            if (hooveredTile.unitAbove.state == Unit.UnitState.Friendly)
            {
                if (hooveredTile.unitAbove.movementLeft>0)
                {
                    hooveredTile.unitAbove.animator.SetBool("IsHoovered", true); ;
                }
                
            }
            
        }
    }

    public void OutOfHoover(Tile tile)
    {
        if (tile.isOcupied)
        {
            tile.unitAbove.animator.SetBool("IsHoovered", false);
        }
    }

}
