using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject dragonEggPregab;
    public float speed = 1;
    public float timeBetweeenEggDrops = 1f;
    public float leftRightDistance = 10f;
    public float chanceDirectional = 0.1f;
    void Start()
    {
        Invoke("DroppEgg", 2f);
    }

    void DroppEgg()
    {
        Vector3 myVector = new Vector3(0f, 5f, 0f);
        GameObject egg = Instantiate<GameObject>(dragonEggPregab);
        egg.transform.position = transform.position + myVector;
        Invoke("DroppEgg", timeBetweeenEggDrops);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance)
        {
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftRightDistance)
        {
            speed = -Mathf.Abs(speed);
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceDirectional)
        {
            speed *= -1;
        }
    }
}

