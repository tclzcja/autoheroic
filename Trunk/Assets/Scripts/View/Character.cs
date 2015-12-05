using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public class Character_Part
    {
        public List<Cub.View.Cubon> Head { get; set; }
        public List<Cub.View.Cubon> Body { get; set; }
        public List<Cub.View.Cubon> Arms_Left { get; set; }
        public List<Cub.View.Cubon> Arms_Right { get; set; }
        public List<Cub.View.Cubon> Legs_Left { get; set; }
        public List<Cub.View.Cubon> Legs_Right { get; set; }
    }

    public class Character_Stat
    {
        public System.Guid ID { get; set; }
        public string Name { get; set; }
        public Cub.Model.Team Team { get; set; }
        public int MHP { get; set; }
        public int HP { get; set; }
        public int Value { get; set; }
        public Cub.Position2 Position { get; set; }

        public Part_Head Head { get; set; }
        public Part_Body Body { get; set; }
        public Part_Arms Arms { get; set; }
        public Part_Legs Legs { get; set; }
    }

    public class Character : MonoBehaviour
    {
        public Character_Stat Stat { get; private set; }
        public Character_Part Part { get; private set; }

        public Character()
        {
            this.Stat = new Character_Stat();
            this.Part = new Character_Part();
        }

        public void Initialize_Stat(Guid _ID, string n, int _MHP, int _HP, Position2 _Position, Cub.Model.Team _Team, 
            Part_Head _Head, Part_Body _Body, Part_Arms _Arms, Part_Legs _Legs, int _Value)
        {
            this.Stat.ID = _ID;
            this.Stat.Name = n;
            this.Stat.MHP = _MHP;
            this.Stat.HP = _HP;
            this.Stat.Position = _Position;
            this.Stat.Team = _Team;
            this.Stat.Value = _Value;

            this.Stat.Head = _Head;
            this.Stat.Body = _Body;
            this.Stat.Arms = _Arms;
            this.Stat.Legs = _Legs;
        }

        public void Delete_Part()
        {
            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Arms_Left = this.gameObject.transform.FindChild("Arms_Left").gameObject;
            GameObject GO_Arms_Right = this.gameObject.transform.FindChild("Arms_Right").gameObject;
            GameObject GO_Legs_Left = this.gameObject.transform.FindChild("Legs_Left").gameObject;
            GameObject GO_Legs_Right = this.gameObject.transform.FindChild("Legs_Right").gameObject;

            foreach (Transform t in GO_Head.transform)
                Destroy(t.gameObject);
            foreach (Transform t in GO_Body.transform)
                Destroy(t.gameObject);
            foreach (Transform t in GO_Arms_Left.transform)
                Destroy(t.gameObject);
            foreach (Transform t in GO_Arms_Right.transform)
                Destroy(t.gameObject);
            foreach (Transform t in GO_Legs_Left.transform)
                Destroy(t.gameObject);
            foreach (Transform t in GO_Legs_Right.transform)
                Destroy(t.gameObject);
        }

        public void Initialize_Part()
        {
            this.Part.Head = Library.Get_Part_Head(this.Stat.Head);
            this.Part.Body = Library.Get_Part_Body(this.Stat.Body);
            this.Part.Arms_Left = Library.Get_Part_Arms_Left(this.Stat.Arms);
            this.Part.Arms_Right = Library.Get_Part_Arms_Right(this.Stat.Arms);
            this.Part.Legs_Left = Library.Get_Part_Legs_Left(this.Stat.Legs);
            this.Part.Legs_Right = Library.Get_Part_Legs_Right(this.Stat.Legs);

            GameObject GO_Head = this.gameObject.transform.FindChild("Head").gameObject;
            GameObject GO_Body = this.gameObject.transform.FindChild("Body").gameObject;
            GameObject GO_Arms_Left = this.gameObject.transform.FindChild("Arms_Left").gameObject;
            GameObject GO_Arms_Right = this.gameObject.transform.FindChild("Arms_Right").gameObject;
            GameObject GO_Legs_Left = this.gameObject.transform.FindChild("Legs_Left").gameObject;
            GameObject GO_Legs_Right = this.gameObject.transform.FindChild("Legs_Right").gameObject;

            Dictionary<Colour, Material> Dictionary_Material = new Dictionary<Colour, Material>();
            Dictionary_Material.Add(Colour.Skin, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Black, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.White, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Grey, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Brown, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Silver, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Team_Primary, new Material(Library.Get_Material()));
            Dictionary_Material.Add(Colour.Team_Secondary, new Material(Library.Get_Material()));
            Dictionary_Material[Colour.Skin].color = new Color(0.95F, 0.88F, 0.88F);
            Dictionary_Material[Colour.Black].color = Color.black;
            Dictionary_Material[Colour.White].color = Color.white;
            Dictionary_Material[Colour.Grey].color = new Color(0.25F, 0.25F, 0.25F);
            Dictionary_Material[Colour.Brown].color = new Color(0.64F, 0.32F, 0.32F, 1F);
            Dictionary_Material[Colour.Silver].color = new Color(0.88F, 0.88F, 0.88F, 1F);
            Dictionary_Material[Colour.Team_Primary].color = Stat.Team.Colour_Primary;
            Dictionary_Material[Colour.Team_Secondary].color = Stat.Team.Colour_Secondary;
            
            foreach (Cubon C in Part.Head)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Head.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            foreach (Cubon C in Part.Body)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Body.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            foreach (Cubon C in Part.Arms_Left)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Arms_Left.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            foreach (Cubon C in Part.Arms_Right)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Arms_Right.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            foreach (Cubon C in Part.Legs_Left)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Legs_Left.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            foreach (Cubon C in Part.Legs_Right)
            {
                foreach (Vector3 P in C.Position)
                {
                    GameObject G = Instantiate(Library.Get_Cube()) as GameObject;

                    G.transform.parent = GO_Legs_Right.transform;
                    G.transform.localPosition = P;
                    G.transform.localScale = G.transform.lossyScale;
                    G.transform.localRotation = Quaternion.identity;
                    G.renderer.material = Dictionary_Material[C.Colour];
                }
            }

            switch (Stat.Legs)
            {
                case Part_Legs.Humanoid:
                    {
                        GO_Legs_Left.GetComponent<Animator>().SetTrigger("Humanoid");
                        GO_Legs_Right.GetComponent<Animator>().SetTrigger("Humanoid");
                        break;
                    }
                case Part_Legs.Tread:
                    {
                        GO_Legs_Left.GetComponent<Animator>().SetTrigger("Tread");
                        GO_Legs_Right.GetComponent<Animator>().SetTrigger("Tread");
                        break;
                    }
                case Part_Legs.Hover:
                    {
                        GO_Legs_Left.GetComponent<Animator>().SetTrigger("Hover");
                        GO_Legs_Right.GetComponent<Animator>().SetTrigger("Hover");
                        this.gameObject.transform.FindChild("PS_Hover").gameObject.SetActive(true);
                        break;
                    }
            }
        }

        private Color32 Get_Color(Colour _Colour)
        {
            switch (_Colour)
            {
                case Colour.Black:
                    {
                        return new Color32(0, 0, 0, 255);
                    }
                case Colour.White:
                    {
                        return new Color32(255, 255, 255, 255);
                    }
                case Colour.Silver:
                    {
                        return new Color32(128, 128, 128, 255);
                    }
                case Colour.Team_Primary:
                    {
                        return this.Stat.Team.Colour_Primary;
                    }
                case Colour.Team_Secondary:
                    {
                        return this.Stat.Team.Colour_Secondary;
                    }
                default:
                    {
                        return new Color32(0, 0, 0, 255);
                    }

            }
        }

        public AudioClip PlaySound(AudioClip sound)
        {
            audio.PlayOneShot(sound);
            return sound;
        }

        public void Explode()
        {
            Vector3 V = new Vector3(this.transform.position.x, 0, this.transform.position.z);
            Instantiate(Library.Get_Explosion(), V, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.forward, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.back, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.left, Quaternion.identity);
            Instantiate(Library.Get_Explosion(), V + Vector3.right, Quaternion.identity);
        }
    }
}
