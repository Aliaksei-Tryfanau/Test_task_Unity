using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour, ILevelObject
{
    [SerializeField] GameObject sucessVFX;
    [SerializeField] GameObject loseVFX;
    [SerializeField] GameObject player;

    private Vector3 oldPlayerPos;
    private float anglesSum;
    private bool withinRadius;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            withinRadius = true;
            oldPlayerPos = transform.position - player.transform.position;
            StartCoroutine("CalculateAngles");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            withinRadius = false;
            StopCoroutine("CalculateAngles");
            anglesSum = 0;
        }
    }

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

    private IEnumerator CalculateAngles()
    {
        while (withinRadius)
        {
            Vector3 newPlayerPos = transform.position - player.transform.position;
            anglesSum += Vector3.SignedAngle(oldPlayerPos, newPlayerPos, Vector3.up);
            oldPlayerPos = newPlayerPos;
            print(anglesSum);
            if (Mathf.Abs(anglesSum) >= 360f)
            {
                CheckActivation();
                break;
            }
            yield return new WaitForSeconds(0.5f);
        }
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
