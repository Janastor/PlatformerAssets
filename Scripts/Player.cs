using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private int _coinCount = 0;
    
    public void AddCoin()
    {
        _coinCount++;
    }
}
