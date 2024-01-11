namespace YNL.Pattern.Command
{
    public class Command
    {
        public object Data;

        public virtual void Execute() { }
        public virtual void Undo() { }
    }
}