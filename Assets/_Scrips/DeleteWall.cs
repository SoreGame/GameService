using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteWall : MonoBehaviour
{

    public GameObject deleteWall;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere") 
        {
            Destroy(deleteWall);
        }
    }
}
