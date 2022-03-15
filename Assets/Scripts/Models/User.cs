using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class User
    {
        [SerializeField] public int id;
        [SerializeField] public Position position;

        public User(int id, Position position)
        {
            this.id = id;
            this.position = position;
        }
    }
}