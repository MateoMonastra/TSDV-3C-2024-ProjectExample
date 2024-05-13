using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class JumpBehaviour : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float force = 15f;
    private bool _iWantToJump;
    [SerializeField] private float groundedDistance = .01f;
    [SerializeField] private LayerMask floor;
    [SerializeField] private Transform feetPivot;
    private Ray _groundRay;
    [SerializeField] private float maxFloorAngle = 60f;

    private void Reset()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        //These are both the same
        // if (!_rigidbody)
        // {
        //     _rigidbody = GetComponent<Rigidbody>();
        // }

        _rigidbody ??= GetComponent<Rigidbody>();
    }

    public IEnumerator JumpCoroutine()
    {
        if(!CanJump())
            yield break;
        yield return new WaitForFixedUpdate();
        _rigidbody.AddForce(Vector3.up * force, ForceMode.Impulse);
    }

    private bool CanJump()
    {
        if (!feetPivot)
        {
            Debug.LogWarning($"{name}: {nameof(feetPivot)} is null!");
            return false;
        }
        //I must be at ground level
        if (Physics.Raycast(feetPivot.position, Vector3.down, out var hit, groundedDistance, floor))
        {
            var contactAngle = Vector3.Angle(hit.normal, Vector3.up);
            if (contactAngle >= maxFloorAngle)
                return false;
            return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(feetPivot.position, Vector3.down * groundedDistance);
    }
}