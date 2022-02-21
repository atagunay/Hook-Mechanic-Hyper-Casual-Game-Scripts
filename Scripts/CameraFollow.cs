using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private float subset = 0f;

    [SerializeField]
    private GameObject cameraFinishPosition;

    public bool CanFollow = true;
    private bool isFollow = true;
  
    public static Vector3 offset;


    private void Start()
    {
        offset = new Vector3(0, 6f, -23);
    }

    void FixedUpdate()
    {
        if (isFollow == true)
        {
            Vector3 desiredPosition = Player.transform.position + offset;
            Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, 0.125f);
            transform.position = smoothPosition;
        }


    }

    public void GoFinishPosition()
    {
        isFollow = false;
        Camera.main.transform.DOMove(cameraFinishPosition.transform.position, 1f);
        Camera.main.transform.DORotate(cameraFinishPosition.transform.rotation.eulerAngles, 1f);
    }

    //public static void IncreaseCamera()
    //{
    //    offset = Vector3.Lerp(offset, offset + new Vector3(0, 2, -2),1f);
    //}

}


