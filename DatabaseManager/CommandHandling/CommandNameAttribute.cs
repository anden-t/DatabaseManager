namespace DatabaseManager
{
    /// <summary>
    /// Атрибут для указания имени команды
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    internal class CommandNameAttribute : Attribute
    {
        /// <summary>
        /// Имя команды
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name">Имя команды</param>
        public CommandNameAttribute(string name)
        {
            Name = name;
        }
    }
}
