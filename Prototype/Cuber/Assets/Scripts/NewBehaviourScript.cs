using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            GameObject[] GOL = GameObject.FindGameObjectsWithTag("Cube");
            foreach (GameObject GO in GOL)
            {
                GO.rigidbody.useGravity = true;
                GO.rigidbody.AddForce(new Vector3(-1F, Random.Range(-0.5F, 0.5F), Random.Range(-0.5F, 0.5F)), ForceMode.Impulse);
                Destroy(GO, 10F);
            }
        }
    }
}
