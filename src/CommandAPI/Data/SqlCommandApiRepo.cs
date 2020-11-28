using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;

namespace CommandAPI.Data
{
    public class SqlCommandApiRepo : ICommandAPIRepo
    {
        public CommandContext Context { get; }
        public SqlCommandApiRepo(CommandContext ctx)
        {
            Context = ctx;
        }

      

        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return Context.CommandItems.ToList();
        }

        public Command GetCommandById(int id)
        {
            return Context.CommandItems
                .FirstOrDefault( commandItem => commandItem.Id == id);
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}