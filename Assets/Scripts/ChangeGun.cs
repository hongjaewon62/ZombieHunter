using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    public Behaviour gun1;
    public Behaviour gun2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Change1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Change2();
        }
    }
    public void Change1()
    {
        gun1.enabled = true;
        gun2.enabled = false;
    }

    public void Change2()
    {
        gun1.enabled = false;
        gun2.enabled = true;
    }
}
