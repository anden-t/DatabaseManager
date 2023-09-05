using DatabaseManager.Models;

namespace DatabaseManager.Commands
{
    /// <summary>
    /// Команда для добавления игры в базу данных.
    /// Формат: <code>add название издатель год_издания</code>
    /// </summary>
    [CommandName("add")]
    internal class AddCommand : Command
    {
        public override string Description => "add: Добавляет игру в базу данных.\n\tИспользование: add название издатель год_издания";

        public override void Execute(string[] args)
        {
            // Проверка аргументов на правильность
            if (args.Length != 4 || args.Any(x => string.IsNullOrWhiteSpace(x)) || !int.TryParse(args[3], out _))
            {
                throw new ArgumentException("Проверьте правильность написания команды!");
            }
            
            //Добавление игры в базу данных
            var name = args[1];
            var publisher = args[2];
            var yearOfPublising = int.Parse(args[3]);

            var game = new GameModel()
            {
                Name = name,
                Publisher = publisher,
                YearOfPublishing = yearOfPublising
            };

            using (var db = new DatabaseContext())
            {
                db.Add(game);
                db.SaveChanges();
            }

            Console.WriteLine($"Игра {game.Name} успешно добавлена в базу данных");
        }
    }
}
