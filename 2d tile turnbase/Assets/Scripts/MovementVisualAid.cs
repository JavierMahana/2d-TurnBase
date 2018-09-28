using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVisualAid : MonoBehaviour
{
    public List<Tile> tilesWithDisplayedVisualHepl = new List<Tile>();
    Map map;

    void Start()
    {
        Selector selector = GameObject.FindObjectOfType<Selector>();
        map = GameObject.FindObjectOfType<Map>();
        selector.selectionEvent += ShowMovementOptions;
        selector.deselectEvent += HideMovementOptions;
    }

    public void ShowMovementOptions(Unit unit)
    {
     

        Debug.Log("LLegue a djistras");
       
        tilesWithDisplayedVisualHepl = PathFinding.Dijkstras(map, unit.standingTile, unit.movementLeft);
        foreach (Tile tile in tilesWithDisplayedVisualHepl)
        {
            //set the tiles parameter to true, to activate the animation
            tile.transform.GetComponentInChildren<Animator>().SetBool("CanMoveTo", true);
        }
        
    }

    public void HideMovementOptions()
    {
        
        foreach (Tile tile in tilesWithDisplayedVisualHepl)
        {
            //set the tiles parameter to false, to activate the animation
            tile.transform.GetComponentInChildren<Animator>().SetBool("CanMoveTo", false);
           
        }
        tilesWithDisplayedVisualHepl.Clear();
    }

    

}
