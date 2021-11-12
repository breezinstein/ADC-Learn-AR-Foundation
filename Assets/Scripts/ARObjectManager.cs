using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ARObjectManager : MonoBehaviour
{
    public ARRaycastManager aRRaycastManager;
    public GameObject ARObjectPrefab;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTouch();
    }

    /// We want to check for touches on the screen
    /// we would want it to run every frame.
    private void CheckTouch()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Ended)
            {
                if(Input.touchCount == 1)
                {
                    if(aRRaycastManager.Raycast(touch.position, hits))
                    {
                        Pose pose = hits[0].pose;
                        CreateObject(pose.position);
                        return;
                    }
                }
            }
        }
    }

    //We would also want to create the object in the scene.
    // The method takes a position as a parameter.
    private void CreateObject(Vector3 position)
    {
        GameObject obj = Instantiate(ARObjectPrefab, position, Quaternion.identity);
        if(obj.GetComponent<ARAnchor>() == null)
        {
            obj.AddComponent<ARAnchor>();
        }
    }
}
