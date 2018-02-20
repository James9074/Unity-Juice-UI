using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour {
    
    RectTransform mTweenPanel;
    RectTransform mTweenPanelArrow;
    RectTransform mCreditsPanelArrow;
    RectTransform mInstructionsPanel;
    RectTransform mMiddlePanel;

    CanvasGroup mDot;
    RectTransform mMoveDot;

    [SerializeField]
    Transform aCube;

    void Start () {
        mMiddlePanel = transform.Find("Middle").GetComponent<RectTransform>();

        mTweenPanel = mMiddlePanel.Find("TweenTypesPanel").GetComponent<RectTransform>();
        mTweenPanelArrow = mTweenPanel.Find("Arrow").GetComponent<RectTransform>();
        mCreditsPanelArrow = mMiddlePanel.Find("CreditsPanel/Arrow").GetComponent<RectTransform>();

        mInstructionsPanel = mMiddlePanel.Find("Instructions").GetComponent<RectTransform>();
        mMoveDot = mInstructionsPanel.Find("MoveDot").GetComponent<RectTransform>();
        
        mDot = mInstructionsPanel.Find("Instructions/Juice/Dot").GetComponent<CanvasGroup>();
        Juice.Instance.PulseGroup(mDot, 2, 0, 1);

        Juice.Instance.Delay(2f, () =>
        {
            for (int i = 0; i < mMoveDot.childCount; i++)
            {
                Transform dot = mMoveDot.GetChild(i).transform;
                Juice.Instance.Delay(i / 10f, () => { MoveDot(dot); });
            }
        });

        RotateCube();
        MoveCube();

        for (int i = 0; i < customCurve.keys.Length; i++)
        {
            //AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));
            //Keyframe frame = new Keyframe()
            //Debug.Log("new Keyframe ("+customCurve.keys[i].time +"," + customCurve.keys[i].value + "," + customCurve.keys[i].inTangent + "," + customCurve.keys[i].outTangent+"),");

        }
    }

    [SerializeField]
    AnimationCurve easeInSine;
    [SerializeField]
    AnimationCurve easeOutSine;
    [SerializeField]
    AnimationCurve easeInOutSine;
    [SerializeField]
    AnimationCurve easeInQuad;
    [SerializeField]
    AnimationCurve easeOutQuad;
    [SerializeField]
    AnimationCurve easeInOutQuad;
    [SerializeField]
    AnimationCurve easeInCubic;
    [SerializeField]
    AnimationCurve easeOutCubic;
    [SerializeField]
    AnimationCurve easeInOutCubic;
    [SerializeField]
    AnimationCurve easeInQuart;
    [SerializeField]
    AnimationCurve easeOutQuart;
    [SerializeField]
    AnimationCurve easeInOutQuart;
    [SerializeField]
    AnimationCurve easeInQuint;
    [SerializeField]
    AnimationCurve easeOutQuint;
    [SerializeField]
    AnimationCurve easeInOutQuint;
    [SerializeField]
    AnimationCurve easeInExpo;
    [SerializeField]
    AnimationCurve easeOutExpo;
    [SerializeField]
    AnimationCurve easeInOutExpo;
    [SerializeField]
    AnimationCurve easeInCirc;
    [SerializeField]
    AnimationCurve easeOutCirc;
    [SerializeField]
    AnimationCurve easeInOutCirc;
    [SerializeField]
    AnimationCurve easeInBack;
    [SerializeField]
    AnimationCurve easeOutBack;
    [SerializeField]
    AnimationCurve easeInOutBack;
    [SerializeField]
    AnimationCurve easeInElastic;
    [SerializeField]
    AnimationCurve easeOutElastic;
    [SerializeField]
    AnimationCurve easeInOutElastic;
    [SerializeField]
    AnimationCurve easeInBounce;
    [SerializeField]
    AnimationCurve easeOutBounce;
    [SerializeField]
    AnimationCurve easeInOutBounce;

    int mDirection = -1;
    public void MoveCube()
    {
        Juice.Instance.MoveTo(aCube, 1f, Vector3.up * mDirection, false, GetCurrentCurve(), () =>
        {
            mDirection = mDirection * -1;
            MoveCube();
        });
    }

    int mRotation = -1;
    public void RotateCube()
    {
        Juice.Instance.Rotate(aCube, 1f, Vector3.up * 50f * mRotation, mRotation, GetCurrentCurve(), () =>
        {
            mRotation = mRotation * -1;
            RotateCube();
        });
    }

    public void MoveDot(Transform aDot)
    {
        int dir = (int)aDot.transform.localPosition.y / (int)Mathf.Abs(aDot.transform.localPosition.y);
        Juice.Instance.MoveTo(aDot, 2f, aDot.localPosition + new Vector3(0, -dir * mInstructionsPanel.rect.height * .5f * .80f, 0),true, GetCurrentCurve(), ()=> { MoveDot(aDot); });
    }

    public AnimationCurve GetCurrentCurve()
    {
        foreach(Toggle toggle in mTweenPanel.GetComponent<ToggleGroup>().ActiveToggles())
        {
            switch (toggle.name)
            {
                case "1":
                    return Juice.Instance.Linear;
                case "2":
                    return Juice.Instance.Exponential;
                case "3":
                    return Juice.Instance.Hop;
                case "4":
                    return Juice.Instance.FastFalloff;
                case "5":
                    return Juice.Instance.Elastic;
                case "6":
                    return Juice.Instance.Bounce;
                case "8":
                    return customCurve;
                default:
                    return Juice.Instance.Linear;
            }
        }
        return Juice.Instance.Linear;
    }

    [SerializeField]
    AnimationCurve customCurve;
    public void ToggleTweenTypesPanel(){
        int direction = 0;
        direction = mMiddlePanel.anchoredPosition.x > 1 ? -1 : 1;
        mTweenPanelArrow.GetComponent<Button>().interactable = false;
        mCreditsPanelArrow.GetComponent<Button>().interactable = false;

        Juice.Instance.Tween(mMiddlePanel, 1f,new Vector2(direction * mTweenPanel.rect.width,0),direction == -1 ? customCurve : Juice.Instance.Exponential,()=>{
            mTweenPanelArrow.GetComponent<Button>().interactable = true;
            mCreditsPanelArrow.GetComponent<Button>().interactable = true;
        });

        Juice.Instance.Rotate(mTweenPanelArrow, .5f, new Vector3(0,0,180), direction, Juice.Instance.Exponential);
        Juice.Instance.Rotate(mCreditsPanelArrow, .5f, new Vector3(0, 0, 180), direction, Juice.Instance.Exponential);
    }

    public void OpenURL(string aUrl)
    {
        Application.OpenURL(aUrl);
    }

    public void Reset()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ToggleExamplesPanel()
    {
        int direction = 0;
        direction = mInstructionsPanel.anchoredPosition.y > 1 ? -1 : 1;
        Juice.Instance.Tween(mInstructionsPanel, 1f, new Vector2(0, direction * GetComponent<RectTransform>().rect.height), Juice.Instance.Exponential);
    }

    public void ToggleFAQsPanel()
    {
        int direction = 0;
        direction = mInstructionsPanel.anchoredPosition.y < -1 ? 1 : -1;
        Juice.Instance.Tween(mInstructionsPanel, 1f, new Vector2(0, direction * GetComponent<RectTransform>().rect.height), Juice.Instance.Exponential);
    }
}