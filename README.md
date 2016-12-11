# Unity Juice UI Tools
<p align="center">
  <img src="https://puu.sh/sLpgn/c27bd836cc.gif">
  <br/><br/>
  <span>A Juicy system for tweening Unity3D sprites, UI elements, and more. Tested in Unity 4.6 - 5.3</span>
</p>

How To Use
=======

	1.  Download the Juice.cs file and throw it under /Assets/Plugins/RotaryDesign/Juice
	2.  Attach Juice.cs as a component to any gameobject that won't be destroyed (such as GameMaster. Juice is a singleton)
	3.  Call the library in any script via Juice.Instance.[JuiceMethodName]
	4.  Take a deep breath
	5.  Enjoy one-line tweens and fades!
	6.  Ex: You want to fade a UI Text Component out over 4 seconds, and then destroy it after it's faded.
		Just do this: Juice.Instance.FadeOutGroup(GetComponent<CanvasGroup>(), 4, true, ()=> { Destroy(gameObject); });
		Make sure you have a CanvasGroup attached for all UI fade methods.
	7.  Check out the demo project if you're curious, or want examples of usage.

Examples
=======
###Here are a few examples to get you going

The following will move 'myRect' 5 units up and 6 units to the left in exactly 1 second, linearly. After completing, it will print a debug statement

	Juice.Instance.Tween(myRect, 1f, new Vector2(-6,5),Juice.Instance.Linear, ()=>{
		Debug.Log("All done!");
	});

The following will fade out a CanvasGroup linearly, regardless of it's current alpha

	Juice.Instance.FadeGroup(myGroup, 0.5f, 0.0f)
