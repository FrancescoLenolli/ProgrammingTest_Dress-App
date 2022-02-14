using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIFramework.StateMachine
{
    public class UIStateMachine : MonoBehaviour
    {
        [SerializeField]
        private UIRoot root = null;
        [SerializeField]
        private UIState startingState = null;

        private List<UIState> states = new List<UIState>();
        private bool statesPrepared = false;

        public UIRoot Root { get => root; set => root = value; }
        public UIState CurrentState { get; private set; }

        public Type CurrentType => CurrentState.GetType();
        public Type StartingType => startingState.GetType();

        private void Start()
        {
            FirstStart();
        }

        private void Update()
        {
            if (CurrentState != null)
            {
                CurrentState.UpdateState();
            }
        }

        /// <summary>
        /// Initialise the Machine's States starting values.
        /// </summary>
        public void FirstStart()
        {
            if (!statesPrepared)
            {
                states = GetComponents<UIState>().ToList();
                states.ForEach(state => state.PrepareState(this));
                statesPrepared = true;
            }

            ChangeState(startingState.GetType());
        }

        public void ChangeState(Type stateType)
        {
            if (!statesPrepared)
            {
                Debug.LogWarning($"UI States of {gameObject.name} not initialised.");
                return;
            }

            UIState newState = states.FirstOrDefault(state => stateType == state.GetType());

            if(CurrentState) CurrentState.HideState();
            CurrentState = newState;
            if(CurrentState) CurrentState.ShowState();
        }
    }
}