using System;
using UnityEngine;

namespace Cub.Scene
{
    public class Intro : MonoBehaviour
    {
        private GameObject T1 { get; set; }
        private GameObject T2 { get; set; }

        public void Awake()
        {
            Cub.View.Library.Initialization();
        }

        public void Start()
        {
            Material M = new Material(Cub.View.Library.Get_Material());
            M.color = new Color(1.0F, 1.0F, 1.0F);

            Material R = new Material(Cub.View.Library.Get_Material());
            R.color = new Color(1.0F, 0F, 0F);

            Cub.View.Alphabet.Create("AUTOHEROIC", new Vector3(6.25f, 2.5f, 0), Vector3.zero, new Vector3(0.3F, 0.3F, 0.1F), M);
            //Cub.View.Alphabet.Create("WELCOME TO AUTOHEROIC", new Vector3(0, 0, 0), Vector3.zero, new Vector3(0.1F, 0.1F, 0.1F), M);
            Cub.View.Alphabet.Create("ASSEMBLE A TEAM OF ROBOTS", new Vector3(0, -1, 0), Vector3.zero, new Vector3(0.1F, 0.1F, 0.1F), M);
            Cub.View.Alphabet.Create("THEN THROW THEM INTO THE WAR", new Vector3(0, -2, 0), Vector3.zero, new Vector3(0.1F, 0.1F, 0.1F), M);
            Cub.View.Alphabet.Create("POINTS ARE GAINED FROM", new Vector3(0, -3, 0), Vector3.zero, new Vector3(0.1F, 0.1F, 0.1F), M);
            T1 = Cub.View.Alphabet.Create("DESTRUCTION", new Vector3(-2F, -4.5F, 0), new Vector3(0, 0, -5), new Vector3(0.15F, 0.15F, 0.15F), R);
            Cub.View.Alphabet.Create("THE TEAM WITH THE MOST POINTS", new Vector3(0, -5.5F, 0), new Vector3(0, 0, 0), new Vector3(0.1F, 0.1F, 0.1F), M);
            T2 = Cub.View.Alphabet.Create("WINS", new Vector3(-5, -7, 0), new Vector3(0, 0, -5), new Vector3(0.15F, 0.15F, 0.15F), R);

            //Cub.View.Alphabet.Create("FIGHT", new Vector3(5.65f, -6.65F, 0), new Vector3(0, 0, 0), new Vector3(0.15F, 0.15F, 0.1F), M);
            //Cub.View.Alphabet.Create("CREDITS", new Vector3(5.65f, -8F, 0), new Vector3(0, 0, 0), new Vector3(0.15F, 0.15F, 0.1F), M);
            //Cub.View.Alphabet.Create("EXIT", new Vector3(5.65f, -9.35F, 0), new Vector3(0, 0, 0), new Vector3(0.15F, 0.15F, 0.1F), M);

            Cub.View.Alphabet.Box.transform.rotation = Quaternion.Euler(new Vector3(317,156,0));
            Cub.View.Alphabet.Box.transform.position = new Vector3(-0.6f, -0.15f, 4.7f);
            //317,156,0
            Sha();
        }

        public void Sha()
        {
            foreach (Cub.View.Cube CO in T1.GetComponentsInChildren<Cub.View.Cube>())
            {
                iTween.ShakeScale(CO.gameObject, new Vector3(0.8F, 0.8F, 0.8F), 1.0F);
            }

            foreach (Cub.View.Cube CO in T2.GetComponentsInChildren<Cub.View.Cube>())
            {
                iTween.ShakeScale(CO.gameObject, new Vector3(0.8F, 0.8F, 0.8F), 1.0F);
            }

            Invoke("Sha", 1.5F);
        }
    }
}
