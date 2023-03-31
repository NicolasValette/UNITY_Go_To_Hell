using System;
using System.Collections;
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
        #region Events
        public event Action<DiceFace> OnDiceRollFinished;
        public event Action OnRollInvalid;
        #endregion

        private Rigidbody _rigidbody;
        [SerializeField]
        private float _rollingTime = 5f;
        [SerializeField]
        private float _flickForce = 2f;

        private DiceFace _face;
        public DiceFace Face { get => _face; } 
        public bool IsLaunched { get; private set; }
        
        //events

        private float _actualRolling = 0;
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
            gameObject.transform.SetParent(transform.parent.parent);
            gameObject.tag = "Untagged";
            _actualRolling = 0;
            StartCoroutine(WaitingRollingTime());
        }
        public IEnumerator WaitingRollingTime()
        {
            yield return new WaitForSeconds(0.2f);
            while (_actualRolling < _rollingTime)
            {
                _actualRolling += Time.deltaTime;
                if (_actualRolling < _rollingTime && _rigidbody.velocity.sqrMagnitude <= 0.01f && _rigidbody.angularVelocity.sqrMagnitude <= 0.01f)
                {
                    DiceFace face = GetDiceNumber();
                    
                    Debug.Log("Roll finish");
                    if (face == DiceFace.Invalid)
                    {
                        //Debug.Log("Invalid");
                        //OnRollInvalid?.Invoke();
                        //break;
                        FlickDice();
                    }
                    else
                    {
                        OnDiceRollFinished?.Invoke(face);
                        break;
                    }
                }
                yield return null;
            }
            Debug.Log("Invalid");
            OnRollInvalid?.Invoke();
        }

        private void FlickDice()
        {
            _rigidbody.AddForce(Vector3.up * _flickForce, ForceMode.Impulse);
            _rigidbody.AddTorque(UnityEngine.Random.insideUnitSphere * _flickForce, ForceMode.Impulse);
        }
        private DiceFace GetDiceNumber()
        {
            if (Vector3.Dot(transform.forward, Vector3.up) > 0.9f)
            {
                _face = DiceFace.One;
            }
            else if (Vector3.Dot(transform.right, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Two;
            }
            else if (Vector3.Dot(-transform.up, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Three;
            }
            else if (Vector3.Dot(transform.up, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Four;
            }
            else if (Vector3.Dot(-transform.right, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Five;
            }
            else if (Vector3.Dot(-transform.forward, Vector3.up) > 0.9f)
            {
                _face = DiceFace.Six;
            }
            else
            {
                _face = DiceFace.Invalid;
            }
            return _face;
        }
       
        

    }





    
}
