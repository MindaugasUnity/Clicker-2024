using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Building : MonoBehaviour
    {
        // Building
            // Buy
                // Variables: BasePrice, CurrentPrice, PriceScaling
                // Check if player has enough money
                    // Decrease money amount
                    // Increase building price
                    // Update the UI
                // call GenerateMoney method
            
            // Note: Use EverythingScript currency (TotalMoney)
            
        // Bonus
            // How we going to generate money (there can be 10 000 buildings in other words cannot use InvokeRepeating)
                // [IDEA: 1] Cap at max 10 k building -> HARD cap
                    // [IDEA: 1.1] Say that is sold-out at the hard cap
                // [IDEA: 2] Increase current price at milestones (ex: current price * 3) so you never reach 10k
                // [IDEA: 3] Hard cap but From x building start increasing price scaling
                // Hint: Think about the process
                // [IDEA: 4] money per fill * totalNumberOfBuilding
                
        // Buy Building variables
        public float BasePrice = 50f;
        public float CurrentPrice;
        public float PriceScaling = 1.1f;
       
        // GenerateMoney Variables
        public float BuildingCount;
        public float MoneyAmountPerFill = 5;
        public float FillTime = 2f;
        
        // Button Related
        public Button Button;
        
        public string Name = "New Building";
        public TextMeshProUGUI ButtonText;
        public TextMeshProUGUI PriceText;

        public GameManager GameManager;

        private Coroutine generateMoneyOvertimeCoroutine;
        
        // For Data Saving Key (PlayerPrefKey)
            // + If it building
            // + Which building
            // Specific value
        // !!! Don't change player prefs keys for live games
        private string PrefKeyBuildingCount => $"{nameof(Building)}/{Name}/{nameof(BuildingCount)}";

        private void Start()
        {
            CurrentPrice = BasePrice;

            Load();
            UpdateBuildingText();
        }
        
        // Enable building button if you have enough money
            // Variable - button 
            // Total money > CurrentPrice
            // Update
        private void Update()
        {
            Button.interactable = GameManager.TotalMoney >= CurrentPrice;
            
            // The same as doing this
            // if (GameManager.TotalMoney >= CurrentPrice)
            // {
            //     Button.interactable = true;
            // }
            // else
            // {
            //     Button.interactable = false;
            // }
        }

        public void Buy()
        {
            if (GameManager.TotalMoney >= CurrentPrice)
            {
                // Decrease the amount
                GameManager.TotalMoney -= CurrentPrice;
                CurrentPrice *= PriceScaling;
                
                // Update the UI
                GameManager.UpdateScoreUI();
                UpdateBuildingText();
                
                // The same as BuildingCount += 1;
                BuildingCount++;

                Save();
                
                // Generate Money
                if (generateMoneyOvertimeCoroutine is null)
                {
                    generateMoneyOvertimeCoroutine = StartCoroutine(GenerateMoney());
                }
            }
        }
        
        // Generate money
            // Variables: buildingCount, moneyAmountPerFill, fillTime
            // Logic (In Coroutine)
            // Generate money until stopped (while true)
            // Wait for the fill time
            // GenerateMoney using this formula = money per fill * totalNumberOfBuilding
            // Add that money to GameManager.TotalAmount
            // Update the UI

        private IEnumerator GenerateMoney()
        {
            while (true)
            {
                yield return new WaitForSeconds(FillTime);

                float buildingIncome = BuildingCount * MoneyAmountPerFill;

                GameManager.TotalMoney += buildingIncome * GameManager.GlobalMultiplier;
                GameManager.UpdateScoreUI();
            }
        }
        
        // Display Building name and price
            // variables: Name, ButtonText (extra one if you want double price)
            // Connect Button text in Unity
            // Method: Display text: "Building - Price"
            // Where to call: Buy, 
        
        public void UpdateBuildingText()
        {
            // The same
            // ButtonText.text = Name + " - " + CurrentPrice;
            ButtonText.text = $"{Name}";
            PriceText.text = $"{Mathf.CeilToInt(CurrentPrice)}";
            
            // New variables.text = CurrentPrice
        }

        public void Save()
        {
            PlayerPrefs.SetFloat(PrefKeyBuildingCount, BuildingCount);
            
            PlayerPrefs.Save();
        }

        private void Load()
        {
            BuildingCount = PlayerPrefs.GetFloat(PrefKeyBuildingCount, 0);

            if (BuildingCount > 0)
            {
                generateMoneyOvertimeCoroutine = StartCoroutine(GenerateMoney());
            }

            for (int i = 0; i < BuildingCount; i++)
            {
                CurrentPrice *= PriceScaling;
            }
        }

        private void SoftReset()
        {
            BuildingCount = 0;
            CurrentPrice = BasePrice;
            
            StopCoroutine(generateMoneyOvertimeCoroutine);
            
            Save();
            UpdateBuildingText();
        }
    }
}