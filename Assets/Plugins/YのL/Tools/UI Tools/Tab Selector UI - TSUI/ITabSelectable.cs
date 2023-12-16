namespace YNL.Tools.UI
{
    public interface ITabSelectable
    {
        void Selected();
        void Deselected();

        void SelectingEvent();
    }
}