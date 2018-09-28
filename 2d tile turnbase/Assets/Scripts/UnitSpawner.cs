using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawner : MonoBehaviour {

    MapGenerator mapGen;

    public Texture2D unitSpawningInfo;
    public ColorToPrefav[] colorMappings;

    private void Start()
    {
        mapGen = GameObject.FindObjectOfType<MapGenerator>();
        //hay que asegurarse que las 2 texturas tienen las mismas proporciones;
        Debug.Assert(mapGen.dataToGenerateMap.width == unitSpawningInfo.width && mapGen.dataToGenerateMap.height == unitSpawningInfo.height, "the 2 textures must have the same bunds");
        //hay que asegurarse que los prefavs contienen unidades
        foreach (ColorToPrefav m in colorMappings)
        {
            Debug.Assert(m.prefav.GetComponent<Unit>() != null,"all the pefabs must have a unit component");
        }
        

        Spawn();
        
    }
    void Spawn()
    {
        UnitContainer container = GameObject.FindObjectOfType<UnitContainer>();
        for (int y = 0; y < unitSpawningInfo.height; y++)
        {
            for (int x = 0; x < unitSpawningInfo.width; x++)
            {
                Color pixelColor = unitSpawningInfo.GetPixel(x, y);

                foreach (ColorToPrefav m in colorMappings)
                {
                    if (pixelColor == m.color)
                    {
                        Unit newUnit = m.prefav.GetComponent<Unit>();
                        GameObject newObject = Instantiate(m.prefav, new Vector3(x, y, 0), Quaternion.identity, container.transform);
                        container.unitList.Add(newUnit);
                    }
                }
            }
        }
    }
}
