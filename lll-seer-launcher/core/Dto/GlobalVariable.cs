using System;
using System.Collections.Generic;
using System.Collections;

namespace lll_seer_launcher.core.Dto
{
    public static class GlobalVariable
    {
        #region
        /*================================================窗口组件用全局变量=====================================================*/
        public static seerMainWindow mainForm;
        public const string seerFiddlerTitle = "seerFiddler";
        public static bool loadingComplate = false;
        #endregion
        #region
        /*===========================================游戏账号信息用全局变量=================================================*/
        /// <summary>
        /// 称号字典
        /// </summary>
        public static Dictionary<int, AchieveTitleInfo> achieveTitleDictionary { get; set; } = new Dictionary<int, AchieveTitleInfo>();

        /// <summary>
        /// 套装字典
        /// </summary>
        public static Dictionary<int, SuitInfo> suitDictionary { get; set; } = new Dictionary<int, SuitInfo>();

        /// <summary>
        /// 目镜字典
        /// </summary>
        public static Dictionary<int, GlassesInfo> glassesDictionary { get; set; } = new Dictionary<int, GlassesInfo>();

        /// <summary>
        /// key:登录的游戏账号
        /// value;对应游戏账号所持有的称号
        /// </summary>
        public static Dictionary<int, ArrayList> userAchieveTitleDictionary { get; set; } = new Dictionary<int, ArrayList>();

        /// <summary>
        /// key:登录的游戏账号
        /// value;对应游戏账号所持有的套装
        /// </summary>
        public static Dictionary<int, Dictionary<int, int>> userSuitClothDictionary { get; set; } = new Dictionary<int, Dictionary<int, int>>();
        #endregion
        #region
        /*=============================================窗口UI相关全局变量===================================================*/
        /// <summary>
        /// 主窗口线程控制flag
        /// </summary>
        public static bool stopThread { get; set; } = false;

        /// <summary>
        /// 套装・称号信息加载完成flag
        /// 0:称号
        /// 1:装备
        /// </summary>
        public static bool[] initSuitGroupBoxsCompleteFlg { get; set; } = new bool[2] { true, true };
        #endregion
        #region
        /*=============================================封包相关用全局变量================================================*/
        /// <summary>
        /// 当前是否处于登录状态
        /// </summary>
        public static bool isLogin { get; set; }

        public static bool isLoginSend { get; set; }

        /// <summary>
        /// 当前游戏的套接字
        /// </summary>
        public static int gameSocket { get; set; }

        /// <summary>
        /// 当前所登录账号
        /// </summary>
        public static int userId { get; set; }

        /// <summary>
        /// 封包加密key字符串
        /// </summary>
        public static string keyString { get; set; } = "!crAckmE4nOthIng:-)";
        /// <summary>
        /// 封包加密key的字节数组
        /// </summary>
        public static byte[] Key { get; set; }
        /// <summary>
        /// 封包加密key字节长度
        /// </summary>
        public static int keyLen { get; set; }
        /// <summary>
        /// 封包加密key字节指针
        /// </summary>
        public static IntPtr keyPtr { get; set; }

        /// <summary>
        /// 封包顺序码
        /// </summary>
        public static int seq { get; set; }

        /// <summary>
        /// 在登录时，解密key初始化失败时刷新游戏的flag。
        /// </summary>
        public static bool gameReloadFlg { get; set; } = false;
        #endregion
        #region
        /*=============================================json相关用全局变量================================================*/
        public static Dictionary<string,bool> shoudUpdateJsonDic { get; set; }
        public static Dictionary<string,string> jsonPathDic { get; set; }
        #endregion
    }
}
