using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RandomNumberDisplayManager : MonoBehaviour
{
    public Transform scrollContent;
    public GameObject resultTextPrefab;
    public float errorFontSize = 30f;
    public float resultFontSize = 72f;
    private Queue<GameObject> resultPool = new Queue<GameObject>();

    public void InitializeObjectPool(int poolSize)
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newObj = Instantiate(resultTextPrefab, scrollContent);
            newObj.SetActive(false);
            resultPool.Enqueue(newObj);
        }
    }

    public void ClearScrollArea()
    {
        while (scrollContent.childCount > 0)
        {
            ReturnToPool(scrollContent.GetChild(0).gameObject);
        }
    }

    public void DisplayError(string order, string errorMessage)
    {
        GameObject newTextObj = GetPooledObject();
        TMP_Text[] resultComponents = newTextObj.GetComponentsInChildren<TMP_Text>();
        if (resultComponents.Length >= 2)
        {
            resultComponents[0].text = order;
            resultComponents[1].text = errorMessage;
            resultComponents[1].fontSize = errorFontSize;
        }
    }

    public void DisplayResult(int order, int number)
    {
        GameObject newTextObj = GetPooledObject();
        TMP_Text[] resultComponents = newTextObj.GetComponentsInChildren<TMP_Text>();
        resultComponents[0].text = order + GetOrdinalSuffix(order);
        resultComponents[1].text = number.ToString();
        resultComponents[1].fontSize = resultFontSize;
    }

    private GameObject GetPooledObject()
    {
        if (resultPool.Count > 0)
        {
            GameObject obj = resultPool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else if (scrollContent.childCount < 100) // 최대 100개의 오브젝트 제한
        {
            GameObject newObj = Instantiate(resultTextPrefab, scrollContent);
            return newObj;
        }
        else
        {
            Debug.LogWarning("Object pool exhausted! Consider increasing pool size.");
            return null;
        }
    }


    private void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        resultPool.Enqueue(obj);
    }

    private string GetOrdinalSuffix(int number)
    {
        int lastTwoDigits = number % 100;
        int lastDigit = number % 10;

        if (lastTwoDigits >= 11 && lastTwoDigits <= 13)
        {
            return "th";
        }

        if (lastDigit == 1) return "st";
        if (lastDigit == 2) return "nd";
        if (lastDigit == 3) return "rd";
        return "th";
    }
}