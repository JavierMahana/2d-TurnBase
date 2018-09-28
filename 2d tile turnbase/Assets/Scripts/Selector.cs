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
        NotifyIfSelectionChanges();
	}
    void NotifyIfSelectionChanges()
    {
        if (previouslySelected != tileSelected)
        {
            if (tileSelected.isOcupied)
            {
                if (selectionEvent != null)
                {
                    selectionEvent(tileSelected.unitAbove);
                }
            }
            else
            {
                deselectEvent();
                previouslySelected = tileSelected;
                return;
            }
            if (previouslySelected.isOcupied)
            {
                if (deselectEvent != null)
                {
                    deselectEvent();
                }
            }
        }
        previouslySelected = tileSelected;
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

    

}
