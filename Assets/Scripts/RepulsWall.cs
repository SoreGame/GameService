using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsWall : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rb;
    public float repulceForece;
    void Start()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Debug.Log(rb.velocity);
            rb.velocity = new Vector3(0, collision.relativeVelocity.y*repulceForece*0.1f, 0);
            Debug.Log("Posle " +rb.velocity);
        }
    }
}
