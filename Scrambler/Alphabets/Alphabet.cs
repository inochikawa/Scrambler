using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Scrambler.Alphabets
{
    [XmlRoot("Alphabet")]
    public class Alphabet
    {
        public static List<Alphabet> Alphabets = new List<Alphabet>();
        [XmlElement]
        public int Quantity { get; set; }
        [XmlElement]
        public string Letters { get; set; }
        [XmlElement]
        public string Name { get; set; }
        [XmlElement]
        public bool Chosen { get; set; }
        public Alphabet(string name, string letters)
        {
            Name = name;
            Letters = letters;
            Quantity = Letters.Length;
            Chosen = false;
            Alphabets.Add(this);
        }

        public Alphabet()
        {

        }

        public char this[int index]
        {
            get { return Letters[index]; }
            private set { }
        }

        public static void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Alphabet>));
            using (TextWriter writer = new StreamWriter(@"..\..\Alphabets\Alphabets.xml"))
            {
                serializer.Serialize(writer, Alphabets); 
            }  
        }

        public static void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Alphabet>));
            using (FileStream ReadFileStream = new FileStream(@"..\..\Alphabets\Alphabets.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Alphabets = (List<Alphabet>)serializer.Deserialize(ReadFileStream);
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
