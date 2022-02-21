using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnterTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           
           
            other.transform.DOMove(target.transform.position, 1.6f);       
        }
    }
}
