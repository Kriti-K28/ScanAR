﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.XR.Interaction.Toolkit.AR;
public class InputManager : ARBaseGestureInteractable
{
    [SerializeField] private Camera arCam;
    [SerializeField] private ARRaycastManager _raycastManager;
   [SerializeField] private GameObject crosshair;
    
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();

    private Touch touch;
    private Pose pose;
   

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    // void Update()
    // {
      
       
    //     if(Input.touchCount>0){
    //         touch=Input.GetTouch(0);
    //     }
    //     if(Input.touchCount<=0||touch.phase!=TouchPhase.Began)
    //         return;
    //    // if(IsPointerOverUI(touch)) return;
    //     Ray ray=arCam.ScreenPointToRay(touch.position);
    //           if(_raycastManager.Raycast(ray,_hits))
    //           {
    //               Pose pose=_hits[0].pose;
    //               Instantiate(DataHandler.Instance.furniture,pose.position,pose.rotation);
    //           }
       
    // }
   protected override bool CanStartManipulationForGesture(TapGesture gesture)
   {
       if(gesture.targetObject==null)
           return true;
       return false;
   }
    protected override void OnEndManipulation(TapGesture gesture)
   {
       if(gesture.isCanceled)
           return ;
       if(gesture.targetObject !=null && IsPointerOverUI(gesture))
       {
           return;
       }
       
       if (GestureTransformationUtility.Raycast(gesture.startPosition, _hits,TrackableType.PlaneWithinPolygon))
        {
            GameObject placeObj= Instantiate(DataHandler.Instance.furniture,pose.position,pose.rotation);
            var anchorObj=new GameObject("PlacementAnchor");
            anchorObj.transform.position=pose.position;
            anchorObj.transform.rotation=pose.rotation;
            placeObj.transform.parent=anchorObj.transform;
        }
   }
   void FixedUpdate()
   {
      CrosshairCalculation();
   }
   //bool IsPointerOverUI(Touch touch)
    bool IsPointerOverUI(TapGesture touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        //eventData.position = new Vector2(touch.position.x, touch.position.y);
       eventData.position = new Vector2(touch.startPosition.x, touch.startPosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    //private RaycastHit hit;
    void CrosshairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        Ray ray = arCam.ScreenPointToRay(origin);
        
       // if (_raycastManager.Raycast(ray, _hits))
       if (GestureTransformationUtility.Raycast(origin, _hits,TrackableType.PlaneWithinPolygon))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(90,0,0);
        }
        // else if (Physics.Raycast(ray, out hit))
        // {
        //     crosshair.transform.position = hit.point;
        //     crosshair.transform.up = hit.normal;
        // }
    }
}
 