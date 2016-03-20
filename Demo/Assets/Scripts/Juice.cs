/*     Juice Library (2015) - Created by James Thomas (http://rotary-design.com)
*  ===============================================================================
*  This library is free to use, manipulate, and redistribute under the MIT License
*  Please give me credit if you use it!
*
*  Usage: This is mostly used to tween and fade Unity 4.6 UI elements.
*         Throw this script on a GameObject and reference it as such:
*         Juice.Instance.[MethodName](....);
*
*  Let me know if it's useful or if you have questions: james9074@gmail.com
*
*  Version: 0.40 - Developed with Unity 4.6 - 5.3.2f1
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum TweenType
{
    Linear,
    Exponential,
    InvertedExponential,
    Quadratic,
    Bounce,
    Elastic,
    Rand,
    Wobble
}

public class Juice : MonoBehaviour
{
    public static Juice Instance;
    public List<System.Action> mCallbacks;

    [SerializeField]
    private List<Component> mComponents;

    #region Predefined curves
    public AnimationCurve SproingIn
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.7f, 1.1f), new Keyframe(0.85f, 0.9f), new Keyframe(1, 1));
        }
    }
    public AnimationCurve FastFalloff
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(0.25f, 0.8f, 1, 1), new Keyframe(1, 1));
        }
    }
    public AnimationCurve LateFalloff
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 0.0f), new Keyframe(1, 1));
        }
    }
    public AnimationCurve Wobble
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.25f, 1), new Keyframe(0.75f, -1), new Keyframe(1, 0));
        }
    }
    public AnimationCurve Linear
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(1, 1, 1, 1));
        }
    }
    public AnimationCurve Hop
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.5f, 1), new Keyframe(1, 0));
        }
    }
    public AnimationCurve SharpHop
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 1), new Keyframe(1, 0));
        }
    }

    public AnimationCurve Exponential
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(0.25f, 0.8f, 1, 1), new Keyframe(1, 1));
        }
    }
    public AnimationCurve DelayedExponential
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(0.75f, 0.0f, 1, 1), new Keyframe(1, 1));
        }
    }
    //TODO: Fix this. It doesn't work.
    public AnimationCurve InvertedExponential
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 1, 1), new Keyframe(0.75f, 0.0f, 1, 1), new Keyframe(1, 1));
        }
    }
    [HideInInspector]
    public AnimationCurve Bounce;
    public AnimationCurve Elastic
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));
        }
    }
#pragma warning disable 0414
    [SerializeField]
    AnimationCurve Custom = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));
#pragma warning restore 0414
    #endregion

    void Awake()
    {
        Instance = this;
        mCallbacks = new List<System.Action>();
        mComponents = new List<Component>();
    }

    void Update()
    {
        if (mCallbacks.Count > 0)
        {
            foreach (System.Action callback in mCallbacks)
                if (callback != null) callback.Invoke();
            mCallbacks.Clear();
        }
    }

    #region Public Methods

    #region UI Objects
    /// <summary>
    /// Tween moves a RectTransform a certain distance in a certain time.
    /// </summary>
    /// <param name="aRect">The RectTransform to me moved</param>
    /// <param name="aTime">The time it should take to complete</param>
    /// <param name="aDistance">A distance: Vector2(width,height)</param>
    /// <param name="aCurve">How should the animation look?</param>
    /// <param name="aCallback">Should anything happen after we finish?</param>
    public void Tween(RectTransform aRect, float aTime, Vector2 aDistance, AnimationCurve aCurve, System.Action aCallback = null)
    {
        if (!RegisterObject(aRect))
            return;
        StartCoroutine(CoTween(aRect, aTime, aDistance, aCurve, aCallback));
        return;
    }

    /// <summary>
    /// Rotates the object a number of degrees in a specified direction.
    /// </summary>
    /// <param name="aRect">The RectTransform to be rotated</param>
    /// <param name="aTime">The time it should take to complete</param>
    /// <param name="aRotationAmount">In how many euler angles to rotate</param>
    /// <param name="aDirection">should we rotate positive or negative (i.e., right or left)</param>
    /// <param name="aCurve">How should the animation look?</param>
    /// <param name="aCallback">Should anything happen after we finish?</param>
    public void Rotate(RectTransform aRect, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null)
    {
        if (!RegisterObject(aRect))
            return;
        StartCoroutine(CoRotate(aRect, aTime, aRotationAmount, aDirection, aCurve, aCallback));
        return;
    }



    //TODO: Add in AnimationCurve for Fade Operations
    /// <summary>
    /// Fades a CanvasGroup to a target alpha.
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aTime">The time it should take</param>
    /// <param name="aTarget">The end alpha</param>
    /// <param name="aScaledTime">Should this be scaled with Time.TimeScale?</param>
    /// <param name="aCallback">Any logic to execute once the target state is reached</param>
    public void FadeGroup(CanvasGroup aGroup, float aTime, float aTarget, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (!RegisterObject(aGroup)) return;
        StartCoroutine(CoFadeGroup(aGroup, aTime, aTarget, aScaledTime, aCallback: aCallback));
    }

    //TODO: Add AnimationCurves, a Callback, and more control to this one.
    //      There should be parameters to control how long it stays lit and how long it stays unlit. See the Blink method as an example.
    /// <summary>
    /// Pulses a CanvasGroup
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aRepeatTime">How long should one entire pulse take?</param>
    /// <param name="aLowAlpha">How transparent is the group at it's lowest point?</param>
    /// <param name="aHighAlpha">How transparent is the group at it's highest point?</param>
    /// <param name="aUp"></param>
    /// <param name="aTime">How long do we pulse for? (0 = infinity)</param>
	public void PulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp = true, float aTime = 0)
    {
        if (!RegisterObject(aGroup)) return;
        aLowAlpha = aLowAlpha < 0f ? 0f : (aLowAlpha > 1f ? 1f : aLowAlpha);
        aHighAlpha = aHighAlpha < 0f ? 0f : (aHighAlpha > 1f ? 1f : aHighAlpha);
        StartCoroutine(CoPulseGroup(aGroup, aRepeatTime, aLowAlpha, aHighAlpha, aUp, aTime < 0f ? 0f : aTime));
    }

    //TODO: Change this to accept a Vector2(x,y) for size, so we can change width and height. Currently, we can only change height.
    /// <summary>
    /// Currently used to resize a Unity UI LayoutElement Vertically.
    /// </summary>
    /// <param name="aElement">LayoutElement to resize</param>
    /// <param name="aSize">How tall should it become?</param>
    /// <param name="aTime">How long should it take?</param>
    /// <param name="aCurve">How should it look?</param>
    /// <param name="aCallback">What should happen after we're done, if anything?</param>
    public void ResizeLayoutElement(UnityEngine.UI.LayoutElement aElement, float aSize, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (!RegisterObject(aElement)) return;
        StartCoroutine(CoResizeElementVertical(aElement, aSize, aTime, aCurve, aCallback));
    }
    #endregion

    #region Transforms
    /// <summary>
    /// Moves a transform to any given position over a specified time
    /// </summary>
    /// <param name="aTransform">The transform to move</param>
    /// <param name="aTime">How long it should take to complete the move</param>
    /// <param name="aPos">Where should we move this transform to?</param>
    /// <param name="aLocalPos">Should we use transform.localPosition? If false, this will use transform.position.</param>
    /// <param name="aCurve">How should it look?</param>
    /// <param name="aCallback">What should happen after we're done, if anything?</param>
    public void MoveTo(Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (!RegisterObject(aTransform)) return;
        StartCoroutine(CoMoveTo(aTransform, aTime, aPos, aLocalPos, aCurve, aCallback));
    }

    public void Scale(Transform aTransform, Vector3 aScale, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (!RegisterObject(aTransform)) return;
        StartCoroutine(CoScale(aTransform, aScale, aTime, aCurve, aCallback));
    }
    #endregion

    #region Sprites

    /// <summary>
    /// Fades a Sprite to a target alpha.
    /// </summary>
    /// <param name="aSprite">The Sprite</param>
    /// <param name="aTime">The time it should take</param>
    /// <param name="aTarget">The end alpha</param>
    public void FadeSprite(SpriteRenderer aSprite, float aTime, float aTarget, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (!RegisterObject(aSprite)) return;
        StartCoroutine(CoFadeSprite(aSprite, aTime, aTarget, aScaledTime, aCallback: aCallback));
    }

    /// <summary>
    /// Fades a Sprite out.
    /// </summary>
    /// <param name="aSprite">The Sprite</param>
    /// <param name="aTime">The time it should take</param>
    public void FadeOutSprite(SpriteRenderer aSprite, float aTime, System.Action aCallback = null)
    {
        if (!RegisterObject(aSprite)) return;
        StartCoroutine(CoFadeSprite(aSprite, aTime, 0, true, aCallback));
    }

    /// <summary>
    /// Fades a Sprite in.
    /// </summary>
    /// <param name="aSprite">The Sprite</param>
    /// <param name="aTime">The time it should take</param>
    public void FadeInSprite(SpriteRenderer aSprite, float aTime)
    {
        if (!RegisterObject(aSprite)) return;
        StartCoroutine(CoFadeSprite(aSprite, aTime, 1));
    }

    /// <summary>
    /// Lerps a sprite's color to a target color over time.
    /// </summary>
    /// <param name="aSprite">The spriterenderer</param>
    /// <param name="aTime">How long should this take?</param>
    /// <param name="aTarget">What color are we going to?</param>
    /// <param name="aScaledTime">Should this be affected by scaled time?</param>
    /// <param name="aCallback">Now what?</param>
    public void LerpSpriteColor(SpriteRenderer aSprite, float aTime, Color aTarget, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (!RegisterObject(aSprite)) return;
        StartCoroutine(CoLerpSpriteColor(aSprite, aTime, aTarget, aScaledTime, aCallback: aCallback));
    }

    /// <summary>
    /// Quickly increase a Text element's number value
    /// </summary>
    /// <param name="aText">The text element</param>
    /// <param name="aStartingNumber">The starting number</param>
    /// <param name="aModifierNumber">The amount to increase or decrease aStartingNumber by</param>
    /// <param name="aTime">How long should this take?</param>
    /// <param name="aScale">Should we change the size over time?</param>
    /// <param name="aScaledTime">Should this be affected by scaled time?</param>
    /// <param name="aCallback">Now what?</param>
    public void NumberCounter(Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale = null, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (aModifierNumber == 0)
            return;
        if (!RegisterObject(aText)) return;
        StartCoroutine(CoNumberCounter(aText, aStartingNumber, aModifierNumber, aTime, aScale, aScaledTime, aCallback));
    }
    #endregion

    #endregion

    #region Coroutines

    #region UI Objects
    IEnumerator CoNumberCounter(Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale, bool aScaledTime = true, System.Action aCallback = null)
    {

        float startTime = Time.time;
        float percentCompleted = 0;
        float aCurrentNumber = aStartingNumber;
        int aEndingNumber = aStartingNumber + aModifierNumber;

        Vector3 startScale = aText.rectTransform.localScale;

        while (aCurrentNumber != aEndingNumber && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;

            aCurrentNumber = Mathf.Round(Mathf.Lerp(aStartingNumber, aEndingNumber, percentCompleted));
            aText.text = aCurrentNumber.ToString();

            if (aScale != null)
            {
                aText.rectTransform.localScale = Vector3.Lerp(Vector3.zero, startScale, aScale.Evaluate(percentCompleted));
            }
            yield return new WaitForEndOfFrame();
            if (aText == null)
            {
                DeregisterObject(aText);
                yield break;
            }
        }
        aText.text = aEndingNumber.ToString();
        DeregisterObject(aText);
        mCallbacks.Add(aCallback);
        yield break;
    }
    IEnumerator CoPulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp, float aTime)
    {
        if (aLowAlpha < aHighAlpha)
        {
            aGroup.alpha = aUp ? aLowAlpha : aHighAlpha;
            float timePassed = 0f;
            while (timePassed < aTime || aTime == 0f)
            { //TODO: break on some flag
                aGroup.alpha = Mathf.Sin((timePassed + (aUp ? -1f : 1f) * aRepeatTime / 4f) * 2f * Mathf.PI / aRepeatTime) * ((aHighAlpha - aLowAlpha) / 2f) + ((aHighAlpha + aLowAlpha) / 2f);
                timePassed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
                if (aGroup == null)
                {
                    DeregisterObject(aGroup);
                    yield break;
                }
            }
        }
        DeregisterObject(aGroup);
        yield break;
    }

    IEnumerator CoTween(RectTransform aRect, float aTime, Vector2 aDistance, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Vector2 startPos = aRect.anchoredPosition;
        Vector2 targetPos = aRect.anchoredPosition + aDistance;
        float percentCompleted = 0;
        while (Vector2.Distance(aRect.anchoredPosition, targetPos) > .5f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
            if (aRect == null)
            {
                DeregisterObject(aRect);
                yield break;
            }
        }
        DeregisterObject(aRect);
        mCallbacks.Add(aCallback);
        yield break;
    }


    IEnumerator CoRotate(RectTransform aRect, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Quaternion startRot = aRect.rotation;
        Quaternion targetRot = aRect.rotation * Quaternion.Euler(aRotationAmount.x, aRotationAmount.y, aRotationAmount.z);
        float percentCompleted = 0;
        while (percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aRect.rotation = Quaternion.Slerp(startRot, targetRot, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
            if (aRect == null)
            {
                DeregisterObject(aRect);
                yield break;
            }
        }
        DeregisterObject(aRect);
        mCallbacks.Add(aCallback);
        yield break;
    }

    IEnumerator CoResizeElementVertical(UnityEngine.UI.LayoutElement aElement, float aSize, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        float startSize = aElement.minHeight;
        while (Mathf.Abs(aElement.minHeight - aSize) > .5f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aElement.minHeight = Mathf.Lerp(startSize, aSize, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
            if (aElement == null)
            {
                DeregisterObject(aElement);
                yield break;
            }
        }
        DeregisterObject(aElement);
        mCallbacks.Add(aCallback);
        yield break;
    }

    IEnumerator CoFadeGroup(CanvasGroup aGroup, float aTime, float targetAlpha, bool scaledTime = true, System.Action aCallback = null)
    {
        if (aTime != 0)
        {
            if (targetAlpha > 1) targetAlpha = 1;
            if (targetAlpha < 0) targetAlpha = 0;
            if (aTime <= 0) aTime = .1f;

            float timePassed = 0f;
            float m = (targetAlpha - aGroup.alpha) / (aTime);
            float b = aGroup.alpha;
            while (timePassed < aTime)
            {
                aGroup.alpha = m * timePassed + b;
                timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
                if (aGroup == null)
                {
                    DeregisterObject(aGroup);
                    yield break;
                }
            }
        }
        aGroup.alpha = targetAlpha;
        DeregisterObject(aGroup);
        mCallbacks.Add(aCallback);
        yield break;

    }
    #endregion

    #region Sprites
    IEnumerator CoFadeSprite(SpriteRenderer aSprite, float aTime, float targetAlpha, bool scaledTime = true, System.Action aCallback = null)
    {
        Color target = new Color(aSprite.color.r, aSprite.color.b, aSprite.color.g, targetAlpha);
        if (aTime != 0)
        {
            if (targetAlpha > 1) targetAlpha = 1;
            if (targetAlpha < 0) targetAlpha = 0;
            if (aTime <= 0)
                aTime = .1f;

            float timePassed = 0f;
            float m = (targetAlpha - aSprite.color.a) / (aTime);
            float b = aSprite.color.a;
            while (timePassed < aTime)
            {
                aSprite.color = new Color(aSprite.color.r, aSprite.color.b, aSprite.color.g, m * timePassed + b);
                timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
                if (aSprite == null)
                {
                    DeregisterObject(aSprite);
                    yield break;
                }
            }
        }
        aSprite.color = target;
        DeregisterObject(aSprite);
        mCallbacks.Add(aCallback);
        yield break;

    }

    IEnumerator CoLerpSpriteColor(SpriteRenderer aSprite, float aTime, Color aTargetColor, bool scaledTime = true, System.Action aCallback = null)
    {
        Color target = aTargetColor;
        if (aTime != 0)
        {
            float timePassed = 0f;
            while (timePassed < aTime)
            {
                float lerpAmount = scaledTime ? Time.deltaTime / aTime : Time.unscaledDeltaTime / aTime;
                aSprite.color = Color.Lerp(aSprite.color, aTargetColor, lerpAmount);
                timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
                if (aSprite == null)
                {
                    DeregisterObject(aSprite);
                    yield break;
                }
            }
        }
        aSprite.color = target;
        DeregisterObject(aSprite);
        mCallbacks.Add(aCallback);
        yield break;
    }
    #endregion

    #region Helper/Utility Methods
    public bool RegisterObject(Component aObject)
    {
        if (mComponents.Contains(aObject))
        {
            //Debug.LogWarning("Component " + aObject.name + " is already being Juiced.");
            return false;
        }
        else { mComponents.Add(aObject); return true; }
    }

    public bool DeregisterObject(Component aObject)
    {
        if (mComponents.Contains(aObject)) { mComponents.Remove(aObject); return true; }
        else
        {
            //Cleanup
            for (int i = 0; i < mComponents.Count; i++)
                if (mComponents[i] == null) mComponents.RemoveAt(i);
            return false;
        }

    }

    /// <summary>
    /// Delays a delegated action.
    /// </summary>
    /// <param name="aTime">How long to wait before executing the action</param>
    /// <param name="aCallback">What logic to execute after the delay</param>
    public void Delay(float aTime, System.Action aCallback)
    {
        StartCoroutine(CoDelay(aTime, aCallback));
    }

    IEnumerator CoDelay(float atime, System.Action aCallback)
    {
        yield return new WaitForSeconds(atime);
        aCallback.Invoke();
    }

    private void BlinkGroup(CanvasGroup aGroup, int aNumPulses, float aTimeBetweenPulses, float aTimeLitUp, System.Action aCallback = null)
    {
        if (aNumPulses == 0)
        {
            DeregisterObject(aGroup);
            if (aCallback != null) aCallback.Invoke();
            return;
        }
        Delay(aTimeLitUp, () => { aGroup.alpha = 0; Delay(aTimeBetweenPulses, () => { aGroup.alpha = 1; BlinkGroup(aGroup, (aNumPulses - 1), aTimeBetweenPulses, aTimeLitUp, aCallback); }); });
    }

    /// <summary>
    /// Blinks a CanvasGroup a number of times.
    /// </summary>
    /// <param name="aGroup">A CanvasGroup</param>
    /// <param name="aNumPulses">How many blinks to perform (-1 = infinite)</param>
    /// <param name="aTimeBetweenPulses">How long to pause between blinks</param>
    /// <param name="aTimeLitUp">How long to stay lit</param>
    /// <param name="aCallback">What logic to execute after the last blink, if any</param>
    public void Blink(CanvasGroup aGroup, int aNumPulses, float aTimeBetweenPulses, float aTimeLitUp, System.Action aCallback = null)
    {
        if (RegisterObject(aGroup))
            BlinkGroup(aGroup, aNumPulses, aTimeBetweenPulses, aTimeLitUp, aCallback);
    }

    #endregion

    #region Transforms
    IEnumerator CoMoveTo(Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null) aCurve = Linear;

        Vector3 startPos = aLocalPos ? aTransform.localPosition : aTransform.position;
        float timeStarted = Time.time;

        while (true)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / aTime;
            Vector3 newPos = Vector3.Lerp(startPos, aPos, aCurve.Evaluate(percentageComplete));

            if (aLocalPos)
                aTransform.localPosition = newPos;
            else
                aTransform.position = newPos;


            if (percentageComplete >= 1.0f)
            {
                mCallbacks.Add(aCallback);
                DeregisterObject(aTransform);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CoScale(Transform aTransform, Vector3 aScale, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        Vector3 startScale = aTransform.localScale;
        float timeStarted = Time.time;

        while (true)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / aTime;
            if (aCurve == null) aCurve = Linear;

            aTransform.localScale = (startScale + (aCurve.Evaluate(percentageComplete) * aScale));

            if (percentageComplete >= 1.0f)
            {
                mCallbacks.Add(aCallback);
                DeregisterObject(aTransform);
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    #endregion

    #endregion
}