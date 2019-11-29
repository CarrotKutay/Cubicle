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
        }
        objCheck();
    }

    private void objCheck()
    {
        foreach (var obj in ObjectList)
        {
            if (obj.transform.position.y < -5)
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
        int number = Random.Range(0, 10);
        if (number < 4)
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
        Vector3 rdm = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-2.0f, 2.0f), 0);
        if (type.Equals("DistanceWeapon"))
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                obj = Instantiate(Resources.Load<GameObject>("MiniGun"), rdm, Quaternion.identity);
            }
            else
            {
                obj = Instantiate(Resources.Load<GameObject>("RocketLauncher"), rdm, Quaternion.identity);
            }
        }
        else
        {
            obj = Instantiate(Resources.Load<GameObject>("Healthcube"), new Vector3(5,5,0), Quaternion.identity);
        }
        ObjectList.Add(obj);
        yield return new WaitForSeconds(Random.Range(5, 8));
    }

}
