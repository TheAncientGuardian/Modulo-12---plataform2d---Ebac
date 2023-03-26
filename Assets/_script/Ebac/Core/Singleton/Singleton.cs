using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ebac.Core.Singleton
{
    public class Singleton<t> : MonoBehaviour where t : MonoBehaviour
    {
        public static t Instance;
        private void Awake() 
        {
            if(Instance == null)
                Instance = GetComponent<t>();
        else
            Destroy(gameObject);    
        }
    }

}

