using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lortedo.Utilities.Extensions
{
    public static class QueueExtension
    {
        public static void RemoveEldestEntry<T>(this Queue<T> queue, int size)
        {
            while (queue.Count > size)
            {
                queue.Dequeue();
            }
        }
    }
}
