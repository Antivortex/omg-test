using System.Collections.Generic;
using UnityEngine;

namespace EngineCore
{
    public class GoPool
    {
        private readonly Stack<GameObject> _inactive = new Stack<GameObject>();
        private readonly Transform _parent;

        public GoPool(Transform parent)
        {
            _parent = parent;
        }

        public GameObject Get(string name, params System.Type[] components)
        {
            GameObject obj;
            if (_inactive.Count > 0)
            {
                obj = _inactive.Pop();
                obj.name = name;
                obj.SetActive(true);
            }
            else
            {
                obj = new GameObject(name, components);
                obj.transform.SetParent(_parent, false);
            }

            return obj;
        }

        public void Return(GameObject obj)
        {
            obj.SetActive(false);
            _inactive.Push(obj);
        }
    }
}
