using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using UnityEngine.UI;

public class ObjectSpawner : MonoBehaviour
{
    public string cubeCount;
    public float pauseTime;
    public GameObject spawnObject;
    public GameObject inputField;

    // Start is called before the first frame update
    void Start()
    {
        waiter();
    }

    public async void waiter()
    {
        cubeCount = inputField.GetComponent<Text>().text;

        for (int i = 0; i < int.Parse(cubeCount); i++)
        {
            Instantiate(spawnObject, this.transform.position, this.transform.rotation);
            await Task.Delay(TimeSpan.FromSeconds(pauseTime));
        }
    }
}
