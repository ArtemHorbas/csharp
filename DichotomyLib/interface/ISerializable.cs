namespace DichotomyLib
{
    public interface ISerializable
    {
        /// <summary>
        /// Здійснює зчитування з документу
        /// </summary>
        /// <param name="fileName">ім'я файлу</param>
        void Deserialize(string fileName);

        /// <summary>
        /// Здійснює запис у документ
        /// </summary>
        /// <param name="fileName">ім'я файлу</param>
        void Serialize(string fileName);
    }
}
