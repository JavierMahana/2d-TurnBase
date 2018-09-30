using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Selector : MonoBehaviour {

    public Tile tileSelected;
    
    Tile previouslySelected;
    Map map;

    public event Action<Unit> selectionEvent;
    public event Action deselectEvent;
    

	// Use this for initialization
	void Start () {
        previouslySelected = tileSelected;
        map = GameObject.FindObjectOfType<Map>().GetComponent<Map>();
	}
	
	void Update ()
    {
        Select();
        NotifyChanges();
	}
    


    void NotifyChanges()
    {
        if (TheSelectionHasChange())
        {

            
            if (previouslySelected.isOcupied)
            {
                if (deselectEvent != null)
                {
                    deselectEvent();
                }
            }


            if (tileSelected.isOcupied)
            {
                if (selectionEvent != null)
                {
                    selectionEvent(tileSelected.unitAbove);
                }
            }
            else
            {
                if (deselectEvent != null)
                {
                    deselectEvent();
                }
            }
        }
    }


    void Select()
    {
        if (Input.GetMouseButtonDown(0))
        {
            

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30f))
            {
                int x = Mathf.FloorToInt(hit.point.x);
                int y = Mathf.FloorToInt(hit.point.y);
                tileSelected = map.GetTile(x, y);
            }

        }
        
    }

    bool TheSelectionHasChange()
    {
        if (previouslySelected != tileSelected)
        {
            previouslySelected = tileSelected;
            return true;
        }
        previouslySelected = tileSelected;
        return false;
    }

}
