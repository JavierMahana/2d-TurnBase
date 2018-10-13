using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Selector : MonoBehaviour {

    public static Tile tileSelected;

    [SerializeField]
    Tile previouslySelected;
    Map map;

    public event Action<Tile> selectionEvent;
    public event Action deselectEvent;
    

	void Start () {
        tileSelected = previouslySelected;
        map = GameObject.FindObjectOfType<Map>().GetComponent<Map>();
	}
	
	void Update ()
    {
        Select();
        NotifySelectionChanges();
	}

    void NotifySelectionChanges()
    {
        if (TheSelectionHasChange())
        {
            if (deselectEvent != null)
            {
                deselectEvent();
            }
            if (selectionEvent != null)
            {
                selectionEvent(tileSelected);
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
                if (x > map.GetWidth() || y > map.GetHeigth())
                {
                    return;
                }
                tileSelected = map.GetTile(x, y);
            }

        }
        
    }

    public bool TheSelectionHasChange()
    {
        if (previouslySelected != tileSelected)
        {
            previouslySelected = tileSelected;
            return true;
        }
        return false;
    }

}
