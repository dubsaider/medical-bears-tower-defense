using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    public float speed;
    public float availableOffset;
    
    private bool movingLeft;
    private RectTransform rectTransform;

    private void OnEnable()
    {
        movingLeft = false;

        rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(0, 0);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (rectTransform.anchoredPosition.x * -1 > availableOffset) //rectTransform.sizeDelta.x + rectTransform.anchoredPosition.x)
                movingLeft = false;

            rectTransform.anchoredPosition += new Vector2(-1f * speed, 0);
        }
        else
        {
            if(rectTransform.anchoredPosition.x * -1 <= 0)
                movingLeft = true;

            rectTransform.anchoredPosition += new Vector2(1f * speed, 0);
        }
    }
}