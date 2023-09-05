namespace DatabaseManager.Commands
{
    /// <summary>
    /// Выводит все игры из базы данных. 
    /// Формат: <code>printall</code>
    /// </summary>
    [CommandName("printall")]
    internal class PrintAllCommand : Command
    {
        public override string Description => "printall: Выводит все игры из базы данных.\n\tИспользование: printall";

        public override void Execute(string[] args)
        {
            using (var db = new DatabaseContext())
            {
                // Получение всех игр из базы данных
                var allGames = db.Games.ToList();

                if(allGames.Count == 0)
                {
                    Console.WriteLine("В базе данных нет ни одной игры");
                }
                else
                {
                    Console.WriteLine("Название | Издатель | Год издания");
                    foreach (var game in allGames)
                    {
                        Console.WriteLine($"{game.Name} | {game.Publisher} | {game.YearOfPublishing}");
                    }
                }
            }
        }
    }
}
