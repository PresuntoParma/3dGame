using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
    

        public Dictionary<T, StateBase> dictionaryState;

        private StateBase currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get { return currentState; }
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            //dictionaryState = new Dictionary<States, StateBase>();
            dictionaryState.Add(typeEnum, state);


            //Invoke(nameof(StartGame), timeToStartGame);
        }


        public void SwitchState(T state)
        {
            if (currentState != null) currentState.OnStateExit();

            currentState = dictionaryState[state];

            currentState.OnStateEnter();
        }

        public void Update()
        {
            if (currentState != null) currentState.OnStateStay();
        }
    }

}
