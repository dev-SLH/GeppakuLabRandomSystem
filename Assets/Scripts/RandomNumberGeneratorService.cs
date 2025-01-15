using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomNumberGeneratorService
{
    public List<int> GenerateNumbers(int minValue, int maxValue, int count, bool allowDuplicates)
    {
        List<int> generatedNumbers = new List<int>();

        if (allowDuplicates)
        {
            for (int i = 0; i < count; i++)
            {
                generatedNumbers.Add(Random.Range(minValue, maxValue + 1));
            }
        }
        else
        {
            List<int> range = Enumerable.Range(minValue, maxValue - minValue + 1).ToList();
            for (int i = 0; i < count && range.Count > 0; i++)
            {
                int index = Random.Range(0, range.Count);
                generatedNumbers.Add(range[index]);
                range.RemoveAt(index);
            }
        }

        return generatedNumbers;
    }
}