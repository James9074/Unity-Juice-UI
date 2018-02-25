# Unity Juice UI Tools | Tested For Unity 5.x - 2017+

<p align="center">
  <img src="https://puu.sh/zvGnH/b382b6c3dd.gif">
<br/><br/>
<span>A library for juicing Unity3D sprites, models, UI elements, shaders, and more.</span><br/>
<a href="https://github.com/James9074/Unity-Juice-UI/wiki/API-Reference-Docs">Comprehensive API documentation can be found here</a>
</p>

How To Use
=======

	1.  Download the Juice.cs file and throw it wherever you'd like in your assets folder
	2.  Attach Juice.cs as a component to any gameobject that won't be destroyed (such as GameManager). Juice is a singleton.
	3.  Call the library in any script anywhere (not just on that GameObject) via Juice.Instance.[JuiceMethodName]
	4.  Enjoy one-line tweens, fades, and color lerps!

Examples
=======
### Here are a few examples to get you going

The following will move 'myRect' 5 units up and 6 units to the left in exactly 1 second, linearly. After completing, it will print a debug statement

	Juice.Instance.Tween(myRect, 1f, new Vector2(-6,5),Juice.Instance.Linear, ()=>{
		Debug.Log("All done!");
	});

The following will fade out a CanvasGroup linearly, regardless of it's current alpha

	Juice.Instance.FadeGroup(myGroup, 0.5f, 0.0f)

Animations
======
The following animations from [easings.net](http://easings.net) are included. Please note that any animations on that page that go outside the bounds of (0,1) are not enabled in Juice at this time.
![easings.net](https://puu.sh/zvGP5/ec4c2b2446.png)

Contributions
=======
Please raise an issue for feature requests, and a PR into master for any contributions with the following:
* **Summary of changes**
* **How to test (convince me this won't break existing Juice implementations)**
