using UnityEngine;
using UnityEngine.Events;

public static class ValidateUtils
{
	public static bool Validate(GameObject gameObject, UnityEvent buttonClickEvent)
	{
		bool result = true;
		if (Application.isEditor) {
			for (int index = 0; index < buttonClickEvent.GetPersistentEventCount(); index++) {
				UnityEngine.Object temp = buttonClickEvent.GetPersistentTarget (index);
				if (temp == null) {
					gameObject.LogError("TARGET[" + index + "] is null");
					result = false;
				}
				if (buttonClickEvent.GetPersistentMethodName (index) == null) {
					gameObject.LogError("METHOD[" + index + "] is null");
					result = false;
				}
			}
		}
		return result;
	}
}