using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gotohell.Dice
{
    public enum DiceFace
    {
        Invalid,
        One,
        Two,
        Three,
        Four,
        Five,
        Six
    }

    [RequireComponent(typeof(Rigidbody))]
    public class DiceBehaviour : MonoBehaviour
    {
        public event Action<DiceFace> OnRollFinished;
        public event Action OnRollInvalid;
        private Rigidbody _rigidbody;
        private float _rollingTime = 5f;

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
            StartCoroutine(WaitingRollingTime());
        }
        public IEnumerator WaitingRollingTime()
        {
            Debug.Log("Rolling");
            yield return new WaitForSeconds(_rollingTime);
            if (_rigidbody.velocity.sqrMagnitude <= 0.01f && _rigidbody.angularVelocity.sqrMagnitude <= 0.01f)
            {
                DiceFace face = GetDiceNumber();
                Debug.Log("Roll finish");
                if (face == DiceFace.Invalid)
                {
                    OnRollInvalid?.Invoke();
                }
                else
                {
                    OnRollFinished?.Invoke(face);
                }
                
            }
            else
            {
                OnRollInvalid?.Invoke();
            }
        }

        private DiceFace GetDiceNumber()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) > 0.9f)
            {
                return DiceFace.One;
            }
            if (Vector3.Dot(transform.right, Vector3.up) > 0.9f)
            {
                return DiceFace.Two;
            }
            if (Vector3.Dot(-transform.up, Vector3.up) > 0.9f)
            {
                return DiceFace.Three;
            }
            if (Vector3.Dot(transform.up, Vector3.up) > 0.9f)
            {
                return DiceFace.Four;
            }
            if (Vector3.Dot(-transform.right, Vector3.up) > 0.9f)
            {
                return DiceFace.Five;
            }
            if (Vector3.Dot(-transform.forward, Vector3.up) > 0.9f)
            {
                return DiceFace.Six;
            }
            return DiceFace.Invalid;

        }
    }
}
