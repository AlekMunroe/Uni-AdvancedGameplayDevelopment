using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboLockController : MonoBehaviour
{
    public int[] combination;
    public int corectCombination;
    
    public TMPro.TextMeshProUGUI[] combinationDisplay;
    
    void Start()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            combination[i] = 0;
        }
    }

    void Update()
    {
        ComboDisplay();
    }
    
    public void ComboDisplay()
    {
        for (var i = 0; i < combination.Length; i++)
        {
            combinationDisplay[i].text = combination[i].ToString();
        }
    }
    
    public void IncrementCombination(int index)
    {
        combination[index]++;
        if (combination[index] > 9)
        {
            combination[index] = 0;
        }
    }
    
    public void DecrementCombination(int index)
    {
        combination[index]--;
        if (combination[index] < 0)
        {
            combination[index] = 9;
        }
    }
}


