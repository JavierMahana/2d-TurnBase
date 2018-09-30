using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinding {


    struct NodeRecord
    {
        public Tile node;
        public int costSoFar;
        public Conection conection;
        public int estimatedCost;
    }
    /// <summary>
    /// dadas una lista de tiles y una unidad retorna todas las tiles a las que puede atacar
    /// </summary>
    /// <param name="map"></param>
    /// <param name="canMoveTiles"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static List<Tile> GetCanAtackTiles(Map map, List<Tile> canMoveTiles, Unit unit)
    {
        //no devuelve nada :C
        List<Tile> output = new List<Tile>();
        Debug.Assert(canMoveTiles.Count > 0);
        int i = 0;
        foreach (int range in unit.weaponRange)
        {
            foreach (Tile tile in canMoveTiles)
            {
                
                List<Tile> temporal = GetTilesAtDistance(map, tile, range);
                Debug.Log(i.ToString() + " vuelta. Hay temporales" + temporal.Count.ToString());
                i++;

                foreach (Tile tempTile in temporal)
                {
                    //se be si la tile ya se ha agragado.
                    //si no esta, se agrega al output
                    if (output.Contains(tempTile) == false)
                    {
                        output.Add(tempTile);
                    }
                }
            }
        }
        return output;
    }

    /// <summary>
    /// Retorna todas las tiles a las que una unidad se puede mover
    /// </summary>
    /// <param name="map"></param>
    /// <param name="start"></param>
    /// <param name="unit"></param>
    /// <returns></returns>
    public static List<Tile> GetCanMoveTiles(Map map, Unit unit)
    {
        int movement = unit.movementLeft;

        List<Tile> output = new List<Tile>();
        List<NodeRecord> open = new List<NodeRecord>();
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord current = new NodeRecord();

        current.node = unit.standingTile;
        current.costSoFar = 0;

        open.Add(current);
        int width = map.GetWidth();
        int heigth = map.GetHeigth();

        while (current.costSoFar <= movement && open.Count > 0)
        {


            int x = (int)current.node.transform.position.x;
            int y = (int)current.node.transform.position.y;



            // si esta en el limite izquierdo (x = 0) no hay coneccion a la izquierda
            if (x > 0)
            {

                Tile leftTile = map.GetTile(x - 1, y);

                //se debe ver si la tile es pasable, debido a la unidad que esta encima
                if (leftTile.Evaluate(unit))
                {
                    //aca se ve si la tile es Inpasable
                    if (Contains(open, leftTile) == false)
                    {
                        if (Contains(closed, leftTile) == false)
                        {
                            NodeRecord newRecord = new NodeRecord();
                            newRecord.node = leftTile;
                            newRecord.costSoFar = leftTile.terrain.movementCost + current.costSoFar;
                            open.Add(newRecord);
                        }
                    }
                }


            }
            // si esta en el limite derecho (x = width) no hay coneccion a la derecha
            if (x < width)
            {

                Tile rigthTile = map.GetTile(x + 1, y);

                if (rigthTile.Evaluate(unit))
                {
                    if (Contains(open, rigthTile) == false)
                    {
                        if (Contains(closed, rigthTile) == false)
                        {
                            NodeRecord newRecord = new NodeRecord();
                            newRecord.node = rigthTile;
                            newRecord.costSoFar = rigthTile.terrain.movementCost + current.costSoFar;
                            open.Add(newRecord);
                        }
                    }
                }


            }
            // si esta en el limite inferior (y = 0) no hay coneccion hacia abajo
            if (y > 0)
            {

                Tile downTile = map.GetTile(x, y - 1);

                if (downTile.Evaluate(unit))
                {
                    if (Contains(open, downTile) == false)
                    {
                        if (Contains(closed, downTile) == false)
                        {
                            NodeRecord newRecord = new NodeRecord();
                            newRecord.node = downTile;
                            newRecord.costSoFar = downTile.terrain.movementCost + current.costSoFar;
                            open.Add(newRecord);
                        }
                    }
                }


            }
            // si esta en el limite superior (y = heigth) no hay coneccion hacia arriba
            if (y < heigth)
            {

                Tile upperTile = map.GetTile(x, y + 1);
                if (upperTile.Evaluate(unit))
                {
                    if (Contains(open, upperTile) == false)
                    {
                        if (Contains(closed, upperTile) == false)
                        {
                            NodeRecord newRecord = new NodeRecord();
                            newRecord.node = upperTile;
                            newRecord.costSoFar = upperTile.terrain.movementCost + current.costSoFar;
                            open.Add(newRecord);
                        }
                    }
                }


            }
            //se termino de revisar las conecciones

            open.Remove(current);
            closed.Add(current);
            output.Add(current.node);


            //find the smallest element in open
            if (open.Count <= 0)
            {
                break;
            }
            current = SmallestNodeInListCSF(open);
        }
        //se termino de revisar
        return output;
    }


    static List<Tile> GetTilesAtDistance(Map map, Tile tile, int distance)
    {
        //debe Retornar todas las tiles que esten a cierta distancia de x tile
        List<Tile> output = new List<Tile>();

        List<NodeRecord> open = new List<NodeRecord>();
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord current = new NodeRecord();
        current.node = tile;
        current.costSoFar = 0;
        open.Add(current);

        int width = map.GetWidth();
        int heigth = map.GetHeigth();
        

        while (open.Count > 0 && current.costSoFar < distance)
        {
            
            
            int x = (int)current.node.transform.position.x;
            int y = (int)current.node.transform.position.y;

            //Debug.Log(x.ToString() + "|" + y.ToString());
            
            //Debug.Log(current.costSoFar.ToString());

            // si esta en el limite izquierdo (x = 0) no hay coneccion a la izquierda
            if (x > 0)
            {

                Tile leftTile = map.GetTile(x - 1, y);
                
                if (Contains(open, leftTile) == false)
                {
                    if (Contains(closed, leftTile) == false)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = leftTile;
                        newRecord.costSoFar = 1 + current.costSoFar;
                        open.Add(newRecord);
                        if (newRecord.costSoFar == distance)
                        {
                            output.Add(newRecord.node);
                        }
                    }
                }

            }
            // si esta en el limite derecho (x = width) no hay coneccion a la derecha
            if (x < width - 1)
            {
                
                Tile rigthTile = map.GetTile(x + 1, y);

                if (Contains(open, rigthTile) == false)
                {
                    if (Contains(closed, rigthTile) == false)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = rigthTile;
                        newRecord.costSoFar = 1 + current.costSoFar;
                        open.Add(newRecord);
                        if (newRecord.costSoFar == distance)
                        {
                            output.Add(newRecord.node);
                        }
                    }
                }

            }
            // si esta en el limite inferior (y = 0) no hay coneccion hacia abajo
            if (y > 0)
            {

                Tile downTile = map.GetTile(x, y - 1);

                if (Contains(open, downTile) == false)
                {
                    if (Contains(closed, downTile) == false)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = downTile;
                        newRecord.costSoFar = 1 + current.costSoFar;
                        open.Add(newRecord);
                        if (newRecord.costSoFar == distance)
                        {
                            output.Add(newRecord.node);
                        }
                    }
                }
            }
            // si esta en el limite superior (y = heigth) no hay coneccion hacia arriba
            if (y < heigth - 1)
            {

                Tile upperTile = map.GetTile(x, y + 1);

                if (Contains(open, upperTile) == false)
                {
                    if (Contains(closed, upperTile) == false)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = upperTile;
                        newRecord.costSoFar = 1 + current.costSoFar;
                        open.Add(newRecord);
                        if (newRecord.costSoFar == distance)
                        {
                            output.Add(newRecord.node);
                        }
                    }
                }
            }
            open.Remove(current);
            closed.Add(current);

            //find the smallest element in open
            if (open.Count <= 0)
            {
                break;
            }
            current = SmallestNodeInListCSF(open);

        }

        return output;

    }

    public static List<Conection> AStar(Map map, Tile start, Tile goal)
    {
        List<Conection> output = new List<Conection>();

        List<NodeRecord> open = new List<NodeRecord>();
        List<NodeRecord> closed = new List<NodeRecord>();

        NodeRecord startRecord = new NodeRecord();

        startRecord.node = start;
        startRecord.estimatedCost = GetDistance(start, goal);
        startRecord.costSoFar = 0;
        startRecord.conection = null;

        open.Add(startRecord);

        NodeRecord current = startRecord;

        int width = map.GetWidth();
        int heigth = map.GetHeigth();


        while (open.Count > 0)
        {
            if (current.node == goal)
            {
                break;
            }

            //tener conecciones y veciones(mismo codigo que diskras)
            int x = (int)current.node.transform.position.x;
            int y = (int)current.node.transform.position.y;

           
            // si esta en el limite izquierdo (x = 0) no hay coneccion a la izquierda
            if (x > 0)
            {

                Tile endTile = map.GetTile(x - 1, y);

                if (Contains(open, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(open, endTile);
                    //mejorRuta crear record y borrar antiguo
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);
                        
                        open.Add(newRecord);
                        open.Remove(oldRecord);
                    }
                }
                if (Contains(closed, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(closed, endTile);
                    //mejor ruta borrar antigua
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);
                        
                        closed.Remove(oldRecord);
                        open.Add(newRecord);
                    }
                    
                }

            }
            // si esta en el limite derecho (x = width) no hay coneccion a la derecha
            if (x < width)
            {

                Tile endTile = map.GetTile(x - 1, y);

                if (Contains(open, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(open, endTile);
                    //mejorRuta crear record y borrar antiguo
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        open.Add(newRecord);
                        open.Remove(oldRecord);
                    }
                }
                if (Contains(closed, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(closed, endTile);
                    //mejor ruta borrar antigua
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        closed.Remove(oldRecord);
                        open.Add(newRecord);
                    }

                }

            }
            // si esta en el limite inferior (y = 0) no hay coneccion hacia abajo
            if (y > 0)
            {

                Tile endTile = map.GetTile(x - 1, y);

                if (Contains(open, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(open, endTile);
                    //mejorRuta crear record y borrar antiguo
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        open.Add(newRecord);
                        open.Remove(oldRecord);
                    }
                }
                if (Contains(closed, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(closed, endTile);
                    //mejor ruta borrar antigua
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        closed.Remove(oldRecord);
                        open.Add(newRecord);
                    }
                }

            }
            // si esta en el limite superior (y = heigth) no hay coneccion hacia arriba
            if (y < heigth)
            {
                Tile endTile = map.GetTile(x - 1, y);

                if (Contains(open, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(open, endTile);
                    //mejorRuta crear record y borrar antiguo
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        open.Add(newRecord);
                        open.Remove(oldRecord);
                    }
                }
                if (Contains(closed, endTile))
                {
                    int newCostSoFar = endTile.terrain.movementCost + current.costSoFar;
                    NodeRecord oldRecord = Find(closed, endTile);
                    //mejor ruta borrar antigua
                    if (oldRecord.costSoFar > newCostSoFar)
                    {
                        NodeRecord newRecord = new NodeRecord();
                        newRecord.node = endTile;
                        newRecord.costSoFar = newCostSoFar;
                        newRecord.estimatedCost = GetDistance(endTile, goal) + newRecord.costSoFar;
                        newRecord.conection = new Conection(current.node, endTile);

                        closed.Remove(oldRecord);
                        open.Add(newRecord);
                    }

                }
            }
            //se termino de revisar las conecciones
            closed.Add(current);
            open.Remove(current);

            //hay que seleccionar el siguiente a ver;
            current = EstimatedSmallestNodeInList(open);

        }
        //Se acabaron las open Tiles o llegamos a la meta;
        if (current.node != goal)
        {
            //fracaso
            return null;
        }
        Tile t = current.node;
        while (t != start)
        {
            Conection c = current.conection;
            output.Add(c);
            t = c.fromNome;
        }
        //se parte del principio
        output.Reverse();
        return output;

    }

    static NodeRecord EstimatedSmallestNodeInList(List<NodeRecord> list)
    {
        
        NodeRecord smallestNode = list[0];
        foreach (NodeRecord record in list)
        {
            if (record.estimatedCost < smallestNode.estimatedCost)
            {
                smallestNode = record;
            }
        }
        return smallestNode;

    }
    static bool Contains(List<NodeRecord> list, Tile node)
    {
        foreach (NodeRecord record in list)
        {
            if (record.node == node)
            {
                return true;
            }
        }
        return false;
    }
    static NodeRecord Find(List<NodeRecord> list, Tile node)
    {
        foreach (NodeRecord record in list)
        {
            if (record.node == node)
            {
                return record;
            }
        }
        Debug.Log("ojo, que se esta creando una instacia. Puede que no quieras esto");
        NodeRecord nuevo = new NodeRecord();
        return nuevo;
    }
    static NodeRecord SmallestNodeInListCSF(List<NodeRecord> list)
    {
        NodeRecord smallestNode = list[0];
        foreach (NodeRecord record in list)
        {
            if (record.costSoFar < smallestNode.costSoFar)
            {
                smallestNode = record;
            }
        }
        return smallestNode;
    }
    static int GetDistance(Tile t1, Tile t2)
    {
        int xDif = Mathf.Abs((int)(t1.transform.position.x - t2.transform.position.x));
        int yDif = Mathf.Abs((int)(t1.transform.position.y - t2.transform.position.y));
        int distance = xDif + yDif;
        return distance;
    }

}
