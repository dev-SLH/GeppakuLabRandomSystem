using UnityEngine;
using UnityEngine.Events;

public class AnimationEvents : MonoBehaviour
{
    public UnityEvent[] animEvent;

    public void AnimEvent(int num)
    {
        animEvent[num].Invoke();
    }
}
