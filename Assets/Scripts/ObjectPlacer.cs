using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ObjectPlacer : MonoBehaviour
{

    public GameObject objectToPlace;
    public GameObject currentObject;
    public ARRaycastManager raycastManager;
    public GameObject cursor;
    public bool useCursor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (useCursor)
        {
            Vector2 screenPosition = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            List<ARRaycastHit> hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenPosition, hits, TrackableType.Planes);
            if (hits.Count > 0)
            {
                cursor.transform.position = hits[0].pose.position;
            }
        }

        if (Input.GetTouch(0).phase == TouchPhase.Ended && !IsPointerOverUIObject(Input.GetTouch(0)))
        {
            if (useCursor)
            {  
                currentObject = Instantiate(objectToPlace, cursor.transform.position, cursor.transform.rotation);
                currentObject.transform.rotation = Quaternion.identity;
            }

            else
            {
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                raycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);
                currentObject = Instantiate(objectToPlace, hits[0].pose.position, hits[0].pose.rotation);
            }
        }
    }

    bool IsPointerOverUIObject(Touch touch)
    {
        PointerEventData eventPosition = new PointerEventData(EventSystem.current);
        eventPosition.position = new Vector2(touch.position.x, touch.position.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventPosition, results);

        return results.Count > 0;
    }
    public void ScaleUp()
    {
        currentObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void ScaleDown()
    {
        currentObject.transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("Marker");
    }

    public void ToggleCursor()
    {
        if (useCursor == true)
        {
            useCursor = false;
            cursor.SetActive(false);
        }
        else if (useCursor == false)
        {
            useCursor = true;
            cursor.SetActive(true);
        }
    }
}
