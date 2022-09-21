using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBoom : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5.0f;
    public float force = 10.0f;

    void Start()
    {
        Vector3 boomPosition = transform.position;
        Collider[] colliders = Physics.OverlapSphere(boomPosition, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, boomPosition, 3.0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
