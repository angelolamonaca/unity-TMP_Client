using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Position
    {
        [SerializeField] public float x;
        [SerializeField] public float y;
        [SerializeField] public float z;

        public Position(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}