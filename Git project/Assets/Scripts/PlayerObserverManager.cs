using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//isso seria o nosso Youtube.com
//modificar static diz que pode ser acessado 
//de qualquer lugar no código 
public static class PlayerObserverManager 
{
    //canal da variavel coin de PlayerController
    // 1 - parte da inscrição 
    public static Action<int> OnCoinsChanged; 
    // 2 - parte do sininho (notificação) 
    public static void CoinsChanged(int value)
    {
        //Existe alguém inscreito em CoinChanged?
        //caso tenha, mande o value para todos
        OnCoinsChanged?.Invoke(value);
    }
}
