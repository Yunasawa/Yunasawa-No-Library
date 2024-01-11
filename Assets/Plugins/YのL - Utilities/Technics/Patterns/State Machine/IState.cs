namespace YNL.Pattern.StateMachine
{
    /// <summary>
    /// Interface used to define a state for State Machine Pattern.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Called when enter the state.
        /// </summary>
        void Enter();

        /// <summary>
        /// Called when the state need to be updated every frame.
        /// </summary>
        void Update();

        /// <summary>
        /// Called when exit the state.
        /// </summary>
        void Exit();
    }
}