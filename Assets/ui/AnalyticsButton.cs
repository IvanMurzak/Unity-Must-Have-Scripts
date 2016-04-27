using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class AnalyticsButton : Button
{
    protected override void Start()
    {
        base.Start();
        ValidateUtils.Validate(gameObject, onClick);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        UltimateAnalytics.AnalyticsLogEvent(gameObject, UltimateAnalytics.Action.ON_CLICK, null, 0);
    }

    public override void OnSubmit(BaseEventData eventData)
    {
        base.OnSubmit(eventData);
        UltimateAnalytics.AnalyticsLogEvent(gameObject, UltimateAnalytics.Action.ON_SUBMIT, null, 0);
    }
}
