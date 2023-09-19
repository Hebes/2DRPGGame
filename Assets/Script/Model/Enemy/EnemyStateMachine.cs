using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGGame
{
    public class EnemyStateMachine
    {
        public EnemyState currentState { get; private set; }

        public void Initialize(EnemyState _startState)
        {
            currentState = _startState;
            currentState.Enter();
        }

        public void ChangeState(EnemyState _newState)
        {
            currentState.Exit();
            currentState = _newState;
            currentState.Enter();
        }
    }
}
