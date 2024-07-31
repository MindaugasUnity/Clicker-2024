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
            // How we going to access TotalMoney or UpdateUI method

        public float BasePrice = 50f;
        public float CurrentPrice;

        public float PriceScaling = 1.1f;

        public void Buy()
        {
            // 0 currently means TotalMoney
            if (0 >= CurrentPrice)
            {
                // Decrease the amount
                CurrentPrice *= PriceScaling;
                
                // Update the UI
                // Generate Money
            }
        }
    }
}