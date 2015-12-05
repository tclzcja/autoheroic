using UnityEngine;
using System.Collections;

public class Cube : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Plane")
        {
            iTween.ScaleTo(this.gameObject, Vector3.zero, 2.0F);
            Destroy(this.gameObject, 2.0F);
        }
    }
}
