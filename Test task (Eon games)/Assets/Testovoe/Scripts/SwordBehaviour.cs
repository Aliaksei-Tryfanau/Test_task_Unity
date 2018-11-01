using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBehaviour : MonoBehaviour
{
    // Check if sword hits something
    public void SwordStrike()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, out hit, 2f))
        {
                hit.collider.gameObject.GetComponent<ILevelObject>().CheckActivation();
        }
    }
}
