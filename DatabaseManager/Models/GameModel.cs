namespace DatabaseManager.Models
{
    /// <summary>
    /// Класс, представляющий информацию о игре
    /// </summary>
    internal class GameModel
    {
        /// <summary>
        /// Идентификатор в базе данных
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Издатель
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// Год издания
        /// </summary>
        public int YearOfPublishing { get; set; }
    }
}
