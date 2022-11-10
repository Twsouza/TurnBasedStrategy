using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
  [SerializeField] private Animator unitAnimator;
  private Vector3 targetPosition;
  private GridPosition gridPosition;

  private void Awake()
  {
    targetPosition = transform.position;
  }

  private void Start()
  {
    GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

    this.gridPosition = gridPosition;
  }

  private void Update()
  {
    float stoppingDistance = .1f;
    if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
    {
      unitAnimator.SetBool("IsWalking", true);

      Vector3 moveDirection = (targetPosition - transform.position).normalized;
      float moveSpeed = 4f;
      transform.position += moveDirection * moveSpeed * Time.deltaTime;

      float rotateSpeed = 10f;
      transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }
    else
    {
      unitAnimator.SetBool("IsWalking", false);
    }

    GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
    if (newGridPosition != gridPosition)
    {
      LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
      gridPosition = newGridPosition;
    }
  }

  public void Move(Vector3 targetPosition)
  {
    this.targetPosition = targetPosition;
  }
}
