using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Method - Description for PC to do something
    // Algorithm - Steps how to do something

    // Variables - stores data (Currency/Clicking power)
    // Variables types - stores different type (whole number, number with decimal value, text, symbol, image, bool (0,1 | True or false))

    // Naming conventions - how to name variables
    // camelCase - start with lower letter, everyNextWorldIsCapitalized
    // PascalCase - start with Capital letter and everyWorldIsCapitalized
        // public variables uses PascalCase

    // Scopes - defines area where methods and variables lasts/valid
    // Defined by {}

    // Task 1 - button Click
    // On Button Click -> Give user feedback that he clicked (Unity) -> Show that was clicked in Console
    // Task 2 Display Score/currency
    // During the Click method add to variables (totalMoney, clickPower) totalMoney with the click power 
    // Update the Score UI element during the click
    
    // Bug - Currency text is only updating after the first click
        // On lunch -> Set money to TotalMoney
    // Bug - Money variable can  Overflow (if money is over +-2 billions)
        // Change to float (3.1E+38) or double (1.7E+308) 

    

    // Note If TotalMoney exceeds (3.1E+38) change to double (1.7E+308) 
    public float TotalMoney = 0;   // Starting amount of money
    public int ClickPower = 1;   // Initial click power
    public TextMeshProUGUI moneyText;       // Reference to the Text component that displays the Money

    // Cursors click every 1 second
    public float CursorClickingRate = 1f;
    
    // Int Max value is 2,1B
    // Float max value 10 ^ 38
    // Double max value 10 ^ 308
    public int CursorPrice = 5;
    
    private string PrefKeyForTotalMoney = $"{nameof(GameManager)}/{nameof(TotalMoney)}";
    
    public float GlobalMultiplier => baseGlobalMultiplayer + prestigeCount / 100 * 5; // 5 percent
    
    private float baseGlobalMultiplayer = 1;
    private float prestigeCount = 1;
    
    private void Start()
    {
        TotalMoney = PlayerPrefs.GetFloat(PrefKeyForTotalMoney, 0);
        
        UpdateScoreUI();
    }

    // Method signature - visibility(public/private) | returnType | MethodName (MethodVariable)
    public void Click()
    {
        TotalMoney += ClickPower * GlobalMultiplier; // Is the same as -> totalMoney = totalMoney + clickPower;  
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        Save();
        
        moneyText.text = Mathf.FloorToInt(TotalMoney).ToString();
    }
    
    
    // Task 3 - How to Do Idle clicking?
        // Loop that uses Click() every x (variable) second 
            // Global multiplier
            
    // Task 4 - Cursors cost money
        // Before InvokeRepeating check if can you afford
        
    // Bug from Task 4 - Money text does not update after we bought the cursor
        // After we spend the money call method - UpdateScoreUI
        
    // Bonus task - Add Cursor price scaling
        // --> Incremental price increase (price + 2)
        // Percentage price increase (10% per each building - price * 1.1 ) 
        // Custom Function (2% ---- 40% ----- 5%)
    
    public void CreateCursor()
    {
        if (TotalMoney >= CursorPrice)
        {
            InvokeRepeating(nameof(Click), 1.0f, CursorClickingRate);

            TotalMoney -= CursorPrice;
            CursorPrice += 2;
            
            UpdateScoreUI();
        }
    }
    
    private void Save()
    {
        PlayerPrefs.SetFloat(PrefKeyForTotalMoney, TotalMoney);
    
        PlayerPrefs.Save();
    }

    public void SoftReset()
    {
        TotalMoney = 0;

        CursorPrice = 5;
        CancelInvoke(nameof(Click));
        
        UpdateScoreUI();
        
        Save();
    }
}