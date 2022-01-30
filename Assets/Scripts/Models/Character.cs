using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class Character
    {
        [SerializeField] public int id;
        [SerializeField] public Position position;
    }
}