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

public class Juice : MonoBehaviour
{
    public class Curves
    {
        // Convenience references
        public static AnimationCurve Smooth
        {
            get
            {
                //return new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 0, 0));
                return QuadEaseInOut;
            }
        }
        public static AnimationCurve Square
        {
            get
            {
                return new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, 0.5f, Mathf.Infinity, Mathf.Infinity), new Keyframe(1, 1, 0, 0));
            }
        }

        // Standard Library. Anything that goes outside of 0,1 is commented out for now.
        public static AnimationCurve QuadEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 1.875f, 1.875f), new Keyframe(0.1250001f, 0.2343751f, 1.75f, 1.75f), new Keyframe(0.2500003f, 0.4375005f, 1.5f, 1.5f), new Keyframe(0.3749987f, 0.6093734f, 1.250003f, 1.250003f), new Keyframe(0.4999971f, 0.7499971f, 1.000006f, 1.000006f), new Keyframe(0.6249955f, 0.8593717f, 0.7500091f, 0.7500091f), new Keyframe(0.7499939f, 0.937497f, 0.5315118f, 0.5315118f), new Keyframe(0.8119931f, 0.9646534f, 0.3755139f, 0.3755139f), new Keyframe(0.8749923f, 0.9843731f, 0.2500154f, 0.2500154f), new Keyframe(0.9379915f, 0.996155f, 0.1245122f, 0.1245122f), new Keyframe(1f, 1f, 0.06200821f, 0.06200821f)); } }

        public static AnimationCurve QuadEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.06199995f, 0.06199995f), new Keyframe(0.06199995f, 0.003843994f, 0.1245f, 0.1245f), new Keyframe(0.1250001f, 0.01562502f, 0.2500001f, 0.2500001f), new Keyframe(0.1880002f, 0.03534408f, 0.3755004f, 0.3755004f), new Keyframe(0.2500003f, 0.06250016f, 0.5314998f, 0.5314998f), new Keyframe(0.3749987f, 0.140624f, 0.7499974f, 0.7499974f), new Keyframe(0.4999971f, 0.2499971f, 0.9999943f, 0.9999943f), new Keyframe(0.6249955f, 0.3906194f, 1.249991f, 1.249991f), new Keyframe(0.7499939f, 0.5624909f, 1.499988f, 1.499988f), new Keyframe(0.8749923f, 0.7656115f, 1.749989f, 1.749989f), new Keyframe(1f, 1f, 1.874992f, 1.874992f)); } }

        public static AnimationCurve QuadEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.1239999f, 0.1239999f), new Keyframe(0.06199995f, 0.007687988f, 0.249f, 0.249f), new Keyframe(0.1250001f, 0.03125004f, 0.5000002f, 0.5000002f), new Keyframe(0.1880002f, 0.07068815f, 0.7510008f, 0.7510008f), new Keyframe(0.2500003f, 0.1250003f, 1.005f, 1.005f), new Keyframe(0.3169995f, 0.2009773f, 1.268998f, 1.268998f), new Keyframe(0.3849986f, 0.2964478f, 1.535994f, 1.535994f), new Keyframe(0.4489978f, 0.403198f, 1.760303f, 1.760303f), new Keyframe(0.586996f, 0.6588554f, 1.691315f, 1.691315f), new Keyframe(0.6479952f, 0.7521853f, 1.409019f, 1.409019f), new Keyframe(0.7079945f, 0.8294655f, 1.155022f, 1.155022f), new Keyframe(0.7809935f, 0.9040723f, 0.876026f, 0.876026f), new Keyframe(0.8539926f, 0.9573637f, 0.5840293f, 0.5840293f), new Keyframe(0.9269916f, 0.9893395f, 0.2920241f, 0.2920241f), new Keyframe(1f, 1f, 0.1460171f, 0.1460171f)); } }

        public static AnimationCurve QuadEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 1.876f, 1.876f), new Keyframe(0.06199995f, 0.1163119f, 1.751f, 1.751f), new Keyframe(0.1250001f, 0.2187501f, 1.501f, 1.501f), new Keyframe(0.1870002f, 0.3040622f, 1.250999f, 1.250999f), new Keyframe(0.2500003f, 0.3750003f, 0.9949998f, 0.9949998f), new Keyframe(0.3179995f, 0.4337516f, 0.7290023f, 0.7290023f), new Keyframe(0.3849986f, 0.4735494f, 0.4630056f, 0.4630056f), new Keyframe(0.4489978f, 0.4947976f, 0.2111176f, 0.2111176f), new Keyframe(0.5369967f, 0.5027375f, 0.1691064f, 0.1691064f), new Keyframe(0.586996f, 0.5151366f, 0.3589838f, 0.3589838f), new Keyframe(0.6479952f, 0.5438052f, 0.5909812f, 0.5909812f), new Keyframe(0.7079945f, 0.5865234f, 0.8449775f, 0.8449775f), new Keyframe(0.7809935f, 0.6579147f, 1.123974f, 1.123974f), new Keyframe(0.8539926f, 0.7506215f, 1.415971f, 1.415971f), new Keyframe(0.9269916f, 0.8646438f, 1.707976f, 1.707976f), new Keyframe(1f, 1f, 1.853983f, 1.853983f)); } }

        public static AnimationCurve ExpoEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 5.763271f, 5.763271f), new Keyframe(0.05499996f, 0.3169797f, 4.799641f, 4.799641f), new Keyframe(0.1180001f, 0.5586487f, 3.276206f, 3.276206f), new Keyframe(0.1530001f, 0.6537229f, 2.413263f, 2.413263f), new Keyframe(0.1910002f, 0.7339078f, 1.855161f, 1.855161f), new Keyframe(0.2330003f, 0.8011163f, 1.390295f, 1.390295f), new Keyframe(0.279f, 0.8554139f, 1.016459f, 1.016459f), new Keyframe(0.3269993f, 0.8963346f, 0.7269369f, 0.7269369f), new Keyframe(0.3799987f, 0.9282057f, 0.5035844f, 0.5035844f), new Keyframe(0.4409979f, 0.9529604f, 0.332077f, 0.332077f), new Keyframe(0.510997f, 0.9710435f, 0.1871421f, 0.1871421f), new Keyframe(0.6869947f, 0.9914505f, 0.07163221f, 0.07163221f), new Keyframe(1f, 1f, 0.02731427f, 0.02731427f)); } }

        public static AnimationCurve ExpoEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.02731359f, 0.02731359f), new Keyframe(0.3129995f, 0.008549141f, 0.07162966f, 0.07162966f), new Keyframe(0.4889973f, 0.02895533f, 0.1871349f, 0.1871349f), new Keyframe(0.5589964f, 0.04703778f, 0.3320636f, 0.3320636f), new Keyframe(0.6199956f, 0.07179146f, 0.5035642f, 0.5035642f), new Keyframe(0.6729949f, 0.1036613f, 0.726908f, 0.726908f), new Keyframe(0.7209943f, 0.1445803f, 1.016417f, 1.016417f), new Keyframe(0.7669937f, 0.1988754f, 1.390236f, 1.390236f), new Keyframe(0.8089932f, 0.2660799f, 1.855075f, 1.855075f), new Keyframe(0.8469927f, 0.3462598f, 2.413142f, 2.413142f), new Keyframe(0.8819922f, 0.4413277f, 3.276026f, 3.276026f), new Keyframe(0.9449914f, 0.6829795f, 4.799451f, 4.799451f), new Keyframe(1f, 1f, 5.76311f, 5.76311f)); } }

        public static AnimationCurve ExpoEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.03080955f, 0.03080955f), new Keyframe(0.1720002f, 0.005299248f, 0.09096838f, 0.09096838f), new Keyframe(0.2660001f, 0.0195052f, 0.3020885f, 0.3020885f), new Keyframe(0.3349992f, 0.05076523f, 0.7420957f, 0.7420957f), new Keyframe(0.3869986f, 0.1043839f, 1.475271f, 1.475271f), new Keyframe(0.4259981f, 0.1792396f, 2.535056f, 2.535056f), new Keyframe(0.4589976f, 0.2832117f, 3.921552f, 3.921552f), new Keyframe(0.4839973f, 0.40052f, 5.371501f, 5.371501f), new Keyframe(0.5229968f, 0.6364909f, 5.060319f, 5.060319f), new Keyframe(0.5549964f, 0.7667302f, 3.291924f, 3.291924f), new Keyframe(0.5929959f, 0.8622541f, 1.958489f, 1.958489f), new Keyframe(0.6399953f, 0.9282017f, 1.060573f, 1.060573f), new Keyframe(0.6899947f, 0.9641005f, 0.5220275f, 0.5220275f), new Keyframe(0.7559938f, 0.9856209f, 0.2206133f, 0.2206133f), new Keyframe(0.8439927f, 0.9957545f, 0.0711851f, 0.0711851f), new Keyframe(1f, 1f, 0.02721322f, 0.02721322f)); } }

        public static AnimationCurve ExpoEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 5.744604f, 5.744604f), new Keyframe(0.028f, 0.1608489f, 4.783195f, 4.783195f), new Keyframe(0.05899996f, 0.2793241f, 3.107495f, 3.107495f), new Keyframe(0.09600002f, 0.3678728f, 1.88221f, 1.88221f), new Keyframe(0.1400001f, 0.4282064f, 1.035915f, 1.035915f), new Keyframe(0.1940002f, 0.4660396f, 0.4992211f, 0.4992211f), new Keyframe(0.2660001f, 0.4874833f, 0.1971408f, 0.1971408f), new Keyframe(0.3609989f, 0.4966462f, 0.06458478f, 0.06458478f), new Keyframe(0.6989946f, 0.5077044f, 0.1110358f, 0.1110358f), new Keyframe(0.7749936f, 0.5220951f, 0.3261757f, 0.3261757f), new Keyframe(0.8309929f, 0.5480226f, 0.6442649f, 0.6442649f), new Keyframe(0.8609925f, 0.5727883f, 1.020204f, 1.020204f), new Keyframe(0.8869922f, 0.6043746f, 1.604182f, 1.604182f), new Keyframe(0.9309916f, 0.692087f, 2.73636f, 2.73636f), new Keyframe(0.9679911f, 0.8208169f, 4.538574f, 4.538574f), new Keyframe(1f, 1f, 5.597916f, 5.597916f)); } }

        public static AnimationCurve CubicEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 2.712801f, 2.712801f), new Keyframe(0.09900002f, 0.2685674f, 2.440202f, 2.440202f), new Keyframe(0.2020002f, 0.4918309f, 1.915563f, 1.915563f), new Keyframe(0.3099996f, 0.6714904f, 1.489522f, 1.489522f), new Keyframe(0.3659988f, 0.7451585f, 1.208114f, 1.208114f), new Keyframe(0.4229981f, 0.807898f, 1.002042f, 1.002042f), new Keyframe(0.4799974f, 0.8593898f, 0.8122928f, 0.8122928f), new Keyframe(0.5399966f, 0.9026619f, 0.6371516f, 0.6371516f), new Keyframe(0.6019958f, 0.9369532f, 0.4774654f, 0.4774654f), new Keyframe(0.666995f, 0.9630723f, 0.3360345f, 0.3360345f), new Keyframe(0.7339941f, 0.9811776f, 0.2144666f, 0.2144666f), new Keyframe(0.8079932f, 0.9929214f, 0.09778383f, 0.09778383f), new Keyframe(1f, 1f, 0.03686665f, 0.03686665f)); } }

        public static AnimationCurve CubicEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.03686408f, 0.03686408f), new Keyframe(0.1920002f, 0.007077911f, 0.09777816f, 0.09777816f), new Keyframe(0.2660001f, 0.01882112f, 0.2144573f, 0.2144573f), new Keyframe(0.3329993f, 0.03692579f, 0.3360234f, 0.3360234f), new Keyframe(0.3979984f, 0.06304404f, 0.4774516f, 0.4774516f), new Keyframe(0.4599976f, 0.09733449f, 0.6371353f, 0.6371353f), new Keyframe(0.5199969f, 0.1406055f, 0.8122748f, 0.8122748f), new Keyframe(0.5769961f, 0.1920962f, 1.002023f, 1.002023f), new Keyframe(0.6339954f, 0.2548346f, 1.208092f, 1.208092f), new Keyframe(0.6899947f, 0.3285014f, 1.489497f, 1.489497f), new Keyframe(0.7979933f, 0.5081568f, 1.915531f, 1.915531f), new Keyframe(0.900992f, 0.7314132f, 2.440172f, 2.440172f), new Keyframe(1f, 1f, 2.712778f, 2.712778f)); } }

        public static AnimationCurve CubicEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.03686401f, 0.03686401f), new Keyframe(0.09600002f, 0.003538946f, 0.1247061f, 0.1247061f), new Keyframe(0.1670002f, 0.0186299f, 0.3459287f, 0.3459287f), new Keyframe(0.2310003f, 0.04930575f, 0.6469367f, 0.6469367f), new Keyframe(0.2889998f, 0.0965501f, 1.021622f, 1.021622f), new Keyframe(0.349999f, 0.1714986f, 1.475532f, 1.475532f), new Keyframe(0.4069983f, 0.2696732f, 1.987467f, 1.987467f), new Keyframe(0.4589976f, 0.3868043f, 2.45385f, 2.45385f), new Keyframe(0.5709962f, 0.6841773f, 2.305769f, 2.305769f), new Keyframe(0.6219956f, 0.7839518f, 1.720932f, 1.720932f), new Keyframe(0.6749949f, 0.862681f, 1.258207f, 1.258207f), new Keyframe(0.739994f, 0.9296912f, 0.8191346f, 0.8191346f), new Keyframe(0.8119931f, 0.9734184f, 0.4391121f, 0.4391121f), new Keyframe(0.8909921f, 0.9948187f, 0.1592124f, 0.1592124f), new Keyframe(1f, 1f, 0.04753099f, 0.04753099f)); } }

        public static AnimationCurve CubicEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 2.665456f, 2.665456f), new Keyframe(0.05799996f, 0.1545963f, 2.352992f, 2.352992f), new Keyframe(0.1180001f, 0.2770282f, 1.759349f, 1.759349f), new Keyframe(0.1810002f, 0.3701532f, 1.228747f, 1.228747f), new Keyframe(0.2490003f, 0.4367473f, 0.7857753f, 0.7857753f), new Keyframe(0.3079996f, 0.4716883f, 0.4508696f, 0.4508696f), new Keyframe(0.3729987f, 0.4918062f, 0.2114633f, 0.2114633f), new Keyframe(0.4359979f, 0.4989513f, 0.06805319f, 0.06805319f), new Keyframe(0.5829961f, 0.5022869f, 0.1004769f, 0.1004769f), new Keyframe(0.6569951f, 0.5154781f, 0.3115331f, 0.3115331f), new Keyframe(0.7259942f, 0.5461692f, 0.6222044f, 0.6222044f), new Keyframe(0.7889934f, 0.5965437f, 1.004278f, 1.004278f), new Keyframe(0.8449927f, 0.6642441f, 1.436203f, 1.436203f), new Keyframe(0.898992f, 0.7540695f, 1.914187f, 1.914187f), new Keyframe(0.9499913f, 0.8644789f, 2.437436f, 2.437436f), new Keyframe(1f, 1f, 2.709952f, 2.709952f)); } }

        public static AnimationCurve QuartEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 3.528984f, 3.528984f), new Keyframe(0.08299999f, 0.2929057f, 3.098549f, 3.098549f), new Keyframe(0.1710002f, 0.5277002f, 2.380212f, 2.380212f), new Keyframe(0.2180003f, 0.6260389f, 1.918066f, 1.918066f), new Keyframe(0.2660001f, 0.7097422f, 1.584128f, 1.584128f), new Keyframe(0.3169995f, 0.7823873f, 1.279033f, 1.279033f), new Keyframe(0.3699988f, 0.8424692f, 1.008335f, 1.008335f), new Keyframe(0.4219981f, 0.8883864f, 0.7751342f, 0.7751342f), new Keyframe(0.4779974f, 0.925751f, 0.5726975f, 0.5726975f), new Keyframe(0.5379966f, 0.9544404f, 0.3984523f, 0.3984523f), new Keyframe(0.6029958f, 0.9751583f, 0.2305797f, 0.2305797f), new Keyframe(0.7499939f, 0.9960934f, 0.07902162f, 0.07902162f), new Keyframe(1f, 1f, 0.01562605f, 0.01562605f)); } }

        public static AnimationCurve QuartEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.01562506f, 0.01562506f), new Keyframe(0.2500003f, 0.00390627f, 0.07901731f, 0.07901731f), new Keyframe(0.3969984f, 0.02484021f, 0.2305698f, 0.2305698f), new Keyframe(0.4619976f, 0.04555739f, 0.3984376f, 0.3984376f), new Keyframe(0.5219969f, 0.07424574f, 0.5726785f, 0.5726785f), new Keyframe(0.5779961f, 0.1116091f, 0.7751111f, 0.7751111f), new Keyframe(0.6299955f, 0.1575251f, 1.008307f, 1.008307f), new Keyframe(0.6829948f, 0.2176053f, 1.279001f, 1.279001f), new Keyframe(0.7339941f, 0.2902487f, 1.58409f, 1.58409f), new Keyframe(0.7819935f, 0.3739492f, 1.91802f, 1.91802f), new Keyframe(0.8289929f, 0.472284f, 2.380151f, 2.380151f), new Keyframe(0.9169918f, 0.7070689f, 3.098492f, 3.098492f), new Keyframe(1f, 1f, 3.52894f, 3.52894f)); } }

        public static AnimationCurve QuartEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.01562503f, 0.01562503f), new Keyframe(0.1250001f, 0.00195313f, 0.07865179f, 0.07865179f), new Keyframe(0.1980002f, 0.01229568f, 0.2678885f, 0.2678885f), new Keyframe(0.2610002f, 0.03712387f, 0.5826137f, 0.5826137f), new Keyframe(0.3149995f, 0.0787643f, 1.03552f, 1.03552f), new Keyframe(0.3709988f, 0.1515584f, 1.64749f, 1.64749f), new Keyframe(0.4209981f, 0.2513105f, 2.396834f, 2.396834f), new Keyframe(0.4659975f, 0.3772459f, 3.22551f, 3.22551f), new Keyframe(0.5249968f, 0.5927359f, 3.360608f, 3.360608f), new Keyframe(0.5599964f, 0.7001424f, 2.706222f, 2.706222f), new Keyframe(0.6039958f, 0.8032616f, 1.998943f, 1.998943f), new Keyframe(0.6519952f, 0.8826641f, 1.346243f, 1.346243f), new Keyframe(0.7119944f, 0.9449581f, 0.7846171f, 0.7846171f), new Keyframe(0.7809935f, 0.9815958f, 0.3611877f, 0.3611877f), new Keyframe(0.8619925f, 0.997098f, 0.106208f, 0.106208f), new Keyframe(1f, 1f, 0.02102806f, 0.02102806f)); } }

        public static AnimationCurve QuartEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 3.45998f, 3.45998f), new Keyframe(0.04799997f, 0.1660789f, 2.971538f, 2.971538f), new Keyframe(0.1f, 0.2952f, 2.069876f, 2.069876f), new Keyframe(0.1560001f, 0.3879729f, 1.321569f, 1.321569f), new Keyframe(0.2190003f, 0.4501215f, 0.756956f, 0.756956f), new Keyframe(0.274f, 0.4791299f, 0.381305f, 0.381305f), new Keyframe(0.3409992f, 0.4948868f, 0.129308f, 0.129308f), new Keyframe(0.6109957f, 0.5012143f, 0.06936252f, 0.06936252f), new Keyframe(0.6889947f, 0.5102068f, 0.239189f, 0.239189f), new Keyframe(0.7569938f, 0.5348964f, 0.5596648f, 0.5596648f), new Keyframe(0.8149931f, 0.5787579f, 1.016198f, 1.016198f), new Keyframe(0.8669924f, 0.645117f, 1.594229f, 1.594229f), new Keyframe(0.9139918f, 0.7349941f, 2.28556f, 2.28556f), new Keyframe(0.9579912f, 0.8519806f, 3.091177f, 3.091177f), new Keyframe(1f, 1f, 3.523537f, 3.523537f)); } }

        public static AnimationCurve QuintEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 4.330001f, 4.330001f), new Keyframe(0.07199997f, 0.3117599f, 3.735899f, 3.735899f), new Keyframe(0.1490001f, 0.5536788f, 2.758875f, 2.758875f), new Keyframe(0.1910002f, 0.653469f, 2.148519f, 2.148519f), new Keyframe(0.2350003f, 0.7379969f, 1.7198f, 1.7198f), new Keyframe(0.2809999f, 0.8078481f, 1.340698f, 1.340698f), new Keyframe(0.3309993f, 0.8659914f, 1.015333f, 1.015333f), new Keyframe(0.3789987f, 0.9076445f, 0.7484275f, 0.7484275f), new Keyframe(0.430998f, 0.9403557f, 0.5290898f, 0.5290898f), new Keyframe(0.4879973f, 0.9648147f, 0.3479316f, 0.3479316f), new Keyframe(0.5529965f, 0.9821534f, 0.1855956f, 0.1855956f), new Keyframe(0.7009946f, 0.99761f, 0.0562155f, 0.0562155f), new Keyframe(1f, 1f, 0.007993056f, 0.007993056f)); } }

        public static AnimationCurve QuintEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.007992506f, 0.007992506f), new Keyframe(0.2989997f, 0.002389757f, 0.05621202f, 0.05621202f), new Keyframe(0.4469978f, 0.01784542f, 0.185586f, 0.185586f), new Keyframe(0.511997f, 0.03518334f, 0.3479164f, 0.3479164f), new Keyframe(0.5689963f, 0.05964129f, 0.5290686f, 0.5290686f), new Keyframe(0.6209956f, 0.0923512f, 0.7484f, 0.7484f), new Keyframe(0.668995f, 0.1340029f, 1.015298f, 1.015298f), new Keyframe(0.7189943f, 0.1921442f, 1.340654f, 1.340654f), new Keyframe(0.7649937f, 0.2619928f, 1.719746f, 1.719746f), new Keyframe(0.8089932f, 0.3465168f, 2.148448f, 2.148448f), new Keyframe(0.8509926f, 0.4463021f, 2.75878f, 2.75878f), new Keyframe(0.9279916f, 0.6882089f, 3.735807f, 3.735807f), new Keyframe(1f, 1f, 4.329928f, 4.329928f)); } }

        public static AnimationCurve QuintEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.01129463f, 0.01129463f), new Keyframe(0.1630002f, 0.001841026f, 0.07887016f, 0.07887016f), new Keyframe(0.2430003f, 0.0135567f, 0.3119798f, 0.3119798f), new Keyframe(0.3089996f, 0.04507226f, 0.7584687f, 0.7584687f), new Keyframe(0.3639989f, 0.1022398f, 1.413462f, 1.413462f), new Keyframe(0.4079983f, 0.1808888f, 2.241844f, 2.241844f), new Keyframe(0.4479978f, 0.2887349f, 3.1983f, 3.1983f), new Keyframe(0.4789974f, 0.4034462f, 4.101107f, 4.101107f), new Keyframe(0.5299968f, 0.6330353f, 3.904679f, 3.904679f), new Keyframe(0.5689963f, 0.7620283f, 2.774437f, 2.774437f), new Keyframe(0.6139957f, 0.8628863f, 1.799552f, 1.799552f), new Keyframe(0.665995f, 0.9334903f, 1.048361f, 1.048361f), new Keyframe(0.7159944f, 0.9704366f, 0.5394579f, 0.5394579f), new Keyframe(0.7769936f, 0.9911752f, 0.2216771f, 0.2216771f), new Keyframe(0.8509926f, 0.9988247f, 0.05563026f, 0.05563026f), new Keyframe(1f, 1f, 0.007887824f, 0.007887824f)); } }

        public static AnimationCurve QuintEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 4.330001f, 4.330001f), new Keyframe(0.03599999f, 0.15588f, 3.732531f, 3.732531f), new Keyframe(0.07499997f, 0.2781473f, 2.633865f, 2.633865f), new Keyframe(0.1180001f, 0.3698523f, 1.729284f, 1.729284f), new Keyframe(0.1660002f, 0.4334955f, 1.023763f, 1.023763f), new Keyframe(0.2200003f, 0.4724635f, 0.5150232f, 0.5150232f), new Keyframe(0.2859999f, 0.4928189f, 0.1951178f, 0.1951178f), new Keyframe(0.3649988f, 0.4992825f, 0.04744737f, 0.04744737f), new Keyframe(0.6849948f, 0.5034667f, 0.1010192f, 0.1010192f), new Keyframe(0.7509939f, 0.5159381f, 0.3359873f, 0.3359873f), new Keyframe(0.8039932f, 0.5415374f, 0.6602474f, 0.6602474f), new Keyframe(0.8349928f, 0.567499f, 1.014021f, 1.014021f), new Keyframe(0.8629925f, 0.6008343f, 1.514281f, 1.514281f), new Keyframe(0.9139918f, 0.6945713f, 2.378709f, 2.378709f), new Keyframe(0.9589912f, 0.8259432f, 3.581897f, 3.581897f), new Keyframe(1f, 1f, 4.24438f, 4.24438f)); } }

        public static AnimationCurve CircEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 19.97499f, 19.97499f), new Keyframe(0.005f, 0.09987493f, 13.34938f, 13.34938f), new Keyframe(0.019f, 0.1940077f, 5.363557f, 5.363557f), new Keyframe(0.04299998f, 0.2900878f, 3.400225f, 3.400225f), new Keyframe(0.07599998f, 0.3823924f, 2.456375f, 2.456375f), new Keyframe(0.1180001f, 0.4712495f, 1.893083f, 1.893083f), new Keyframe(0.1680002f, 0.5547759f, 1.510095f, 1.510095f), new Keyframe(0.2270003f, 0.6344064f, 1.226405f, 1.226405f), new Keyframe(0.2929998f, 0.7072133f, 1.005035f, 1.005035f), new Keyframe(0.3649988f, 0.7725112f, 0.8247947f, 0.8247947f), new Keyframe(0.4439978f, 0.8311808f, 0.6714305f, 0.6714305f), new Keyframe(0.5279968f, 0.8815968f, 0.5368982f, 0.5368982f), new Keyframe(0.6169956f, 0.9237465f, 0.4157177f, 0.4157177f), new Keyframe(0.7099944f, 0.9570249f, 0.3040619f, 0.3040619f), new Keyframe(0.8049932f, 0.9808019f, 0.1992874f, 0.1992874f), new Keyframe(0.901992f, 0.9951856f, 0.09870509f, 0.09870509f), new Keyframe(1f, 1f, 0.04912236f, 0.04912236f)); } }

        public static AnimationCurve CircEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.04911823f, 0.04911823f), new Keyframe(0.09800002f, 0.004813587f, 0.09869924f, 0.09869924f), new Keyframe(0.1950002f, 0.0191968f, 0.1992801f, 0.1992801f), new Keyframe(0.2899998f, 0.0429733f, 0.3040551f, 0.3040551f), new Keyframe(0.3829986f, 0.07625108f, 0.4157104f, 0.4157104f), new Keyframe(0.4719975f, 0.1184001f, 0.5368896f, 0.5368896f), new Keyframe(0.5559964f, 0.1688153f, 0.6714204f, 0.6714204f), new Keyframe(0.6349954f, 0.2274841f, 0.8247823f, 0.8247823f), new Keyframe(0.7069945f, 0.2927809f, 1.005019f, 1.005019f), new Keyframe(0.7729936f, 0.3655862f, 1.22638f, 1.22638f), new Keyframe(0.8319929f, 0.4452137f, 1.510052f, 1.510052f), new Keyframe(0.8819922f, 0.5287361f, 1.893003f, 1.893003f), new Keyframe(0.9239917f, 0.6175874f, 2.456207f, 2.456207f), new Keyframe(0.9569913f, 0.7098832f, 3.399782f, 3.399782f), new Keyframe(0.9809909f, 0.8059465f, 5.361624f, 5.361624f), new Keyframe(0.9949908f, 0.9000331f, 13.33854f, 13.33854f), new Keyframe(1f, 1f, 19.95652f, 19.95652f)); } }

        public static AnimationCurve CircEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.1165625f, 0.1165625f), new Keyframe(0.1150001f, 0.0134047f, 0.239202f, 0.239202f), new Keyframe(0.2230003f, 0.05248366f, 0.5064324f, 0.5064324f), new Keyframe(0.3189994f, 0.1149814f, 0.8485672f, 0.8485672f), new Keyframe(0.3989984f, 0.1986692f, 1.332243f, 1.332243f), new Keyframe(0.4479978f, 0.2779685f, 2.119068f, 2.119068f), new Keyframe(0.4819973f, 0.3670392f, 3.918075f, 3.918075f), new Keyframe(0.4969971f, 0.445284f, 9.937382f, 9.937382f), new Keyframe(0.5059971f, 0.5772082f, 9.228888f, 9.228888f), new Keyframe(0.5319967f, 0.6759913f, 2.892379f, 2.892379f), new Keyframe(0.5799961f, 0.7712872f, 1.612492f, 1.612492f), new Keyframe(0.6459953f, 0.8531015f, 1.030909f, 1.030909f), new Keyframe(0.7219943f, 0.9155873f, 0.6789858f, 0.6789858f), new Keyframe(0.8079932f, 0.9616637f, 0.4202141f, 0.4202141f), new Keyframe(0.901992f, 0.9903004f, 0.2018084f, 0.2018084f), new Keyframe(1f, 1f, 0.09896783f, 0.09896783f)); } }

        public static AnimationCurve CircEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 8.713385f, 8.713385f), new Keyframe(0.013f, 0.113274f, 5.750962f, 5.750962f), new Keyframe(0.05199997f, 0.222027f, 2.164071f, 2.164071f), new Keyframe(0.1150001f, 0.319022f, 1.248537f, 1.248537f), new Keyframe(0.1980002f, 0.3984923f, 0.7882434f, 0.7882434f), new Keyframe(0.278f, 0.4480134f, 0.5015587f, 0.5015587f), new Keyframe(0.3649988f, 0.4814298f, 0.2907181f, 0.2907181f), new Keyframe(0.4419979f, 0.4966244f, 0.1345652f, 0.1345652f), new Keyframe(0.5809961f, 0.506604f, 0.170126f, 0.170126f), new Keyframe(0.6769949f, 0.5323753f, 0.3872727f, 0.3872727f), new Keyframe(0.7719936f, 0.5804533f, 0.65813f, 0.65813f), new Keyframe(0.8539926f, 0.6468863f, 1.015686f, 1.015686f), new Keyframe(0.9159918f, 0.7225999f, 1.54738f, 1.54738f), new Keyframe(0.9619912f, 0.8087825f, 2.57383f, 2.57383f), new Keyframe(0.9899908f, 0.9004561f, 6.609685f, 6.609685f), new Keyframe(1f, 1f, 9.945268f, 9.945268f)); } }

        public static AnimationCurve SineEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 1.548781f, 1.548781f), new Keyframe(0.1850002f, 0.2865249f, 1.497203f, 1.497203f), new Keyframe(0.3209994f, 0.4831288f, 1.368925f, 1.368925f), new Keyframe(0.4459978f, 0.6446549f, 1.197663f, 1.197663f), new Keyframe(0.5609964f, 0.7715099f, 0.9944606f, 0.9944606f), new Keyframe(0.6749949f, 0.8724921f, 0.7657173f, 0.7657173f), new Keyframe(0.7849935f, 0.9435087f, 0.5190955f, 0.5190955f), new Keyframe(0.8929921f, 0.9859065f, 0.262141f, 0.262141f), new Keyframe(1f, 1f, 0.1317054f, 0.1317054f)); } }

        public static AnimationCurve SineEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.1316955f, 0.1316955f), new Keyframe(0.107f, 0.01409143f, 0.2621276f, 0.2621276f), new Keyframe(0.2150003f, 0.05648797f, 0.5190804f, 0.5190804f), new Keyframe(0.3249994f, 0.1275035f, 0.7657046f, 0.7657046f), new Keyframe(0.4389979f, 0.2284843f, 0.9944497f, 0.9944497f), new Keyframe(0.5539964f, 0.3553382f, 1.197654f, 1.197654f), new Keyframe(0.6789948f, 0.5168633f, 1.368918f, 1.368918f), new Keyframe(0.8149931f, 0.713465f, 1.4972f, 1.4972f), new Keyframe(1f, 1f, 1.54878f, 1.54878f)); } }

        public static AnimationCurve SineEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 1.50548f, 1.50548f), new Keyframe(0.1600001f, 0.240877f, 1.399913f, 1.399913f), new Keyframe(0.2220003f, 0.3211266f, 1.20002f, 1.20002f), new Keyframe(0.2799999f, 0.3852566f, 0.9926128f, 0.9926128f), new Keyframe(0.3409992f, 0.4389073f, 0.7501102f, 0.7501102f), new Keyframe(0.3999984f, 0.4755275f, 0.487473f, 0.487473f), new Keyframe(0.4549977f, 0.4950113f, 0.2259122f, 0.2259122f), new Keyframe(0.5319967f, 0.502524f, 0.1812712f, 0.1812712f), new Keyframe(0.5759962f, 0.5141827f, 0.3807511f, 0.3807511f), new Keyframe(0.6289955f, 0.5404983f, 0.6175001f, 0.6175001f), new Keyframe(0.6829948f, 0.5803753f, 0.8613304f, 0.8613304f), new Keyframe(0.7489939f, 0.6453309f, 1.102256f, 1.102256f), new Keyframe(0.818993f, 0.7307524f, 1.353911f, 1.353911f), new Keyframe(1f, 1f, 1.487499f, 1.487499f)); } }

        public static AnimationCurve SineEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 1.50548f, 1.50548f), new Keyframe(0.1600001f, 0.240877f, 1.399913f, 1.399913f), new Keyframe(0.2220003f, 0.3211266f, 1.20002f, 1.20002f), new Keyframe(0.2799999f, 0.3852566f, 0.9926128f, 0.9926128f), new Keyframe(0.3409992f, 0.4389073f, 0.7501102f, 0.7501102f), new Keyframe(0.3999984f, 0.4755275f, 0.487473f, 0.487473f), new Keyframe(0.4549977f, 0.4950113f, 0.2259122f, 0.2259122f), new Keyframe(0.5319967f, 0.502524f, 0.1812712f, 0.1812712f), new Keyframe(0.5759962f, 0.5141827f, 0.3807511f, 0.3807511f), new Keyframe(0.6289955f, 0.5404983f, 0.6175001f, 0.6175001f), new Keyframe(0.6829948f, 0.5803753f, 0.8613304f, 0.8613304f), new Keyframe(0.7489939f, 0.6453309f, 1.102256f, 1.102256f), new Keyframe(0.818993f, 0.7307524f, 1.353911f, 1.353911f), new Keyframe(1f, 1f, 1.487499f, 1.487499f)); } }

        // public static AnimationCurve ElasticEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 10.36873f, 10.36873f), new Keyframe(0.021f, 0.2177434f, 12.19689f, 12.19689f), new Keyframe(0.08499999f, 1.115346f, 11.11521f, 11.11521f), new Keyframe(0.107f, 1.295865f, 6.376305f, 6.376305f), new Keyframe(0.1180001f, 1.345884f, 3.371145f, 3.371145f), new Keyframe(0.1290001f, 1.37003f, 1.352682f, 1.352682f), new Keyframe(0.1350001f, 1.373092f, -0.07435381f, -0.07435381f), new Keyframe(0.1420001f, 1.368479f, -1.505643f, -1.505643f), new Keyframe(0.1570001f, 1.333195f, -3.58006f, -3.58006f), new Keyframe(0.2340003f, 0.9629892f, -3.892906f, -3.892906f), new Keyframe(0.2560003f, 0.8974742f, -2.122678f, -2.122678f), new Keyframe(0.278f, 0.8695918f, -0.6290157f, -0.6290157f), new Keyframe(0.2919998f, 0.869723f, 0.4204966f, 0.4204966f), new Keyframe(0.3069996f, 0.8821972f, 1.267978f, 1.267978f), new Keyframe(0.3829986f, 1.011725f, 1.392157f, 1.392157f), new Keyframe(0.4049983f, 1.035484f, 0.7774903f, 0.7774903f), new Keyframe(0.426998f, 1.045934f, 0.1661084f, 0.1661084f), new Keyframe(0.4569977f, 1.04165f, -0.3734318f, -0.3734318f), new Keyframe(0.5319967f, 0.9963446f, -0.4466513f, -0.4466513f), new Keyframe(0.5749962f, 0.9839084f, -0.07087262f, -0.07087262f), new Keyframe(0.7219943f, 1.005587f, 0.0479492f, 0.0479492f), new Keyframe(0.8669924f, 0.9981084f, -0.01867645f, -0.01867645f), new Keyframe(1f, 1f, 0.01422139f, 0.01422139f)); } }

        // public static AnimationCurve ElasticEaseIn { get { return new AnimationCurve(new Keyframe(0f, -0.0004882813f, 0.01817452f, 0.01817452f), new Keyframe(0.1320001f, 0.001910757f, -0.01658952f, -0.01658952f), new Keyframe(0.278f, -0.005586856f, 0.04806209f, 0.04806209f), new Keyframe(0.4249981f, 0.01609209f, -0.07084332f, -0.07084332f), new Keyframe(0.4679975f, 0.003658183f, -0.446627f, -0.446627f), new Keyframe(0.5429966f, -0.04164798f, -0.3734932f, -0.3734932f), new Keyframe(0.5729962f, -0.04593482f, 0.1659742f, 0.1659742f), new Keyframe(0.5949959f, -0.03548837f, 0.777335f, 0.777335f), new Keyframe(0.6169956f, -0.01173252f, 1.392085f, 1.392085f), new Keyframe(0.6929947f, 0.117796f, 1.268128f, 1.268128f), new Keyframe(0.7079945f, 0.1302745f, 0.4208228f, 0.4208228f), new Keyframe(0.7219943f, 0.1304108f, -0.6286148f, -0.6286148f), new Keyframe(0.743994f, 0.1025379f, -2.122234f, -2.122234f), new Keyframe(0.7659937f, 0.03703367f, -3.892709f, -3.892709f), new Keyframe(0.8429927f, -0.3331709f, -3.580593f, -3.580593f), new Keyframe(0.8579925f, -0.3684695f, -1.506784f, -1.506784f), new Keyframe(0.8649924f, -0.3730915f, -0.07569993f, -0.07569993f), new Keyframe(0.8709924f, -0.3700382f, 1.351186f, 1.351186f), new Keyframe(0.8819922f, -0.3459103f, 3.369501f, 3.369501f), new Keyframe(0.8929921f, -0.2959101f, 6.374571f, 6.374571f), new Keyframe(0.9149918f, -0.1154329f, 11.11417f, 11.11417f), new Keyframe(0.978991f, 0.7821381f, 12.19732f, 12.19732f), new Keyframe(1f, 1f, 10.36992f, 10.36992f)); } }

        // public static AnimationCurve ElasticEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 8.478915E-05f, -0.02518584f, -0.02518584f), new Keyframe(0.1920002f, -0.004750898f, 0.08651267f, 0.08651267f), new Keyframe(0.2240003f, 0.001591873f, 0.2956483f, 0.2956483f), new Keyframe(0.279f, 0.02321145f, 0.2010942f, 0.2010942f), new Keyframe(0.3019997f, 0.02342081f, -0.2830561f, -0.2830561f), new Keyframe(0.3189994f, 0.01364228f, -0.879728f, -0.879728f), new Keyframe(0.3349992f, -0.005305331f, -1.525205f, -1.525205f), new Keyframe(0.3909985f, -0.1098095f, -1.310914f, -1.310914f), new Keyframe(0.4019984f, -0.1181216f, -0.1918603f, -0.1918603f), new Keyframe(0.4119982f, -0.1144023f, 1.39209f, 1.39209f), new Keyframe(0.429998f, -0.07098248f, 3.977216f, 3.977216f), new Keyframe(0.4479978f, 0.02877569f, 7.339958f, 7.339958f), new Keyframe(0.5469965f, 0.933399f, 7.827611f, 7.827611f), new Keyframe(0.5639963f, 1.044195f, 4.966269f, 4.966269f), new Keyframe(0.5819961f, 1.105665f, 2.226186f, 2.226186f), new Keyframe(0.5939959f, 1.118113f, 0.2877505f, 0.2877505f), new Keyframe(0.6069958f, 1.112109f, -1.151576f, -1.151576f), new Keyframe(0.664995f, 1.005314f, -1.503408f, -1.503408f), new Keyframe(0.6819948f, 0.9855007f, -0.8531691f, -0.8531691f), new Keyframe(0.6989946f, 0.9763065f, -0.2543923f, -0.2543923f), new Keyframe(0.7219943f, 0.9770438f, 0.2138309f, 0.2138309f), new Keyframe(0.7759936f, 0.9984062f, 0.2969316f, 0.2969316f), new Keyframe(0.8079932f, 1.00475f, 0.08675907f, 0.08675907f), new Keyframe(1f, 1f, -0.02474064f, -0.02474064f)); } }

        public static AnimationCurve ElasticEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 13.09082f, 13.09082f), new Keyframe(0.04299998f, 0.5629051f, 10.53881f, 10.53881f), new Keyframe(0.05399996f, 0.6507598f, 5.57284f, 5.57284f), new Keyframe(0.06499995f, 0.6855074f, 1.304553f, 1.304553f), new Keyframe(0.07299997f, 0.6811092f, -1.746875f, -1.746875f), new Keyframe(0.08099999f, 0.6575573f, -3.867259f, -3.867259f), new Keyframe(0.1200001f, 0.4707259f, -3.690704f, -3.690704f), new Keyframe(0.1300001f, 0.4448172f, -1.779534f, -1.779534f), new Keyframe(0.1410001f, 0.4341669f, -0.1236582f, -0.1236582f), new Keyframe(0.1570001f, 0.4457011f, 1.217627f, 1.217627f), new Keyframe(0.1940002f, 0.5091329f, 1.205191f, 1.205191f), new Keyframe(0.2140003f, 0.5230532f, 0.2492215f, 0.2492215f), new Keyframe(0.2310003f, 0.5196945f, -0.3958523f, -0.3958523f), new Keyframe(0.2710001f, 0.4959292f, -0.4010276f, -0.4010276f), new Keyframe(0.2909998f, 0.4917709f, -0.02522608f, -0.02522608f), new Keyframe(0.3609989f, 0.5027934f, 0.06834626f, 0.06834626f), new Keyframe(0.6349954f, 0.4971014f, 0.06094041f, 0.06094041f), new Keyframe(0.7119944f, 0.5080857f, -0.1464642f, -0.1464642f), new Keyframe(0.7839935f, 0.4767241f, 0.07606834f, 0.07606834f), new Keyframe(0.8039932f, 0.4884783f, 1.134078f, 1.134078f), new Keyframe(0.8439927f, 0.5556949f, 1.120004f, 1.120004f), new Keyframe(0.8609925f, 0.5652075f, -0.314763f, -0.314763f), new Keyframe(0.8709924f, 0.5533167f, -1.967482f, -1.967482f), new Keyframe(0.8809922f, 0.5258583f, -3.797368f, -3.797368f), new Keyframe(0.9179918f, 0.3464525f, -4.019552f, -4.019552f), new Keyframe(0.9259917f, 0.3209309f, -2.041322f, -2.041322f), new Keyframe(0.9339916f, 0.3137918f, 1.02884f, 1.02884f), new Keyframe(0.9459914f, 0.3491924f, 5.466541f, 5.466541f), new Keyframe(0.9569913f, 0.4370041f, 10.53663f, 10.53663f), new Keyframe(1f, 1f, 13.09026f, 13.09026f)); } }

        public static AnimationCurve BounceEaseOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.166375f, 0.166375f), new Keyframe(0.022f, 0.003660251f, 0.3365312f, 0.3365312f), new Keyframe(0.04499998f, 0.01531405f, 0.7638124f, 0.7638124f), new Keyframe(0.09f, 0.06125626f, 1.36125f, 1.36125f), new Keyframe(0.1350001f, 0.1378268f, 2.045658f, 2.045658f), new Keyframe(0.1810002f, 0.2477556f, 2.733847f, 2.733847f), new Keyframe(0.2260003f, 0.3862632f, 3.418253f, 3.418253f), new Keyframe(0.2710001f, 0.5553978f, 4.264294f, 4.264294f), new Keyframe(0.3639989f, 0.9990041f, 4.770022f, -2.404209f), new Keyframe(0.4089983f, 0.8908161f, -2.060119f, -2.060119f), new Keyframe(0.4549977f, 0.8118798f, -1.371941f, -1.371941f), new Keyframe(0.4999971f, 0.765627f, -0.7707295f, -0.7707295f), new Keyframe(0.5229968f, 0.7538142f, -0.3396727f, -0.3396727f), new Keyframe(0.5459965f, 0.7500022f, 0.004415691f, 0.004415691f), new Keyframe(0.5679963f, 0.7538427f, 0.344725f, 0.344725f), new Keyframe(0.590996f, 0.7656848f, 0.7720006f, 0.7720006f), new Keyframe(0.6359954f, 0.8119947f, 1.373211f, 1.373211f), new Keyframe(0.6819948f, 0.8909895f, 2.06139f, 2.06139f), new Keyframe(0.7269942f, 0.9992347f, 2.40548f, -1.006366f), new Keyframe(0.7729936f, 0.9529424f, -0.7617323f, -0.7617323f), new Keyframe(0.7949933f, 0.9415664f, -0.3469441f, -0.3469441f), new Keyframe(0.817993f, 0.9375003f, -0.002855882f, -0.002855882f), new Keyframe(0.8409927f, 0.941435f, 0.3450143f, 0.3450143f), new Keyframe(0.8639925f, 0.9533707f, 0.7760727f, 0.7760727f), new Keyframe(0.9089919f, 0.9998639f, 0.2635064f, 0.2635064f), new Keyframe(0.9319916f, 0.9882219f, -0.3366877f, -0.3366877f), new Keyframe(0.9549913f, 0.9843765f, 0.006743193f, 0.006743193f), new Keyframe(0.977991f, 0.9885321f, 0.3508679f, 0.3508679f), new Keyframe(1f, 1f, 0.5210562f, 0.5210562f)); } }

        public static AnimationCurve BounceEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.521125f, 0.521125f), new Keyframe(0.022f, 0.01146475f, 0.3509688f, 0.3509688f), new Keyframe(0.04499998f, 0.01562344f, 0.006875299f, 0.006875299f), new Keyframe(0.06799996f, 0.01178101f, -0.3369233f, -0.3369233f), new Keyframe(0.09100001f, 0.0001249452f, -0.5067846f, 1.033312f), new Keyframe(0.1360001f, 0.04662407f, 0.7761862f, 0.7761862f), new Keyframe(0.1590001f, 0.05856249f, 0.3451228f, 0.3451228f), new Keyframe(0.1820002f, 0.06249975f, 0.001028411f, 0.001028411f), new Keyframe(0.2040002f, 0.05877892f, -0.3392848f, -0.3392848f), new Keyframe(0.2270003f, 0.04706175f, -0.7581172f, -0.7581172f), new Keyframe(0.273f, 0.0007495246f, -1.006793f, 2.405566f), new Keyframe(0.3179995f, 0.1089986f, 2.065259f, 2.065259f), new Keyframe(0.3629989f, 0.1866204f, 1.384642f, 1.384642f), new Keyframe(0.4079983f, 0.2336148f, 0.7872121f, 0.7872121f), new Keyframe(0.430998f, 0.2458067f, 0.3561553f, 0.3561553f), new Keyframe(0.4539977f, 0.2499977f, 0.00828483f, 0.00828483f), new Keyframe(0.4769974f, 0.2461878f, -0.3358045f, -0.3358045f), new Keyframe(0.4989971f, 0.2350569f, -0.7630804f, -0.7630804f), new Keyframe(0.5449966f, 0.1881281f, -1.364292f, -1.364292f), new Keyframe(0.589996f, 0.111252f, -2.05247f, -2.05247f), new Keyframe(0.6359954f, 0.001011657f, -2.39656f, 4.769598f), new Keyframe(0.7289942f, 0.4445786f, 4.264126f, 4.264126f), new Keyframe(0.7739936f, 0.6137159f, 3.418347f, 3.418347f), new Keyframe(0.818993f, 0.7522259f, 2.737731f, 2.737731f), new Keyframe(0.8639925f, 0.8601085f, 2.053333f, 2.053333f), new Keyframe(0.9099919f, 0.9387327f, 1.365154f, 1.365154f), new Keyframe(0.9549913f, 0.98468f, 0.7639434f, 0.7639434f), new Keyframe(0.977991f, 0.9963368f, 0.3366325f, 0.3366325f), new Keyframe(1f, 1f, 0.1664428f, 0.1664428f)); } }

        public static AnimationCurve BounceEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.35475f, 0.35475f), new Keyframe(0.022f, 0.0078045f, 0.01443765f, 0.01443765f), new Keyframe(0.04499998f, 0.0003093889f, 0.330406f, 0.330406f), new Keyframe(0.06899996f, 0.02398985f, 0.6583436f, 0.6583436f), new Keyframe(0.09100001f, 0.03124988f, -0.002750158f, -0.002750158f), new Keyframe(0.1130001f, 0.02386884f, -0.675814f, -0.675814f), new Keyframe(0.1360001f, 0.000497868f, -1.016127f, 2.032646f), new Keyframe(0.1820002f, 0.09399976f, 1.534695f, 1.534695f), new Keyframe(0.2040002f, 0.1168082f, 0.6964303f, 0.6964303f), new Keyframe(0.2270003f, 0.1249989f, 0.01580395f, 0.01580395f), new Keyframe(0.2490003f, 0.1178597f, -0.6648198f, -0.6648198f), new Keyframe(0.272f, 0.09474194f, -1.353001f, -1.353001f), new Keyframe(0.2949997f, 0.05562239f, -2.048742f, -2.048742f), new Keyframe(0.3179995f, 0.000501003f, -2.396613f, 4.675514f), new Keyframe(0.3709988f, 0.2483001f, 3.888093f, 3.888093f), new Keyframe(0.4239981f, 0.4126336f, 2.533492f, 2.533492f), new Keyframe(0.4459978f, 0.4558919f, 1.633566f, 1.633566f), new Keyframe(0.4679975f, 0.4845096f, 0.9983245f, 0.9983245f), new Keyframe(0.4859973f, 0.4970343f, 0.4755524f, 0.4755524f), new Keyframe(0.5189969f, 0.5054584f, 0.588901f, 0.588901f), new Keyframe(0.5419966f, 0.5266762f, 1.285521f, 1.285521f), new Keyframe(0.5669963f, 0.5678886f, 2.019075f, 2.019075f), new Keyframe(0.590996f, 0.625239f, 2.911438f, 2.911438f), new Keyframe(0.6359954f, 0.779733f, 4.105585f, 4.105585f), new Keyframe(0.6819948f, 0.9995148f, 4.777926f, -2.396785f), new Keyframe(0.7049945f, 0.9443894f, -2.048915f, -2.048915f), new Keyframe(0.7279942f, 0.9052659f, -1.360738f, -1.360738f), new Keyframe(0.7499939f, 0.8828167f, -0.6801221f, -0.6801221f), new Keyframe(0.7729936f, 0.8750011f, 0.0004945099f, 0.0004945099f), new Keyframe(0.7949933f, 0.8824986f, 0.6811106f, 0.6811106f), new Keyframe(0.817993f, 0.905991f, 1.527259f, 1.527259f), new Keyframe(0.8639925f, 0.9995123f, 2.033099f, -1.016358f), new Keyframe(0.8869922f, 0.9761364f, -0.6760508f, -0.6760508f), new Keyframe(0.9089919f, 0.9687501f, -0.002996519f, -0.002996519f), new Keyframe(0.9309916f, 0.9760045f, 0.658464f, 0.658464f), new Keyframe(0.9549913f, 0.9996965f, 0.3305174f, 0.3305174f), new Keyframe(0.977991f, 0.9921953f, 0.01423536f, 0.01423536f), new Keyframe(1f, 1f, 0.3546134f, 0.3546134f)); } }

        public static AnimationCurve BounceEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 0f, 0.33275f, 0.33275f), new Keyframe(0.022f, 0.007320501f, 0.6730623f, 0.6730623f), new Keyframe(0.04499998f, 0.0306281f, 1.535187f, 1.535187f), new Keyframe(0.09100001f, 0.1252501f, 2.745188f, 2.745188f), new Keyframe(0.1360001f, 0.2797524f, 4.105244f, 4.105244f), new Keyframe(0.1820002f, 0.4995f, 4.777112f, -2.396619f), new Keyframe(0.2050002f, 0.4443776f, -2.048743f, -2.048743f), new Keyframe(0.2280003f, 0.4052576f, -1.360554f, -1.360554f), new Keyframe(0.2500003f, 0.3828123f, -0.6799299f, -0.6799299f), new Keyframe(0.273f, 0.3750011f, 0.0006885529f, 0.0006885529f), new Keyframe(0.2949997f, 0.3825029f, 0.6813046f, 0.6813046f), new Keyframe(0.3179995f, 0.4059997f, 1.527165f, 1.527165f), new Keyframe(0.3639989f, 0.4995036f, 2.032717f, -1.031289f), new Keyframe(0.3859986f, 0.4768155f, -0.6909808f, -0.6909808f), new Keyframe(0.4089983f, 0.4687501f, -0.01036425f, -0.01036425f), new Keyframe(0.430998f, 0.4760088f, 0.6583821f, 0.6583821f), new Keyframe(0.4549977f, 0.4996922f, 0.3304357f, 0.3304357f), new Keyframe(0.4779974f, 0.4921955f, 0.02491324f, 0.02491324f), new Keyframe(0.5189969f, 0.507602f, 0.04768628f, 0.04768628f), new Keyframe(0.5449966f, 0.5003117f, 0.3729249f, 0.3729249f), new Keyframe(0.5659963f, 0.5218627f, 0.7386848f, 0.7386848f), new Keyframe(0.585996f, 0.5308849f, 0.1108088f, 0.1108088f), new Keyframe(0.6109957f, 0.5251475f, -0.6076199f, -0.6076199f), new Keyframe(0.6359954f, 0.5005043f, -0.98574f, 2.075445f), new Keyframe(0.6789948f, 0.5897473f, 1.616679f, 1.616679f), new Keyframe(0.6989946f, 0.6129053f, 0.8478519f, 0.8478519f), new Keyframe(0.7199943f, 0.6241987f, 0.1974846f, 0.1974846f), new Keyframe(0.743994f, 0.6207711f, -0.5133803f, -0.5133803f), new Keyframe(0.7689937f, 0.5986729f, -1.262059f, -1.262059f), new Keyframe(0.7939934f, 0.5576689f, -2.010737f, -2.010737f), new Keyframe(0.817993f, 0.5005186f, -2.381296f, 4.776113f), new Keyframe(0.8639925f, 0.7202169f, 4.104863f, 4.104863f), new Keyframe(0.9089919f, 0.8747275f, 2.745433f, 2.745433f), new Keyframe(0.9549913f, 0.96936f, 1.53545f, 1.53545f), new Keyframe(0.977991f, 0.9926735f, 0.673265f, 0.673265f), new Keyframe(1f, 1f, 0.3328856f, 0.3328856f)); } }

        // public static AnimationCurve BackEaseOut { get { return new AnimationCurve(new Keyframe(0f, 2.220446E-16f, 4.159023f, 4.159023f), new Keyframe(0.088f, 0.365994f, 3.6479f, 3.6479f), new Keyframe(0.1800002f, 0.6545781f, 2.782354f, 2.782354f), new Keyframe(0.2280003f, 0.7711191f, 2.207131f, 2.207131f), new Keyframe(0.277f, 0.8684487f, 1.778697f, 1.778697f), new Keyframe(0.3279993f, 0.9485719f, 1.376616f, 1.376616f), new Keyframe(0.3809986f, 1.011226f, 1.015597f, 1.015597f), new Keyframe(0.427998f, 1.05113f, 0.7071039f, 0.7071039f), new Keyframe(0.4779974f, 1.079388f, 0.4364755f, 0.4364755f), new Keyframe(0.5299968f, 1.095392f, 0.194539f, 0.194539f), new Keyframe(0.585996f, 1.099946f, -0.03897001f, -0.03897001f), new Keyframe(0.6869947f, 1.083861f, -0.2411657f, -0.2411657f), new Keyframe(0.9099919f, 1.011815f, -0.2271752f, -0.2271752f), new Keyframe(1f, 1f, -0.1312694f, -0.1312694f)); } }

        // public static AnimationCurve BackEaseIn { get { return new AnimationCurve(new Keyframe(0f, 0f, -0.1312594f, -0.1312594f), new Keyframe(0.09f, -0.01181335f, -0.2272844f, -0.2272844f), new Keyframe(0.3119995f, -0.0835879f, -0.2418396f, -0.2418396f), new Keyframe(0.4139982f, -0.09994541f, -0.03954107f, -0.03954107f), new Keyframe(0.4699975f, -0.09539336f, 0.1945146f, 0.1945146f), new Keyframe(0.5219969f, -0.079391f, 0.4364468f, 0.4364468f), new Keyframe(0.5719962f, -0.05113376f, 0.7070708f, 0.7070708f), new Keyframe(0.6189956f, -0.01123176f, 1.015559f, 1.015559f), new Keyframe(0.6719949f, 0.05142021f, 1.376572f, 1.376572f), new Keyframe(0.7229943f, 0.131541f, 1.778649f, 1.778649f), new Keyframe(0.7719936f, 0.2288675f, 2.207076f, 2.207076f), new Keyframe(0.819993f, 0.3454038f, 2.782284f, 2.782284f), new Keyframe(0.9119918f, 0.6339763f, 3.647836f, 3.647836f), new Keyframe(1f, 1f, 4.158975f, 4.158975f)); } }

        // public static AnimationCurve BackEaseInOut { get { return new AnimationCurve(new Keyframe(0f, 0f, -0.2455352f, -0.2455352f), new Keyframe(0.05599996f, -0.01374996f, -0.410437f, -0.410437f), new Keyframe(0.1700002f, -0.07933869f, -0.4785642f, -0.4785642f), new Keyframe(0.2180003f, -0.09766463f, -0.171038f, -0.171038f), new Keyframe(0.2690001f, -0.09563924f, 0.3434681f, 0.3434681f), new Keyframe(0.3139995f, -0.0665146f, 0.9432822f, 0.9432822f), new Keyframe(0.3419991f, -0.03181348f, 1.496781f, 1.496781f), new Keyframe(0.3679988f, 0.01379564f, 2.173368f, 2.173368f), new Keyframe(0.4179982f, 0.1434198f, 3.206171f, 3.206171f), new Keyframe(0.4639976f, 0.3191295f, 4.454323f, 4.454323f), new Keyframe(0.5259968f, 0.6346325f, 4.665299f, 4.665299f), new Keyframe(0.5619963f, 0.7873345f, 3.691966f, 3.691966f), new Keyframe(0.6069958f, 0.9287296f, 2.597142f, 2.597142f), new Keyframe(0.6549951f, 1.027231f, 1.681778f, 1.681778f), new Keyframe(0.6809948f, 1.061327f, 1.084659f, 1.084659f), new Keyframe(0.7079945f, 1.08449f, 0.6545982f, 0.6545982f), new Keyframe(0.7359941f, 1.097126f, 0.2697054f, 0.2697054f), new Keyframe(0.7669937f, 1.099858f, -0.1077417f, -0.1077417f), new Keyframe(0.822993f, 1.082857f, -0.4357334f, -0.4357334f), new Keyframe(0.9469914f, 1.012441f, -0.4012879f, -0.4012879f), new Keyframe(1f, 1f, -0.2347007f, -0.2347007f)); } }

        public static AnimationCurve BackEaseOutIn { get { return new AnimationCurve(new Keyframe(0f, 1.110223E-16f, 4.159023f, 4.159023f), new Keyframe(0.04399998f, 0.1829969f, 3.647901f, 3.647901f), new Keyframe(0.09f, 0.3272888f, 2.673029f, 2.673029f), new Keyframe(0.1380001f, 0.4333344f, 1.794815f, 1.794815f), new Keyframe(0.1900002f, 0.5051128f, 1.037488f, 1.037488f), new Keyframe(0.2410003f, 0.5405387f, 0.4266107f, 0.4266107f), new Keyframe(0.2989997f, 0.5497373f, -0.01107711f, -0.01107711f), new Keyframe(0.3459991f, 0.5412421f, -0.2508451f, -0.2508451f), new Keyframe(0.4589976f, 0.5049765f, -0.2264434f, -0.2264434f), new Keyframe(0.5489965f, 0.4931013f, -0.219433f, -0.219433f), new Keyframe(0.6809948f, 0.4525886f, -0.1597189f, -0.1597189f), new Keyframe(0.7329941f, 0.4519376f, 0.1850969f, 0.1850969f), new Keyframe(0.7779936f, 0.4691595f, 0.590108f, 0.590108f), new Keyframe(0.8089932f, 0.4938818f, 0.9988991f, 0.9988991f), new Keyframe(0.8389928f, 0.5298902f, 1.543002f, 1.543002f), new Keyframe(0.895992f, 0.6373742f, 2.402222f, 2.402222f), new Keyframe(0.9489914f, 0.7920651f, 3.497599f, 3.497599f), new Keyframe(1f, 1f, 4.076463f, 4.076463f)); } }

        public static AnimationCurve Linear { get { return new AnimationCurve(new Keyframe(0f, 0f, 1f, 1f), new Keyframe(1f, 1f, 1f, 1f)); } }


    }
    public static Juice Instance;
    public List<System.Action> mCallbacks;

    [SerializeField]
    private List<Object> mComponents;

    [SerializeField]
    bool debug = false;
    
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
    /// Lerps a UI image's color to a target color over time.
    /// </summary>
    /// <param name="aImage">The Image</param>
    /// <param name="aTime">How long should this take?</param>
    /// <param name="aTarget">What color are we going to?</param>
    /// <param name="aScaledTime">Should this be affected by scaled time?</param>
    /// <param name="aCurve">An AnimationCurve to use</param>
    /// <param name="aCallback">Now what?</param>
    public void LerpImageColor(Image aImage, float aTime, Color aTarget, bool aScaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aTime <= 0f)
        {
            aImage.color = aTarget;
            mCallbacks.Add(aCallback);
            return;
        }

        string juiceID = RegisterObject(aImage);
        if (juiceID == "") return;
        StartCoroutine(CoLerpImageColor(juiceID, aImage, aTime, aTarget, aScaledTime, aCurve, aCallback: aCallback));
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
    public void LerpSpriteColor(SpriteRenderer aSprite, float aTime, Color aTarget, bool aScaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aTime <= 0f)
        {
            aSprite.color = aTarget;
            mCallbacks.Add(aCallback);
            return;
        }

        string juiceID = RegisterObject(aSprite);
        if (juiceID == "") return;
        StartCoroutine(CoLerpSpriteColor(juiceID, aSprite, aTime, aTarget, aScaledTime, aCurve, aCallback));
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
        if (aCurve == null) aCurve = Curves.Linear;
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
        if (aCurve == null) aCurve = Curves.Linear;
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

    IEnumerator CoLerpImageColor(string aJuiceID, Image aImage, float aTime, Color aTargetColor, bool scaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        Color aStartColor = aImage.color;
        Color target = aTargetColor;
        float startTime = Time.time;
        float percentCompleted = 0;

        if (aCurve == null) aCurve = Curves.Linear;

        while (ObjectRegistered(aImage, aJuiceID) && percentCompleted < 1f)
        {
            percentCompleted = Mathf.Clamp01((Time.time - startTime) / aTime);
            //OLD WAY (for scaled time): float lerpAmount = scaledTime ? Time.deltaTime / aTime : Time.unscaledDeltaTime / aTime;
            //OLD WAY: timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            aImage.color = Color.Lerp(aStartColor, aTargetColor, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        if (aImage != null) aImage.color = target;
        DeregisterObject(aImage, aJuiceID, aCallback); yield break;
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
        Color aStartColor = aSprite.color;
        Color target = aTargetColor;
        float startTime = Time.time;
        float percentCompleted = 0;

        if (aCurve == null) aCurve = Curves.Linear;

        while (ObjectRegistered(aSprite, aJuiceID) && percentCompleted < 1f)
        {
            percentCompleted = Mathf.Clamp01((Time.time - startTime) / aTime);
            //OLD WAY (for scaled time): float lerpAmount = scaledTime ? Time.deltaTime / aTime : Time.unscaledDeltaTime / aTime;
            //OLD WAY: timePassed += scaledTime ? Time.deltaTime : Time.unscaledDeltaTime;
            aSprite.color = Color.Lerp(aStartColor, aTargetColor, aCurve.Evaluate(percentCompleted));
            yield return new WaitForEndOfFrame();
        }
        if (aSprite != null) aSprite.color = target;
        DeregisterObject(aSprite, aJuiceID, aCallback); yield break;
    }

    IEnumerator CoLerpMaterialProptery(string aJuiceID, Material aMaterial, float aTime, MaterialPropertyType aPropertyType, string aPropertyName, Color aTargetColor = default(Color), float aTargetValue = 0f, bool scaledTime = true, AnimationCurve aCurve = null, System.Action aCallback = null)
    {
        if (aCurve == null) aCurve = Curves.Linear;

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
        if (aCurve == null) aCurve = Curves.Linear;
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
        if (aCurve == null) aCurve = Curves.Linear;

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
        if (aCurve == null) aCurve = Curves.Linear;

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
        if (aCurve == null) aCurve = Curves.Linear;

        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1f)
        {
            timeSinceStarted = Time.time - timeStarted;
            percentageComplete = Mathf.Clamp01(timeSinceStarted / aTime);
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
        if (aCurve == null) aCurve = Curves.Linear;

        while (ObjectRegistered(aTransform, aJuiceID) && percentageComplete < 1f)
        {
            timeSinceStarted = Time.time - timeStarted;
            percentageComplete = Mathf.Clamp01(timeSinceStarted / aTime);
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
