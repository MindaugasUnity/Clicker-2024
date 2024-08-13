using System;
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
            // Generate money
            // Note: Use EverythingScript currency (TotalMoney)
            
        // Bonus
            // How we going to generate money (there can be 10 000 buildings in other words cannot use InvokeRepeating)
                // [IDEA: 1] Cap at max 10 k building -> HARD cap
                    // [IDEA: 1.1] Say that is sold-out at the hard cap
                // [IDEA: 2] Increase current price at milestones (ex: current price * 3) so you never reach 10k
                // [IDEA: 3] Hard cap but From x building start increasing price scaling
                // Hint: Think about the process
            // How we going to access TotalMoney or UpdateUI method
                // Create variable and using it access it as object

        public float BasePrice = 50f;
        public float CurrentPrice;

        public float PriceScaling = 1.1f;

        public EverythingScript GameManager;

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
                // Generate Money
            }
        }
    }
}