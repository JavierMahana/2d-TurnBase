using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVisualAid : MonoBehaviour
{
    public List<Tile> displayedCanMoveTiles = new List<Tile>();
    public List<Tile> displayedCanAtackToTiles = new List<Tile>();

    Map map;

    void Start()
    {
        Selector selector = GameObject.FindObjectOfType<Selector>();
        map = GameObject.FindObjectOfType<Map>();


        selector.selectionEvent += Show;

        selector.deselectEvent += Hide;
        
        //DesactivateAllTileEfects();
    }


    public void ShowMovementPath(Unit unit)
    {

    }


    public void Show(Tile other)
    {
        if (other.isOcupied == false)
        {
            return;
        }
        Unit unit = other.unitAbove;
        ShowMovementOptions(unit);
        ShowAtackOptions(unit);
    }
    void ShowMovementOptions(Unit unit)
    {

        displayedCanMoveTiles = PathFinding.GetCanMoveTiles(map, unit);
        foreach (Tile tile in displayedCanMoveTiles)
        {

            Animator a = tile.transform.GetComponentInChildren<Animator>();
            a.SetBool("CanMoveTo", true);
        }

    }
    void ShowAtackOptions(Unit unit)
    {
        displayedCanAtackToTiles = PathFinding.GetCanAtackTiles(map, displayedCanMoveTiles, unit);

        foreach (Tile tile in displayedCanAtackToTiles)
        {

            Animator a = tile.transform.GetComponentInChildren<Animator>();
            a.SetBool("CanAtackTo", true);
        }
    }



    public void Hide()
    {
        HideMovementOptions();
        HideAtackOptions();
       
    }
    void HideMovementOptions()
    {

        foreach (Tile tile in displayedCanMoveTiles)
        {
            //set the tiles parameter to false, to activate the animation
            Animator a = tile.transform.GetComponentInChildren<Animator>();
            a.SetBool("CanMoveTo", false);
        }
        displayedCanMoveTiles.Clear();
    }
    void HideAtackOptions()
    {

        foreach (Tile tile in displayedCanAtackToTiles)
        {
            //set the tiles parameter to false, to activate the animation
            Animator a = tile.transform.GetComponentInChildren<Animator>();
            a.SetBool("CanAtackTo", false);
        }
        displayedCanMoveTiles.Clear();
    }




}
