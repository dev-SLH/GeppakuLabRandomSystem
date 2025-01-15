using System.Text.RegularExpressions;
using Michsky.MUIP;
using TMPro;
using UnityEngine;

public class InputFieldRule : MonoBehaviour
{
    [SerializeField] private TMP_InputField minimumInputField;
    [SerializeField] private TMP_InputField maximumInputField;
    [SerializeField] private TMP_InputField quantityInputField;

    public NotificationManager minPopup, maxPopup, quantityPopup;

    private void Start()
    {
        minimumInputField.characterLimit = 8;
        minimumInputField.onValueChanged.AddListener(
            (word) => minimumInputField.text = Regex.Replace(word, @"[^0-9]", "")
        );

        maximumInputField.characterLimit = 8;
        maximumInputField.onValueChanged.AddListener(
            (word) => maximumInputField.text = Regex.Replace(word, @"[^0-9]", "")
        );

        quantityInputField.characterLimit = 3;
        quantityInputField.onValueChanged.AddListener(
            (word) => quantityInputField.text = Regex.Replace(word, @"[^0-9]", "")
        );
    }

    public void OnMinInputRule()
    {
        if (int.TryParse(minimumInputField.text, out int min) &&
            int.TryParse(maximumInputField.text, out int max))
        {
            if (min > max)
            {
                minPopup.OpenNotification();
            }
        }
    }

    public void OnMaxInputRule()
    {
        if (int.TryParse(minimumInputField.text, out int min) &&
            int.TryParse(maximumInputField.text, out int max))
        {
            if (min > max)
            {
                maxPopup.OpenNotification();
            }
        }
    }

    public void OnQuantityInputRule()
    {
        if (int.TryParse(quantityInputField.text, out int value))
        {
            if (value < 1 || value > 100)
            {
                quantityPopup.OpenNotification();
            }
        }
    }
}