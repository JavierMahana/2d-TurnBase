//usa Update
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PathDrawer : MonoBehaviour{

    Selector selector;
    Hoover hoover;
    Map map;
    MovementVisualAid movementVisualAid;

    [SerializeField]
    GameObject[] prefabs = new GameObject[15];
    List<GameObject> instantited;
    Unit selectedUnit;
    

    void Start()
    {
        hoover = FindObjectOfType<Hoover>();
        map = GameObject.FindObjectOfType<Map>();
        selector = GameObject.FindObjectOfType<Selector>();
        movementVisualAid = FindObjectOfType<MovementVisualAid>();

        selector.selectionEvent += UpdateUnitSelected;
    }

    void Update()
    {
        //DrawPath();
    }

    void DrawPath()
    {
        if (selectedUnit == null || selectedUnit.state != Unit.UnitState.Friendly)
        {
            return;
        }
        List<Tile> path;
        int lap = 0;
        int pathCost;
        Tile start = selectedUnit.standingTile;

        while (Input.GetMouseButton(1))
        {
            if (lap < 1)
            {
                path = PathFinding.AStar(map, selectedUnit.standingTile, Hoover.hooveredTile, out pathCost);
                int length = path.Count;
                //cc/cu/cl/cd/cr.../pv/ph..../eld/elu/eru/erd/....fu/fl/fd/fr



                for (int i = 0; i < path.Count; i++)
                {
                    Tile currentTile = path[i];
                    Vector3 currentPos = currentTile.transform.position;
                    if (i == 0)
                    {
                        if (i == path.Count - 1)
                        {
                            instantited.Add(Instantiate(prefabs[0], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        Tile nextTile = path[i + 1];
                        Vector3 nextTPos = nextTile.transform.position;
                        if (currentPos.y + 1 == nextTPos.y)
                        {
                            instantited.Add(Instantiate(prefabs[1], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x + 1 == nextTPos.x)
                        {
                            instantited.Add(Instantiate(prefabs[2], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.y - 1 == nextTPos.y)
                        {
                            instantited.Add(Instantiate(prefabs[3], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x - 1 == nextTPos.x)
                        {
                            instantited.Add(Instantiate(prefabs[4], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                    }
                    else if (i == path.Count - 1)
                    {
                        Tile prevTile = path[i - 1];
                        Vector3 prevTPos = prevTile.transform.position;

                        if (currentPos.y == prevTPos.y + 1)
                        {
                            instantited.Add(Instantiate(prefabs[11], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x == prevTPos.x + 1)
                        {
                            instantited.Add(Instantiate(prefabs[12], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.y == prevTPos.y - 1)
                        {
                            instantited.Add(Instantiate(prefabs[13], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x == prevTPos.x - 1)
                        {
                            instantited.Add(Instantiate(prefabs[14], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }

                    }
                    else
                    {
                        Tile prevTile = path[i - 1];
                        Tile nextTile = path[i + 1];
                        Vector3 nextPos = nextTile.transform.position;
                        Vector3 prevPos = prevTile.transform.position;

                        if (currentPos.x == prevPos.x && currentPos.x == nextPos.x)
                        {
                            instantited.Add(Instantiate(prefabs[5], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.y == prevPos.y && currentPos.y == nextPos.y)
                        {
                            instantited.Add(Instantiate(prefabs[6], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        //-------------------------------
                        else if (currentPos.x == prevPos.x - 1 && currentPos.y == nextPos.y - 1 || currentPos.x == nextPos.x - 1 && currentPos.y == prevPos.y - 1)
                        {
                            instantited.Add(Instantiate(prefabs[7], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x == prevPos.x - 1 && currentPos.y == nextPos.y + 1 || currentPos.x == nextPos.x - 1 && currentPos.y == prevPos.y + 1)
                        {
                            instantited.Add(Instantiate(prefabs[8], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x == prevPos.x + 1 && currentPos.y == nextPos.y + 1 || currentPos.x == nextPos.x + 1 && currentPos.y == prevPos.y + 1)
                        {
                            instantited.Add(Instantiate(prefabs[9], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                        else if (currentPos.x == prevPos.x + 1 && currentPos.y == nextPos.y - 1 || currentPos.x == nextPos.x + 1 && currentPos.y == prevPos.y - 1)
                        {
                            instantited.Add(Instantiate(prefabs[8], currentPos, Quaternion.identity, this.transform));
                            continue;
                        }
                    }
                }



            }
            if (hoover.HasChange())
            {
                Hoover.hooveredTile.terrain.movementCost
            }
            lap++;
        }
    }

    void ClearPath()
    {
    }
    public void UpdateUnitSelected(Tile other)
    {
        selectedUnit = other.unitAbove;
    }
}
