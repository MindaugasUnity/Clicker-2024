using System.Collections;
using UnityEngine;

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

        public GameManager GameManager;

        private Coroutine generateMoneyOvertimeCoroutine;

        private void Start()
        {
            CurrentPrice = BasePrice;
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
                
                // The same as BuildingCount += 1;
                BuildingCount++;
                
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

                GameManager.TotalMoney += buildingIncome;
                GameManager.UpdateScoreUI();
            }
        }
    }
}