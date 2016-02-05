# Juice UI
A Juicy system for tweening Unity3D sprites, UI elements, and more.

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
