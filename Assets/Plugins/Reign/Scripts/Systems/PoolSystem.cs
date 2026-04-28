using System;
using System.Collections.Generic;
using Reign.Interfaces;
using Reign.Utility;
using UnityEngine;

namespace Reign.Systems
{
    public sealed class PoolSystem : System<PoolSystem>
    {
        private readonly Dictionary<Type, Queue<IPoolable>> pools = new(); // Every type has designated queue

        private Queue<IPoolable> GetPool(Type type)
        {
            if (!pools.TryGetValue(type, out var singular))
            {
                singular = new();
                pools[type] = singular;
            }

            return singular;
        }

        /// <summary>
        /// Spawn an object from object pool
        /// </summary>
        public T Spawn<T>(T prefab) where T : MonoBehaviour, IPoolable
        {
            Type type = typeof(T);

            T obj;
            Queue<IPoolable> singular = GetPool(type);

            if (singular.Count > 0)
            {
                obj = (T)singular.Dequeue();
            }
            else
            {
                obj = Instantiate(prefab);
            }

            obj.OnSpawn();
            obj.gameObject.SetActive(true);

            return obj;
        }

        /// <summary>
        /// Make the object return to the pool
        /// </summary>
        public void Reclaim<T>(T obj) where T : MonoBehaviour, IPoolable
        {
            obj.OnReclaim();
            obj.OnReset();
            obj.gameObject.SetActive(false);

            Type type = obj.GetType();
            Queue<IPoolable> singular = GetPool(type);

            singular.Enqueue(obj);
        }
    }
}