using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBoundaries : MonoBehaviour
{
    private float tLX;
    private float tLY;
    private float bRX;
    private float bRY;

    private Transform target;
    public GameObject TargetObject;
    public GameObject initialMap;

    private void Update()
    {
        target = TargetObject.transform;
    }

    void Start()
    {
        Camera.main.GetComponent<CameraBoundaries>().SetBound(initialMap);
    }

    public void SetBound(GameObject map)
    {
        SuperTiled2Unity.SuperMap config = map.GetComponent<SuperTiled2Unity.SuperMap>();

        float cameraSize = Camera.main.orthographicSize;
        float aspectRatio = Camera.main.aspect * cameraSize;

        // Lasketaan rajat
        tLX = map.transform.position.x + aspectRatio;
        tLY = map.transform.position.y - cameraSize;
        bRX = map.transform.position.x + config.m_Width - aspectRatio;
        bRY = map.transform.position.y - config.m_Height + cameraSize;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(target.position.x, tLX, bRX), Mathf.Clamp(target.position.y, bRY, tLY), transform.position.z);
    }
}
