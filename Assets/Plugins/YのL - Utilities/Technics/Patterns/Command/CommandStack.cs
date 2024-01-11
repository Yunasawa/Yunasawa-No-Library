using System.Collections.Generic;

namespace YNL.Pattern.Command
{
    public class CommandStack
    {
        protected Stack<CommandNode> _commandStack = new();

        protected virtual void ExecuteStackable(string name, object data)
        {
            _commandStack.Push(CommandManager.Instance.Execute(name, data));
        }

        protected virtual void UndoStackable()
        {
            CommandNode node = _commandStack.Pop();
            CommandManager.Instance.Undo(node.Name, node.Data);
        }
    }
}