﻿#region 模块信息
/*----------------------------------------------------------------
// Copyright (C) 2013 广州，爱游
//
// 模块名：Utils
// 创建者：Ash Tang
// 修改者列表：
// 创建日期：2013.2.1
// 模块描述：通用工具类。
//----------------------------------------------------------------*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.IO;
using UnityEngine;
using System.Security.Cryptography;
using System.Net;
using System.Runtime.InteropServices;

namespace Mogo.Util
{
    public enum LayerMask
    {
        Default = 1,
        Character = 1 << 8,
        Monster = 1 << 11,
        Npc = 1 << 12,
        Terrain = 1 << 9,
        Trap = 1 << 17
    }

    /// <summary>
    /// 通用工具类。
    /// </summary>
    public static class Utils
    {
        #region 常量

        /// <summary>
        /// 键值分隔符： ‘:’
        /// </summary>
        private const Char KEY_VALUE_SPRITER = ':';
        /// <summary>
        /// 字典项分隔符： ‘,’
        /// </summary>
        private const Char MAP_SPRITER = ',';
        /// <summary>
        /// 数组分隔符： ','
        /// </summary>
        private const Char LIST_SPRITER = ',';

        public const String RPC_HEAD = "RPC_";

        public const String SVR_RPC_HEAD = "SVR_RPC_";

        #endregion

        #region 时间格式化

        /// <summary>
        /// 格式化日期格式。（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        /// <param name="datetime">日期对象</param>
        /// <returns>日期字符串</returns>
        public static String FormatTime(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 格式化日期格式。（yyyy-MM-dd HH:mm:ss）
        /// </summary>
        /// <param name="datetime">日期值</param>
        /// <returns>日期字符串</returns>
        public static String FormatTime(this long datetime)
        {
            DateTime.FromBinary(datetime);
            return datetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>
        /// 时间戳转为C#格式时间。
        /// </summary>
        /// <param name="timeStamp">时间戳</param>
        /// <returns></returns>
        public static DateTime GetTime(this int timeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return startTime.AddSeconds(timeStamp);
        }

        #endregion

        #region 字典转换

        /// <summary>
        /// 将字典字符串转换为键类型与值类型都为整型的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<Int32, Int32> ParseMapIntInt(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            Dictionary<Int32, Int32> result = new Dictionary<Int32, Int32>();
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                int key;
                int value;
                if (int.TryParse(item.Key, out key) && int.TryParse(item.Value, out value))
                    result.Add(key, value);
                else
                    Debuger.LogWarning(String.Format("Parse failure: {0}, {1}", item.Key, item.Value));
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型为整型，值类型为单精度浮点数的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<Int32, float> ParseMapIntFloat(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            var result = new Dictionary<Int32, float>();
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                int key;
                float value;
                if (int.TryParse(item.Key, out key) && float.TryParse(item.Value, out value))
                    result.Add(key, value);
                else
                    Debuger.LogWarning(String.Format("Parse failure: {0}, {1}", item.Key, item.Value));
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型为整型，值类型为字符串的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<Int32, String> ParseMapIntString(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            Dictionary<Int32, String> result = new Dictionary<Int32, String>();
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                int key;
                if (int.TryParse(item.Key, out key))
                    result.Add(key, item.Value);
                else
                    Debuger.LogWarning(String.Format("Parse failure: {0}", item.Key));
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型为字符串，值类型为单精度浮点数的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<String, float> ParseMapStringFloat(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            Dictionary<String, float> result = new Dictionary<String, float>();
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                float value;
                if (float.TryParse(item.Value, out value))
                    result.Add(item.Key, value);
                else
                    Debuger.LogWarning(String.Format("Parse failure: {0}", item.Value));
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型为字符串，值类型为整型的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<String, Int32> ParseMapStringInt(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            Dictionary<String, Int32> result = new Dictionary<String, Int32>();
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                int value;
                if (int.TryParse(item.Value, out value))
                    result.Add(item.Key, value);
                else
                    Debuger.LogWarning(String.Format("Parse failure: {0}", item.Value));
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型为 T，值类型为 U 的字典对象。
        /// </summary>
        /// <typeparam name="T">字典Key类型</typeparam>
        /// <typeparam name="U">字典Value类型</typeparam>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<T, U> ParseMapAny<T, U>(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            var typeT = typeof(T);
            var typeU = typeof(U);
            var result = new Dictionary<T, U>();
            //先转为字典
            var strResult = ParseMap(strMap, keyValueSpriter, mapSpriter);
            foreach (var item in strResult)
            {
                try
                {
                    T key = (T)GetValue(item.Key, typeT);
                    U value = (U)GetValue(item.Value, typeU);

                    result.Add(key, value);
                }
                catch (Exception)
                {
                    Debuger.LogWarning(String.Format("Parse failure: {0}, {1}", item.Key, item.Value));
                }
            }
            return result;
        }

        /// <summary>
        /// 将字典字符串转换为键类型与值类型都为字符串的字典对象。
        /// </summary>
        /// <param name="strMap">字典字符串</param>
        /// <param name="keyValueSpriter">键值分隔符</param>
        /// <param name="mapSpriter">字典项分隔符</param>
        /// <returns>字典对象</returns>
        public static Dictionary<String, String> ParseMap(this String strMap, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            Dictionary<String, String> result = new Dictionary<String, String>();
            if (String.IsNullOrEmpty(strMap))
            {
                return result;
            }

            var map = strMap.Split(mapSpriter);//根据字典项分隔符分割字符串，获取键值对字符串
            for (int i = 0; i < map.Length; i++)
            {
                if (String.IsNullOrEmpty(map[i]))
                {
                    continue;
                }

                var keyValuePair = map[i].Split(keyValueSpriter);//根据键值分隔符分割键值对字符串
                if (keyValuePair.Length == 2)
                {
                    if (!result.ContainsKey(keyValuePair[0]))
                        result.Add(keyValuePair[0], keyValuePair[1]);
                    else
                        Debuger.LogWarning(String.Format("Key {0} already exist, index {1} of {2}.", keyValuePair[0], i, strMap));
                }
                else
                {
                    Debuger.LogWarning(String.Format("KeyValuePair are not match: {0}, index {1} of {2}.", map[i], i, strMap));
                }
            }
            return result;
        }

        /// <summary>
        /// 将字典对象转换为字典字符串。
        /// </summary>
        /// <typeparam name="T">字典Key类型</typeparam>
        /// <typeparam name="U">字典Value类型</typeparam>
        /// <param name="map">字典对象</param>
        /// <returns>字典字符串</returns>
        public static String PackMap<T, U>(this IEnumerable<KeyValuePair<T, U>> map, Char keyValueSpriter = KEY_VALUE_SPRITER, Char mapSpriter = MAP_SPRITER)
        {
            if (map.Count() == 0)
                return "";
            else
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in map)
                {
                    sb.AppendFormat("{0}{1}{2}{3}", item.Key, keyValueSpriter, item.Value, mapSpriter);
                }
                return sb.ToString().Remove(sb.Length - 1, 1);
            }
        }

        #endregion

        #region 列表转换

        /// <summary>
        /// 将列表字符串转换为类型为 T 的列表对象。
        /// </summary>
        /// <typeparam name="T">列表值对象类型</typeparam>
        /// <param name="strList">列表字符串</param>
        /// <param name="listSpriter">数组分隔符</param>
        /// <returns>列表对象</returns>
        public static List<T> ParseListAny<T>(this String strList, Char listSpriter = LIST_SPRITER)
        {
            var type = typeof(T);
            var list = strList.ParseList(listSpriter);
            var result = new List<T>();
            foreach (var item in list)
            {
                result.Add((T)GetValue(item, type));
            }
            return result;
        }

        /// <summary>
        /// 将列表字符串转换为字符串的列表对象。
        /// </summary>
        /// <param name="strList">列表字符串</param>
        /// <param name="listSpriter">数组分隔符</param>
        /// <returns>列表对象</returns>
        public static List<String> ParseList(this String strList, Char listSpriter = LIST_SPRITER)
        {
            var result = new List<String>();
            if (String.IsNullOrEmpty(strList))
                return result;

            var trimString = strList.Trim();
            if (String.IsNullOrEmpty(strList))
            {
                return result;
            }
            var detials = trimString.Split(listSpriter);//.Substring(1, trimString.Length - 2)
            foreach (var item in detials)
            {
                if (!String.IsNullOrEmpty(item))
                    result.Add(item.Trim());
            }

            return result;
        }

        /// <summary>
        /// 将列表对象转换为列表字符串。
        /// </summary>
        /// <typeparam name="T">列表值对象类型</typeparam>
        /// <param name="list">列表对象</param>
        /// <param name="listSpriter">列表分隔符</param>
        /// <returns>列表字符串</returns>
        public static String PackList<T>(this List<T> list, Char listSpriter = LIST_SPRITER)
        {
            if (list.Count == 0)
                return "";
            else
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append("[");
                foreach (var item in list)
                {
                    sb.AppendFormat("{0}{1}", item, listSpriter);
                }
                sb.Remove(sb.Length - 1, 1);
                //sb.Append("]");

                return sb.ToString();
            }

        }

        public static String PackArray<T>(this T[] array, Char listSpriter = LIST_SPRITER)
        {
            var list = new List<T>();
            list.AddRange(array);
            return PackList(list, listSpriter);
        }

        #endregion

        #region 随机数

        /// <summary>
        /// 创建一个产生不重复随机数的随机数生成器。
        /// </summary>
        /// <returns>随机数生成器</returns>
        public static System.Random CreateRandom()
        {
            long tick = DateTime.Now.Ticks;
            return new System.Random((int)(tick & 0xffffffffL) | (int)(tick >> 32));
        }

        public static T Choice<T>(List<T> list)
        {
            if (list.Count == 0)
            {
                return default(T);
            }

            int index = UnityEngine.Random.Range(0, list.Count);
            return list[index];
        }

        #endregion

        #region 文件读取

        public static String LoadResource(String fileName)
        {
            var text = Resources.Load(fileName);
            if (text != null)
            {
                var result = text.ToString();
                Resources.UnloadAsset(text);
                return result;
            }
            else
                return String.Empty;
        }

        public static String LoadFile(String fileName)
        {
            //Debuger.LogDebug(fileName);
            if (File.Exists(fileName))
                using (StreamReader sr = File.OpenText(fileName))
                    return sr.ReadToEnd();
            else
                return String.Empty;
        }

        public static byte[] LoadByteResource(String fileName)
        {
            TextAsset binAsset = Resources.Load(fileName, typeof(TextAsset)) as TextAsset;
            var result = binAsset.bytes;
            Resources.UnloadAsset(binAsset);

            return result;
        }

        public static byte[] LoadByteFile(String fileName)
        {
            if (File.Exists(fileName))
                return File.ReadAllBytes(fileName);
            else
                return null;
        }

        #endregion

        #region 类型转换

        /// <summary>
        /// 将字符串转换为对应类型的值。
        /// </summary>
        /// <param name="value">字符串值内容</param>
        /// <param name="type">值的类型</param>
        /// <returns>对应类型的值</returns>
        public static object GetValue(String value, Type type)
        {
            if (type == null)
                return null;
            else if (type == typeof(string))
                return value;
            else if (type == typeof(Int32))
                return Convert.ToInt32(Convert.ToDouble(value));
            else if (type == typeof(float))
                return float.Parse(value);
            else if (type == typeof(byte))
                return Convert.ToByte(Convert.ToDouble(value));
            else if (type == typeof(sbyte))
                return Convert.ToSByte(Convert.ToDouble(value));
            else if (type == typeof(UInt32))
                return Convert.ToUInt32(Convert.ToDouble(value));
            else if (type == typeof(Int16))
                return Convert.ToInt16(Convert.ToDouble(value));
            else if (type == typeof(Int64))
                return Convert.ToInt64(Convert.ToDouble(value));
            else if (type == typeof(UInt16))
                return Convert.ToUInt16(Convert.ToDouble(value));
            else if (type == typeof(UInt64))
                return Convert.ToUInt64(Convert.ToDouble(value));
            else if (type == typeof(double))
                return double.Parse(value);
            else if (type == typeof(bool))
            {
                if (value == "0")
                    return false;
                else if (value == "1")
                    return true;
                else
                    return bool.Parse(value);
            }
            else if (type.BaseType == typeof(Enum))
                return GetValue(value, Enum.GetUnderlyingType(type));
            else if (type == typeof(Vector3))
            {
                Vector3 result;
                ParseVector3(value, out result);
                return result;
            }
            else if (type == typeof(Quaternion))
            {
                Quaternion result;
                ParseQuaternion(value, out result);
                return result;
            }
            else if (type == typeof(Color))
            {
                Color result;
                ParseColor(value, out result);
                return result;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<,>))
            {
                Type[] types = type.GetGenericArguments();
                var map = ParseMap(value);
                var result = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                foreach (var item in map)
                {
                    var key = GetValue(item.Key, types[0]);
                    var v = GetValue(item.Value, types[1]);
                    type.GetMethod("Add").Invoke(result, new object[] { key, v });
                }
                return result;
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                Type t = type.GetGenericArguments()[0];
                var list = ParseList(value);
                var result = type.GetConstructor(Type.EmptyTypes).Invoke(null);
                foreach (var item in list)
                {
                    var v = GetValue(item, t);
                    type.GetMethod("Add").Invoke(result, new object[] { v });
                }
                return result;
            }
            else
                return null;
        }

        /// <summary>
        /// 将指定格式(255, 255, 255, 255) 转换为 Color 
        /// </summary>
        /// <param name="_inputString"></param>
        /// <param name="result"></param>
        /// <returns>返回 true/false 表示是否成功</returns>
        public static bool ParseColor(string _inputString, out Color result)
        {
            string trimString = _inputString.Trim();
            result = Color.clear;
            if (trimString.Length < 9)
            {
                return false;
            }
            //if (trimString[0] != '(' || trimString[trimString.Length - 1] != ')')
            //{
            //    return false;
            //}
            try
            {
                string[] _detail = trimString.Split(LIST_SPRITER);//.Substring(1, trimString.Length - 2)
                if (_detail.Length != 4)
                {
                    return false;
                }
                result = new Color(float.Parse(_detail[0]) / 255, float.Parse(_detail[1]) / 255, float.Parse(_detail[2]) / 255, float.Parse(_detail[3]) / 255);
                return true;
            }
            catch (Exception e)
            {
                Debuger.LogError("Parse Color error: " + trimString + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 将指定格式(1.0, 2, 3.4) 转换为 Vector3 
        /// </summary>
        /// <param name="_inputString"></param>
        /// <param name="result"></param>
        /// <returns>返回 true/false 表示是否成功</returns>
        public static bool ParseVector3(string _inputString, out Vector3 result)
        {
            string trimString = _inputString.Trim();
            result = new Vector3();
            if (trimString.Length < 7)
            {
                return false;
            }
            //if (trimString[0] != '(' || trimString[trimString.Length - 1] != ')')
            //{
            //    return false;
            //}
            try
            {
                string[] _detail = trimString.Split(LIST_SPRITER);//.Substring(1, trimString.Length - 2)
                if (_detail.Length != 3)
                {
                    return false;
                }
                result.x = float.Parse(_detail[0]);
                result.y = float.Parse(_detail[1]);
                result.z = float.Parse(_detail[2]);
                return true;
            }
            catch (Exception e)
            {
                Debuger.LogError("Parse Vector3 error: " + trimString + e.ToString());
                
                return false;
            }
        }

        /// <summary>
        /// 将指定格式(1.0, 2, 3.4) 转换为 Vector3 
        /// </summary>
        /// <param name="_inputString"></param>
        /// <param name="result"></param>
        /// <returns>返回 true/false 表示是否成功</returns>
        public static bool ParseQuaternion(string _inputString, out Quaternion result)
        {
            string trimString = _inputString.Trim();
            result = new Quaternion();
            if (trimString.Length < 9)
            {
                return false;
            }
            //if (trimString[0] != '(' || trimString[trimString.Length - 1] != ')')
            //{
            //    return false;
            //}
            try
            {
                string[] _detail = trimString.Split(LIST_SPRITER);//.Substring(1, trimString.Length - 2)
                if (_detail.Length != 4)
                {
                    return false;
                }
                result.x = float.Parse(_detail[0]);
                result.y = float.Parse(_detail[1]);
                result.z = float.Parse(_detail[2]);
                result.w = float.Parse(_detail[3]);
                return true;
            }
            catch (Exception e)
            {
                Debuger.LogError("Parse Quaternion error: " + trimString + e.ToString());
                return false;
            }
        }

        /// <summary>
        /// 替换字符串中的子字符串。
        /// </summary>
        /// <param name="input">原字符串</param>
        /// <param name="oldValue">旧子字符串</param>
        /// <param name="newValue">新子字符串</param>
        /// <param name="count">替换数量</param>
        /// <param name="startAt">从第几个字符开始</param>
        /// <returns>替换后的字符串</returns>
        public static String ReplaceFirst(this string input, string oldValue, string newValue, int startAt = 0)
        {
            int pos = input.IndexOf(oldValue, startAt);
            if (pos < 0)
            {
                return input;
            }
            return string.Concat(input.Substring(0, pos), newValue, input.Substring(pos + oldValue.Length));
        }

        #endregion

        #region Lua table转换

        #region 实体转换

     
        #endregion

        #endregion

        #region 文件路径处理

        public static string GetFileNameWithoutExtention(string fileName, char separator = '/')
        {
            var name = GetFileName(fileName, separator);
            return GetFilePathWithoutExtention(name);
        }

        public static string GetFilePathWithoutExtention(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('.'));
        }

        public static string GetDirectoryName(string fileName)
        {
            return fileName.Substring(0, fileName.LastIndexOf('/'));
        }

        public static string GetFileName(string path, char separator = '/')
        {
            return path.Substring(path.LastIndexOf(separator) + 1);
        }

        public static string PathNormalize(this string str)
        {
            return str.Replace("\\", "/").ToLower();
        }

        #endregion


        #region state
        public static ulong BitSet(ulong data, int nBit)
        {
            if (nBit >= 0 && nBit < (int)sizeof(ulong) * 8)
            {
                data |= (ulong)(1 << nBit);
            }

            return data;
        }

        public static ulong BitReset(ulong data, int nBit)
        {
            if (nBit >= 0 && nBit < (int)sizeof(ulong) * 8)
            {
                data &= (ulong)(~(1 << nBit));
            };

            return data;
        }

        public static int BitTest(ulong data, int nBit)
        {
            int nRet = 0;
            if (nBit >= 0 && nBit < (int)sizeof(ulong) * 8)
            {
                data &= (ulong)(1 << nBit);
                if (data != 0) nRet = 1;
            }
            return nRet;
        }
        #endregion

        #region 几何相关

        public static void CircleXYByAngle(float angle, Vector3 O, Vector3 A, out Vector3 rnt)
        {

            float r = Vector3.Distance(O, A);
            //rnt = new Vector3();
            rnt.y = A.y;
            rnt.x = r * (float)Math.Cos(angle) + O.x;
            rnt.z = r * (float)Math.Sin(angle) + O.z;



            //return rnt;
        }

        #endregion

        static public string GetFullName(Transform rootTransform, Transform currentTransform)
        {
            string fullName = String.Empty;

            while (currentTransform != rootTransform)
            {
                fullName = currentTransform.name + fullName;

                if (currentTransform.parent != rootTransform)
                {
                    fullName = String.Concat('/', fullName);
                }

                currentTransform = currentTransform.parent;
            }

            return fullName;
        }

        /// <summary>
        /// 挂载object在某父上并保持本地坐标、转向、大小不变
        /// </summary>
        /// <param name="child"></param>
        /// <param name="parent"></param>
        public static void MountToSomeObjWithoutPosChange(Transform child, Transform parent)
        {
            Vector3 scale = child.localScale;
            Vector3 position = child.localPosition;
            Vector3 angle = child.localEulerAngles;
            child.parent = parent;
            child.localScale = scale;
            child.localEulerAngles = angle;
            child.localPosition = position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="datastr"></param>
        /// <returns>返回字符串</returns>
        public static void SendPostHttp(string Url, string datastr, Action<string> onDone, Action<HttpStatusCode> onFail)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes(datastr);
            // 准备请求... 
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Url);
            req.Method = "Post"; //Getor Post
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            // 发送数据 
            stream.Write(data, 0, data.Length);
            stream.Close();

            uint timerId = TimerHeap.AddTimer(15000, 0, () => { onFail(HttpStatusCode.RequestTimeout); });
            HttpWebResponse rep = (HttpWebResponse)req.GetResponse();


            if (rep.StatusCode != HttpStatusCode.OK)
            {
                TimerHeap.DelTimer(timerId);
                onFail(rep.StatusCode);
            }
            else
            {
                TimerHeap.DelTimer(timerId);
                Stream receiveStream = rep.GetResponseStream();
                Encoding encode = System.Text.Encoding.UTF8;
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();

                onDone(sb.ToString());
            }
        }


        public static void GetHttp(string Url, Action<string> onDone, Action<HttpStatusCode> onFail)
        {
            //Debug.LogError("GetHttp");
            // 准备请求... 
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            HttpWebRequest req = null;
            HttpWebResponse rep = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(Url);
            }
            catch (Exception e)
            {
                //Debug.LogError(e.ToString());
                onFail(HttpStatusCode.NotAcceptable);
                return;
            }

            stopWatch.Stop();
            //Debug.LogError("req Create:" + stopWatch.ElapsedMilliseconds);

            uint timerId = TimerHeap.AddTimer(15000, 0, () => { onFail(HttpStatusCode.RequestTimeout); });
            stopWatch.Start();
            rep = (HttpWebResponse)req.GetResponse();
            stopWatch.Stop();
            //Debug.LogError("req GetResponse:" + stopWatch.ElapsedMilliseconds);

            if (rep.StatusCode != HttpStatusCode.OK)
            {

                stopWatch.Start();
                TimerHeap.DelTimer(timerId);
                onFail(rep.StatusCode);
                stopWatch.Stop();
                //Debug.LogError("req onFail:" + stopWatch.ElapsedMilliseconds);
            }
            else
            {
                TimerHeap.DelTimer(timerId);
                stopWatch.Start();
                Stream receiveStream = rep.GetResponseStream();
                stopWatch.Stop();
                //Debug.LogError("req GetResponseStream:" + stopWatch.ElapsedMilliseconds);

                stopWatch.Start();
                Encoding encode = System.Text.Encoding.UTF8;
                // Pipes the stream to a higher level stream reader with the required encoding format. 
                StreamReader readStream = new StreamReader(receiveStream, encode);

                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                stopWatch.Stop();
                //Debug.LogError("req readStream:" + stopWatch.ElapsedMilliseconds);

                stopWatch.Start();
                rep.Close();
                readStream.Close();
                stopWatch.Stop();
                //Debug.LogError("req Close:" + stopWatch.ElapsedMilliseconds);


                stopWatch.Start();
                onDone(sb.ToString());
                stopWatch.Stop();
                //Debug.LogError("req onDone:" + stopWatch.ElapsedMilliseconds);
            }
        }

    }
}