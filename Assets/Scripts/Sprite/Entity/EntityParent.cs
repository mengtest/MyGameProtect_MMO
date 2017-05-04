﻿using UnityEngine;
using System.Collections;
using BattleFramework.Data;
using Mogo.Util;
using System.Collections.Generic;
using System;

public abstract class EntityParent {

    public uint ID;
    public Animator animator;
    public GameObject gameObject { get; set; }
    public Transform transform { get; set; }
    public Vector3 bornPosition = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    public Vector3 scale = new Vector3(1, 1, 1);
    protected EntityServerInfo serverInfo;
    protected AdvanceFSM fsm;
    public PropertyManager propertyManager;
    private SfxManager sfxManager;
    public SfxHandler sfxHandler;
    public SkillManager skillManager;
    public BattleManager battleManager;
    public SpriteType spriteType;
    /// <summary>
    /// 职业
    /// </summary>
    private Vocation m_vocation;
    public Animator weaponAnimator;
    public int currSpellID;////小于等于0为当前空闲
    public float aiRate = 1;
    public List<uint> hitTimer = new List<uint>(); //技能hit的timer id
    public bool immuneShift = false;//是否免疫受击位移等不受控制状态(眩晕，定身，魅惑)
    public int ShowHitAct;
    public int level;
    public ulong stateFlag;

    /// <summary>
    /// 职业
    /// </summary>
    public Vocation vocation
    {
        get { return m_vocation; }
        set
        {
            m_vocation = value;
        }
    }
    public ActorParent Actor { get; set; }
    public MotorParent Motor { get; set; }
    /// <summary>
    /// 状态机初始化
    /// </summary>
    private void ConstructFSM()
    {
        fsm.AddFSMState(FSMStateType.Patroling, new PatrolBase(this));
        fsm.AddFSMState(FSMStateType.Chasing, new ChaseBase(this));
        fsm.AddFSMState(FSMStateType.Attacking, new AttackBase(this));
        fsm.AddFSMState(FSMStateType.Dead, new DeadBase(this));
    }
    public void OnUpdate()
    {
        if (propertyManager.GetPropertyValue(PropertyType.HP) <= 0)
        {
            fsm.ChangeState(FSMStateType.Dead);
            return;
        }
        fsm.FSMUpdate();
    }
    public void ChangeState(FSMStateType fSMStateType)
    {
        fsm.ChangeState(fSMStateType);
    }
    public bool HasState(FSMStateType fSMStateType)
    {
        return fsm.HasState(fSMStateType);
    }
    public void CreateModel()
    {
        OnCreateModel();
    }
    public void EnterWorld()
    {
        propertyManager = new PropertyManager();
        sfxManager = new SfxManager(this);
        sfxHandler = this.gameObject.AddComponent<SfxHandler>();
        if(this is EntityMyself)
        {
            skillManager = new PlayerSkillManager(this);
        }
        else
        {
            skillManager = new SkillManager(this);
        }
        
        fsm = new AdvanceFSM(this);
        if (this is EntityMonster)
            ConstructFSM();
        OnEnterWorld();
    }
    public void LeaveWorld()
    {
        OnLeaveWorld();
    }
    /// <summary>
    /// 创建模型
    /// </summary>
    protected abstract void OnCreateModel();
    /// <summary>
    /// 进入场景
    /// </summary>
    protected abstract void OnEnterWorld();
    /// <summary>
    /// 离开场景
    /// </summary>
    protected abstract void OnLeaveWorld();
   
    /// <summary>
    /// 动画是否在融合中
    /// </summary>
    /// <returns></returns>
    public bool IsInTransition()
    {
        return animator.IsInTransition(0);
    }

