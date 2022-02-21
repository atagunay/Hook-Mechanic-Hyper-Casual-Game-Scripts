using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Swot : MonoBehaviour
{
    

    [SerializeField]
    private Animator swotAnimatorController;

    [SerializeField]
    private GameObject bina;


    Tween temp;

    GameObject sceneManager;




    private void Start()
    {
        RunRope();
        sceneManager = GameObject.Find("SceneManager");
    }

    private void RunRope()
    {
        temp = gameObject.transform.DOMove(bina.transform.GetChild(0).position, 3f).OnComplete(() => Climb());
    }

    private void Climb()
    {
        Debug.Log("býttý");
        DOTween.Kill(temp);

        swotAnimatorController.SetBool("isClimb", true);
        // gameObject.transform.transform.DOMoveY(30, 3f);

        Vector3 minus = new Vector3(0, -1, 0);

        gameObject.transform.DOMove(sceneManager.GetComponent<Target>().targetArr[Target.targetIndex] .transform.position + minus, 3).OnComplete(() => DestroySwot());



    }

    private void DestroySwot()
    {
        Debug.Log("run girdi");
        swotAnimatorController.SetBool("isClimb", false);
        swotAnimatorController.SetBool("isRun", true);

        //gameObject.transform.rotation./ = new Vector3(0, 0, 0); 

        gameObject.transform.Rotate(new Vector3(0, -90, 0));


        gameObject.transform.position = new Vector3(bina.transform.position.x, 1, bina.transform.position.z);

        gameObject.transform.DOMove(new Vector3(bina.transform.position.x - 20,1,bina.transform.position.z), 2).OnComplete(() => Dance());
        Camera.main.GetComponent<CameraFollow>().GoFinishPosition();
        
    }

    private void Dance()
    {
        swotAnimatorController.SetBool("isDance", true);
        StartCoroutine(OpenWinPanel());
        
    }

    IEnumerator OpenWinPanel()
    {
        yield return new WaitForSeconds(1);
        sceneManager.GetComponent<UIManager>().WinPanel();
    }
}
