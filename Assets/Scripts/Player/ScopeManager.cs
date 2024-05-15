using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScopeManager : MonoBehaviour
{
    public Transform playerTransform;
    //
    public GameObject shootPosNoScope;
    public Camera mainCamera;

    // scope
    public GameObject shootPosWithScope;
    public Camera cameraScope;
    public GameObject scopeUI;
    private int scopeSight;
    private int scopeMaxSight = 50;
    private int scopeMinSight = 20;
    public int reductionRateView;
    //
    private bool isScope = false;
    // Start is called before the first frame update
    void Start()
    {
        isScope = false;
        ActiviteScope(false);
        scopeSight = scopeMaxSight;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isScope = !isScope;
            ActiviteScope(isScope);
        }
        CustomizeScopeSight();
    }

    private void ActiviteScope(bool isOnScope)
    {

        shootPosNoScope.SetActive(!isOnScope);
        mainCamera.enabled = !isOnScope;

        // bat nham
        shootPosWithScope.SetActive(isOnScope);
        cameraScope.enabled = isOnScope;
        scopeUI.SetActive(isOnScope);

    }
    public void CustomizeScopeSight()
    {
        if (isScope)
        {
            float mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");
            //Debug.Log(mouseScrollValue);
            scopeSight += reductionRateView*(int)(mouseScrollValue*10);
            scopeSight = Math.Clamp(scopeSight, scopeMinSight, scopeMaxSight);
            cameraScope.fieldOfView = scopeSight;

        }
    }

    private void OnEnable()
    {
        if (isScope)
        {
            ActiviteScope(true);
        }
    }

    private void OnDisable()
    {
        if(isScope)
        {
            ActiviteScope(false);
        }
    }
}
