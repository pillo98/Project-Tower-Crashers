using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ObjectPlacer : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> placedGameObjects = new();

    [SerializeField]
    PlacementSystem placementSystem;
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        newObject.transform.position = position;
        newObject.tag = this.gameObject.tag;
        placedGameObjects.Add(newObject);
        return placedGameObjects.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObjects.Count <= gameObjectIndex
            || placedGameObjects[gameObjectIndex] == null)
            return;
        Destroy(placedGameObjects[gameObjectIndex]);
        placedGameObjects[gameObjectIndex] = null;

    }

    public void AddPhysics()
    {
        foreach (GameObject gameObject in placedGameObjects)
        {
            if (gameObject != null)
            {
                Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
                rb.simulated = true;
            }
        }

    }
      
}

