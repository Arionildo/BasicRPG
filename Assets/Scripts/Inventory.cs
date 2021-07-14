using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<GameObject> loots;

    public void SpawnLoots()
    {
        if ((bool)(loots?.Any()))
        {
            foreach (GameObject loot in loots)
            {
                InstantiateRandomCoordinates(loot);
            }
            loots = new List<GameObject>();
        }
    }

    private void InstantiateRandomCoordinates(GameObject loot)
    {
        float x = Random.Range(transform.position.x - 1, transform.position.x + 1);
        float y = Random.Range(transform.position.y - 1, transform.position.y + 1);
        float z = Random.Range(transform.position.z - 1, transform.position.z + 1);
        Vector3 position = new Vector3(x, y, z);
        Quaternion rotation = loot.transform.rotation;

        Instantiate(loot, position, rotation);
    }
}
