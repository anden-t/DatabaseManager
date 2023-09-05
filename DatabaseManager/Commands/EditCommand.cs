namespace DatabaseManager.Commands
{
    /// <summary>
    /// Изменяет данные об игре. 
    /// Формат: <code>edit название издатель год новое_название новый_издатель новый_год</code>
    /// </summary>
    [CommandName("edit")]
    internal class EditCommand : Command
    {
        public override string Description => "edit: Изменяет данные об игре.\n\tИспользование: edit название издатель год новое_название новый_издатель новый_год";

        public override void Execute(string[] args)
        {
            // Проверка аргументов на правильность
            if (args.Length != 7 || args.Any(x => string.IsNullOrWhiteSpace(x)) || !int.TryParse(args[6], out _))
            {
                throw new ArgumentException("Проверьте правильность написания команды!");
            }

            var oldName = args[1];
            var oldPublisher = args[2];
            var oldYear = args[3];

            var name = args[4];
            var publisher = args[5];
            var year = int.Parse(args[6]);

            using (var db = new DatabaseContext())
            {
                // Поиск указанной игры
                var gameToEdit = db.Games.FirstOrDefault(x => x.Name == oldName && x.Publisher == oldPublisher && x.YearOfPublishing.ToString() == oldYear);

                // Изменение игры
                if(gameToEdit != null)
                {
                    gameToEdit.Name = name;
                    gameToEdit.Publisher = publisher;
                    gameToEdit.YearOfPublishing = year;
                    db.SaveChanges();
                    Console.WriteLine($"Игра {oldName} была изменена.");
                }
                else
                {
                    Console.WriteLine("Не найдена указанная игра.");
                }
            }

        }
    }
}
