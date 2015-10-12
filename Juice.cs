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
*/


using UnityEngine;
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

public enum FadeType{
	Linear,
	Sinusoidal
}

public class Juice : MonoBehaviour {
    public static Juice Instance;
    public List<System.Action> mCallbacks;

    [SerializeField]
    private List<Component> mComponents;

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
    /// <param name="aTweenType">The type of tween (Linear, Exponential, Etc)</param>
	public void Tween(RectTransform aRect, float aTime, Vector2 aDistance, TweenType aTweenType = TweenType.Linear, System.Action aCallback = null){
        if (!RegisterObject(aRect))
            return;
        switch (aTweenType) {
		case TweenType.Linear:
            StartCoroutine(CoLinearTween(aRect, aTime, aDistance, aCallback));
			break;
		case TweenType.Bounce:
            throw new System.NotImplementedException();
		case TweenType.Elastic:
            throw new System.NotImplementedException();
		case TweenType.Exponential:
            StartCoroutine(CoExpTween(aRect, aTime, aDistance,aCallback:aCallback));
			break;
        case TweenType.InvertedExponential:
            StartCoroutine(CoExpTween(aRect, aTime, aDistance, true, aCallback));
            break;
		case TweenType.Quadratic:
            throw new System.NotImplementedException();
		case TweenType.Rand:
            throw new System.NotImplementedException();
        case TweenType.Wobble:
            StartCoroutine(CoWobble(aRect,aTime,aDistance,aCallback));
            break;
		default:
            Debug.LogWarning("TweenType " + aTweenType.ToString() + " isn't implemented yet!");
			break;
		}
        return;
	}

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

    /// <summary>
    /// Fades a CanvasGroup out.
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aTime">The time it should take</param>
    /// <param name="aScaledTime">Should this be scaled with Time.TimeScale?</param>
    /// <param name="aCallback">Any logic to execute once the target state is reached</param>
    public void FadeOutGroup(CanvasGroup aGroup, float aTime, bool aScaledTime = true, System.Action aCallback = null)
    {
        FadeGroup(aGroup, aTime, 0, aScaledTime, aCallback);
    }

    /// <summary>
    /// Fades a CanvasGroup in.
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aTime">The time it should take</param>
    /// <param name="aScaledTime">Should this be scaled with Time.TimeScale?</param>
    /// <param name="aCallback">Any logic to execute once the target state is reached</param>
    public void FadeInGroup(CanvasGroup aGroup, float aTime, bool aScaledTime = true, System.Action aCallback = null)
    {
        if (!RegisterObject(aGroup)) return;
        FadeGroup(aGroup, aTime, 1, aScaledTime, aCallback);
    }

    /// <summary>
    /// Pulses a CanvasGroup
    /// </summary>
    /// <param name="aGroup">The CanvasGroup</param>
    /// <param name="aRepeatTime">The RepeatTime</param>
    /// <param name="aLowAlpha">How transparent is the group at it's lowest point?</param>
    /// <param name="aHighAlpha">How transparent is the group at it's highest point?</param>
    /// <param name="aUp"></param>
    /// <param name="aTime">How long do we pulse for?</param>
	public void PulseGroup(CanvasGroup aGroup, float aRepeatTime, float aLowAlpha, float aHighAlpha, bool aUp = true, float aTime = 0){
        if (!RegisterObject(aGroup)) return;
        aLowAlpha = aLowAlpha < 0f ? 0f : (aLowAlpha > 1f ? 1f : aLowAlpha);
		aHighAlpha = aHighAlpha < 0f ? 0f : (aHighAlpha > 1f ? 1f : aHighAlpha);
		StartCoroutine(CoPulseGroup(aGroup, aRepeatTime, aLowAlpha, aHighAlpha, aUp, aTime < 0f ? 0f : aTime));
	}
    #endregion

    #region Transforms
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
    #endregion

    #endregion

    #region Coroutines

    #region UI Objects
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
    
    IEnumerator CoWobble(RectTransform aRect, float aDuration, Vector2 aDistance, System.Action aCallback = null){
        //TODO: Finish this. It's a shake animation, but needs to be able to lock to an axis. (only x or only y)
        //float startTime = Time.time;
        //Vector2 startPos = aRect.anchoredPosition;
		Vector2 targetPos = aRect.anchoredPosition + aDistance;
        float aMagnitude = (targetPos - aRect.anchoredPosition).magnitude;
        //In case the above line doesn't work well aMagnitude = 1;
        
        /*****Wobble*****/
        float elapsed = 0.0f;
        //This needs to come from aDistance. Maybe the magnitude of the distance between the target distance and rect pos.

    
        Vector2 originalPos = aRect.anchoredPosition;
        
        while (elapsed < aDuration) {
            
            elapsed += Time.deltaTime;          
            
            float percentComplete = elapsed / aDuration;         
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);
            
            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= aMagnitude * damper;
            y *= aMagnitude * damper;
            
            //Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);
            //Should I use WaitForEndOfFrame or return a null? yield return new WaitForEndOfFrame();
            yield return null;
        }
    
        aRect.anchoredPosition = originalPos;    
        mCallbacks.Add(aCallback);
		yield break;
    }

    IEnumerator CoLinearTween(RectTransform aRect, float aTime, Vector2 aDistance, System.Action aCallback = null)
    {
        float startTime = Time.time;
		Vector2 targetPos = aRect.anchoredPosition + aDistance;
		float percentCompleted = 0;
        Vector2 startPos = aRect.anchoredPosition;
		while(Vector2.Distance(aRect.anchoredPosition,targetPos) > .5f && percentCompleted < 1){
			percentCompleted = (Time.time - startTime) / aTime;
			aRect.anchoredPosition = Vector2.Lerp (startPos, targetPos, percentCompleted);
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

    IEnumerator CoExpTween(RectTransform aRect, float aTime, Vector2 aDistance, bool aInverted = false, System.Action aCallback = null)
    {
        float startTime = Time.time;
        Vector2 targetPos = aRect.anchoredPosition + aDistance;
        float percentCompleted = 0;
        Vector2 startPos = aRect.anchoredPosition;
        while (Vector2.Distance(aRect.anchoredPosition, targetPos) > .5f && percentCompleted < 1)
        {
            percentCompleted = (Time.time - startTime) / aTime;
            if (aInverted)
                aRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, Mathf.Sqrt(percentCompleted));
            else
                aRect.anchoredPosition = Vector2.Lerp(startPos, targetPos, Mathf.Pow(percentCompleted,2));
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

    #region Helper Methods
    public bool RegisterObject(Component aObject)
    {
        if (mComponents.Contains(aObject)) {
            //Debug.LogWarning("Component " + aObject.name + " is already being Juiced.");
            return false; }
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
    #endregion

    #endregion
}