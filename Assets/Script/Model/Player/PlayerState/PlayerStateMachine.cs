using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class PlayerStateMachine
    {
        public PlayerState currentState { get; private set; }

        public void Initialize(PlayerState _startState)
        {
            currentState = _startState;
            currentState.Enter();
        }

        public void ChangeState(PlayerState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}
