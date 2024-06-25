using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace HungrySurvivor.StateMachine
{
   public class StateMachine : MonoBehaviour
    {
        [HideInInspector]
        public List<State> substates;

        public State CurrentSubstate
        {
            get
            {
                return currentSubstate;
            }
            set
            {
                currentSubstate?.ExitState();
                currentSubstate = value;
            }
        }

        [SerializeField]
        private bool enterDefaultStateOnStart = true;

        [ShowIf("enterDefaultStateOnStart")]
        [SerializeField]
        private State defaultState;

        private State currentSubstate;

        private void Awake()
        {
            substates = new List<State>();
        }

        protected virtual IEnumerator Start()
        {
            if (!enterDefaultStateOnStart)
            {
                yield break;
            }

            if (defaultState == null)
            {
                if (substates.Count > 0)
                {
                    defaultState = substates[0];
                }
                else
                {
                    Debug.LogWarning("Default state for state machine " + name + " wasn't found.");
                    yield break;
                }
            }

            yield return null;

            defaultState.EnterState();
        }

        public void AddSubstate(State newSubstate, float posX = 0, float posY = 0)
        {
            if (substates.Contains(newSubstate))
            {
                return;
            }

            if (newSubstate.transform.parent != transform)
            {
                Vector2 newPos = new Vector2(posX, posY);

                newSubstate.gameObject.name += " [" + newSubstate.transform.parent.gameObject.name + "]";
                newSubstate.transform.SetParent(transform);
                newSubstate.transform.localScale = Vector3.one;
                newSubstate.transform.position = Vector3.zero;
                newSubstate.rectTransform.anchoredPosition = newPos;
                newSubstate.rectTransform.localRotation = Quaternion.identity;
                newSubstate.parentState = this;
            }

            newSubstate.SetupTransitions();
            substates.Add(newSubstate);
        }

        public void ExitCurrentState()
        {
            CurrentSubstate = null;
        }

        public void RemoveSubstate(State substateToRemove)
        {
            if (substates.Contains(substateToRemove))
            {
                substates.Remove(substateToRemove);
                Destroy(substateToRemove.gameObject);
            }
        }



        public void EnterPreviousSubstate(bool rotate)
        {
            int nextSubstateIndex = 0;

            for (int i = 0; i < substates.Count; i++)
            {
                if (substates[i].isActive)
                {
                    nextSubstateIndex = i + 1;
                    break;
                }
            }

            if (nextSubstateIndex >= substates.Count)
            {
                if (rotate)
                {
                    nextSubstateIndex = 0;
                }
                else
                {
                    nextSubstateIndex = substates.Count - 1;
                }
            }

            substates[nextSubstateIndex].EnterState();
        }

        public void EnterNextSubstate(bool rotate)
        {
            int previousSubstateIndex = 0;

            for (int i = 0; i < substates.Count; i++)
            {
                if (substates[i].isActive)
                {
                    previousSubstateIndex = i - 1;
                    break;
                }
            }

            if (previousSubstateIndex < 0)
            {
                if (rotate)
                {
                    previousSubstateIndex = substates.Count - 1;
                }
                else
                {
                    previousSubstateIndex = 0;
                }
            }

            substates[previousSubstateIndex].EnterState();
        }
    }
}