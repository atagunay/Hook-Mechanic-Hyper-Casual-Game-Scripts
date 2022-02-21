using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceManager : MonoBehaviour
{
    private int turn = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = gameObject.transform.position + new Vector3(turn * 0.05f, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PoliceTurn")
        {
         
            turn = turn * -1;
            gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, -1 * gameObject.transform.eulerAngles.y, 0));
            //gameObject.transform.rotation.eulerAngles = ;
        }
    }
}