    public void SetEntityServerInfo(EntityServerInfo info)
    {
        serverInfo = info;
        ID = info.id;
        bornPosition = info.position;
    }
    public virtual void UpdatePosition()
    {
        Vector3 point;
        if (transform)
        {
            if (GameCommonUtils.GetPointInTerrain(bornPosition.x, bornPosition.z, out point))
            {
                transform.position = new Vector3(point.x, point.y + 0.3f, point.z);
                if (rotation != Vector3.zero)
                    transform.eulerAngles = new Vector3(0, rotation.y, 0);
            }
            else
            {
                var map = GameStateManager.CurrentSceneData;
                var myself = this as EntityMyself;
                if (myself != null)
                {
                    Vector3 bornPoint;
                    if (GameCommonUtils.GetPointInTerrain((float)(map.enterPoint.x), (float)(map.enterPoint.z), out bornPoint))
                    {
                        transform.position = new Vector3(bornPoint.x, bornPoint.y + 0.5f, bornPoint.z);
                    }
                    else
                    {
                        transform.position = new Vector3(bornPoint.x, bornPoint.y, bornPoint.z);
                    }
                }
                else
                {
                    transform.position = new Vector3(point.x, point.y + 1, point.z);
                }
                
            }
        }

    }
    /// <summary>
    /// 播放特效
    /// </summary>
    public void PlaySfx(int id)
    {
        if (sfxManager == null)
        {
            return;
        }
        sfxManager.PlaySfx(id);
    }
    
    public void RemoveSfx(int nSpellID)
    {
        if (sfxManager == null)
        {
            return;
        }
        sfxManager.RemoveSfx(nSpellID);
    }
    public void PlayFX(int fxID, Transform target = null, Action<GameObject, string> action = null)
    {
        if (sfxHandler == null)
        {
            return;
        }
        sfxHandler.HandleFx(fxID, target, action,"");
    }
    public void RemoveFx(int fxID)
    {
        if (sfxHandler)
            sfxHandler.RemoveFXs(fxID);
    }
    internal bool IsDead()
    {
        return propertyManager.GetPropertyValue(PropertyType.HP)<=0;
    }
    virtual public void SetAction(int act)
    {
        if (animator == null)
        {
            return;
        }
        animator.SetInteger("Action", act);
        if (weaponAnimator)
        {
            weaponAnimator.SetInteger("Action", act);
        }
        if (act == ActionConstants.HIT_AIR)
        {

        }
        else if (act == ActionConstants.KNOCK_DOWN)
        {

        }
        else if (act == ActionConstants.HIT)
        {

        }
        else if (act == ActionConstants.PUSH)
        {

        }
        else if (act == ActionConstants.HIT_GROUND)
        {

        }
    }
    /// <summary>
    /// 分身
    /// </summary>
    public void CreateDuplication()
    {
        GameObject duplication = (GameObject)UnityEngine.Object.Instantiate(Actor.gameObject, Vector3.zero, Quaternion.identity);

        MonoBehaviour[] scripts = duplication.GetComponentsInChildren<MonoBehaviour>();
        foreach (MonoBehaviour script in scripts)
        {
            if (script.GetType() != typeof(Transform))
            {
                UnityEngine.Object.Destroy(script);
            }
        }

        Animator anima = duplication.GetComponent<Animator>();
        if (anima != null)
            UnityEngine.Object.Destroy(anima);

        CharacterController chaController = duplication.GetComponent<CharacterController>();
        if (chaController != null)
            UnityEngine.Object.Destroy(chaController);

        AIPath AIPath = duplication.GetComponent<AIPath>();
        if (AIPath != null)
            UnityEngine.Object.Destroy(AIPath);

        duplication.transform.position = transform.position;
        duplication.transform.rotation = transform.rotation;
        TimerHeap.AddTimer<GameObject>(1000, 0, (copy) => { UnityEngine.Object.Destroy(copy); }, duplication);
    }

    virtual public void CastSkill(int nSkillID)
    {

        currSpellID = nSkillID;
        //hitActionIdx = 1;
        SkillData s = SkillData.GetByID(currSpellID);
        if (s == null)
        {//没有技能配置
            return;
        }

        if (ID == GameWorld.player.ID)
        {
            //TODO 给服务器发送消息
        }

        battleManager.CastSkill(currSpellID);
    }
    virtual public void ClearSkill(bool isRemove = false)
    {
        currSpellID = -1;
    }
    public string CurrActStateName()
    {
        if (animator == null)
        {
            return "";
        }
        AnimatorClipInfo[] st = animator.GetCurrentAnimatorClipInfo(0);
        if (st.Length == 0)
        {
            return "";
        }
        return st[0].clip.name;
    }

    public int curHp
    {
        get
        {
            return propertyManager.GetPropertyValue(PropertyType.HP);
        }
        set
        {
            propertyManager.ChangeProperty(PropertyType.HP, value);
        }
    }

    public void OnDeath(int hitActionID)
    {
    }
}
