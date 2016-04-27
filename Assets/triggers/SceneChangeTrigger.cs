using UnityEngine;
using System.Collections;

public class SceneChangeTrigger : ActionTrigger
{
    public Scenes scene;

    protected override void ExecuteAction()
    {
        base.ExecuteAction();
        Application.LoadLevel(EnumExtantions.GetDescription<Scenes>(scene));
    }
}
