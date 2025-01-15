using Michsky.MUIP;
using UnityEngine;

public class SetUpHorizontalSelector : MonoBehaviour
{
    public HorizontalSelector selector;

    public void SelectHorizontal(int horizontal)
    {
        selector.index = horizontal;
        selector.UpdateUI();
    }
}
