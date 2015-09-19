using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Scrambler.Strategies
{
    public abstract class Strategy
    {
        public Cyphers.Cypher Cypher;
        public Alphabets.Alphabet Alphabet;
        protected bool KeyChange = false;
        public abstract void AddElements(StackPanel parent);
        public abstract void DeleteElements(StackPanel parent);
        public abstract string Encrypt(string text);
        public abstract string Decrypt(string text);
        protected abstract void createNewCypher();

        public Strategy()
        {
            //Assembly assembly = Assembly.GetAssembly(typeof(Cyphers.Caesar));
            //Type[] types = assembly.DefinedTypes.ToArray();
            //foreach (var classType in types)
            //{
            //    if(GetType() == classType)
            //        if (Attribute.IsDefined(classType, typeof(Attributes.Strategy)))
            //        {
            //            var attrValue = Attribute.GetCustomAttribute(classType, typeof(Attributes.Strategy)) as Attributes.Strategy;
            //            if (attrValue.Type != null)
            //            {
            //                Cypher = (Cyphers.Cypher)Activator.CreateInstance(attrValue.Type);

            //            }
            //        }
            //}
        }

    }
}
