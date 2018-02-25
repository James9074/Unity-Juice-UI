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
    Transform mCube;

    [SerializeField]
    Color mColorA;

    [SerializeField]
    Color mColorB;

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
        ColorCube();

        string keyframe = "new AnimationCurve(";
        for (int i = 0; i < customCurve.keys.Length; i++)
        {
            //AnimationCurve(new Keyframe(0, 0), new Keyframe(0.75f, 1.1f), new Keyframe(0.85f, .9f), new Keyframe(1, 1));
            //Keyframe frame = new Keyframe()
            keyframe += "new Keyframe (" + customCurve.keys[i].time + "," + customCurve.keys[i].value + "," + customCurve.keys[i].inTangent + "," + customCurve.keys[i].outTangent + ")" + (i < customCurve.keys.Length-1 ? "," : "");
        }
        keyframe += ");";
        //Debug.Log(keyframe);
    }
    
    int mDirection = -1;
    public void MoveCube()
    {
        Juice.Instance.MoveTo(mCube, 1f, Vector3.up * mDirection, false, GetCurrentCurve(), () =>
        {
            mDirection = mDirection * -1;
            MoveCube();
        });
    }

    int mRotation = -1;
    public void RotateCube()
    {
        Juice.Instance.Rotate(mCube, 1f, Vector3.up * 50f * mRotation, mRotation, GetCurrentCurve(), () =>
        {
            mRotation = mRotation * -1;
            RotateCube();
        });
    }

    int mColor = -1;
    public void ColorCube()
    {
        MeshRenderer cubeRend = mCube.GetComponent<MeshRenderer>();
        Juice.Instance.LerpMaterialProptery(cubeRend.material, 1f, Juice.MaterialPropertyType.Color, "_Color", mColor == -1 ? mColorA : mColorB, 0f, true, GetCurrentCurve() , () =>
        {
            mColor = mColor * -1;
            ColorCube();
        });
    }

    public void MoveDot(Transform aDot)
    {
        int dir = (int)aDot.transform.localPosition.y / (int)Mathf.Abs(aDot.transform.localPosition.y);
        Juice.Instance.MoveTo(aDot, 2f, aDot.localPosition + new Vector3(0, -dir * mInstructionsPanel.rect.height * .5f * .80f, 0),true, GetCurrentCurve(), ()=> { MoveDot(aDot); });
        Juice.Instance.LerpImageColor(aDot.GetComponent<Image>(), 1.9f, dir > 0 ? mColorA : mColorB, true);
    }

    public AnimationCurve GetCurrentCurve()
    {
        foreach(Toggle toggle in mTweenPanel.GetComponent<ToggleGroup>().ActiveToggles())
        {
            switch (toggle.name)
            {
                case "1":
                    return Juice.Curves.Smooth;
                case "2":
                    return Juice.Curves.Linear;
                case "3":
                    return Juice.Curves.ExpoEaseIn;
                case "4":
                    return Juice.Curves.CircEaseIn;
                case "5":
                    return Juice.Curves.ElasticEaseOutIn;
                case "6":
                    return Juice.Curves.BounceEaseOut;
                case "7":
                    return Juice.Curves.Square;
                case "8":
                    return customCurve;
                default:
                    return Juice.Curves.Linear;
            }
        }
        return Juice.Curves.Linear;
    }

    [SerializeField]
    AnimationCurve customCurve;
    public void ToggleTweenTypesPanel(){
        int direction = 0;
        direction = mMiddlePanel.anchoredPosition.x > 1 ? -1 : 1;
        mTweenPanelArrow.GetComponent<Button>().interactable = false;
        mCreditsPanelArrow.GetComponent<Button>().interactable = false;

        Juice.Instance.Tween(mMiddlePanel, 1f,new Vector2(direction * mTweenPanel.rect.width,0), direction > 0 ? Juice.Curves.ExpoEaseOut : Juice.Curves.BounceEaseOut, ()=>{
            mTweenPanelArrow.GetComponent<Button>().interactable = true;
            mCreditsPanelArrow.GetComponent<Button>().interactable = true;
        });

        Juice.Instance.Rotate(mTweenPanelArrow, .5f, new Vector3(0,0,180), direction, Juice.Curves.ExpoEaseOut);
        Juice.Instance.Rotate(mCreditsPanelArrow, .5f, new Vector3(0, 0, 180), direction, Juice.Curves.ExpoEaseOut);
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
        Juice.Instance.Tween(mInstructionsPanel, 1f, new Vector2(0, direction * GetComponent<RectTransform>().rect.height), Juice.Curves.ExpoEaseOut);
    }

    public void ToggleFAQsPanel()
    {
        int direction = 0;
        direction = mInstructionsPanel.anchoredPosition.y < -1 ? 1 : -1;
        Juice.Instance.Tween(mInstructionsPanel, 1f, new Vector2(0, direction * GetComponent<RectTransform>().rect.height), Juice.Curves.ExpoEaseOut);
    }
}