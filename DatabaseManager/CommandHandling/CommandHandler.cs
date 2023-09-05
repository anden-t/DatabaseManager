namespace DatabaseManager
{
    /// <summary>
    /// Класс, отвечающий за обработку и исполнение команды
    /// </summary>
    internal class CommandHandler
    {
        private readonly List<Type> _commands;

        public CommandHandler()
        {
            _commands = new List<Type>();
        }

        /// <summary>
        /// Добавляет команду <see cref="Command"/> в список, из которого они могут выполняться
        /// </summary>
        /// <typeparam name="T">Тип команды, должен наследоваться от <see cref="Command"/></typeparam>
        public void AddCommand<T>() where T : Command
        {
            _commands.Add(typeof(T));
        }

        /// <summary>
        /// Выполняет команду
        /// </summary>
        /// <param name="commandWithArgs">Команда, полученная от пользователя (вместе с аргументами)</param>
        /// <exception cref="CommandNotFoundException">Исключение, возникающее, если <see cref="CommandHandler"/> не смог найти подходящую команду</exception>
        public void Execute(string commandWithArgs)
        {
            var args = GetCommandArgs(commandWithArgs);

            if(args.Length > 0)
            {
                var commandName = args[0];
                var command = FindCommandForExecute(commandName);

                if(command != null)
                {
                    command.Execute(args);
                }
                else
                {
                    throw new CommandNotFoundException($"Could not found command with name {commandName}", commandName);
                }
            }
            else
            {
                throw new CommandNotFoundException("Could not found command with empty name", string.Empty);
            }
        }

        /// <summary>
        /// Получает описание всех команд
        /// </summary>
        public string[] GetDescriptions()
        {
            return _commands.Select(x => ((Command?)Activator.CreateInstance(x))?.Description ?? string.Empty).ToArray();
        }

        /// <summary>
        /// Ищет команду по её имени (первый аргумент)
        /// </summary>
        /// <param name="commandName">Имя команды</param>
        /// <returns>Команда, имя которой совпадает с указанной. Если не найдено, возвращается <see cref="null"/></returns>
        private Command? FindCommandForExecute(string commandName)
        {
            Command? command;

            foreach (Type type in _commands)
            {
                foreach (Attribute attribute in type.GetCustomAttributes(true))
                {
                    if (attribute is CommandNameAttribute commandNameAttribute)
                    {
                        if (commandNameAttribute.Name == commandName)
                        {
                            command = (Command?)Activator.CreateInstance(type);

                            return command;
                        }
                    }
                }
            }

            return null;
        }
        
        /// <summary>
        /// Разбивает текст на аргументы для команды
        /// </summary>
        /// <param name="commandWithArgs">Текст, полученный от пользователя</param>
        /// <returns>Возвращает массив строк, каждая из которых - аргумент команды. Первый аргумент - название команды</returns>
        private static string[] GetCommandArgs(string commandWithArgs)
        {
            bool merge = false;

            var charArray = commandWithArgs.ToCharArray();

            for (int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == '"')
                {
                    merge = !merge;
                }

                if (merge && charArray[i] == ' ')
                {
                    charArray[i] = '\0';
                }
            }

            return new string(charArray).Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Replace("\0", " ").Replace("\"", "")).ToArray();
        }
    }
}
