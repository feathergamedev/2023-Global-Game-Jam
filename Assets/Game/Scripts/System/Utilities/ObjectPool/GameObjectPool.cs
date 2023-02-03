using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Lapis.Utilities
{
    public class GameObjectPool<T> where T : Component
    {
        private Queue<T> pool;
        private T objectRef;
        private Transform objectRoot;
        private int initNum = 10;
        
        public GameObjectPool(T target, int initNum = 10)
        {
            Init(target, initNum);
        }

        public void Init(T target, int initNum)
        {
            objectRef = target;
            this.initNum = initNum;
            pool = new Queue<T>();
            objectRoot = new GameObject($"{typeof(T).Name}_ObjectPool").transform;

            for(int i = 0; i < initNum; i++)
            {
                var obj = Object.Instantiate(objectRef, objectRoot);
                obj.gameObject.SetActive(false);

                pool.Enqueue(obj);
            }
        }

        public T GetObject()
        {
            T obj;
            if (pool.Count == 0)
                obj = Object.Instantiate(objectRef, objectRoot);
            else
                obj = pool.Dequeue();

            obj.gameObject.SetActive(true);
            return obj;
        }

        public void RecycleObject(T obj)
        {
            obj.gameObject.SetActive(false);
            pool.Enqueue(obj);
        }
    }
}

