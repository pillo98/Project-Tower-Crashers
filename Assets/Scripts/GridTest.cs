using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTest : MonoBehaviour
{
    public GameObject indicator;
    public Camera sceneCamera;
    public LayerMask placementLayermask;

    private Vector3 lastPosition;

    // Method to get the selected map position in 2D
    public Vector3 GetSelectedMapPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector2 worldPos = sceneCamera.ScreenToWorldPoint(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, placementLayermask);

        if (hit.collider != null)
        {
            lastPosition = hit.point;
        }

        return lastPosition;
    }

    void Update()
    {
        Vector3 mousePos = GetSelectedMapPosition();
        // Update the indicator position in 2D (ignoring the z-axis)
        indicator.transform.position = new Vector3(mousePos.x, mousePos.y, indicator.transform.position.z);
    }
}
