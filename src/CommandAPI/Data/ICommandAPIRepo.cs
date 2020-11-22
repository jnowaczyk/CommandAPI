using System.Collections.Generic;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    interface ICommandAPIRepo 
    {
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);

        bool SaveChanges();
    }
}