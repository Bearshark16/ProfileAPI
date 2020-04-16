using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileAPI
{
    public class Person
    {
        private string name;
        private int age;
        private string klass;
        private string email;
        private string description;


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
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                age = value;
            }
        }
        public string Klass
        {
            get
            {
                return klass;
            }
            set
            {
                klass = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
    }
}
