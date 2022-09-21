using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float radius = 5.0f;
    public float force = 10.0f;

    public GameObject prefabBoomPoint;
    public GameObject prefabSphere;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Icosphere")
        {
            Destroy(collision.gameObject);
            Vector3 boomPosition = collision.gameObject.transform.position;
            Instantiate(prefabBoomPoint, collision.transform.position, collision.transform.rotation);
            Instantiate(prefabSphere, collision.transform.position, collision.transform.rotation);
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
    }
}
