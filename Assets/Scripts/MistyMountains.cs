using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MistyMountains : MonoBehaviour
{
    private float eventInterval = 10f;
    private bool noEventRunning;
    private GameObject[] players;
    private void Awake()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        noEventRunning = true;
    }

    private void gravityEvent()
    {
        foreach (var player in players)
            if (player.TryGetComponent<CustomGravity>(out CustomGravity gravity))
            {
                StartCoroutine(change(gravity));
            }
    }


    private IEnumerator change(CustomGravity gravity)
    {
        int multiplier = 1;
        do
        {
            gravity.GravityScale -= 0.1f * multiplier;
            yield return new WaitForSeconds(0.5f);
            if (gravity.GravityScale <= 1) { multiplier = -1; }
        } while (gravity.GravityScale != 3f);
    }

    private IEnumerator startEvent()
    {
        noEventRunning = false;
        gravityEvent();
        yield return new WaitForSeconds(eventInterval);
        noEventRunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (noEventRunning)
        {
            StartCoroutine(startEvent());
        }

    }
}
