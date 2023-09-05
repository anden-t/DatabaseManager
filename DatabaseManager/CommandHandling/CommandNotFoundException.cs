namespace DatabaseManager
{
    /// <summary>
    /// Исключение, сообщающее о том, что указанную команду не удалось найти
    /// </summary>
    internal class CommandNotFoundException : Exception
    {
        /// <summary>
        /// Указанное имя, по которому не удалось найти команду
        /// </summary>
        public string CommandName { get; private set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="commandName">Указанное имя, по которому не удалось найти команду</param>
        public CommandNotFoundException(string? message, string commandName) : base(message)
        {
            CommandName = commandName;
        }
    }
}
