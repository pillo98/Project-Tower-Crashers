using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField]
    Transform Point1;
    [SerializeField]
    Transform Point2;
    [SerializeField]
    GameObject sPoint;

    [SerializeField]
    List<GameObject> PowerUps;

    public void SpawnItem()
    {
        GameObject PowerUp = PowerUps[Random.Range(0, PowerUps.Count)];

        float x = Random.Range(Point1.position.x, Point2.position.x);
        float y = Point1.position.y;
        float z = 0;
        Vector3 pos = new Vector3(x, y, z);
        sPoint.transform.position = pos;

        Instantiate(PowerUp,sPoint.transform.position, sPoint.transform.rotation);
    }
}
