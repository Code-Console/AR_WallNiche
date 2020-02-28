//================================================================================================================================
//
//  Copyright (c) 2015-2019 VisionStar Information Technology (Shanghai) Co., Ltd. All Rights Reserved.
//  EasyAR is the registered trademark or trademark of VisionStar Information Technology (Shanghai) Co., Ltd in China
//  and other countries for the augmented reality technology developed by VisionStar Information Technology (Shanghai) Co., Ltd.
//
//================================================================================================================================
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace easyar
{
    public class EasyARBehaviour : MonoBehaviour
    {
        public static bool Initialized { get; private set; }
        public static DelayedCallbackScheduler Scheduler { get; private set; }


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void GlobalInitialization()
        {
            Initialized = false;
            Scheduler = new DelayedCallbackScheduler();
            var easyarSettings = Resources.Load<EasyARSettings>("EasyARKey");
            var key = easyarSettings.EasyARKey;
            //Debug.Log("0000    ->>>>>. "+easyarSettings.EasyARKey);
            //key = "r0kFOKuPAj6z4Kbnk3UyuvCxogPKXApvloMb9aPEj/inxQi1pIDb5neESzPnRk+y48eKceUTnSbvBAgwpUbY8CTGm+P3hos0IgSP+iZDgTH3k18j94bJMCZGT7Gj1V3+IdVL9aSEyHGtRM+gcYsb8CTGmPInQ4mwoVZOsKQGyPKnx8o0N5bb8CTGmPInQ4mwoVZOsKQGyPKnx8o0N4qYYGKFj/KnRk50N5NFoGeFj7Kn1UQj94HIcKIESLRkQRvmadVOsqSEyLUj1VhgZEWLoGhW2/GhAck0ZkjJM6ZJDnCkQdvmZICIc/QVSTQsBguwpBVd8WdGz7GgVs2gZ4CI8eQEgTHj1V3+N4UIs7SHCzalxY0jYsWIc+SHi7LmQRv/tBVO8KOHizNiARvmadVL8KPHi6BoVtv05AWOcWTBSDQ3k0WgZ0ZKdGTHimBoVtvxoQHJNGZIyTOmSQ5wpEHb5mSAiHP0FUk0LAYLsKQVXfFnRs+xoFbNoGeAiPHkBIEx49Vd/jeFCLO0hws2pcWNI2LFiHPkh4uy5kEb/7QVTvCjh4szYgEb5mnVS/Cjx4ugaFbb9OQFjnFkwUg0N5NFoGVGD6BoVtvxoQHJNGZIyTOmSQ5wpEHb5mSAiHP0FUk0LAYLsKQVXfFnRs+xoEqMO3RY54NP4gQ7AAIFWiA338gz8whWqRc2OPUFnqNFJ6OHcfSJ6HTVCqll5fiqGtzHi42qqBMa2hj4AMb1i7HG4VKfTqvUQCrMjAIVaEUUiX6G6W1HZyARdgqtZwEXbABrEH/zpkDE9lymak5xIUVkDHteKqcsK25TjTcfwkz3lFXt1Qe4xVrnTTT6iGNV2D1RZ8JbknxN8OYud0u5DCpVxYApfsbzKJaOmm2XoPgLOUsjci3M0bS4zn9FRQ1cNta2LqYepTSatWbFl619EgQjByF0kX/9+Pn5mhFieSuIGTqtBLXopL4nsQSfFmcXqxzORWmxNZ2mrc9cVAJUaP8d00=";
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var easyarEngineClass = new AndroidJavaClass("cn.easyar.Engine"))
            {
                var activityclassloader = currentActivity.Call<AndroidJavaObject>("getClass").Call<AndroidJavaObject>("getClassLoader");
                if (activityclassloader == null)
                {
                    Debug.Log("ActivityClassLoader is null");
                }
                if (!easyarEngineClass.CallStatic<bool>("initialize", currentActivity, key))
                {
                    Debug.Log("EasyAR initialization failed");
                    Initialized = false;
                    return;
                }else
                {
                    Initialized = true;
                }
            }
#else
            if (!Engine.initialize(key))
            {
                Debug.LogError("[EasyAR] EasyAR initialization failed");
                Initialized = false;
                return;
            }
            else
            {
                Debug.Log("[EasyAR] EasyAR initialization successful");
                Debug.Log("[EasyAR] EasyAR Version : " + Engine.versionString());
                Initialized = true;
            }
#endif
#if UNITY_EDITOR
            Log.setLogFunc((LogLevel, msg) =>
            {
                switch (LogLevel)
                {
                    case LogLevel.Error:
                        Debug.LogError("[EasyAR]" + msg);
                        break;
                    case LogLevel.Warning:
                        Debug.LogWarning("[EasyAR]" + msg);
                        break;
                    case LogLevel.Info:
                        Debug.Log("[EasyAR]" + msg);
                        break;
                    default:
                        break;
                }
            });
#endif
            System.AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Debug.Log("UnhandledException: " + e.ExceptionObject.ToString());
            };
            System.AppDomain.CurrentDomain.DomainUnload += (sender, e) =>
            {
                if (Scheduler != null)
                {
                    Scheduler.Dispose();
                }
                Log.resetLogFunc();
            };
        }

        private void Awake()
        {
            StartCoroutine(waitForUI());
        }

        IEnumerator waitForUI()
        {
            yield return new WaitForSeconds(1f);
            if (!Initialized)
            {
                GUIPopup.AddShowMessage(Engine.errorMessage(), 10000);
            }
        }

        public void Update()
        {
            if (Scheduler != null)
            {
                while (Scheduler.runOne())
                {
                }
            }
        }

        public void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                Engine.onPause();
            }
            else
            {
                Engine.onResume();
            }
        }
    }
}