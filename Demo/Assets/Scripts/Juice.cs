/*     Juice Library (2018) - Created by James Thomas (https://wlan1.net)
*  ===============================================================================
*  This library is free to use, manipulate, and redistribute under the MIT License
*  Please give me credit if you use it!
*
*  Usage: This is mostly used to tween and fade Unity UI elements.
*         Throw this script on a GameObject and reference it as such:
*         Juice.Instance.[MethodName](....);
*
*  Let me know if it's useful or if you have questions: james9074@gmail.com
*
*  Version: 1.10 - Developed with Unity 5.3.5f1 - 2017.3.0f3
*  Last Updated: Feb 17th, 2018
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
    private List<Object> mComponents;

    [SerializeField]
    bool debug = false;

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
    public AnimationCurve Smooth
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0));
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
    public AnimationCurve Bounce
    {
        get {
            return new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.35f, 1, 9.5f, -2.5f), new Keyframe(0.6f, 1, 3.75f, -1.5f), new Keyframe(0.85f, 1, 2, -0.85f), new Keyframe(1, 1, 0.5f, 0));
        }
    }
    public AnimationCurve Elastic
    {
        get
        {
            return new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));
        }
    }
#pragma warning disable 0414
    [SerializeField]
    public AnimationCurve Custom = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));

#pragma warning restore 0414
    #endregion

    void Awake()
    {
        Instance = this;
        mCallbacks = new List<System.Action>();
        mComponents = new List<Object>();
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
        string juiceID = RegisterObject(aRect);
        if (juiceID == "") return;
        StartCoroutine(CoTween(juiceID, aRect, aTime, aDistance, aCurve, aCallback));
        return;
    }

    /// <summary>
    /// Stretch a RectTransform to a specified size over time
    /// </summary>
    /// <param name="aRect">The RectTransform to me stretched</param>
    /// <param name="aTime">The time it should take to complete</param>
    /// <param name="aSize">A size: Vector2(width,height)</param>
    /// <param name="aCurve">How should the animation look?</param>
    /// <param name="aCallback">Should anything happen after we finish?</param>
    public void Stretch(RectTransform aRect, float aTime, Vector2 aSize, AnimationCurve aCurve, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aRect);
        if (juiceID == "") return;
        StartCoroutine(CoStretch(juiceID, aRect, aTime, aSize, aCurve, aCallback));
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
    public void RotateRect(RectTransform aRect, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aRect);
        if (juiceID == "") return;
        StartCoroutine(CoRotateRect(juiceID, aRect, aTime, aRotationAmount, aDirection, aCurve, aCallback));
        return;
    }

    /// <summary>
    /// Rotates the object a number of degrees in a specified direction.
    /// </summary>
    /// <param name="aRect">The Transform to be rotated</param>
    /// <param name="aTime">The time it should take to complete</param>
    /// <param name="aRotationAmount">In how many euler angles to rotate</param>
    /// <param name="aDirection">should we rotate positive or negative (i.e., right or left)</param>
    /// <param name="aCurve">How should the animation look?</param>
    /// <param name="aCallback">Should anything happen after we finish?</param>
    public void Rotate(Transform aTransform, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aTransform);
        if (juiceID == "") return;
        StartCoroutine(CoRotate(juiceID, aTransform, aTime, aRotationAmount, aDirection, aCurve, aCallback));
        return;
    }

    /// <summary>
    /// Slides a Slider object to a specified value over X time
    /// </summary>
    /// <param name="aSlider">The slider to move</param>
    /// <param name="aTime">How long should this process take?</param>
    /// <param name="aTargetValue">Where should the slider land?</param>
    /// <param name="aCurve">What type of animation should we apply?</param>
    /// <param name="aCallback">Anything to perform after it's all over (if anything)</param>
    public void MoveSlider(Slider aSlider, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aSlider);
        if (juiceID == "") return;
        StartCoroutine(CoSlideSlider(juiceID, aSlider, aTime, aTargetValue, aCurve, aCallback));
        return;
    }

    /// <summary>
    /// Fills (or unfills) an image over time (think progress bars)
    /// </summary>
    /// <param name="aImage">An image to fill</param>
    /// <param name="aTime">How long this should take</param>
    /// <param name="aTargetValue">A target fill value</param>
    /// <param name="aCurve">An AnimationCurve to fill on</param>
    /// <param name="aCallback">What to do at the end, if anything</param>
    public void FillImage(Image aImage, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aImage);
        if (juiceID == "") return;
        NextFrame(() =>
        {
            StartCoroutine(CoFillImage(juiceID, aImage, aTime, aTargetValue, aCurve, aCallback));
        }, aImage);
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
        if (aTime <= 0)
        {
            aGroup.alpha = aTarget;
            mCallbacks.Add(aCallback);
            return;
        }

        string juiceID = RegisterObject(aGroup);
        if (juiceID == "") return;
        StartCoroutine(CoFadeGroup(juiceID, aGroup, aTime, aTarget, aScaledTime, aCallback: aCallback));
    }

    //TODO: Add AnimationCurves, a Callback, and more control to this one.
    //      There should be parameters to control how long it stays lit and how long it stays unlit. See the Blink method as an example.
    /// <summary>
    /// Pulses a CanvasGroup by fading it's alpha in and out
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aRepeatTime">How long should one entire pulse take?</param>
    /// <param name="aLowAlpha">How transparent is the group at it's lowest point?</param>
    /// <param name="aHighAlpha">How transparent is the group at it's highest point?</param>
    /// <param name="aUp">Set to true start the pulse on an "up" beat</param>
    /// <param name="aTime">How long do we pulse for? (0 = infinity)</param>
    public void PulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp = true, float aTime = 0)
    {
        string juiceID = RegisterObject(aGroup);
        if (juiceID == "") return;
        aLowAlpha = aLowAlpha < 0f ? 0f : (aLowAlpha > 1f ? 1f : aLowAlpha);
        aHighAlpha = aHighAlpha < 0f ? 0f : (aHighAlpha > 1f ? 1f : aHighAlpha);
        StartCoroutine(CoPulseGroup(juiceID, aGroup, aRepeatTime, aLowAlpha, aHighAlpha, aUp, aTime < 0f ? 0f : aTime));
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
        string juiceID = RegisterObject(aElement);
        if (juiceID == "") return;
        StartCoroutine(CoResizeElementVertical(juiceID, aElement, aSize, aTime, aCurve, aCallback));
    }

    /// <summary>
    /// Invokes a typewriter effect the specified a text element, slowly revealing or redacting text over time, character by character.
    /// </summary>
    /// <param name="aTextElement">A text element.</param>
    /// <param name="aText">Some text</param>
    /// <param name="aTime">The time it should take for the effect to finish</param>
    /// <param name="aReverse">If set to <c>true</c> reverse the effect (hide characters over time).</param>
    /// <param name="aCurve">An animation curve.</param>
    /// <param name="aCallback">A callback to invoke upon completion, if any.</param>
    public void Typewriter(Text aTextElement, string aText, float aTime, bool aReverse = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aTime <= 0)
        {
            aTextElement.text = aReverse ? "" : aText;
            return;
        }

        string juiceID = RegisterObject(aTextElement);
        if (juiceID == "") return;
        StartCoroutine(CoTypewriter(juiceID, aTextElement, aText, aTime, aReverse, aCurve, aCallback));
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
        string juiceID = RegisterObject(aGroup);
        if (juiceID == "") return;
        BlinkGroup(aGroup, aNumPulses, aTimeBetweenPulses, aTimeLitUp, aCallback);
    }

    //TODO: Registration, use the unique JuiceID here!
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

    #endregion

    #region Sound

    /// <summary>
    /// Fades the volume on an AudioSource over time
    /// </summary>
    /// <param name="aSource">An AudioSource to fade</param>
    /// <param name="aTime">How long the fade should take</param>
    /// <param name="aVolume">A target volume</param>
    /// <param name="aCurve">Optionally, an AnimationCurve to fade the volume through</param>
    /// <param name="aCallback">What happens after we're finished fading, if anything?</param>
    public void FadeVolume(AudioSource aSource, float aTime, float aVolume, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aSource);
        if (juiceID == "") return;
        StartCoroutine(CoFadeVolume(juiceID, aSource, aTime, aVolume, aCurve, aCallback));
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
        if(aTime <= 0f)
        {
            if (aLocalPos)
                aTransform.localPosition = aPos;
            else
                aTransform.position = aPos;

            mCallbacks.Add(aCallback);
            return;
        }
        
        string juiceID = RegisterObject(aTransform);
        if (juiceID == "") return;
        StartCoroutine(CoMoveTo(juiceID, aTransform, aTime, aPos, aLocalPos, aCurve, aCallback));
    }

    /// <summary>
    /// Moves a transform to any given position over a specified time, but respects other forces of movement, unlike MoveTo
    /// </summary>
    /// <param name="aTransform">The transform to move</param>
    /// <param name="aTime">How long it should take to complete the move</param>
    /// <param name="aPos">A position to end up at, relative to the start pos</param>
    /// <param name="aLocalPos">Should we use transform.localPosition? If false, this will use transform.position.</param>
    /// <param name="aCurve">How should it look?</param>
    /// <param name="aCallback">What should happen after we're done, if anything?</param>
    public void ApplyForce(Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aTransform);
        if (juiceID == "") return;
        StartCoroutine(CoApplyForce(juiceID, aTransform, aTime, aPos, aLocalPos, aCurve, aCallback));
    }

    /// <summary>
    /// Scales the transform to it's current scale plus aScale
    /// </summary>
    /// <param name="aTransform">The transform to scale</param>
    /// <param name="aScale">How much should we scale this transform ("Ex new Vector3(1,1,1) will add to it's scale by 1)</param>
    /// <param name="aTime">The time it takes to scale</param>
    /// <param name="aCurve">A curve to follow</param>
    /// <param name="aCallback">What to do at the end, if anything</param>
    public void Scale(Transform aTransform, Vector3 aScale, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aTransform);
        if (juiceID == "") return;
        StartCoroutine(CoScale(juiceID, aTransform, aScale, aTime, aCurve, aCallback));
    }

    /// <summary>
    /// Spin a transform on any axis!
    /// </summary>
    /// <param name="aTransform">A transform to spin</param>
    /// <param name="aTime">How long do we spin for?</param>
    /// <param name="aSpeed">How fast should we spin?</param>
    /// <param name="aAxis">A vector3 axis to spin on</param>
    /// <param name="aCurve">An animationCurve to spin on</param>
    /// <param name="aScaledTime">Should this respect scaled time?</param>
    /// <param name="aCallback">What to do at the end, if anything</param>
    public void Spin(Transform aTransform, float aTime, float aSpeed, Vector3 aAxis, AnimationCurve aCurve, bool aScaledTime = true, System.Action aCallback = null)
    {
        string juiceID = RegisterObject(aTransform);
        if (juiceID == "") return;
        StartCoroutine(CoSpin(juiceID, aTransform, aTime, aSpeed, aAxis, aCurve, aScaledTime, aCallback: aCallback));
    }
    #endregion

    #region Sprites

    //TODO: Add in AnimationCurve
    /// <summary>
    /// Fades a Sprite to a target alpha.
    /// </summary>
    /// <param name="aSprite">The Sprite</param>
    /// <param name="aTime">The time it should take</param>
    /// <param name="aTarget">The end alpha</param>
    public void FadeSprite(SpriteRenderer aSprite, float aTime, float aTarget, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (aTime <= 0)
        {
            aSprite.color = new Color(aSprite.color.r, aSprite.color.b, aSprite.color.g, aTarget);
            mCallbacks.Add(aCallback);
            return;
        }
        string juiceID = RegisterObject(aSprite);
        if (juiceID == "") return;
        StartCoroutine(CoFadeSprite(juiceID, aSprite, aTime, aTarget, aScaledTime, aCallback: aCallback));
    }

    //TODO: Add in AnimationCurve
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
        if (aTime <= 0f)
        {
            aSprite.color = aTarget;
            mCallbacks.Add(aCallback);
            return;
        }

        string juiceID = RegisterObject(aSprite);
        if (juiceID == "") return;
        StartCoroutine(CoLerpSpriteColor(juiceID, aSprite, aTime, aTarget, aScaledTime, aCallback: aCallback));
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
    /// <param name="aCallback">A callback to invoke afterwards, if any</param>
    public void NumberCounter(Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale = null, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (aModifierNumber == 0) return;
        string juiceID = RegisterObject(aText);
        if (juiceID == "") return;
        NextFrame(() => {
            StartCoroutine(CoNumberCounter(juiceID, aText, aStartingNumber, aModifierNumber, aTime, aScale, aScaledTime, aCallback));
        });
    }
    #endregion

    #region Materials

    public enum MaterialPropertyType { Color = 0, Float = 1 }

    /// <summary>
    /// Lerps a material's propery (color or any float value) to a target over time.
    /// </summary>
    /// <param name="aMaterial">The material instance</param>
    /// <param name="aTime">How long should this take?</param>
    /// <param name="aTarget">What color are we going to?</param>
    /// <param name="aPropertyType">What is the property's type?</param>
    /// <param name="aPropertyName">What is the property's name?</param>
    /// <param name="aTargetColor">If it's a color, what color are we going to</param>
    /// <param name="aTargetValue">If it's a float, what float are we going to</param>
    /// <param name="aScaledTime">Should this be affected by scaled time?</param>
    /// <param name="aCallback">Now what?</param>
    public void LerpMaterialProptery(Material aMaterial, float aTime, MaterialPropertyType aPropertyType, string aPropertyName, Color aTargetColor = default(Color), float aTargetFloat = 0f, bool aScaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null, bool aAllowMultiple = false)
    {
        if (aAllowMultiple && ObjectRegistered(aMaterial)) return;
        string juiceID = RegisterObject(aMaterial);
        if (juiceID == "") return;
        StartCoroutine(CoLerpMaterialProptery(juiceID, aMaterial, aTime, aPropertyType, aPropertyName, aTargetColor, aTargetFloat, aScaledTime, aCurve, aCallback));
    }

    #endregion

    #endregion

    #region Coroutines

    #region UI Objects
    IEnumerator CoTypewriter(string aJuiceID, Text aTextElement, string aText, float aTime, bool aReverse = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        float startTime = Time.time;
        if (aCurve == null) aCurve = Linear;
        float percentCompleted = 0f;
        aTextElement.text = "";
        while (ObjectRegistered(aTextElement, aJuiceID) && percentCompleted < 1.0f)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            int charsToShow = Mathf.Clamp(Mathf.RoundToInt(aCurve.Evaluate(percentCompleted) * aText.Length), 0, aText.Length);
            aTextElement.text = aText.Substring(0, aReverse ? (aText.Length - charsToShow) : charsToShow);
            yield return new WaitForEndOfFrame();
        }

        // Sanity checking, make sure the end result makes sense at least
        if (aTextElement != null)
            aTextElement.text = aReverse ? "" : aText;

        DeregisterObject(aTextElement, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoNumberCounter(string aJuiceID, Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale, bool aScaledTime = true, System.Action aCallback = null)
    {

        float startTime = Time.time;
        float percentCompleted = 0f;
        float aCurrentNumber = aStartingNumber;
        int aEndingNumber = aStartingNumber + aModifierNumber;

        Vector3 startScale = aText.rectTransform.localScale;

        while (ObjectRegistered(aText, aJuiceID) && aCurrentNumber != aEndingNumber && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aCurrentNumber = Mathf.Round(Mathf.Lerp(aStartingNumber, aEndingNumber, percentCompleted));
            aText.text = aCurrentNumber.ToString();
            if (aScale != null)
                aText.rectTransform.localScale = Vector3.Lerp(Vector3.zero, startScale, aScale.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        aText.text = aEndingNumber.ToString();
        DeregisterObject(aText, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoPulseGroup(string aJuiceID, CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp, float aTime)
    {
        if (aLowAlpha < aHighAlpha)
        {
            aGroup.alpha = aUp ? aLowAlpha : aHighAlpha;
            float timePassed = 0f;
            while (ObjectRegistered(aGroup, aJuiceID) && timePassed < aTime || aTime == 0f)
            {
                aGroup.alpha = Mathf.Sin((timePassed + (aUp ? -1f : 1f) * aRepeatTime / 4f) * 2f * Mathf.PI / aRepeatTime) * ((aHighAlpha - aLowAlpha) / 2f) + ((aHighAlpha + aLowAlpha) / 2f);
                timePassed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        DeregisterObject(aGroup, aJuiceID); yield break;
    }

    IEnumerator CoTween(string aJuiceID, RectTransform aRect, float aTime, Vector2 aDistance, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Vector2 startPos = aRect.anchoredPosition;
        Vector2 targetPos = aRect.anchoredPosition + aDistance;
        float percentCompleted = 0;
        while (ObjectRegistered(aRect, aJuiceID) && Vector2.Distance(aRect.anchoredPosition, targetPos) > .5f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aRect, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoStretch(string aJuiceID, RectTransform aRect, float aTime, Vector2 aSize, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Vector2 startSize = aRect.sizeDelta;
        float percentCompleted = 0;
        while (ObjectRegistered(aRect, aJuiceID) && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            if (aSize.x > -1f)
                aRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(startSize.x, aSize.x, aCurve.Evaluate(percentCompleted)));
            if (aSize.y > -1f)
                aRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(startSize.y, aSize.y, aCurve.Evaluate(percentCompleted)));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aRect, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoRotateRect(string aJuiceID, RectTransform aRect, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Quaternion startRot = aRect.rotation;
        Quaternion targetRot = aRect.rotation * Quaternion.Euler(aRotationAmount.x, aRotationAmount.y, aRotationAmount.z);
        float percentCompleted = 0;
        while (ObjectRegistered(aRect) && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aRect.rotation = Quaternion.Slerp(startRot, targetRot, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aRect, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoRotate(string aJuiceID, Transform aTransform, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null)
            aCurve = Linear;
        float startTime = Time.time;
        Quaternion startRot = aTransform.rotation;
        Quaternion targetRot = aTransform.rotation * Quaternion.Euler(aRotationAmount.x, aRotationAmount.y, aRotationAmount.z);
        float percentCompleted = 0;
        while (percentCompleted < 1 && ObjectRegistered(aTransform))
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aTransform.rotation = Quaternion.Slerp(startRot, targetRot, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aTransform, "", aCallback); yield break;
    }

    IEnumerator CoSlideSlider(string aJuiceID, Slider aSlider, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        float startValue = aSlider.value;
        while (ObjectRegistered(aSlider, aJuiceID) && Mathf.Abs(aSlider.value - aTargetValue) > .01f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aSlider.value = Mathf.Lerp(startValue, aTargetValue, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aSlider, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoFillImage(string aJuiceID, Image aImage, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        float startValue = aImage.fillAmount;
        while (ObjectRegistered(aImage, aJuiceID) &&  Mathf.Abs(aImage.fillAmount - aTargetValue) > .01f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aImage.fillAmount = Mathf.Lerp(startValue, aTargetValue, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aImage, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoResizeElementVertical(string aJuiceID, UnityEngine.UI.LayoutElement aElement, float aSize, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        float startSize = aElement.minHeight;
        while (ObjectRegistered(aElement, aJuiceID) && Mathf.Abs(aElement.minHeight - aSize) > .5f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aElement.minHeight = Mathf.Lerp(startSize, aSize, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aElement, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoFadeGroup(string aJuiceID, CanvasGroup aGroup, float aTime, float targetAlpha, bool scaledTime = true, System.Action aCallback = null)
    {
        targetAlpha = Mathf.Clamp01(targetAlpha);
        float timePassed = 0f;
        float m = (targetAlpha - aGroup.alpha) / (aTime);
        float b = aGroup.alpha;
        while (ObjectRegistered(aGroup, aJuiceID) && timePassed < aTime)
        {
            aGroup.alpha = m * timePassed + b;
            timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        aGroup.alpha = targetAlpha;
        DeregisterObject(aGroup, aJuiceID, aCallback); yield break;

    }
    #endregion

    #region Sprites
    IEnumerator CoFadeSprite(string aJuiceID, SpriteRenderer aSprite, float aTime, float targetAlpha, bool scaledTime = true, System.Action aCallback = null)
    {
        Color target = new Color(aSprite.color.r, aSprite.color.b, aSprite.color.g, targetAlpha);
        targetAlpha = Mathf.Clamp01(targetAlpha);

        float timePassed = 0f;
        float m = (targetAlpha - aSprite.color.a) / (aTime);
        float b = aSprite.color.a;
        while (ObjectRegistered(aSprite, aJuiceID) && timePassed < aTime)
        {
            aSprite.color = new Color(aSprite.color.r, aSprite.color.b, aSprite.color.g, m * timePassed + b);
            timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        aSprite.color = target;
        DeregisterObject(aSprite, aJuiceID, aCallback); yield break;

    }

    IEnumerator CoLerpSpriteColor(string aJuiceID, SpriteRenderer aSprite, float aTime, Color aTargetColor, bool scaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        Color target = aTargetColor;
        float startTime = Time.time;
        float percentCompleted = 0;
        
        if (aCurve == null)
            aCurve = Instance.Linear;

        while (ObjectRegistered(aSprite, aJuiceID) && percentCompleted < 1f)
        {
            percentCompleted = Mathf.Clamp01((Time.time - startTime) / aTime);
            //OLD WAY (for scaled time): float lerpAmount = scaledTime ? Time.deltaTime / aTime : Time.unscaledDeltaTime / aTime;
            //OLD WAY: timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            aSprite.color = Color.Lerp(aSprite.color, aTargetColor, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        if (aSprite != null) aSprite.color = target;
        DeregisterObject(aSprite, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoLerpMaterialProptery(string aJuiceID, Material aMaterial, float aTime, MaterialPropertyType aPropertyType, string aPropertyName, Color aTargetColor = default(Color), float aTargetValue = 0f, bool scaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null)
            aCurve = Linear;

        float startTime = Time.time;
        float percentCompleted = 0;
        Color startColor = Color.white;
        float startFloat = 0;

        switch (aPropertyType)
        {
            case MaterialPropertyType.Color:
                startColor = aMaterial.GetColor(aPropertyName);
                break;
            case MaterialPropertyType.Float:
                startFloat = aMaterial.GetFloat(aPropertyName);
                break;
            default:
                break;
        }

        while (ObjectRegistered(aMaterial, aJuiceID) && percentCompleted < 1)
        {
            percentCompleted = Mathf.Clamp01((Time.time - startTime) / aTime);
            switch (aPropertyType)
            {
                case MaterialPropertyType.Color:
                    aMaterial.SetColor(aPropertyName, Color.Lerp(startColor, aTargetColor, aCurve.Evaluate(percentCompleted)));
                    break;
                case MaterialPropertyType.Float:
                    aMaterial.SetFloat(aPropertyName, Mathf.Lerp(startFloat, aTargetValue, aCurve.Evaluate(percentCompleted)));
                    break;
                default:
                    break;
            }

            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aMaterial, aJuiceID, aCallback); yield break;
    }

    #endregion

    #region Sound
    IEnumerator CoFadeVolume(string aJuiceID, AudioSource aSource, float aTime, float aVolume, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float startVol = aSource.volume;
        float percentCompleted = 0f;
        if (aCurve == null)
            aCurve = Instance.Linear;
        while (ObjectRegistered(aSource, aJuiceID) && percentCompleted < 1)
        {
            percentCompleted = Mathf.Clamp01((Time.time - startTime) / aTime);
            aSource.volume = Mathf.Lerp(startVol, aVolume, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        DeregisterObject(aSource, aJuiceID, aCallback); yield break;
    }
    #endregion

    #region Transforms
    IEnumerator CoMoveTo(string aJuiceID, Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null) aCurve = Linear;

        Vector3 startPos = aLocalPos ? aTransform.localPosition : aTransform.position;
        float timeStarted = Time.time;
        float percentageComplete = 0f;

        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1.0f)
        {
            float timeSinceStarted = Time.time - timeStarted;
            percentageComplete = Mathf.Clamp01(timeSinceStarted / aTime);
            Vector3 newPos = Vector3.Lerp(startPos, aPos, aCurve.Evaluate(percentageComplete));

            if (aLocalPos)
                aTransform.localPosition = newPos;
            else
                aTransform.position = newPos;

            yield return new WaitForFixedUpdate();
        }

        DeregisterObject(aTransform, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoApplyForce(string aJuiceID, Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null) aCurve = Linear;

        Vector3 startPos = aTransform.position;
        float timeStarted = Time.time;
        float percentageComplete = 0f;
        float timeSinceStarted = 0f;

        //Store position at the end of the last movement, used to tell if we've been moved since the last frame by an external force
        Vector3 lastPos = aTransform.position;
        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1.0f)
        {
            timeSinceStarted  = Time.time - timeStarted;
            percentageComplete = timeSinceStarted / aTime;
            if (percentageComplete > 1.0f) break;

            //We've been moved
            if (lastPos != aTransform.position)
            {
                Vector3 difference = aTransform.position - lastPos;
                aPos += difference;
                startPos += difference;
            }

            Vector3 newPos = Vector3.Lerp(startPos, aPos, aCurve.Evaluate(percentageComplete));

            if (aLocalPos)
                aTransform.localPosition = newPos;
            else
                aTransform.position = newPos;

            lastPos = aTransform.position;
            yield return new WaitForFixedUpdate();
        }

        DeregisterObject(aTransform, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoSpin(string aJuiceID, Transform aTransform, float aTime, float aSpeed, Vector3 aAxis, AnimationCurve aCurve = null, bool aScaledTime = true, System.Action aCallback = null)
    {
        float timeStarted = Time.time;
        float timeSinceStarted = 0f;
        float percentageComplete = 0f;

        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1f)
        {
            timeSinceStarted = Time.time - timeStarted;
            percentageComplete = Mathf.Clamp01(timeSinceStarted / aTime);
            if (aCurve == null) aCurve = Linear;
            float currentAngle = 0;
            if (aAxis.x != 0) currentAngle = aTransform.rotation.eulerAngles.x;
            else if (aAxis.y != 0) currentAngle = aTransform.rotation.eulerAngles.y;
            else currentAngle = aTransform.rotation.eulerAngles.z;
            float speed = aSpeed * aCurve.Evaluate(percentageComplete);
            aTransform.rotation = Quaternion.AngleAxis(currentAngle + speed, aAxis);
            yield return new WaitForFixedUpdate();
        }
        DeregisterObject(aTransform, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoScale(string aJuiceID, Transform aTransform, Vector3 aScale, float aTime, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        Vector3 startScale = aTransform.localScale;
        float timeStarted = Time.time;
        float timeSinceStarted = 0f;
        float percentageComplete = 0f;

        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1f)
        {
            timeSinceStarted = Time.time - timeStarted;
            percentageComplete = Mathf.Clamp01(timeSinceStarted / aTime);
            if (aCurve == null) aCurve = Linear;
            aTransform.localScale = (startScale + (aCurve.Evaluate(percentageComplete) * aScale));
            yield return new WaitForFixedUpdate();
        }
        DeregisterObject(aTransform, aJuiceID, aCallback); yield break;
    }
    #endregion

    [SerializeField]
    Dictionary<Object, List<string>> mJuiceObjects;
    #region Helper/Utility Methods
    public string RegisterObject(Object aObject)
    {
        System.Diagnostics.StackFrame frame = new System.Diagnostics.StackFrame(1);
        var method = frame.GetMethod();
        var type = method.DeclaringType;
        string name = method.Name;
        string aJuiceID = name + aObject.GetInstanceID();

        if (mJuiceObjects == null)
            mJuiceObjects = new Dictionary<Object, List<string>>();

        // Add object to JuiceID dictionary if needed
        if (!mJuiceObjects.ContainsKey(aObject))
        {
            mJuiceObjects.Add(aObject, new List<string>() { aJuiceID });
            return aJuiceID;
        }

        // Add JuiceID if it doesn't already exist
        if (!mJuiceObjects[aObject].Contains(aJuiceID))
        {
            mJuiceObjects[aObject].Add(aJuiceID);
            return aJuiceID;
        }

        // Return nothing if we didn't add anything
        return "";
    }

    /// <summary>
    /// Deregisters an object from juice. This stops all juice effects and invokes any callbacks you passed in.
    /// </summary>
    /// <param name="aObject">An object being juiced.</param>
    /// <param name="aJuiceID">A juice identifier, leave blank to remove all juice effects from this Object</param>
    /// <param name="aCallback">A callback if desired.</param>
    /// <returns><c>true</c> if object was registered and we were able to deregister it, <c>false</c> otherwise.</returns>
    public bool DeregisterObject(Object aObject, string aJuiceID = "", System.Action aCallback = null)
    {
        // Todo: How do we cleanup this dictionary, how do we prevent this count from being huge?
        // Todo: throw 10k objects through juice and delete them, wait 10 seconds and see how large this dictionary is.
        if (aObject == null)
            return false;

        mCallbacks.Add(aCallback);

        if (mJuiceObjects.ContainsKey(aObject))
        {
            // If we're trying to remove one effect, check and remove it.
            if (aJuiceID.Length > 0 && mJuiceObjects[aObject].Contains(aJuiceID))
            {
                mJuiceObjects[aObject].Remove(aJuiceID);
                if (mJuiceObjects[aObject].Count == 0)
                    mJuiceObjects.Remove(aObject);

                return true;
            }
            else
                mJuiceObjects.Remove(aObject);
        }

        return false;




        /*
        if (mComponents.Contains(aObject)) {
            mComponents.Remove(aObject);
            if (aCallback != null)
            {
                if (aObject == null)
                    Debug.LogWarning("Invoking callback from a null source object. Debugging this might hurt your head.");

                mCallbacks.Add(aCallback);
            }
            return true;
        }
        else
        {
            //Cleanup
            for (int i = 0; i < mComponents.Count; i++)
                if (mComponents[i] == null) mComponents.RemoveAt(i);
            return false;
        }*/
    }

    /// <summary>
    /// Checks if an object and/or juiceID are registered in the system. If they are not, they are invalid and should stop immediately or face some serious prosecution.
    /// </summary>
    /// <param name="aObject">An object.</param>
    /// <param name="aJuiceID">Optionally, a juice identifier.</param>
    /// <returns><c>true</c> if registered, <c>false</c> otherwise.</returns>
    public bool ObjectRegistered(Object aObject, string aJuiceID = "")
    {
        if (aObject != null && mJuiceObjects.ContainsKey(aObject))
            return aJuiceID.Length > 0 ? mJuiceObjects[aObject].Contains(aJuiceID) : true;
        else return false;
    }

    /// <summary>
    /// Delays a delegated action.
    /// </summary>
    /// <param name="aTime">How long to wait before executing the action</param>
    /// <param name="aCallback">What logic to execute after the delay</param>
    /// <param name="aCaller">Pass in a caller to prevent the callback from invoking if the caller is destroyed during the delay</param>
    public void Delay(float aTime, System.Action aCallback, Object aCaller = null)
    {
        StartCoroutine(CoDelay(aTime, aCallback, aCaller));
    }

    public void NextFrame(System.Action aCallback, Object aCaller = null)
    {
        StartCoroutine(CoNextFrame(aCallback, aCaller));
    }

    IEnumerator CoDelay(float aTime, System.Action aCallback, Object aCaller = null)
    {
        bool passedCaller = (aCaller != null);
        string passedCallerName = passedCaller ? aCaller.name : "None";

        yield return new WaitForSeconds(aTime);
        if (!passedCaller || (passedCaller && aCaller != null))
            aCallback.Invoke();
        else
            if (debug) Debug.LogWarning("Passed a caller (" + passedCallerName + "), and the caller has since been destroyed. Not invoking callback!");
    }

    IEnumerator CoNextFrame(System.Action aCallback, Object aCaller = null)
    {
        bool passedCaller = (aCaller != null);
        string passedCallerName = passedCaller ? aCaller.name : "None";

        yield return new WaitForEndOfFrame();
        if (!passedCaller || (passedCaller && aCaller != null))
            aCallback.Invoke();
    }

    #endregion


    #endregion
}