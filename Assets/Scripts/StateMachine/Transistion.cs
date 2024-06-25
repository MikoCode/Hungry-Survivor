using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace HungrySurvivor.StateMachine
{
    public class Transition : MonoBehaviour
    {
        public bool onEnter = true;

        [SerializeField]
        protected float duration = 0.15f;

        [SerializeField]
        protected float delay = 0.0f;

        [SerializeField]
        protected AnimationCurve transitionCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [SerializeField]
        protected bool mirrorEffect = false;

        [SerializeField]
        protected bool lockInput = true;

        [Space]
        public UnityEvent onTransitionStart;

        public UnityEvent onTransitionFinish;


        private State state;
        protected RectTransform stateRectTransform;
        protected CanvasGroup stateCanvasGroup;
        protected GraphicRaycaster raycaster;
        protected AnimationCurve mirroredCurve;

        private bool isValid;

        protected virtual void Start()
        {
            state = transform.parent.GetComponent<State>();
            raycaster = GetComponentInParent<GraphicRaycaster>();

            SetupMirroredCurve();

            if (state == null)
            {
                Debug.LogError("Transistion must be a child of object");
                isValid = false;
                return;
            }

            stateRectTransform = state.GetComponent<RectTransform>();
            stateCanvasGroup = state.GetComponent<CanvasGroup>();

            if (onEnter)
            {
                state.enterTransitions.Add(this);
            }
            else
            {
                state.exitTransitions.Add(this);
            }

            isValid = true;

            //StartCoroutine(SetupTransition());
        }

        public virtual IEnumerator SetupTransition()
        {
            yield return null;
        }

        public void RunTransition(bool forward, UnityEvent<bool> onTransitionFinished)
        {
            if (!isValid)
            {
                return;
            }

            if (raycaster == null)
            {
                raycaster = GetComponentInParent<GraphicRaycaster>();
            }

            StopAllCoroutines();
            StartCoroutine(TransitionCoroutine(forward, onTransitionFinished));
        }

        public virtual IEnumerator TransitionCoroutine(bool forward, UnityEvent<bool> onTransitionFinished)
        {
            yield return null;
        }

        private void SetupMirroredCurve()
        {
            mirroredCurve = new AnimationCurve();

            for (int i = 0; i < transitionCurve.keys.Length; i++)
            {
                Keyframe tmp = new Keyframe(transitionCurve.keys[i].time, 
                    transitionCurve.keys[i].value, 
                    transitionCurve.keys[transitionCurve.keys.Length - i - 1].inTangent, 
                    transitionCurve.keys[transitionCurve.keys.Length - i - 1].outTangent);
                mirroredCurve.AddKey(tmp);
            }
        }

        protected void HandleInput(bool enabled)
        {
            if (lockInput && raycaster != null)
            {
                raycaster.enabled = enabled;
            }
        }
    }
}