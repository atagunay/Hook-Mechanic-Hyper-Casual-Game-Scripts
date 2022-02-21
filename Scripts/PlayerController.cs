using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Joystick joystick;
    
    [SerializeField]
    private GameObject swotPrefab;

    [SerializeField]
    private GameObject sceneThief;

    [SerializeField]
    private GameObject takeParticle;


    public static float process = 0.6f;
    public static float minValue;
    public static float maxValue;

    public static LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
    public static Vector3 rotateVector;
    public static int size;
    public static int flag;

    private UIManager uIManager;
    private Target target;
    private GameObject sceneManager;
    private Vector3 hareket;


    void Start()
    {
        linkedList.Clear();
        size = 0;
        flag = 0;
        process = 0.6f;
        rotateVector = new Vector3(-5, 0, 0);
        minValue = -7;
        maxValue = 22;
        Time.timeScale = 1.2f;

        Vibration.Init();
        linkedList.AddFirst(gameObject);
        sceneThief.GetComponent<ThiefControl>().Target = linkedList.Last.Value;


        sceneManager = GameObject.Find("SceneManager");

       
        target = sceneManager.GetComponent<Target>();
        uIManager = sceneManager.GetComponent<UIManager>();

        Debug.Log("Number:" + linkedList.Count);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (flag == 1)
        {
           
            // joystick.Horizontal / 2
            hareket = new Vector3(joystick.Horizontal / 2.5f, 0, process);
            gameObject.transform.position += hareket;

            float xCordinate = Mathf.Clamp(gameObject.transform.position.x, minValue, maxValue);
            gameObject.transform.position = new Vector3(xCordinate, gameObject.transform.position.y, gameObject.transform.position.z);


        }

        gameObject.transform.GetChild(0).Rotate(rotateVector);


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "take")
        {
            other.gameObject.tag = "taken";

            GameObject destroyIt = Instantiate(takeParticle, other.gameObject.transform.position + Vector3.up ,Quaternion.identity);
            Destroy(destroyIt, 1.5f);

           
            uIManager.IncraseCM();

            
            other.transform.DOScale(new Vector3(0, 0, 0), 0.1f).OnComplete(() => other.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), 0.1f).OnComplete(() =>
            sceneThief.GetComponent<ThiefControl>().Target = other.gameObject));
          

            Follow nesne = other.gameObject.AddComponent<Follow>();

            nesne.Target = linkedList.Last.Value;
            linkedList.AddLast(other.gameObject);
            Debug.Log("Adet:" + linkedList.Count);
            
            
            Debug.Log("Number:" + linkedList.Count);

            Vibration.VibratePop();
            CameraFollow.offset += new Vector3(0, 1, -4);
            size = size + 1;

            //StartCoroutine(sceneThief.GetComponent<ThiefControl>().Cor_ChangeThiefLocation());


        }
        else if (other.gameObject.tag == "finish")
        {


            flag = 0;

            
            uIManager.StartTapAllert();

            foreach (GameObject eleman in linkedList)
            {
                eleman.GetComponent<BoxCollider>().enabled = false;

            }

            
        }
        else if(other.gameObject.tag == "leftTrigger")
        {
            maxValue = 8;
        }
        else if(other.gameObject.tag == "rightTrigger")
        {
            minValue = 8;
        }
        

    }

    public void Jump()
    {
       
        flag = 0;
        //  Camera.main.GetComponent<CameraFollow>().enabled = false;
       // CameraFollow.offset = CameraFollow.offset + new Vector3(0, -8, -10);

        Debug.Log(linkedList.Count);
        //gameObject.transform.DOMove(sceneManager.GetComponent<Target>().Target2.transform.position, 1f).OnComplete(() => StartCoroutine(CloseRigidbody()));

        float targetPoint = (linkedList.Count - 1) + SliderManager.slideMultiply;
       
        Target.targetIndex = Mathf.Clamp((int)Mathf.Round(targetPoint), 0, 10);
      

        gameObject.transform.DOMove(target.targetArr[Target.targetIndex].transform.position, 2f).OnComplete(() => StartCoroutine(CloseRigidbody()));
        StartCoroutine(OpenParticle());

        foreach (GameObject eleman in linkedList)
        {
            if (eleman.name != "PLAYER")
            {
                eleman.GetComponent<BoxCollider>().enabled = false;
                eleman.GetComponent<Rigidbody>().useGravity = true;

            }

        }

        if(Target.targetIndex > 2)
        {
            Sway();
        }
        else
        {
            StartCoroutine(OpenLosePanel());
            
        }

        
        

    }

    IEnumerator OpenParticle()
    {
        for (int i = 0; i <= Target.targetIndex; i++)
        {
            yield return new WaitForSeconds(0.15f);
            sceneManager.GetComponent<SoundManager>().PlayWinSound();
            target.particles[i].SetActive(true);
        }
        

    }

    IEnumerator OpenLosePanel()
    {
        yield return new WaitForSeconds(1);
        uIManager.LosePanel();
    }

    IEnumerator CloseRigidbody()
    {
        yield return new WaitForSeconds(0.2f);

        foreach (GameObject eleman in linkedList)
        {
            if (eleman.name != "PLAYER")
                eleman.GetComponent<Rigidbody>().useGravity = false;

        }


    }

    public void Sway()
    {
        //gameObject.transform.DOPath(target.pathTarget1, 2).SetLoops(-1);
        Instantiate(swotPrefab, Vector3.zero, Quaternion.identity);
    }



}
