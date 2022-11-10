using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryFade : MonoBehaviour
{

    public enum FadePosition
    {
        NONE = 0,
        LEFT = 1,
        RIGHT = 2,
    }

    public FadePosition fadePosition = FadePosition.NONE;
    private float fadeVal = 0.0f;

    CameraRig rig;
    MeshRenderer mren;
    void Start()
    {
        rig = FindObjectOfType<CameraRig>();
    }

    void Update()
    {
        mren = GetComponent<MeshRenderer>();
        if (rig && mren && mren.material)
        {
            float newFaded = 0.0f;
            switch (fadePosition)
            {
                case FadePosition.LEFT:
                    newFaded = rig.GetRotSteps() == 0 ? 1.0f : 0.0f;
                    break;
                case FadePosition.RIGHT:
                    newFaded = rig.GetRotSteps() == 1 ? 1.0f : 0.0f;
                    break;
                default:
                    break;
            }
            var mat = mren.material;

            float interpolatedFade = Mathf.MoveTowards(fadeVal, newFaded, Time.deltaTime);
           
            mat.SetFloat("_FuzzyFade", interpolatedFade);
            fadeVal = interpolatedFade;
            mren.material = mat;
        }
    }
}
