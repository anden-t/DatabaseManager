using DatabaseManager.Models;

namespace DatabaseManager.Commands
{
    /// <summary>
    /// Удаляет игру с указанными параметрами из базы данных. 
    /// Формат: <code>remove название издатель год</code>
    /// </summary>
    [CommandName("remove")]
    internal class RemoveCommand : Command
    {
        public override string Description => "remove: Удаляет игру с указанными параметрами из базы данных.\n\tИспользование: remove название издатель год";

        public override void Execute(string[] args)
        {
            // Проверка аргументов на правильность
            if (args.Length != 4 || args.Any(x => string.IsNullOrWhiteSpace(x)))
            {
                throw new ArgumentException("Проверьте правильность написания команды!");
            }

            var name = args[1];
            var publisher = args[2];
            var year = args[3];

            GameModel? toRemove;
            // Поиск игры по указанным параметрам
            using (var db = new DatabaseContext())
            {
                toRemove = db.Games.FirstOrDefault(x => x.Name == name && x.Publisher == publisher && x.YearOfPublishing.ToString() == year);

                // Удаление игры
                if (toRemove != null)
                {
                    db.Games.Remove(toRemove);
                    db.SaveChanges();
                    Console.WriteLine($"Игра {toRemove.Name} успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Игра с заданными параметрами не найдена.");
                }
            }
        }
    }
}
