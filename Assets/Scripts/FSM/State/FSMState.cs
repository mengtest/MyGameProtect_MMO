﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSMState {
    protected Vector3 destPos;//目标点
    protected float curRotSpeed;//旋转速度
    protected float curSpeed;//移动速度
    protected FSM.AIController aiController;
    

    protected Transform owner;
    protected Animator animator;
    protected AIPath aiPath;
    private Transform pathTarget;
    protected Transform AIPathTarget
    {
        get
        {
            if(pathTarget == null)
            {
                GameObject o = new GameObject(owner.name + "_AIPathTarget");
                pathTarget = o.transform;
            }
            return pathTarget;
        }
    }
    /// <summary>
    /// 进入状态
    /// </summary>
    public abstract void Enter();
    /// <summary>
    /// 离开状态
    /// </summary>
    public abstract void Exit();
    /// <summary>
    /// 更新状态
    /// </summary>
    public abstract void OnUpdate(Transform target);
    /// <summary>
    /// 固定更新
    /// </summary>
    public void OnFiexedUpdate(Transform target)
    {
        bool b = OnUpdateState(target);
        if(b== false)
        {
            OnUpdateAction(target);
        }
    }

    /// <summary>
    /// 状态更新
    /// </summary>
    /// <param name="target"></param>
    /// <returns>是否有状态变换</returns>
    protected abstract bool OnUpdateState(Transform target);
    /// <summary>
    /// 行为更新
    /// </summary>
    /// <param name="target"></param>
    protected abstract void OnUpdateAction(Transform target);
    public float GetDistanceXZ(Vector3 source, Vector3 target)
    {
        return Vector2.Distance(new Vector2(source.x, source.z), new Vector2(target.x, target.z));
    }
   
    /// <summary>
    /// 是否处于某个状态下
    /// </summary>
    /// <param name="animatorStateName"></param>
    /// <returns></returns>
    protected bool IsAnimatorName(string animatorStateName)
    {
        AnimatorStateInfo animatorInfo = animator.GetCurrentAnimatorStateInfo(0);
        return animatorInfo.IsName(animatorStateName) && !animator.IsInTransition(0);
    }
    /// <summary>
    /// 开始移动
    /// </summary>
    protected void StartMove()
    {
        if (aiPath != null)
        {
            //AIPathTarget.position = destPos;
            aiPath.target = AIPathTarget;
            aiPath.speed = curSpeed;
            aiPath.enabled = true;
            animator.SetFloat("Speed", 1f);
        }
    }
    /// <summary>
    /// 停止移动
    /// </summary>
    protected void StopMove()
    {
        if (aiPath != null)
        {
            aiPath.target = null;
            aiPath.enabled = false;
            animator.SetFloat("Speed", 0f);
        }
    }
}
