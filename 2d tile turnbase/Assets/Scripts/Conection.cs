using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conection  {

    public Conection(Tile start, Tile destiny)
    {
        toNode = destiny;
        fromNome = start;
    }
    public Tile toNode;
    public Tile fromNome;
	
}
