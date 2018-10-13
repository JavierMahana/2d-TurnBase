//Maneja el sistema de hoover
//utiliza Update
using UnityEngine;
using System;

public class Hoover : MonoBehaviour {

    public static Tile hooveredTile;
    [SerializeField] //sirve para asignar un primer valor
    Tile prevHoovered;

    Map map;

    public event Action<Tile> hooverEvent;
    public event Action<Tile> outOfHooverEvent;

    void Start()
    {
        map = GameObject.FindObjectOfType<Map>();
        hooveredTile = prevHoovered;
    }
    void Update()
    {
        AssingHooveredTile();
        Snatch();
        PostCastEvents();
    }

    void PostCastEvents()
    {
        if (HasChange())
        {
            outOfHooverEvent(prevHoovered);
            hooverEvent(hooveredTile);
        }
    }


    void AssingHooveredTile()
    {
        prevHoovered = hooveredTile;
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
            hooveredTile = map.GetTile(x, y);
        }
    }

    public bool HasChange()
    {
        if (prevHoovered != hooveredTile)
        {
            
            return true;
        }
        
        return false;
    }

    void Snatch()
    {
        if (HasChange())
        {
            gameObject.transform.localPosition = hooveredTile.transform.position;
        }
    }

}
