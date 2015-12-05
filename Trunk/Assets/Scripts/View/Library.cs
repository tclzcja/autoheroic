using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cub.View
{
    public static class Library
    {
        private static bool Trigger = true;

        private static GameObject Prefab_Cube { get; set; }
        private static GameObject Prefab_Bullet { get; set; }
        private static GameObject Prefab_Character { get; set; }
        private static GameObject Prefab_Rocket { get; set; }
        private static GameObject Prefab_Explosion { get; set; }
        private static GameObject Prefab_Healer { get; set; }
        private static GameObject Prefab_Indicator { get; set; }
        private static GameObject Prefab_Warhead { get; set; }

        private static Dictionary<Part_Head, List<Cub.View.Cubon>> Dictionary_Part_Head { get; set; }
        private static Dictionary<Part_Body, List<Cub.View.Cubon>> Dictionary_Part_Body { get; set; }
        private static Dictionary<Part_Arms, List<Cub.View.Cubon>> Dictionary_Part_Arms_Left { get; set; }
        private static Dictionary<Part_Arms, List<Cub.View.Cubon>> Dictionary_Part_Arms_Right { get; set; }
        private static Dictionary<Part_Legs, List<Cub.View.Cubon>> Dictionary_Part_Legs_Left { get; set; }
        private static Dictionary<Part_Legs, List<Cub.View.Cubon>> Dictionary_Part_Legs_Right { get; set; }

        private static Dictionary<int, Cubon> Dictionary_Damage { get; set; }
        private static Dictionary<char, Cubon> Dictionary_Alphabet { get; set; }

        private static Material Dictionary_Material { get; set; }
        private static PhysicMaterial Dictionary_Physic_Material { get; set; }

        private static Dictionary<Cub.Event, Cub.View.Event.Base> Dictionary_Event { get; set; }
        private static Dictionary<Cub.Sound, AudioClip> Dictionary_Sound { get; set; }
        private static Dictionary<Cub.Terrain, GameObject> Dictionary_Terrain = new Dictionary<Terrain, GameObject>();

        /*
        public static void GetPrefabs(GameObject cube, Material cubeMat, GameObject character, GameObject bullet, GameObject rocket,
            GameObject explosion, GameObject healer, GameObject desert, GameObject grass, AudioClip scream, Material cubMat,
            AudioClip explosionSound, AudioClip laserShotSound, AudioClip footstepSound, AudioClip snipeSound,
            AudioClip explosion2Sound, AudioClip hurtSound)
        {
            Prefab_Cube = cube;
            Prefab_Cube.renderer.material = cubeMat;

            Prefab_Character = character;
            Prefab_Bullet = bullet;
            Prefab_Rocket = rocket;
            Prefab_Explosion = explosion;
            Prefab_Healer = healer;

            Dictionary_Terrain = new Dictionary<Terrain, GameObject>();
            Dictionary_Terrain.Add(Cub.Terrain.Desert, desert);
            Dictionary_Terrain.Add(Cub.Terrain.Grass, grass);

            Dictionary_Sound = new Dictionary<string, AudioClip>();
            Dictionary_Sound.Add("Scream", scream);

            Dictionary_Sound.Add("Explosion", explosionSound);
            Dictionary_Sound.Add("Hurt", hurtSound);
            Dictionary_Sound.Add("Explosion2", explosion2Sound);
            Dictionary_Sound.Add("Laser", laserShotSound);
            Dictionary_Sound.Add("Footstep", footstepSound);
            Dictionary_Sound.Add("Snipe", snipeSound);

            Dictionary_Material = cubMat;
        }
         * */

        public static void Initialization()
        {
            if (Trigger)
            {
                Prefab_Cube = Resources.Load<GameObject>("Prefabs/Cube");
                Prefab_Bullet = Resources.Load<GameObject>("Prefabs/Bullet");
                Prefab_Character = Resources.Load<GameObject>("Prefabs/Character");
                Prefab_Rocket = Resources.Load<GameObject>("Prefabs/Rocket");
                Prefab_Explosion = Resources.Load<GameObject>("Prefabs/Explosion");
                Prefab_Healer = Resources.Load<GameObject>("Prefabs/Healer");
                Prefab_Indicator = Resources.Load<GameObject>("Prefabs/Indicator");
                Prefab_Warhead = Resources.Load<GameObject>("Prefabs/Warhead");

                Dictionary_Material = Resources.Load<Material>("Materials/Cube");
                Dictionary_Physic_Material = Resources.Load<PhysicMaterial>("Physic Materials/Cube");

                Dictionary_Event = new Dictionary<Cub.Event, Event.Base>();
                Dictionary_Event[Cub.Event.Attack_Heal] = new Cub.View.Event.Attack_Heal();
                Dictionary_Event[Cub.Event.Attack_Heal_Harm] = new Cub.View.Event.Attack_Heal_Harm();
                Dictionary_Event[Cub.Event.Attack_Melee] = new Cub.View.Event.Attack_Melee();
                Dictionary_Event[Cub.Event.Attack_Range] = new Cub.View.Event.Attack_Range();
                Dictionary_Event[Cub.Event.Attack_Rocket] = new Cub.View.Event.Attack_Rocket();
                Dictionary_Event[Cub.Event.Attack_Snipe] = new Cub.View.Event.Attack_Snipe();
                Dictionary_Event[Cub.Event.Be_Attacked] = new Cub.View.Event.Be_Attacked();
                Dictionary_Event[Cub.Event.Be_Healed] = new Cub.View.Event.Be_Healed();
                Dictionary_Event[Cub.Event.Die] = new Cub.View.Event.Die();
                Dictionary_Event[Cub.Event.Move] = new Cub.View.Event.Move();
                Dictionary_Event[Cub.Event.Win] = new Cub.View.Event.Win();
                Dictionary_Event[Cub.Event.Time_Out] = new Cub.View.Event.Time_Out();
                Dictionary_Event[Cub.Event.Blow_Up] = new Cub.View.Event.Blow_Up();

                Dictionary_Part_Head = new Dictionary<Part_Head, List<Cubon>>();
                Dictionary_Part_Head.Add(Part_Head.Soldier, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Solider.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Hunter, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Hunter.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Idiot, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Idiot.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Head.Add(Part_Head.Protector, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Head_Protector.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Body = new Dictionary<Part_Body, List<Cubon>>();
                Dictionary_Part_Body.Add(Part_Body.Light, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Light.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Medium, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Medium.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Heavy, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Heavy.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Bomber, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Bomber.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Body.Add(Part_Body.Healer, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Body_Healer.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Arms_Left = new Dictionary<Part_Arms, List<Cubon>>();
                Dictionary_Part_Arms_Left.Add(Part_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Sword, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Sword.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Axe, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Axe.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Pistol, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Pistol.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Sniper_Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Sniper_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.RPG, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_RPG.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Left.Add(Part_Arms.Heal_Gun, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Left_Heal_Gun.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Arms_Right = new Dictionary<Part_Arms, List<Cubon>>();
                Dictionary_Part_Arms_Right.Add(Part_Arms.Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Sword, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Sword.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Axe, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Axe.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Pistol, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Pistol.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Sniper_Rifle, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Sniper_Rifle.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.RPG, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_RPG.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Arms_Right.Add(Part_Arms.Heal_Gun, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Arms_Right_Heal_Gun.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Legs_Left = new Dictionary<Part_Legs, List<Cubon>>();
                Dictionary_Part_Legs_Left.Add(Part_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Hover.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Left.Add(Part_Legs.Humanoid, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Humanoid.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Left.Add(Part_Legs.Tread, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Left_Tread.xml") as List<Cub.View.Cubon>);

                Dictionary_Part_Legs_Right = new Dictionary<Part_Legs, List<Cubon>>();
                Dictionary_Part_Legs_Right.Add(Part_Legs.Hover, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Hover.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Right.Add(Part_Legs.Humanoid, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Humanoid.xml") as List<Cub.View.Cubon>);
                Dictionary_Part_Legs_Right.Add(Part_Legs.Tread, Cub.Tool.Xml.Deserialize(typeof(List<Cub.View.Cubon>), "Data/View_Part_Legs_Right_Tread.xml") as List<Cub.View.Cubon>);

                Dictionary_Damage = new Dictionary<int, Cubon>();
                Dictionary_Damage.Add(1, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_1.xml") as Cubon);
                Dictionary_Damage.Add(2, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_2.xml") as Cubon);
                Dictionary_Damage.Add(3, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_3.xml") as Cubon);
                Dictionary_Damage.Add(4, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_4.xml") as Cubon);
                Dictionary_Damage.Add(5, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_5.xml") as Cubon);
                Dictionary_Damage.Add(6, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_6.xml") as Cubon);
                Dictionary_Damage.Add(7, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_7.xml") as Cubon);
                Dictionary_Damage.Add(8, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_8.xml") as Cubon);
                Dictionary_Damage.Add(9, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_9.xml") as Cubon);
                Dictionary_Damage.Add(99, Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Damage_99.xml") as Cubon);

                Dictionary_Alphabet = new Dictionary<char, Cubon>();
                Dictionary_Alphabet.Add(' ', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_.xml") as Cubon);
                Dictionary_Alphabet.Add('A', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_A.xml") as Cubon);
                Dictionary_Alphabet.Add('B', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_B.xml") as Cubon);
                Dictionary_Alphabet.Add('C', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_C.xml") as Cubon);
                Dictionary_Alphabet.Add('D', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_D.xml") as Cubon);
                Dictionary_Alphabet.Add('E', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_E.xml") as Cubon);
                Dictionary_Alphabet.Add('F', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_F.xml") as Cubon);
                Dictionary_Alphabet.Add('G', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_G.xml") as Cubon);
                Dictionary_Alphabet.Add('H', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_H.xml") as Cubon);
                Dictionary_Alphabet.Add('I', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_I.xml") as Cubon);
                Dictionary_Alphabet.Add('J', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_J.xml") as Cubon);
                Dictionary_Alphabet.Add('K', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_K.xml") as Cubon);
                Dictionary_Alphabet.Add('L', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_L.xml") as Cubon);
                Dictionary_Alphabet.Add('M', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_M.xml") as Cubon);
                Dictionary_Alphabet.Add('N', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_N.xml") as Cubon);
                Dictionary_Alphabet.Add('O', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_O.xml") as Cubon);
                Dictionary_Alphabet.Add('P', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_P.xml") as Cubon);
                Dictionary_Alphabet.Add('Q', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_Q.xml") as Cubon);
                Dictionary_Alphabet.Add('R', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_R.xml") as Cubon);
                Dictionary_Alphabet.Add('S', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_S.xml") as Cubon);
                Dictionary_Alphabet.Add('T', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_T.xml") as Cubon);
                Dictionary_Alphabet.Add('U', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_U.xml") as Cubon);
                Dictionary_Alphabet.Add('V', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_V.xml") as Cubon);
                Dictionary_Alphabet.Add('W', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_W.xml") as Cubon);
                Dictionary_Alphabet.Add('X', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_X.xml") as Cubon);
                Dictionary_Alphabet.Add('Y', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_Y.xml") as Cubon);
                Dictionary_Alphabet.Add('Z', Cub.Tool.Xml.Deserialize(typeof(Cubon), "Data/View_Alphabet_Z.xml") as Cubon);

                Dictionary_Sound = new Dictionary<Cub.Sound, AudioClip>();
                Dictionary_Sound.Add(Sound.Blade, Resources.Load<AudioClip>("Sounds/Blade"));
                Dictionary_Sound.Add(Sound.Button_Cancel, Resources.Load<AudioClip>("Sounds/Button_Cancel"));
                Dictionary_Sound.Add(Sound.Button_Confirm, Resources.Load<AudioClip>("Sounds/Button_Confirm"));
                Dictionary_Sound.Add(Sound.Button_Start, Resources.Load<AudioClip>("Sounds/Button_Start"));
                Dictionary_Sound.Add(Sound.Die, Resources.Load<AudioClip>("Sounds/Die"));
                Dictionary_Sound.Add(Sound.Drip, Resources.Load<AudioClip>("Sounds/Drip"));
                Dictionary_Sound.Add(Sound.Explosion, Resources.Load<AudioClip>("Sounds/Explosion"));
                Dictionary_Sound.Add(Sound.Gun_Crit, Resources.Load<AudioClip>("Sounds/Gun_Crit"));
                Dictionary_Sound.Add(Sound.Gun_Hit, Resources.Load<AudioClip>("Sounds/Gun_Hit"));
                Dictionary_Sound.Add(Sound.Gun_Miss, Resources.Load<AudioClip>("Sounds/Gun_Miss"));
                Dictionary_Sound.Add(Sound.Heal, Resources.Load<AudioClip>("Sounds/Heal"));
                Dictionary_Sound.Add(Sound.Move_Hover, Resources.Load<AudioClip>("Sounds/Move_Hover"));
                Dictionary_Sound.Add(Sound.Move_Humanoid, Resources.Load<AudioClip>("Sounds/Move_Humanoid"));
                Dictionary_Sound.Add(Sound.Move_Tread, Resources.Load<AudioClip>("Sounds/Move_Tread"));
                Dictionary_Sound.Add(Sound.Rocket, Resources.Load<AudioClip>("Sounds/Rocket"));
                Dictionary_Sound.Add(Sound.Scream_Crit, Resources.Load<AudioClip>("Sounds/Scream_Crit"));
                Dictionary_Sound.Add(Sound.Scream_Hit, Resources.Load<AudioClip>("Sounds/Scream_Hit"));
                Dictionary_Sound.Add(Sound.Scream_Miss, Resources.Load<AudioClip>("Sounds/Scream_Miss"));
                Dictionary_Sound.Add(Sound.Snipe, Resources.Load<AudioClip>("Sounds/Snipe"));

                Dictionary_Terrain = new Dictionary<Terrain, GameObject>();
                Dictionary_Terrain.Add(Cub.Terrain.Desert, Resources.Load<GameObject>("Prefabs/Terrains/Desert"));
                Dictionary_Terrain.Add(Cub.Terrain.Grass, Resources.Load<GameObject>("Prefabs/Terrains/Grass"));

                Trigger = false;
            }
        }

        public static void Unlock()
        {
            Trigger = true;
        }

        public static Cub.View.Event.Base Get_Event(Cub.Event _E)
        {
            if (!Dictionary_Event.ContainsKey(_E))
                Debug.Log(_E.ToString());
            return Dictionary_Event[_E];
        }

        public static GameObject Get_Warhead()
        {
            return Prefab_Warhead;
        }

        public static GameObject Get_Indicator()
        {
            return Prefab_Indicator;
        }

        public static GameObject Get_Cube()
        {
            return Prefab_Cube;
        }

        public static GameObject Get_Character()
        {
            return Prefab_Character;
        }

        public static GameObject Get_Terrain(Cub.Terrain t)
        {
            if (Dictionary_Terrain.ContainsKey(t))
                return Dictionary_Terrain[t];
            return null;
        }

        public static GameObject Get_Bullet()
        {
            return Prefab_Bullet;
        }

        public static GameObject Get_Rocket()
        {
            return Prefab_Rocket;
        }

        public static GameObject Get_Explosion()
        {
            return Prefab_Explosion;
        }

        public static GameObject Get_Healer()
        {
            return Prefab_Healer;
        }

        public static AudioClip Get_Sound(Cub.Sound _Sound)
        {
            if (Dictionary_Sound.ContainsKey(_Sound))
                return Dictionary_Sound[_Sound];
            return null;
        }

        public static List<Cubon> Get_Part_Head(Part_Head _Head)
        {
            if (Dictionary_Part_Head.ContainsKey(_Head))
            {
                return Dictionary_Part_Head[_Head];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Body(Part_Body _Body)
        {
            if (Dictionary_Part_Body.ContainsKey(_Body))
            {
                return Dictionary_Part_Body[_Body];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Arms_Left(Part_Arms _Arms)
        {
            if (Dictionary_Part_Arms_Left.ContainsKey(_Arms))
            {
                return Dictionary_Part_Arms_Left[_Arms];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Arms_Right(Part_Arms _Arms)
        {
            if (Dictionary_Part_Arms_Right.ContainsKey(_Arms))
            {
                return Dictionary_Part_Arms_Right[_Arms];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Legs_Left(Part_Legs _Legs)
        {
            if (Dictionary_Part_Legs_Left.ContainsKey(_Legs))
            {
                return Dictionary_Part_Legs_Left[_Legs];
            }
            return null;
        }

        public static List<Cubon> Get_Part_Legs_Right(Part_Legs _Legs)
        {
            if (Dictionary_Part_Legs_Right.ContainsKey(_Legs))
            {
                return Dictionary_Part_Legs_Right[_Legs];
            }
            return null;
        }

        public static Material Get_Material()
        {
            return Dictionary_Material;
        }

        public static PhysicMaterial Get_Physic_Material()
        {
            return Dictionary_Physic_Material;
        }

        public static Cubon Get_Damage(int _Damage)
        {
            return Dictionary_Damage[_Damage];
        }

        public static Cubon Get_Alphabet(char _Char)
        {
            return Dictionary_Alphabet[_Char];
        }
    }
}
