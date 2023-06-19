using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZombie : MonoBehaviour
{
    private void Update()
    {
        if(GameManager.instance.isGameover)
        {
            DestroyChild();
        }
    }
    public void DestroyChild()
    {
        while(transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
