using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Follow : MonoBehaviour
{
    private GameObject target;
    private GameObject sceneManager;
    private Vector3 mesafe = new Vector3(0,0,1.5f);
    private UIManager uIManager;



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
    
    private void Start()
    {
        sceneManager = GameObject.Find("SceneManager");
        uIManager = sceneManager.GetComponent<UIManager>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, target.transform.position - mesafe, 0.14f);
        gameObject.transform.LookAt(target.transform);
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Engel")
        {

            Destroy(gameObject.GetComponent<Follow>());

            LinkedListNode<GameObject> cursor = PlayerController.linkedList.Find(gameObject);


            while(cursor != null)
            {
                LinkedListNode<GameObject> deleteItem = cursor;
                cursor = cursor.Next;

                PlayerController.linkedList.Remove(deleteItem);
                Debug.Log("number:" + PlayerController.linkedList.Count);

                if (PlayerController.linkedList.Count <= 1)
                {
                    Debug.Log("oyun bitti");
                    sceneManager.GetComponent<UIManager>().LosePanel();
                    PlayerController.process = 0;
                }
            }

            
            ThiefControl.Instance.Target = PlayerController.linkedList.Last.Value;


            sceneManager.GetComponent<UIManager>().DecreaseCM();
            CameraFollow.offset = new Vector3(0, 5f, -20) + PlayerController.linkedList.Count * new Vector3(0, 1, -4);
        }
        else if (other.gameObject.tag == "take")
        {
            other.gameObject.tag = "taken";
            uIManager.IncraseCM();

            other.transform.DOScale(new Vector3(0, 0, 0), 0.1f).OnComplete(() => other.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.5f).OnComplete(() =>
            ThiefControl.Instance.Target = other.gameObject));
          

            // Kommentten kaldýrýldý !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            // other.GetComponent<BoxCollider>().enabled = false;
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            Follow nesne = other.gameObject.AddComponent<Follow>();
            if (PlayerController.linkedList.Count == 1)
            {
                nesne.Distance = new Vector3(0, 0, 1.5f);
            }

            //********************************************************************************************************************************
            //nesne.Target = stack.Peek();
            //stack.Push(other.gameObject);


            nesne.Target = PlayerController.linkedList.Last.Value;
            PlayerController.linkedList.AddLast(other.gameObject);
            Debug.Log("Number:" + PlayerController.linkedList.Count);

            Vibration.VibratePop();
            CameraFollow.offset += new Vector3(0, 1, -4);
            PlayerController.size = PlayerController.size + 1;



        }
    }

}
