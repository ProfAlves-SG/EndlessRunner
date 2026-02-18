using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefab;
    [SerializeField] private float zDistanceToSpawn;
    [SerializeField] private float numberOfTiles;
    
    [SerializeField] private float tileLength;
    [SerializeField] private Transform playerTransform;
    
    private List<GameObject> activeTiles = new List<GameObject>();
    
    private void Awake()
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefab.Length));
            }
        }
    }

    private void Update()
    {
        if(playerTransform.position.z - tileLength > zDistanceToSpawn - (numberOfTiles * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefab.Length));
            DeleteTile();
        }
    }
    
    private void SpawnTile(int tileIndex)
    {
        GameObject tile = Instantiate(tilePrefab[tileIndex], transform.forward * zDistanceToSpawn, transform.rotation);
        activeTiles.Add(tile);
        zDistanceToSpawn += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
