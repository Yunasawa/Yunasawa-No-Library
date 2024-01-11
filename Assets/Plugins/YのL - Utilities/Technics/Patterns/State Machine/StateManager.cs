using UnityEngine;
using YNL.Pattern.Singleton;

namespace YNL.Pattern.StateMachine
{
    public abstract class StateManager : Singleton<StateManager>
    {
        [SerializeField] private IState _currentState = new SampleState();

        protected virtual void Start()
        {
            SetState(new SampleState());
        }

        public virtual void Update()
        {
            _currentState.Update();
        }

        public void SetState(IState current)
        {
            _currentState.Exit();
            _currentState = current;
            _currentState.Enter();
        }
    }
}