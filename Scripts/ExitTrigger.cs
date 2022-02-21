using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExitTrigger : MonoBehaviour
{
    private Vector3 target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.maxValue = 22;
            PlayerController.minValue = -7;
            Debug.Log("Count:" + PlayerController.linkedList.Count);
            target = new Vector3(gameObject.transform.position.x, 3.5f, gameObject.transform.position.z + PlayerController.linkedList.Count + 10);
            other.transform.DOMove(target, 1);
        }
    }
}
