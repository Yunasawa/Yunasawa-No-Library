using System;
using System.Collections.Generic;
using UnityEngine;
using YNL.Extension.Method;
using YNL.Pattern.Singleton;

namespace YNL.Pattern.Command
{
    public class CommandManager : Singleton<CommandManager>
    {
        private readonly Dictionary<string, Type> commandDictionary = new();
        public int Amount => commandDictionary.Count;

        /// <summary>
        /// Register a new command.
        /// </summary>
        public void Register(string commandName, Type command)
        {
            if (!MType.IsSameOrSubtype(typeof(Command), command))
            {
                Debug.Log("Objectr is not Command type.");
                return;
            }
            if (commandDictionary.ContainsKey(commandName))
            {
                MDebug.Notification($"Command [{commandName}] is already in system.");
                return;
            }
            commandDictionary[commandName] = command;
        }

        /// <summary>
        /// Unregister a command.
        /// </summary>
        public void Unregister(string commandName)
        {
            commandDictionary.Remove(commandName);
        }

        /// <summary>
        /// Execute command
        /// </summary>
        public CommandNode Execute(string name, object data = null)
        {
            Command command = (Command)Activator.CreateInstance(commandDictionary[name]);
            command.Data = data;
            command.Execute();

            return new(name, data);
        }

        /// <summary>
        /// Undo command
        /// </summary>
        public CommandNode Undo(string name, object data = null)
        {
            Command command = (Command)Activator.CreateInstance(commandDictionary[name]);
            command.Data = data;
            command.Undo();

            return new(name, data);
        }
    }
}