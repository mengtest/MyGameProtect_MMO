//This file is generated by program. Do not edit it manually.

using System;
using System.Collections.Generic;
using System.Text;

namespace com.kz.protocol
{
	public class Protocols
	{
		public static int Type(int id)
		{
			return (int)(0xffff0000 & id);
		}

		public static int IdWithoutType(int id)
		{
			return 0x0000ffff & id;
		}

		public static Boolean isSuccess(int id)
		{
			if(id <= 0)
				return true;
			uint newId = (uint)id;
			return ((id & 0xfff00000)>>20) < 0x10;
		}

		//ServerType 0xxxxx0000
		public const int SERVER_TYPE_AUTH = 0x00020000;
		public const int SERVER_TYP_GAME = 0x00030000;
		public const int SERVER_TYPE_BATTLE = 0x00040000;
		//Protocol Type 0xxxxx0000
		public const uint PROTOCOL_TYPE_ALL = 0xffff0000;
		public const int PROTOCOL_TYPE_AUTH = 0x00020000;
		public const int PROTOCOL_TYPE_GAME = 0x00030000;
		public const int PROTOCOL_TYPE_BATTLE = 0x00040000;
		//Game Protocols for Clients 0x0002xxxx, xxxx start from 0x0000
		public const int P_GC_GAME_ACK = 0x10001;
		public const int P_GC_GAME_CONNECT = 0x10002;
		public const int P_CG_GAME_LOGIN = 0x10003;
		public const int P_GC_GAME_LOGIN = 0x10004;
			public const int GAME_LOGIN_SUCCESS = 0x110004;
			public const int GAME_LOGIN_USERNAME_OR_PWD_NULL = 0x210004;
			public const int GAME_LOGIN_SUCCESS_ROLE_NOT_EXIST = 0x310004;
			public const int GAME_LOGIN_PASSWORD_FAIL = 0x1010004;
		public const int P_CG_GAME_CREATE_ROLE = 0x10005;
		public const int P_GC_GAME_CREATE_ROLE = 0x10006;
			public const int GAME_CREATE_ROLE_SUCCESS = 0x110006;
			public const int GAME_CREATE_ROLE_FAIL = 0x1010006;
			public const int GAME_CREATE_ROLE_FAIL_NAME_EXSIT = 0x1110006;
			public const int GAME_CREATE_ROLE_FAIL_NAME_NULL = 0x1210006;
			public const int GAME_CREATE_ROLE_FAIL_NAME_LENGTH_NOT_VALID = 0x1310006;

	}
}