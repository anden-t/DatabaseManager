using DatabaseManager.Models;

namespace DatabaseManager.Commands
{
    /// <summary>
    /// Ищет игры по указанным параметрам.
    /// Формат: <code>search тип_параметра параметр. </code>
    /// Типы параметров: <code>name - название, publisher - издатель, year - год </code>
    /// </summary>
    [CommandName("search")]
    internal class SearchCommand : Command
    {
        public override string Description => "search: Ищет игры по указанным параметрам.\n\tИспользование: search тип_параметра параметр.\n\tТипы параметров: name - название, publisher - издатель, year - год";

        public override void Execute(string[] args)
        {
            // Проверка аргументов на правильность
            if (args.Length != 3 || args.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException("Проверьте правильность написания команды!");
            }

            var type = args[1];
            var param = args[2];

            List<GameModel> games;
            // Поиск игры по указанным параметрам
            using (var db = new DatabaseContext())
            {
                switch (type)
                {
                    case "name":
                        games = db.Games.Where(x => x.Name == param).ToList();
                        break;
                    case "publisher":
                        games = db.Games.Where(x => x.Publisher == param).ToList();
                        break;
                    case "year":
                        games = db.Games.Where(x => x.YearOfPublishing.ToString() == param).ToList();
                        break;
                    default: throw new ArgumentException("Проверьте правильность написания команды!");
                }

                // Вывод результата в консоль
                if(games.Count == 0)
                {
                    Console.WriteLine("Не найдено ни одной игры, подходящих под указанные параметры");
                }
                else
                {
                    Console.WriteLine("Название | Издатель | Год издания");
                    foreach (var game in games)
                    {
                        Console.WriteLine($"{game.Name} | {game.Publisher} | {game.YearOfPublishing}");
                    }
                }
            }
        }
    }
}
