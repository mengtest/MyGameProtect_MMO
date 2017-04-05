#ifndef _ZONECONFIG_H
#define _ZONECONFIG_H

/*
    ZONE�������:zoneconnect zoneserver zoneconfig
    ʹ�õ�һЩ��������
*/
#include    "define.h"
#include    "work_dir.h"
#include    "LogServerCfg.h"
#include    "shmkeydefine.h"


#define DFT_CONFIG_FILE    "zone/config/zone.conf"

typedef enum
{
    SESSION_TYPE_INIT = 1,      // ZoneConnect����ZoneServer�ĵ�һ������Session,�������⴦��һЩ��Ϣ
    SESSION_TYPE_RUN  = 2,      // ���ӳɹ����������е�Session��Ϊ������
    SESSION_TYPE_STOP = 3,      // ZoneConnect֪ͨZoneServer Client���ߣ���ZoneServer֪ͨZoneConnect�ر�����ʱ
    SESSION_TYPE_QUEUE = 4,     //�Ŷ������Ϣsession
} EOnlineSessionType;

// ZoneConnect��ZoneServer���н�����Session��Ϣ
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

// ��Zone�н����ĸ�Ӧ�ý���ʵ��id
typedef struct
{
	int     m_iZoneConnect;
	int     m_iZoneServer;
	int     m_iZoneCache;
	int     m_iWorldCenter;
	
} STServerEntity;

// ZoneConnect IO�����ר������
typedef struct
{
	char    m_sListenIP[16];
	int     m_iListenPort;

	int     m_iMaxPkgCntInSec;          // ÿ�����Client�������
	int     m_iMaxIdleSec;              // Client������ʱ��
	BYTE    m_bBindToCPU;               //�����̰󶨵�cpuָ������

} STZoneConnectConf;

// Zone��shm key����
typedef struct
{
	int     m_iZoneConfigShmKey;        // Zone��������Ϣshm key
	int     m_iConnContextShmKey;       // ZoneConnect����������Ϣ��ŵ�shm key
	int     m_iPlayerInfoShmKey;        // ���߽�ɫ��Ϣshm key

	int     m_iStatOfCmdShmKey;         // ͳ�� - ������ͳ��shm key
	int     m_iStatCommShmKey;          // Logͨ��ͳ����Ϣ
	int     m_iConnectQueueShmKey;      //�����ŶӶ���shm key
} STShmKeyConf;

typedef struct
{
	BYTE   	    m_bLogicStatOn;             // logicͳ�ƿ����Ƿ��
	BYTE    	m_bIsQueueNeed;             //�ŶӶ��п����Ƿ��
	int     	m_iConnQueueLen;            //�ŶӶ��г���
	BYTE    	m_bBindToCPU;               //�����̰󶨵�cpuָ������
	BYTE        m_bPrintDebugLog;           //�Ƿ��ӡ������־
	BYTE        m_bNewUserGuid;             //�����������
	

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


// Zone��ͳһ������Ϣ
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

