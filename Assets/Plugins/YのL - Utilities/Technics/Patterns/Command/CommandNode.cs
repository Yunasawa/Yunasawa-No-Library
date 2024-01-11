namespace YNL.Pattern.Command
{
    public class CommandNode
    {
        public string Name;
        public object Data;

        public CommandNode(string name, object data)
        {
            Name = name;
            Data = data;
        }
    }
}