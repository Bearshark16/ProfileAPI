using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProfileAPI.Controllers
{
    public abstract class ProfileControllerBase : ControllerBase
    {
        protected static IHostingEnvironment env;

        public static string pokePath;
        public static string persPath;
        public static string userPath;

        public static string imagePath;
        public static string imageName;

        protected XmlSerializer pokemonSerializer = new XmlSerializer(typeof(Pokemon));
        protected XmlSerializer personSerializer = new XmlSerializer(typeof(Person));
        protected XmlSerializer accountSerializer = new XmlSerializer(typeof(UserProfile));

        protected void SerializePokemon(Pokemon pokemon)
        {
            if (!Directory.Exists(pokePath + pokemon.Name.ToLower()))
            {
                Directory.CreateDirectory(pokePath + pokemon.Name.ToLower());
                imagePath = pokePath + pokemon.Name.ToLower() + @"\";
                imageName = pokemon.Name.ToLower();
                using (FileStream fileStream = System.IO.File.Open(pokePath + pokemon.Name.ToLower() + @"\" + pokemon.Name.ToLower() + ".txt", FileMode.OpenOrCreate))
                {
                    pokemonSerializer.Serialize(fileStream, pokemon);
                }
            }
            else
            {
                return;
            }
        }
        protected void SerializePerson(Person person)
        {
            if (!Directory.Exists(persPath + person.Name.ToLower()))
            {
                Directory.CreateDirectory(persPath + person.Name.ToLower());
                imagePath = persPath + person.Name.ToLower() + @"\";
                imageName = person.Name.ToLower();
                using (FileStream fileStream = System.IO.File.Open(persPath + person.Name.ToLower() + @"\" + person.Name.ToLower() + ".txt", FileMode.OpenOrCreate))
                {
                    personSerializer.Serialize(fileStream, person);
                }
            }
            else
            {
                return;
            }
        }
        protected void SerializeUser(UserProfile user)
        {
            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
                
            }
            using (FileStream fileStream = System.IO.File.Open(userPath + user.UserName + ".txt", FileMode.OpenOrCreate))
            {
                accountSerializer.Serialize(fileStream, user);
            }
        }
    }
}