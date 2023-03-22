using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.Dice
{


    [RequireComponent(typeof(Rigidbody))]
    public class DiceBehaviour : MonoBehaviour
    {
        public static event Action OnRollFinished;
        public static event Action OnRollInvalid;
        private Rigidbody _rigidbody;
        private float _rollingTime;

        public bool IsLaunched { get; private set; }
        // Start is called before the first frame update
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Launch()
        {
            IsLaunched = true;
        }
        public IEnumerator WaitingRollingTime()
        {
            yield return new WaitForSeconds(_rollingTime);
            if (_rigidbody.velocity.sqrMagnitude <= 0.01f && _rigidbody.angularVelocity.sqrMagnitude <= 0.01f)
            {
                OnRollFinished?.Invoke();
            }
            //else D
        }
    }
}
