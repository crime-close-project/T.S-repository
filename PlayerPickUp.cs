using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    public GameObject player;
    public Transform holdPos;
    private bool canDrop = true;
    private GameObject heldObj;
    private float PickUpRange = 5f;
    private int LayerNumber;
    private int originalLayer = -1;
    private Rigidbody heldRigidbody;

    void Start()
    {
        LayerNumber = LayerMask.NameToLayer("holdLayer");

        if (holdPos == null)
        {
            Debug.LogError("holdPos is not there");
        }
    }

    void Update()
    {
        // Pick up
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Hold button Pressed");

            if (heldObj == null)
            {
                TryPickUp();
            }
        }

        // Drop object
        if (Input.GetMouseButtonDown(1) && heldObj != null)
        {
            DropObject();
        }
    }

    private void TryPickUp()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position,
                            transform.TransformDirection(Vector3.forward),
                            out hit, PickUpRange))
        {
            
            string objTag = hit.transform.gameObject.tag;

            if (objTag == "CanPickUp")
            {
                heldObj = hit.transform.gameObject;
                originalLayer = heldObj.layer;
                heldRigidbody = heldObj.GetComponent<Rigidbody>();

                if (heldRigidbody != null)
                    heldRigidbody.isKinematic = true;

                heldObj.transform.SetParent(holdPos);
                heldObj.transform.localPosition = Vector3.zero;
                heldObj.transform.localRotation = Quaternion.identity;

                if (LayerNumber != -1)
                    heldObj.layer = LayerNumber;
            }
            else
            {
                if (objTag == "Untagged")
                    Debug.LogWarning("Hit object has no tag assigned. Add the 'canPickUp' tag in the Inspector and assign it to pickable objects.");
                else
                    Debug.Log("no tag");
            }
        }
    }

    private void DropObject()
    {
        heldObj.transform.SetParent(null);

        if (heldRigidbody != null)
            heldRigidbody.isKinematic = false;

        if (originalLayer != -1)
            heldObj.layer = originalLayer;

        heldObj = null;
        heldRigidbody = null;
        originalLayer = -1;
    }
}
