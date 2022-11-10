using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("KH/GPE_EndPosition")]
public class GPE_EndPosition : MonoBehaviour
{
    [SerializeField] private UnityEvent onEndInteraction;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag( "End"))
        {
            //FInish the level
            onEndInteraction?.Invoke();
            print("FInish pull cord");
        }
    }
}
