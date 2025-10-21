using UnityEngine;
using UnityEngine.Serialization;

public class CamHolder : MonoBehaviour
{

    [FormerlySerializedAs("cameraPositon")]
    [Tooltip(" the camera Transform to follow (drag the Camera here).")]

    public Transform cameraPosition;

    private void Update()
    {

        transform.position = cameraPosition.position;
    }
     
        //used to get the cameras pos 
    
}

