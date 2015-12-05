using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GameObject[] GOL = GameObject.FindGameObjectsWithTag("Cube");

        foreach (GameObject GO in GOL)
        {
            GO.rigidbody.useGravity = false;
            GO.GetComponent<BoxCollider>().enabled = false;            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject[] GOL = GameObject.FindGameObjectsWithTag("Cube");
            foreach (GameObject GO in GOL)
            {
                GO.rigidbody.useGravity = true;
                GO.GetComponent<BoxCollider>().enabled = true;
                GO.rigidbody.AddForce(new Vector3(-5F, Random.Range(-5F, 5F), Random.Range(-5F, 5F)), ForceMode.Impulse);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Diss();
        }
    }

    void Diss()
    {
        GameObject[] GOL = GameObject.FindGameObjectsWithTag("Cube");
        int I = Random.Range(0, GOL.Length);

        GOL[I].rigidbody.useGravity = true;
        GOL[I].GetComponent<BoxCollider>().enabled = true;
        GOL[I].rigidbody.AddForce(new Vector3(-5F, Random.Range(-5F, 5F), Random.Range(-5F, 5F)), ForceMode.Impulse);
        //GOL[I].GetComponent<TrailRenderer>().enabled = true;
        iTween.ScaleTo(GOL[I], new Vector3(0.2F, 0.2F, 0.2F), 0.5F);
        iTween.RotateAdd(GOL[I], new Vector3(360, 360, 360), 5F);
        
        Invoke("Diss", 0.5F);
    }
}
