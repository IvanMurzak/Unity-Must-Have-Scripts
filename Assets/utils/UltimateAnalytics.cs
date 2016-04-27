using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

public class UltimateAnalytics : MonoBehaviour
{
    protected static string SceneName { get { return "(" + Application.loadedLevelName + ")"; } }
    protected static string Version { get { return "v" + Application.version; } }

    public enum Action
    {
        [Description("OnShow")]
        ON_SHOW,

        [Description("OnHide")]
        ON_HIDE,

        [Description("OnKey")]
        ON_KEY,

        [Description("OnClick")]
        ON_CLICK,

        [Description("OnSubmit")]
        ON_SUBMIT,


        [Description("OnTrigger")]
        ON_TRIGGER,

        [Description("OnCheckpoint")]
        ON_CHECKPOINT,

        [Description("OnRespawn")]
        ON_RESPAWN        
    }

    protected void AnalyticsLogEvent(Action eventCategory, string eventAction, long value)
    {
        AnalyticsLogEvent(gameObject, eventCategory, eventAction, value);
    }

    public static void AnalyticsLogEvent(GameObject gObject, Action eventCategory, string eventAction, long value)
    {
        AnalyticsLogEvent(gObject, eventCategory, eventAction, value, new Dictionary<string, object>(), new Dictionary<string, string>());
    }

    public static void AnalyticsLogEvent(GameObject gObject, Action eventCategory, string eventAction, long value,
                                         Dictionary<string, object> dataObject, Dictionary<string, string> dataString)
    {
        if (!Application.isEditor)
        {
            // ObjectName:Action
            string subData = SubData(gObject, eventAction);
            // Event:ObjectName:Action:(SceneName)
            string data = FullData(gObject, EnumExtantions.GetDescription<Action>(eventCategory), eventAction);
            // Event:ObjectName:Action:(SceneName):value
            // string fullDataWithValue = data + (value > 0 ? ":" + value : "");


            if (value > 0)
            {
                dataObject.Add("value", value);
                dataString.Add("value", value.ToString());
            }

            // Google       --- Event, ObjectName:Action, (SceneName), value
            try
            {
                //GoogleAnalyticsV3.instance.LogEvent(new EventHitBuilder()
                //                                        .SetEventCategory(eventCategory)
                //                                        .SetEventAction(subData)
                //                                        .SetEventLabel(SceneName)
                //                                        .SetEventValue(value));
            }
            catch (System.Exception e) { AnalyticsError(e); }

            // Game         --- Event:ObjectName:Action:(SceneName), value
            try
            {
                //if (value > 0) GameAnalytics.NewDesignEvent(data, value);
                //else GameAnalytics.NewDesignEvent(data);
            }
            catch (System.Exception e) { AnalyticsError(e); }

            // Flurry       --- Event:ObjectName:Action:(SceneName), value
            try
            {
                //if (dataString.Count > 0) Flurry.Instance.LogEvent(data, dataString);
                //else Flurry.Instance.LogEvent(data);
            }
            catch (System.Exception e) { AnalyticsError(e); }

            // Amplitude    --- Event:ObjectName:Action:(SceneName), value
            try
            {
                //if (dataObject.Count > 0) Amplitude.Instance.logEvent(data, dataObject);
                //else Amplitude.Instance.logEvent(data);
            }
            catch (System.Exception e) { AnalyticsError(e); }
        }
    }

    // ObjectName:Action
    private static string SubData(GameObject gObject, string eventAction)
    {
        return gObject.name + (eventAction != null && eventAction.Length > 0 ? ":" + eventAction : "");
    }

    // Event:ObjectName:Action:(SceneName)
    private static string FullData(GameObject gObject, string eventCategory, string eventAction)
    {
        return eventCategory + ":" + SubData(gObject, eventAction) + ":" + SceneName;
    }


    private static void AnalyticsError(System.Exception e)
    {
        string errorString = "ANALYTICS ERROR(" + SceneName + "):" + e;
        Debug.LogError(errorString);
        try
        {
            //GoogleAnalyticsV3.instance.LogException(new ExceptionHitBuilder()
            //                                 .SetFatal(false)
            //                                 .SetExceptionDescription(errorString));
        }
        catch (System.Exception deepE)
        {
            string deepErrorString = "DEEP ANALYTICS ERROR(" + SceneName + "):" + deepE + ":DIVIDER:" + errorString;
            Debug.LogError(deepErrorString);
        }
    }
}
