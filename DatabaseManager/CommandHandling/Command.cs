namespace DatabaseManager
{
    /// <summary>
    /// Абстрактный базовый класс для всех команд
    /// </summary>
    internal abstract class Command
    {
        /// <summary>
        /// Описание команды и справка о ней
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// Выполянет команду
        /// </summary>
        /// <param name="args">Аргументы к команде. Первый аргумент - название команды</param>
        public abstract void Execute(string[] args);
    }
}
