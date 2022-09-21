using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collorChange : MonoBehaviour
{

    public float value;
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
        if (collision.gameObject.tag == "Sphere") 
        {
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color",Color.Lerp(this.GetComponent<Renderer>().materials[0].color, Color.red, value));
        }
    }
}
