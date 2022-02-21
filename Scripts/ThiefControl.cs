using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefControl : MonoBehaviour
{
    private GameObject target;
    private Vector3 mesafe = Vector3.zero;

    private static ThiefControl instance = null;


    private void Start()
    {
        instance = this;
    }

    // Game Instance Singleton
    public static ThiefControl Instance
    {
        get
        {
            return instance;

        }
    }


    public GameObject Target
    {
        get
        {
            return target;
        }
        set
        {
            target = value;
        }
    }


    public Vector3 Distance
    {
        get
        {
            return mesafe;
        }
        set
        {
            mesafe = value;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position - mesafe, 0.14f);
        gameObject.transform.LookAt(target.transform);

    }


    public IEnumerator Cor_ChangeThiefLocation()
    {
        Debug.Log("change girdi");
        //sceneThief.transform.DOScale(Vector3.zero, 0.5f);
        yield return new WaitForSeconds(0.5f);
        target = PlayerController.linkedList.Last.Value;
        //sceneThief.transform.DOScale(new Vector3(2,2,2), 0.5f);
    }
}
