using System;
using System.Collections.Generic;
using UnityEngine;

namespace Models
{
    [Serializable]
    public class World
    {
        [SerializeField] public List<User> characterList;
    }
}