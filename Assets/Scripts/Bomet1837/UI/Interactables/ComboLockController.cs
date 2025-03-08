using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ComboLockController : MonoBehaviour
{
    
    public int[] combination;
    public int corectCombination;
    
    public bool isUnlocked = false;
    public TMPro.TextMeshProUGUI[] combinationDisplay;
    public GameObject door; 
    
    [HideInInspector] public bool messageDisplayed = false;
    
    void Start()
    {
        for (int i = 0; i < combination.Length; i++)
        {
            combination[i] = 0;
        }
        
        gameObject.SetActive(false);

    }

    void Update()
    {
        ComboDisplay();
        CheckCombination();
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

    public void CheckCombination()
    {
        int tempCombination = 0;
        for (int i = 0; i < combination.Length; i++)
        {
            tempCombination = tempCombination * 10 + combination[i];
        }

        if (tempCombination == corectCombination)
        {
            isUnlocked = true;
            door.SetActive(false);
        }
        else
        {
            isUnlocked = false;
            door.SetActive(true);
        }
    }
}


