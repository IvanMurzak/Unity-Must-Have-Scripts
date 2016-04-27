using UnityEngine;
using System.Collections;

public class ActionTrigger : ActionInvoker
{
    protected override void Awake()
    {
        base.Awake();
#if UNITY_EDITOR
        if (GetComponent<Collider2D>() == null && GetComponent<Collider>())
            gameObject.LogError("No Collider");
#endif
    }

    protected virtual void OnDetect(GameObject gObject)
    {
        Invoke();
    }

    void OnCollisionEnter(Collision collision)
    {
        OnDetect(collision.gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        OnDetect(collision.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        OnDetect(other.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        OnDetect(other.gameObject);
    }
}
