using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Controllers
{
    public class UIBearPediaItemsSwitcher : MonoBehaviour
    {
        [SerializeField] private List<GameObject> items;
        private int _currentItemIndex;

        public void Next()
        {
            if(_currentItemIndex + 1 == items.Count)
                return;
            
            items[_currentItemIndex].SetActive(false);

            _currentItemIndex++;
            items[_currentItemIndex].SetActive(true);
        }

        public void Previous()
        {
            if(_currentItemIndex == 0)
                return;
            
            items[_currentItemIndex].SetActive(false);
            
            _currentItemIndex--;
            items[_currentItemIndex].SetActive(true);
        }

        private void OnEnable()
        {
            _currentItemIndex = 0;
            foreach (var item in items)
                item.SetActive(false);
            
            items[_currentItemIndex].SetActive(true);
        }
    }
}