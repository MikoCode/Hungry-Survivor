using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

namespace HungrySurvivor.StateMachine
{
    [RequireComponent(typeof(CanvasGroup))]
    public class State : StateMachine
    {

        [Header("State settings")]
        [Space]
        [SerializeField]
        private bool centerStateOnAwake = false;

        [HideInInspector]
        public RectTransform rectTransform;

        [HideInInspector]
        public CanvasGroup canvasGroup;

        [HideInInspector]
        public List<Transition> enterTransitions;

        [HideInInspector]
        public List<Transition> exitTransitions;

        [HideInInspector]
        public StateMachine parentState;
        private UnityEvent<bool> onTransitionsFinished;
        private int transitionsFinished = 0;

        [HideInInspector]
        public bool isActive = false;

        public UnityEvent OnEnterState;
        public UnityEvent OnExitState;

        private void Awake()
        {
            substates = new List<State>();
            enterTransitions = new List<Transition>();
            exitTransitions = new List<Transition>();
            onTransitionsFinished = new UnityEvent<bool>();
            onTransitionsFinished.AddListener(OnTransitionsFinished);
            rectTransform = GetComponent<RectTransform>();

            if (centerStateOnAwake)
            {
                rectTransform.anchoredPosition = Vector2.zero;
            }
        }

        protected override IEnumerator Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

            parentState = transform.parent.GetComponent<StateMachine>();

            yield return new WaitForEndOfFrame();

            if (parentState != null)
            {
                parentState.AddSubstate(this);
            }
            else
            {
                Debug.LogWarning("State " + gameObject.name + " is not a child of state or state machine!");
            }

            StartCoroutine(base.Start());
        }

        [Button]
        public void EnterState()
        {
            SetState(true);
            OnEnterState?.Invoke();
        }

        public void EnterStateWithParents()
        {
            parentState.GetComponent<State>()?.EnterStateWithParents();
            SetState(true);
        }

        [Button]
        public void ExitState()
        {
            SetState(false);
            OnExitState?.Invoke();
        }

        public void ToggleState()
        {
            bool setActive = parentState.CurrentSubstate != this;

            SetState(setActive);

            if (!setActive)
            {
                parentState.CurrentSubstate = null;
            }
        }

        public void ToggleState(bool enter)
        {
            SetState(enter);
        }

        private void SetState(bool enter)
        {
            if (parentState == null)
            {
                return;
            }

            transitionsFinished = 0;

            if (isActive == enter)
            {
                return;
            }

            if (enter)
            {
                parentState.CurrentSubstate = this;
            }

            isActive = enter;

            if (enter)
            {
                foreach (Transition transition in enterTransitions)
                {
                    transition.RunTransition(enter, onTransitionsFinished);
                }
            }
            else
            {
                foreach (Transition transition in exitTransitions)
                {
                    transition.RunTransition(enter, onTransitionsFinished);
                }
            }
        }

        public void SetupTransitions()
        {
            foreach (Transition transition in enterTransitions)
            {
                StartCoroutine(transition.SetupTransition());
            }

            foreach (Transition transition in exitTransitions)
            {
                StartCoroutine(transition.SetupTransition());
            }
        }

        private void OnTransitionsFinished(bool forward)
        {
            transitionsFinished++;

            if (transitionsFinished == enterTransitions.Count || transitionsFinished == exitTransitions.Count)
            {
                canvasGroup.alpha = forward ? 1 : 0;
                canvasGroup.interactable = forward;
                canvasGroup.blocksRaycasts = forward;
            }
        }
    }
}