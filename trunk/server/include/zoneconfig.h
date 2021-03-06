#ifndef _ZONECONFIG_H
#define _ZONECONFIG_H

/*
    ZONE层各程序:zoneconnect zoneserver zoneconfig
    使用的一些公共定义
*/
#include    "define.h"
#include    "work_dir.h"
#include    "LogServerCfg.h"
#include    "shmkeydefine.h"


#define DFT_CONFIG_FILE    "zone/config/zone.conf"

typedef enum
{
    SESSION_TYPE_INIT = 1,      // ZoneConnect发给ZoneServer的第一个包的Session,可以特殊处理一些信息
    SESSION_TYPE_RUN  = 2,      // 连接成功建立后所有的Session均为该类型
    SESSION_TYPE_STOP = 3,      // ZoneConnect通知ZoneServer Client断线，或ZoneServer通知ZoneConnect关闭连接时
    SESSION_TYPE_QUEUE = 4,     //排队相关信息session
} EOnlineSessionType;

// ZoneConnect与ZoneServer进行交互的Session信息
struct STZoneConnOnlineSession
{
	char    m_cType;         	// 	Session see EOnlineSessionType
	UINT    m_dwClientPos;    	// 	zoneConnect	: index of CCltMng
	UINT    m_dwClientSeqNo; 	//  	zoneConnect	: timestamp of connect,not for search.For check only
	UINT    m_dwObjID;       	//  	zoneServer	: index of PlayerMgr

	void clone(const STZoneConnOnlineSession& from)
	{
		this->m_cType 		= from.m_cType;
		this->m_dwClientPos	= from.m_dwClientPos;
		this->m_dwClientSeqNo=from.m_dwClientSeqNo;
		this->m_dwObjID		= from.m_dwObjID;
	}

	void Reset()
	{
		this->m_cType 		= -1;
		this->m_dwClientPos	= 0;
		this->m_dwClientSeqNo=0;
		this->m_dwObjID		= 0;
	}

};

// 与Zone有交互的各应用进程实体id
typedef struct
{
	int     m_iZoneConnect;
	int     m_iZoneServer;
	int     m_iZoneCache;
	int     m_iWorldCenter;
	
} STServerEntity;

// ZoneConnect IO接入层专有配置
typedef struct
{
	char    m_sListenIP[16];
	int     m_iListenPort;

	int     m_iMaxPkgCntInSec;          // 每秒最多Client允许包数
	int     m_iMaxIdleSec;              // Client最大空闲时间
	BYTE    m_bBindToCPU;               //将进程绑定到cpu指定核上

} STZoneConnectConf;

// Zone层shm key配置
typedef struct
{
	int     m_iZoneConfigShmKey;        // Zone层配置信息shm key
	int     m_iConnContextShmKey;       // ZoneConnect连接描述信息存放的shm key
	int     m_iPlayerInfoShmKey;        // 在线角色信息shm key

	int     m_iStatOfCmdShmKey;         // 统计 - 命令字统计shm key
	int     m_iStatCommShmKey;          // Log通用统计信息
	int     m_iConnectQueueShmKey;      //连接排队队列shm key
} STShmKeyConf;

typedef struct
{
	BYTE   	    m_bLogicStatOn;             // logic统计开关是否打开
	BYTE    	m_bIsQueueNeed;             //排队队列开关是否打开
	int     	m_iConnQueueLen;            //排队队列长度
	BYTE    	m_bBindToCPU;               //将进程绑定到cpu指定核上
	BYTE        m_bPrintDebugLog;           //是否打印调试日志
	BYTE        m_bNewUserGuid;             //新手引导开�
	

	//kongfu 's conf
	int     m_iZoneServerID;
	int 	m_iPermitCntPerSecond;
	int		m_MaxPlayer;
	int     m_iSupportApple;
	int     m_iSupportAppleSandbox;

	//db's conf
	char	m_sDbIP[16];
	int		m_iDbPort;
	char	m_sDbName[64];
	char	m_sDbUser[16];
	char	m_sDbPassword[16];
	char	m_sDbCharset[16];
	char	m_sDbUnixSocket[128];
	int     m_iCompressBlob;

	//php's conf
	char    m_sPhpIP[16];

} STZoneServerConf;


// Zone层统一配置信息
typedef struct
{
	STServerEntity      m_stSvrEntity;
	char                m_sRes1[1024 - sizeof(STServerEntity)];

	STZoneConnectConf   m_stZoneConnConf;
	char                m_sRes2[1024 - sizeof(STZoneConnectConf)];

	STShmKeyConf        m_stShmKeyConf;
	char                m_sRes3[1024 - sizeof(STShmKeyConf)];

	STZoneServerConf    m_stZoneSvrConf;
	char                m_sRes4[1024 - sizeof(STZoneServerConf)];

} STZoneConfig;

#endif

