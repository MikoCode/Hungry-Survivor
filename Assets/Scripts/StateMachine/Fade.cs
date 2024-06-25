using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace HungrySurvivor.StateMachine
{
    public class Fade : Transition
    {
        public override IEnumerator TransitionCoroutine(bool forward, UnityEvent<bool> onTransitionFinished)
        {
            HandleInput(false);
            yield return new WaitForSeconds(delay);

            onTransitionStart?.Invoke();

            float origin = forward ? stateCanvasGroup.alpha : 1;
            float destination = forward ? 1 : 0;
            AnimationCurve curve = mirrorEffect && !forward ? mirroredCurve : transitionCurve;

            if (duration > 0)
            {
                for (float i = 0; i <= duration; i += Time.deltaTime)
                {
                    stateCanvasGroup.alpha = Mathf.Lerp(origin, destination, curve.Evaluate(i / duration));

                    yield return null;
                }
            }

            stateCanvasGroup.alpha = Mathf.Lerp(origin, destination, curve.Evaluate(1));

            yield return null;

            onTransitionFinish?.Invoke();

            HandleInput(true);

            onTransitionFinished!.Invoke(forward);
        }
    }
}