using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collorChange : MonoBehaviour
{

    public float value;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Sphere") 
        {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(this.GetComponent<Renderer>().materials[0].color, Color.red, value));
        }
    }
}


