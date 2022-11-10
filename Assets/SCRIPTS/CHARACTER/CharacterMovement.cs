using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.HID;

[RequireComponent(typeof(NavMeshMovement))]
[AddComponentMenu("KH/CharacterMovement")]
public class CharacterMovement : MonoBehaviour
{

    private NavMeshMovement movement;
   

    private void OnEnable()
    {
        movement = gameObject.GetComponent<NavMeshMovement>();
        PlayerInteractions.Instance.ev_RayTouching += MoveTo;
    }

    private void OnDisable()
    {
        PlayerInteractions.Instance.ev_RayTouching -= MoveTo;
    }

    private void MoveTo(RaycastHit hit)
    {
        MoveCharacterTo(hit.point);
    }

    public void MoveCharacterTo(Vector3 positionToGo, float maxSampleDistance = 1.0f)
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(positionToGo, out hit, maxSampleDistance, NavMesh.AllAreas);
        movement.MoveTo(hit.position);
        print("Move");
    }


    public void StopMovement()
    {
        movement.StopMovement();
    }
}
