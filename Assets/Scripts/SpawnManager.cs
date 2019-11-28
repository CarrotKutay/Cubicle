using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private readonly int spawnLimit = 5;
    [SerializeField]
    public int SpawnLimit => spawnLimit;

    public List<GameObject> ObjectList { get => objectList; set => objectList = value; }

    private List<GameObject> objectList;
    private bool spawning = false;

    private void Awake()
    {
        ObjectList = new List<GameObject>();
    }

    private void Update()
    {
        if (ObjectList.Count < 5)
        {
            StartCoroutine(spawn());
            objCheck();
        }
    }

    private void objCheck()
    {
        foreach (var obj in ObjectList)
        {
            if (obj.transform.position.y < -10)
            {
                ObjectList.Remove(obj);
                Destroy(obj);
            }
        }
    }
    private IEnumerator spawn()
    {
        if (!spawning)
        {
            spawning = true;
            yield return StartCoroutine(spawnObject(chooseSpawn()));
            spawning = false;
        }
    }

    private string chooseSpawn()
    {
        int number = Random.Range(0, 9);
        if (number < 3)
        {
            return "DistanceWeapon";
        }
        else
        {
            return "HealthCube";
        }
    }

    private IEnumerator spawnObject(string type)
    {
        GameObject obj;
        if (type.Equals("DistanceWeapon"))
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                obj = Instantiate(Resources.Load<GameObject>("MiniGun"), Vector3.zero, Quaternion.identity);
            }
            else
            {
                obj = Instantiate(Resources.Load<GameObject>("RocketLauncher"), Vector3.zero, Quaternion.identity);
            }
        }
        else
        {
            obj = Instantiate(Resources.Load<GameObject>("Healthcube"), Vector3.zero, Quaternion.identity);
        }
        ObjectList.Add(obj);
        yield return new WaitForSeconds(Random.Range(2, 10));
    }

}
