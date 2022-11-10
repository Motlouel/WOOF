using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KH/DetectIfInRange")]
public class DetectIfInRange : MonoBehaviour
{

    [SerializeField] UnityEvent onActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovement>())
        {
            other.GetComponent<CharacterMovement>().StopMovement();
            onActivate?.Invoke();
        }

    }








}
