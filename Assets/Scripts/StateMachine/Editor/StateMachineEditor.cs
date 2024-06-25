using UnityEditor;
using UnityEngine;

namespace HungrySurvivor.StateMachine.Editor
{
    public class StateMachineEditor
    {
        [MenuItem("GameObject/UI/Hungry Survivor/State Machine")]
        private static void CreateNewStateMachine()
        {
            GameObject newStateMachine = new GameObject("StateMachine");
            newStateMachine.transform.SetParent(Selection.activeTransform);
            newStateMachine.AddComponent<StateMachine>();

            StretchToParent(newStateMachine.AddComponent<RectTransform>());

            BuildNewState(newStateMachine.transform);
        }

        [MenuItem("GameObject/UI/Hungry Survivor/State")]
        private static void CreateNewState()
        {
            BuildNewState(Selection.activeTransform);
        }

        private static void BuildNewState(Transform parent)
        {
            GameObject newState = new GameObject("State");
            newState.transform.SetParent(parent);
            newState.AddComponent<State>();

            StretchToParent(newState.AddComponent<RectTransform>());

            GameObject enterTransitions = new GameObject("EnterTransitions");
            enterTransitions.transform.SetParent(newState.transform);
            enterTransitions.AddComponent<Fade>();

            GameObject exitTransitions = new GameObject("ExitTransitions");
            exitTransitions.transform.SetParent(newState.transform);
            exitTransitions.AddComponent<Fade>();
            exitTransitions.GetComponent<Fade>().onEnter = false;
        }

        private static void StretchToParent(RectTransform rt)
        {
            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = Vector2.one * 0.5f;
            rt.sizeDelta = Vector2.zero;
            rt.anchoredPosition = Vector2.zero;
        }
    }
}