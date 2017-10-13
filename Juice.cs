/*     Juice Library (2017) - Created by James Thomas (https://wlan1.net)
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
*  Version: 1.10 - Developed with Unity 4.6 - 2017.2
*  Last Updated: July 27th, 2017
*/


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum TweenType{
	Linear, 
	Exponential,
    InvertedExponential,
	Quadratic,
	Bounce,
	Elastic,
	Rand, 
    Wobble
}

public class Juice : MonoBehaviour {
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
                if(callback != null) callback.Invoke();
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
    public void Tween(RectTransform aRect, float aTime, Vector2 aDistance, AnimationCurve aCurve, System.Action aCallback = null){
        if (!RegisterObject(aRect))
            return;
        StartCoroutine(CoTween(aRect, aTime, aDistance, aCurve, aCallback));
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
        if (!RegisterObject(aRect))
            return;
        StartCoroutine(CoStretch(aRect, aTime, aSize, aCurve, aCallback));
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
    public void Rotate(RectTransform aRect, float aTime, Vector3 aRotationAmount, int aDirection, AnimationCurve aCurve, System.Action aCallback = null){
        if (!RegisterObject(aRect))
            return;
        StartCoroutine(CoRotate(aRect, aTime, aRotationAmount, aDirection, aCurve, aCallback));
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
        if (!RegisterObject(aSlider))
            return;
        StartCoroutine(CoSlideSlider(aSlider,aTime,aTargetValue,aCurve,aCallback));
        return;
    }

	public void FillImage(Image aImage, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
	{
        if (!RegisterObject(aImage))
            DeregisterObject(aImage);

        NextFrame(() =>
        {
            StartCoroutine(CoFillImage(aImage, aTime, aTargetValue, aCurve, aCallback));
        }, aImage);
		
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
    public void FadeGroup(CanvasGroup aGroup, float aTime, float aTarget, bool aScaledTime = true, System.Action aCallback = null){
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
	public void PulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp = true, float aTime = 0){
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
        StartCoroutine(CoResizeElementVertical(aElement,aSize,aTime, aCurve, aCallback));
    }
    
    public void Typewriter(Text aTextElement, string aText, float aTime, bool aReverse = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (!RegisterObject(aTextElement)) return;
        StartCoroutine(CoTypewriter(aTextElement, aText, aTime, aReverse, aCurve, aCallback));
    }
    
    #endregion

	#region Sound

	public void FadeVolume(AudioSource aSource, float aTime, float aVolume, AnimationCurve aCurve = null, System.Action aCallback = null)
	{
		if (!RegisterObject(aSource)) return;
		StartCoroutine(CoFadeVolume(aSource, aTime, aVolume, aCurve, aCallback));
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
        if (!RegisterObject(aTransform)) return;
        StartCoroutine(CoApplyForce(aTransform, aTime, aPos, aLocalPos, aCurve, aCallback));
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
        if (!RegisterObject(aTransform)) return;
        StartCoroutine(CoScale(aTransform, aScale, aTime, aCurve, aCallback));
    }

    public void Spin(Transform aTransform, float aTime, float aSpeed, Vector3 aAxis, AnimationCurve aCurve, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (!RegisterObject(aTransform)) return;
        StartCoroutine(CoSpin(aTransform, aTime, aSpeed, aAxis, aCurve, aScaledTime, aCallback: aCallback));
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
    public void NumberCounter(Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale = null, bool aScaledTime = true, System.Action aCallback = null){
       if(aModifierNumber == 0)
          return;
       if (!RegisterObject(aText)) DeregisterObject(aText);

        NextFrame(() => {
            StartCoroutine(CoNumberCounter(aText, aStartingNumber, aModifierNumber, aTime, aScale, aScaledTime, aCallback));
        }); 
    }
    #endregion

	#region Materials

	public enum MaterialPropertyType
	{
		Color = 0,
		Float = 1
	}

	/// <summary>
	/// Lerps a material's color to a target color over time.
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
		if (aAllowMultiple && !RegisterObject(aMaterial)) return;
		StartCoroutine(CoLerpMaterialProptery(aMaterial, aTime, aPropertyType, aPropertyName, aTargetColor, aTargetFloat, aScaledTime, aCurve, aCallback));
	}

	#endregion

    #endregion

    #region Coroutines

    #region UI Objects
    IEnumerator CoTypewriter(Text aTextElement, string aText, float aTime, bool aReverse = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        if (aCurve == null) aCurve = Linear;
        aTextElement.text = "";
        while (percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            int numThisFrame = Mathf.Clamp(Mathf.RoundToInt(aCurve.Evaluate(percentCompleted) * aText.Length), 0, aText.Length);
            if (aReverse)
            {
                aTextElement.text = aText.Substring(0, (aText.Length - numThisFrame));
            }
            else
                aTextElement.text = aText.Substring(0, numThisFrame);
            yield return new WaitForEndOfFrame();
            if (aTextElement == null)
            {
                DeregisterObject(aTextElement);
                yield break;
            }
            else if (!ObjectRegistered(aTextElement))
            {
                mCallbacks.Add(aCallback);
                yield break;
            }
        }
        aTextElement.text = aReverse ? "" : aText;
        DeregisterObject(aTextElement);
        mCallbacks.Add(aCallback);
        yield break;
    }


    IEnumerator CoNumberCounter(Text aText, int aStartingNumber, int aModifierNumber, float aTime, AnimationCurve aScale, bool aScaledTime = true, System.Action aCallback = null){
        
        float startTime = Time.time;
		float percentCompleted = 0;
        float aCurrentNumber = aStartingNumber;
        int aEndingNumber = aStartingNumber + aModifierNumber;
        
        Vector3 startScale = aText.rectTransform.localScale;
        
		while(aCurrentNumber != aEndingNumber && percentCompleted < 1){
			percentCompleted = (Time.time - startTime) / aTime;
            
			aCurrentNumber = Mathf.Round(Mathf.Lerp(aStartingNumber,aEndingNumber,percentCompleted));
            aText.text = aCurrentNumber.ToString();
            
            if(aScale != null){
                aText.rectTransform.localScale = Vector3.Lerp(Vector3.zero,startScale,aScale.Evaluate(percentCompleted));
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
    IEnumerator CoPulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp, float aTime){
		if(aLowAlpha < aHighAlpha){
			aGroup.alpha = aUp ? aLowAlpha : aHighAlpha;
			float timePassed = 0f;
			while(timePassed < aTime || aTime == 0f){ //TODO: break on some flag
				aGroup.alpha = Mathf.Sin ((timePassed + (aUp ? -1f : 1f)*aRepeatTime/4f)*2f*Mathf.PI/aRepeatTime)*((aHighAlpha-aLowAlpha)/2f)+((aHighAlpha+aLowAlpha)/2f);
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
		while(Vector2.Distance(aRect.anchoredPosition,targetPos) > .5f && percentCompleted < 1){
			percentCompleted = (Time.time - startTime) / aTime;
			aRect.anchoredPosition = Vector2.Lerp (startPos, targetPos, aCurve.Evaluate(percentCompleted));
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

    IEnumerator CoStretch(RectTransform aRect, float aTime, Vector2 aSize, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Vector2 startSize = aRect.sizeDelta;
        float percentCompleted = 0;
        while (percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            if(aSize.x > -1f)
                aRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(startSize.x, aSize.x, aCurve.Evaluate(percentCompleted)));
            if (aSize.y > -1f)
                aRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(startSize.y, aSize.y, aCurve.Evaluate(percentCompleted)));
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
		Quaternion targetRot = aRect.rotation * Quaternion.Euler(aRotationAmount.x,aRotationAmount.y,aRotationAmount.z);
		float percentCompleted = 0;
		while(percentCompleted < 1){
			percentCompleted = (Time.time - startTime) / aTime;
			aRect.rotation = Quaternion.Slerp (startRot, targetRot, aCurve.Evaluate(percentCompleted));
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

    IEnumerator CoSlideSlider(Slider aSlider, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
    {
        float startTime = Time.time;
        float percentCompleted = 0;
        float startValue = aSlider.value;
        while (Mathf.Abs(aSlider.value - aTargetValue) > .01f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            aSlider.value = Mathf.Lerp(startValue, aTargetValue, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
            if (aSlider == null)
            {
                DeregisterObject(aSlider);
                yield break;
            }
        }
        DeregisterObject(aSlider);
        mCallbacks.Add(aCallback);
        yield break;
    }

	IEnumerator CoFillImage(Image aImage, float aTime, float aTargetValue, AnimationCurve aCurve, System.Action aCallback = null)
	{
		float startTime = Time.time;
		float percentCompleted = 0;
		float startValue = aImage.fillAmount;
		while (Mathf.Abs(aImage.fillAmount - aTargetValue) > .01f && percentCompleted < 1)
		{
			percentCompleted = (Time.time - startTime) / aTime;
			aImage.fillAmount = Mathf.Lerp(startValue, aTargetValue, aCurve.Evaluate(percentCompleted));
			yield return new WaitForEndOfFrame();
			if (aImage == null)
			{
				DeregisterObject(aImage);
				yield break;
			}
		}
		DeregisterObject(aImage);
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
                if (!ObjectRegistered(aGroup))
                    yield break;

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

        if (!ObjectRegistered(aGroup))
            yield break;

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
            while (timePassed < aTime && ObjectRegistered(aSprite))
            {
                if (aSprite == null)
                {
                    yield return new WaitForEndOfFrame();
                    DeregisterObject(aSprite);
                    mCallbacks.Add(aCallback);
                    break;
                }
                    
                float lerpAmount = scaledTime ? Time.deltaTime / aTime : Time.unscaledDeltaTime / aTime;
                aSprite.color = Color.Lerp(aSprite.color, aTargetColor, lerpAmount);
                timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
        if(aSprite != null) aSprite.color = target;
        DeregisterObject(aSprite);
        mCallbacks.Add(aCallback);
        yield break;
    }

	IEnumerator CoLerpMaterialProptery(Material aMaterial, float aTime, MaterialPropertyType aPropertyType, string aPropertyName, Color aTargetColor = default(Color), float aTargetValue = 0f, bool scaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
	{
		if (aCurve == null)
			aCurve = Juice.Instance.Linear;

		float startTime = Time.time;
		float percentCompleted = 0;
		Color startColor = Color.white;
		float startFloat = 0;

		switch (aPropertyType) {
		case MaterialPropertyType.Color:
			startColor = aMaterial.GetColor (aPropertyName);
			break;
		case MaterialPropertyType.Float:
			startFloat = aMaterial.GetFloat (aPropertyName);
			break;
		default:
			break;
		}

		while (percentCompleted < 1)
		{
			percentCompleted = (Time.time - startTime) / aTime;
			switch (aPropertyType) {
			case MaterialPropertyType.Color:
				aMaterial.SetColor (aPropertyName, Color.Lerp (startColor, aTargetColor, aCurve.Evaluate (percentCompleted)));
				break;
			case MaterialPropertyType.Float:
				aMaterial.SetFloat (aPropertyName, Mathf.Lerp (startFloat, aTargetValue, aCurve.Evaluate (percentCompleted)));
				break;
			default:
				break;
			}

			yield return new WaitForEndOfFrame();
			if (aMaterial == null)
			{
				DeregisterObject(aMaterial);
				yield break;
			}
		}
		DeregisterObject(aMaterial);
		mCallbacks.Add(aCallback);
		yield break;
	}

    #endregion

    #region Helper/Utility Methods
	public bool RegisterObject(Object aObject)
    {
        if (mComponents.Contains(aObject)) {
            //Debug.LogWarning("Component " + aObject.name + " is already being Juiced.");
            return false; }
        else { mComponents.Add(aObject); return true; }
    }

	public bool DeregisterObject(Object aObject)
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

    public bool ObjectRegistered(Object aObject)
    {
        return mComponents.Contains(aObject);
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
			aCallback.Invoke ();
		else
			if(debug) Debug.LogWarning ("Passed a caller (" + passedCallerName + "), and the caller has since been destroyed. Not invoking callback!");
    }

    IEnumerator CoNextFrame(System.Action aCallback, Object aCaller = null)
    {
        bool passedCaller = (aCaller != null);
        string passedCallerName = passedCaller ? aCaller.name : "None";

        yield return new WaitForEndOfFrame();
        if (!passedCaller || (passedCaller && aCaller != null))
            aCallback.Invoke();
    }

    private void BlinkGroup(CanvasGroup aGroup, int aNumPulses, float aTimeBetweenPulses, float aTimeLitUp, System.Action aCallback = null)
    {
        if (aNumPulses == 0)
        {
            DeregisterObject(aGroup);
            if(aCallback != null) aCallback.Invoke();
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
        if(RegisterObject(aGroup))
            BlinkGroup(aGroup, aNumPulses, aTimeBetweenPulses, aTimeLitUp, aCallback);
    }
    
    #endregion

	#region Sound

	IEnumerator CoFadeVolume(AudioSource aSource, float aTime, float aVolume, AnimationCurve aCurve = null, System.Action aCallback = null){
		float startTime = Time.time;
		float startVol = aSource.volume;
		float percentCompleted = 0;
		if (aCurve == null)
			aCurve = Instance.Linear;
		while(percentCompleted < 1){
			percentCompleted = (Time.time - startTime) / aTime;
			aSource.volume = Mathf.Lerp (startVol, aVolume, aCurve.Evaluate(percentCompleted));
			yield return new WaitForEndOfFrame();
			if (aSource == null)
			{
				DeregisterObject(aSource);
				yield break;
			}
		}
		DeregisterObject(aSource);
		mCallbacks.Add(aCallback);
		yield break;
	}

	#endregion

    #region Transforms
    IEnumerator CoMoveTo(Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if(aTime == 0f)
        {
            if (aLocalPos)
                aTransform.localPosition = aPos;
            else
                aTransform.position = aPos;

            mCallbacks.Add(aCallback);
            DeregisterObject(aTransform);
            yield break;
        }

        if (aCurve == null) aCurve = Linear;

        Vector3 startPos = aLocalPos ? aTransform.localPosition : aTransform.position;
        float timeStarted = Time.time;
        
        while (ObjectRegistered(aTransform))
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / aTime;
            Vector3 newPos = Vector3.Lerp(startPos, aPos, aCurve.Evaluate(percentageComplete));

            if(aTransform == null) yield break;
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

    IEnumerator CoApplyForce(Transform aTransform, float aTime, Vector3 aPos, bool aLocalPos = false, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null) aCurve = Linear;

        Vector3 startPos = aTransform.position;
        float timeStarted = Time.time;

        //Store position at the end of the last movement, used to tell if we've been moved since the last frame by an external force
        Vector3 lastPos = aTransform.position;
        while (true)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / aTime;

            if (percentageComplete >= 1.0f || aTransform == null)
            {
                mCallbacks.Add(aCallback);
                DeregisterObject(aTransform);
                yield break;
            }

            //We've been moved
            if (lastPos != aTransform.position)
            {
                Vector3 difference = aTransform.position - lastPos;
                aPos += difference;
                startPos += difference;
            }
            Vector3 newPos = Vector3.Lerp(startPos, aPos, aCurve.Evaluate(percentageComplete));
            
            if (aTransform == null) yield break;
            if (aLocalPos)
                aTransform.localPosition = newPos;
            else
                aTransform.position = newPos;

            lastPos = aTransform.position;
            

            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator CoSpin(Transform aTransform, float aTime, float aSpeed, Vector3 aAxis, AnimationCurve aCurve = null, bool aScaledTime = true, System.Action aCallback = null)
    {
        float timeStarted = Time.time;

        while (true)
        {
            float timeSinceStarted = Time.time - timeStarted;
            float percentageComplete = timeSinceStarted / aTime;
            if (aCurve == null) aCurve = Linear;

            if (aTransform == null)
                yield break;

            float currentAngle = 0;
            if (aAxis.x !=0) currentAngle = aTransform.rotation.eulerAngles.x;
            else if (aAxis.y != 0) currentAngle = aTransform.rotation.eulerAngles.y;
            else currentAngle = aTransform.rotation.eulerAngles.z;

            float speed = aSpeed * aCurve.Evaluate(percentageComplete);

            aTransform.rotation = Quaternion.AngleAxis(currentAngle + speed, aAxis);
            
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

			if (aTransform == null)
				yield break;
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
