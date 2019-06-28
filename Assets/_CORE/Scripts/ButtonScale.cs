using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonScale : MonoBehaviour
{
    public float ScaleAmount = 1.5f;
    Vector3 startScale;
    Vector3 endScale;
    [SerializeField] float speed = 4; 

    private void Start()
    {
        startScale = transform.localScale;
        endScale = startScale * ScaleAmount;
    }

    public void ButtonEnter()
    {
        Debug.Log("ButtonPressed");
        StartCoroutine(doScaleUp());
       
    }

    public void ButtonExit()
    {
        transform.localScale = startScale;
        Debug.Log("ButtonExit");
        StartCoroutine(doScaleDown());
    }
    private IEnumerator doScaleUp()
    {
        float t = 0;

        while(t < 1)
        {
            // Transforming object on update
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            t += Time.deltaTime * speed;

            yield return null;
        }
    }

    private IEnumerator doScaleDown()
    {
        float t = 1;
        while (t > 0)
        {
            transform.localScale = Vector3.Lerp(startScale, endScale, t);
            t -= Time.deltaTime * speed;

            yield return null;
        }
    }
}
