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

    void Start () {
        mMiddlePanel = transform.FindChild("Middle").GetComponent<RectTransform>();

        mTweenPanel = mMiddlePanel.FindChild("TweenTypesPanel").GetComponent<RectTransform>();
        mTweenPanelArrow = mTweenPanel.FindChild("Arrow").GetComponent<RectTransform>();
        mCreditsPanelArrow = mMiddlePanel.FindChild("CreditsPanel/Arrow").GetComponent<RectTransform>();

        mInstructionsPanel = mMiddlePanel.FindChild("Instructions").GetComponent<RectTransform>();
        mMoveDot = mInstructionsPanel.FindChild("MoveDot").GetComponent<RectTransform>();
        
        mDot = mInstructionsPanel.FindChild("Instructions/Juice/Dot").GetComponent<CanvasGroup>();
        Juice.Instance.PulseGroup(mDot, 2, 0, 1);

        Juice.Instance.Delay(2f, () =>
        {
            for (int i = 0; i < mMoveDot.childCount; i++)
            {
                Transform dot = mMoveDot.GetChild(i).transform;
                Juice.Instance.Delay(i / 10f, () => { MoveDot(dot); });
            }
        });
        
    }

    public void MoveDot(Transform aDot)
    {
        int dir = (int)aDot.transform.localPosition.y / (int)Mathf.Abs(aDot.transform.localPosition.y);
        Juice.Instance.MoveTo(aDot, 2f, aDot.localPosition + new Vector3(0, -dir * mInstructionsPanel.rect.height * .80f, 0),true, GetCurrentCurve(), ()=> { MoveDot(aDot); });
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
                    return panel;
                default:
                    return Juice.Instance.Linear;
            }
        }
        return Juice.Instance.Linear;
    }

    [SerializeField]
    AnimationCurve panel;
    public void ToggleTweenTypesPanel(){
        int direction = 0;
        direction = mMiddlePanel.anchoredPosition.x > 1 ? -1 : 1;
        mTweenPanelArrow.GetComponent<Button>().interactable = false;
        mCreditsPanelArrow.GetComponent<Button>().interactable = false;

        Juice.Instance.Tween(mMiddlePanel, 1f,new Vector2(direction * mTweenPanel.rect.width,0),direction == -1 ? panel : Juice.Instance.Exponential,()=>{
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