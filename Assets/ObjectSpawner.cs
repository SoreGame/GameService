using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

public class ObjectSpawner : MonoBehaviour
{
    public int cubeCount;
    public float pauseTime;
    public GameObject spawnObject;

    // Start is called before the first frame update
    void Start()
    {
        waiter();
    }

    private async void waiter()
    {
        for (int i = 0; i < cubeCount; i++)
        {
            Instantiate(spawnObject, this.transform.position, this.transform.rotation);
            await Task.Delay(TimeSpan.FromSeconds(pauseTime));
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
     
}
