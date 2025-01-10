using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace DichotomyLib
{
    /// <summary>
    /// Клас представляє метод дихотомії для знаходження найменшого значення функції з можливістю сериалізації 
    /// </summary>
    /// <typeparam name="TFunction">Параметр типу функції, мінімум якої буде знаходитись</typeparam>
    public class SerializableDichotomy<TFunction> : Dichotomy<TFunction>, ISerializable where TFunction : AFunction, new()
    {
        public SerializableDichotomy() : base() { }

        public SerializableDichotomy(TFunction f) : base(f) { }

        public SerializableDichotomy(string fileName) 
        {
            Deserialize(fileName);
        }

        /// <summary>
        /// Читання с файлу стану об'єкта
        /// </summary>
        /// <param name="fileName">назва файлу</param>
        public void Deserialize(string fileName)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(TFunction));
            using(TextReader textReader = new StreamReader(fileName))
            {
                Function = (deserializer.Deserialize(textReader) as TFunction) ?? new TFunction();
            }
        }

        /// <summary>
        /// Запис у файл стану об'єкта
        /// </summary>
        /// <param name="fileName">назва файлу</param>
        public void Serialize(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TFunction));
            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                using(XmlWriter xmlWriter = XmlWriter.Create(textWriter, new XmlWriterSettings { Indent = true }))
                {
                    serializer.Serialize(xmlWriter, Function);
                }
            }
        }
    }
}
