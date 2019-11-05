using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTry : MonoBehaviour
{
    public Terrain t;
    // Blend the two terrain textures according to the steepness of
    // the slope at each point.
    void Start()
    {
        float[,,] map = new float[t.terrainData.alphamapWidth, t.terrainData.alphamapHeight, 3];

        // For each point on the alphamap...
        for (int y = 0; y < t.terrainData.alphamapHeight / 2; y++)
        {
            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
            {
                map[x, y, 1] = 0.5f;
            }
        }
                map[1, 1, 0] = 0.5f;
                map[1,1, 2] = 1f;


        t.terrainData.SetAlphamaps(0, 0, map);
    }
}
    