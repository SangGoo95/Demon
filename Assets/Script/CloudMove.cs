using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    // 900 , -750

    private RectTransform rectTransform;

    private bool isLeft = false;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if(rectTransform.localPosition.x >= 850)
        {
            isLeft = true;
        }

        if (rectTransform.localPosition.x <= -750)
        {
            isLeft = false;
        }

        if(isLeft)
        {
            rectTransform.localPosition -= rectTransform.right * 100 * Time.deltaTime;
        }
        else
        {
            rectTransform.localPosition += rectTransform.right * 100 * Time.deltaTime;
        }
    }
}
