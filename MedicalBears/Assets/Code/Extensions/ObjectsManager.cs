using UnityEngine;

namespace Extensions
{
    public static class ObjectsManager
    {
        public static GameObject CreateObject(GameObject obj)
        {
            return Object.Instantiate(obj);
        }
        
        public static GameObject CreateObject(GameObject obj, Vector3 transform)
        {
            return Object.Instantiate(obj, transform, Quaternion.identity);
        }
        
        public static GameObject CreateObject(GameObject obj, GameObject parent)
        {
            var newObj = Object.Instantiate(obj);
            SetParent(newObj, parent);
            return newObj;
        }
        
        public static GameObject CreateObject(GameObject obj, GameObject parent, Vector3 transform)
        {
            var newObj = CreateObject(obj, transform);
            SetParent(newObj, parent);
            return newObj;
        }

        public static void SetParent(GameObject obj, GameObject parent)
        {
            obj.transform.SetParent(parent.transform, false);
        }
        
        public static void SetParentWithWorldPosition(GameObject obj, GameObject parent)
        {
            obj.transform.SetParent(parent.transform, true);
        }

        public static void FillParentWithObject(GameObject obj)
        {
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0, 0);
            rectTransform.anchorMax = new Vector2(1, 1);
            
            rectTransform.anchoredPosition = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);            
            rectTransform.offsetMax = new Vector2(0, 0);
        }
        
        public static void CenterObject(GameObject obj)
        {
            var rectTransform = obj.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            
            rectTransform.anchoredPosition = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);            
            rectTransform.offsetMax = new Vector2(0, 0);
        }
    }
}