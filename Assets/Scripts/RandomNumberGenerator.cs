using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using Michsky.MUIP;
using TMPro;
using UnityEngine.Events;

public class RandomNumberGenerator : MonoBehaviour
{
    public Toggle allowDuplicatesToggle;
    public CustomDropdown sortDropdown;
    public CustomDropdown reSortDropdown;
    public TMP_InputField minInputField;
    public TMP_InputField maxInputField;
    public TMP_InputField countInputField;
    public Transform scrollContent;
    public GameObject resultTextPrefab;
    public ButtonManager clearButton;
    public ButtonManager copyButton;
    public UnityEvent onCopyFail;
    public float errorFontSize = 30f;
    public float resultFontSize = 72f;

    private List<int> generatedNumbers = new List<int>();
    private readonly Queue<GameObject> resultPool = new Queue<GameObject>();
    private readonly int poolSize = 100;
    private bool isProcessing;

    private void Start()
    {
        clearButton.onClick.AddListener(Clear);
        copyButton.onClick.AddListener(CopyToClipboard);
        reSortDropdown.onValueChanged.AddListener(delegate { ReSortNumbers(); });
        SetReSortDropdownState(false);

        InitializeObjectPool();
    }

    private void SetButtonInteractable(bool state)
    {
        clearButton.isInteractable = state;
        copyButton.isInteractable = state;
    }
    
    private void InitializeObjectPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(resultTextPrefab, scrollContent);
            newObj.SetActive(false);
            resultPool.Enqueue(newObj);
        }
    }

    private GameObject GetPooledObject()
    {
        if (resultPool.Count > 0)
        {
            GameObject obj = resultPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject newObj = Instantiate(resultTextPrefab, scrollContent);
            return newObj;
        }
    }

    private void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        resultPool.Enqueue(obj);
    }

    private void PrintError(string order, string error)
    {
        ClearScrollArea();
        DisplayError(order, error);
        SetReSortDropdownState(false);
        isProcessing = false;
        SetButtonInteractable(true);
    }
    
    public void GenerateNumbers()
    {
        if (isProcessing) return;  // 이미 진행 중이라면 무시
        isProcessing = true;       // 클릭 잠금
        SetButtonInteractable(false);

        try
        {
            if (!int.TryParse(minInputField.text, out var minValue) ||
                !int.TryParse(maxInputField.text, out var maxValue) ||
                !int.TryParse(countInputField.text, out var count))
            {
                PrintError("", "無効な入力です。有効な整数を入力してください。\nInvalid input. Please enter valid integers.");
                return;
            }

            if (minValue > maxValue)
            {
                PrintError("", "最小値が最大値を超えることはできません。\nMinimum value cannot be greater than maximum value.");
                return;
            }

            if (count < 1 || count > 100)
            {
                PrintError("", "数値は1から100の間でなければなりません。\nCount must be between 1 and 100.");
                return;
            }

            generatedNumbers.Clear();

            if (allowDuplicatesToggle.isOn)
            {
                for (int i = 0; i < count; i++)
                {
                    generatedNumbers.Add(Random.Range(minValue, maxValue + 1));
                }
            }
            else
            {
                List<int> range = Enumerable.Range(minValue, maxValue - minValue + 1).ToList();
                if (count > range.Count)
                {
                    PrintError("",
                        "生成数が生成できる固有の数より多いです。\n'Number of items' is greater than the unique number that can be generated");
                    countInputField.text = "";
                    return;
                }

                for (int i = 0; i < count && range.Count > 0; i++)
                {
                    int index = Random.Range(0, range.Count);
                    generatedNumbers.Add(range[index]);
                    range.RemoveAt(index);
                }
            }
            SortNumbers(sortDropdown.selectedItemIndex);
            DisplayGeneratedNumbers();
            SetReSortDropdownState(true);
        
            reSortDropdown.onValueChanged.RemoveAllListeners();
            reSortDropdown.ChangeDropdownInfo(sortDropdown.selectedItemIndex);
            reSortDropdown.onValueChanged.AddListener(delegate { ReSortNumbers(); });
        }
        finally
        {
            isProcessing = false;
            SetButtonInteractable(true);
        }
    }
    
    private void ReSortNumbers()
    {
        if (isProcessing) return;  // 클릭 방지
        isProcessing = true;

        if (generatedNumbers.Count == 0)
        {
            onCopyFail?.Invoke();
            Debug.Log("No numbers available for re-sorting.");
            SetReSortDropdownState(false);
            isProcessing = false;
            return;
        }

        SortNumbers(reSortDropdown.selectedItemIndex);
        DisplayGeneratedNumbers();
    
        isProcessing = false;
    }

    private void SortNumbers(int sortOption)
    {
        generatedNumbers = sortOption switch
        {
            1 => generatedNumbers.OrderBy(num => num).ToList(),         // 오름차순
            2 => generatedNumbers.OrderByDescending(num => num).ToList(), // 내림차순
            _ => generatedNumbers.OrderBy(_ => Random.value).ToList()  // 랜덤
        };
    }

    private void DisplayGeneratedNumbers()
    {
        ClearScrollArea();
        for (int i = 0; i < generatedNumbers.Count; i++)
        {
            DisplayResult(i + 1, generatedNumbers[i]);
        }
    }

    private void ClearScrollArea()
    {
        // 모든 오브젝트를 완전히 초기화 (순서 유지)
        List<GameObject> activeObjects = new List<GameObject>();

        foreach (Transform child in scrollContent)
        {
            if (child.gameObject.activeSelf)
            {
                activeObjects.Add(child.gameObject);
            }
        }

        foreach (GameObject obj in activeObjects)
        {
            ReturnToPool(obj);
        }
    }

    private void DisplayError(string order, string errorMessage)
    {
        var newTextObj = GetPooledObject();
        TMP_Text[] resultComponents = newTextObj.GetComponentsInChildren<TMP_Text>();
        if (resultComponents.Length >= 2)
        {
            resultComponents[0].text = order;
            resultComponents[1].text = errorMessage;
            resultComponents[1].fontSize = errorFontSize;
        }
    }

    private void DisplayResult(int order, int number)
    {
        var newTextObj = GetPooledObject();
        TMP_Text[] resultComponents = newTextObj.GetComponentsInChildren<TMP_Text>();
        if (resultComponents.Length >= 2)
        {
            resultComponents[0].text = order + GetOrdinalSuffix(order);
            resultComponents[1].text = number.ToString();
            resultComponents[1].fontSize = resultFontSize;

            // 오브젝트의 Transform 계층 순서 보장
            newTextObj.transform.SetSiblingIndex(order - 1);
        }
    }

    private string GetOrdinalSuffix(int number)
    {
        int lastTwoDigits = number % 100;
        int lastDigit = number % 10;

        if (lastTwoDigits is >= 11 and <= 13) return "th";
        if (lastDigit == 1) return "st";
        if (lastDigit == 2) return "nd";
        if (lastDigit == 3) return "rd";
        return "th";
    }

    private void CopyToClipboard()
    {
        if (generatedNumbers.Count == 0)
        {
            onCopyFail?.Invoke();
            Debug.Log("No numbers generated to copy.");
            SetReSortDropdownState(false);
            return;
        }

        GUIUtility.systemCopyBuffer = string.Join(", ", generatedNumbers);
        Debug.Log("Results copied to clipboard.");
    }

    private void SetReSortDropdownState(bool state)
    {
        reSortDropdown.isInteractable = state;
    }

    private void Clear()
    {
        ClearScrollArea();
        generatedNumbers.Clear();
        SetReSortDropdownState(false);
    }
}