using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI
{
    public class Pokemon
    {
        private string name;
        private int hp;
        private int attack;
        private int defence;
        private int speed;


        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = Program.NameToUpper(value);
            }
        }
        public int Hp
        {
            get
            {
                return hp;
            }
            set
            {
                hp = value;
            }
        }
        public int Attack
        {
            get
            {
                return attack;
            }
            set
            {
                attack = value;
            }
        }
        public int Defence
        {
            get
            {
                return defence;
            }
            set
            {
                defence = value;
            }
        }
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }
    }
}
