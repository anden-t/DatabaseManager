using DatabaseManager.Commands;

namespace DatabaseManager
{
    internal static class Program
    {
        /// <summary>
        /// Точка входа программы
        /// </summary>
        private static void Main()
        {
            // Создание базы данных
            using (var db = new DatabaseContext())
            {
                db.Database.EnsureCreated();
            }

            var commandHandler = new CommandHandler();

            // Добавление команд
            commandHandler.AddCommand<AddCommand>();
            commandHandler.AddCommand<EditCommand>();
            commandHandler.AddCommand<RemoveCommand>();
            commandHandler.AddCommand<SearchCommand>();
            commandHandler.AddCommand<PrintAllCommand>();

            // Вывод справки о командах
            foreach (var description in commandHandler.GetDescriptions())
            {
                Console.WriteLine(description);
            }

            Console.WriteLine(); // Декоративный отступ

            // Главный цикл, в котором происходит выполнение команд
            while (true)
            {
                var commandWithArgs = Console.ReadLine();
                
                Console.WriteLine(); // Декоративный отступ

                if (!string.IsNullOrWhiteSpace(commandWithArgs))
                {
                    try
                    {
                        commandHandler.Execute(commandWithArgs);
                        Console.WriteLine(); // Декоративный отступ
                    }
                    catch (CommandNotFoundException ex)
                    {
                        if (!string.IsNullOrWhiteSpace(ex.Message))
                        {
                            Console.WriteLine($"Не удалось найти команду с именем {ex.CommandName}");
                        }
                        else
                        {
                            Console.WriteLine($"Не удалось найти команду с пустым именем");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Возникла ошибка: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Введите команду");
                }
            }

        }
    }


}

