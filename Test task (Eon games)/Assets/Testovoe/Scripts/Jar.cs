using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jar : MonoBehaviour, ILevelObject
{
    [SerializeField] GameObject sucessVFX;
    [SerializeField] GameObject loseVFX;

    public void Success()
    {
        loseVFX.gameObject.SetActive(false);
        sucessVFX.gameObject.SetActive(true);
    }

    public void Lose()
    {
        loseVFX.gameObject.SetActive(true);
        sucessVFX.gameObject.SetActive(false);
    }

    public void CheckActivation()
    {
        if (gameObject == GameManagerScript.objectToActivate)
        {
            print("Right");
            GameManagerScript.instance.NextObjectToActivate();
            Success();
        }
        else
        {
            print("Wrong");
            GameManagerScript.instance.ResetObjectToActivate();
        }
    }
}
