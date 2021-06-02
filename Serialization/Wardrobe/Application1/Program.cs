using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
//Русаков Иван 194
namespace Application
{
    class Program
    {
        static void XmlSerialize(string path, RusakovWardrobe wardrobe)
        {
            // передаем в конструктор тип класса
            XmlSerializer formatter = new XmlSerializer(typeof(RusakovWardrobe));

            // получаем поток, куда будем записывать сериализованный объект
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, wardrobe);
                Console.WriteLine("Xml Data has been saved to file");
            }
        }

        static void Main(string[] args)
        {
            // Десериализация Json.
            string json;
            using (StreamReader sr = new StreamReader("46 Русаков Иван Николаевич (Rusakov).json"))
            {
                json = sr.ReadLine();
            }
            RusakovWardrobe wardrobe = JsonConvert.DeserializeObject<RusakovWardrobe>(json);

            // Linq Запрос.
            var item = wardrobe.Items.OrderBy(x => x.Name.Length).ThenByDescending(x => x.Value);
            foreach (var ite in item)
            {
                Console.WriteLine(ite.Name + " " + ite.Value);
            }

            // XML Сериализация
            XmlSerialize("Rusakov.xml", wardrobe);

            Console.ReadKey();
        }
    }
}
