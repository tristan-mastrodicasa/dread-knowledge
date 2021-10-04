using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CamCampaign : MonoBehaviour
{
    private Transform daisTransform;
    private Transform cameraPivot;


    private void Awake() {
        daisTransform = GameObject.Find("Dais").transform;
        cameraPivot = GameObject.Find("Camera Pivot").transform;

        postProcessVol = GetComponentInChildren<Volume>();
        postProcessVol.profile.TryGet<DepthOfField>(out dof);
    }


    // The normalized amount the camera is zoomed in from the wide view.
    private float zoomAmount = 1f;

    // Time it takes to zoom fully in or out.
    public float zoomTime = 2f;

    private void Update() {

        // Zoom out to the wide view when Left Shift is held down.
        if (Input.GetKey(KeyCode.LeftShift)){
            zoomAmount -= (1f/zoomTime) * Time.deltaTime;
        }
        else{
            zoomAmount += (1f/zoomTime) * Time.deltaTime;
        }

        // Normalize the value.
        zoomAmount = Mathf.Clamp01(zoomAmount);

        SetCamPosition();

    }


    // Camera's distance from party when fully zoomed in.
    public float zoomedDistance = 20f;


    // Sets the camera's position given the current zoom value.
    void SetCamPosition(){
        Vector3 zoomedOutPos = cameraPivot.position;
        Vector3 zoomedInPos = daisTransform.position + (new Vector3(1f, 1f, 1f)).normalized * zoomedDistance;

        // Interpolate between zoomed in and out positions to place the camera.
        Camera.main.transform.position = Vector3.Lerp(zoomedOutPos, zoomedInPos, zoomAmount);


        // Do the same for its rotation.
        Quaternion zoomedOutRot = cameraPivot.rotation;
        Quaternion zoomedInRot = Quaternion.LookRotation(new Vector3(-1f, -1f, -1f), Vector3.up);

        Camera.main.transform.rotation = Quaternion.Lerp(zoomedOutRot, zoomedInRot, zoomAmount);


        SetDepthOfField();

    }


    private Volume postProcessVol;
    private DepthOfField dof;

    public float zoomedOutFocalDistance = 15f;

    public float zoomedOutFocalLength = 30f;
    public float zoomedInFocalLength = 150f;

    public float zoomedOutAperture = 3f;
    public float zoomedInAperture = 1.5f;


    void SetDepthOfField(){

        float distanceFromGroup = Vector3.Distance(daisTransform.position, Camera.main.transform.position);

        float focalDistance = Mathf.Lerp(zoomedOutFocalDistance, distanceFromGroup, zoomAmount);

        float focalLength = Mathf.Lerp(zoomedOutFocalLength, zoomedInFocalLength, zoomAmount);
        
        float aperture = Mathf.Lerp(zoomedOutAperture, zoomedInAperture, zoomAmount);





        dof.focusDistance.SetValue(new MinFloatParameter(focalDistance, 0f, true));
        dof.focalLength.SetValue(new ClampedFloatParameter(focalLength, 0f, 300f, true));
        dof.aperture.SetValue(new ClampedFloatParameter(aperture, 1f, 32f, true));

        //postProcessVol.profile.TryGet<DepthOfField>(out dof);

        //Debug.Log(dof.focusDistance + ", " + dof.focalLength + ", " + dof.aperture);
    }
}
