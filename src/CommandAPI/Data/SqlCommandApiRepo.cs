using System.Collections.Generic;
using System.Linq;
using CommandAPI.Models;
using System;

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
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            Context.CommandItems.Add(cmd);
        }

        public void DeleteCommand(Command cmd)
        {
            if (cmd == null)
            {
                throw new ArgumentNullException(nameof(cmd));
            }

            Context.CommandItems.Remove(cmd);
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
            return Context.SaveChanges() > 0 ? true : false;
        }

        public void UpdateCommand(Command cmd)
        {
            // to dont need to do anything here
        }
    }
}