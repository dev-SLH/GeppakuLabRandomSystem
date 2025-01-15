// 2025-01-12 AI-Tag
// This was created with assistance from Muse, a Unity Artificial Intelligence product

using System.Collections;
using TMPro;
using UnityEngine;

public class DemoElementSwaySelf : MonoBehaviour
{
    [SerializeField] private Animator titleAnimator;
    [SerializeField] private TextMeshProUGUI elementTitle;
    [SerializeField] private TextMeshProUGUI elementTitleHelper;

    private string prevName;

    public void SetTitle()
    {
        if (titleAnimator == null)
            return;

        elementTitleHelper.text = prevName;
        elementTitle.text = gameObject.name;

        titleAnimator.Play("Idle");
        titleAnimator.Play("Transition");

        prevName = gameObject.name;
    }
}