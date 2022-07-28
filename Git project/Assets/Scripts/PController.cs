using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PController : MonoBehaviour
{
    [SerializeField] private TMP_Text PText;
    
        private void OnEnable()
        {
            PlayerObserverManager.OnCoinsChanged += UpdatePText;
        }
    
        private void OnDisable()
        {
            PlayerObserverManager.OnCoinsChanged -= UpdatePText;
        }
    
        private void UpdateCoinText(int newCoinsValue)
        {
           PTextText.text = newCoinsValue.ToString();
        }
}
