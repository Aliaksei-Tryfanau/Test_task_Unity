using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour, ILevelObject
{
    [SerializeField] GameObject sucessVFX;
    [SerializeField] GameObject loseVFX;

    private float playerVelocity;
    private Rigidbody playerRB;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerRB = collision.gameObject.GetComponent<Rigidbody>();
            StartCoroutine("CheckVelocity");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine("CheckVelocity");
        }
    }

    private IEnumerator CheckVelocity()
    {
        while (true)
        {
            playerVelocity = playerRB.velocity.magnitude;
            if (playerVelocity < 0.01f)
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
