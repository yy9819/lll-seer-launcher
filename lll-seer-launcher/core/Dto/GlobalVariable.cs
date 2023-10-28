using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using lll_seer_launcher.core.Controller;
using lll_seer_launcher.core.Dto.PetDto;

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
        /*================================================游戏用全局变量=====================================================*/
        public static GameConfigFlag gameConfigFlag = new GameConfigFlag();
        public static int fightTurn;
        public class GameConfigFlag
        {
            /// <summary>
            /// true（0） 关闭 false（1）开启
            /// </summary>
            public bool batterySwitch = false;
            public bool disableRecv = false;
            /// <summary>
            /// 
            /// </summary>
            public bool shouldDisableRecv = false;
            /// <summary>
            /// 非VIP自动回血
            /// </summary>
            public bool autoChargeFlag = true;
            /// <summary>
            /// 关闭VIP自动回血
            /// </summary>
            public bool disableVipAutoChargeFlag = false;
            /// <summary>
            /// 压血
            /// </summary>
            public bool lowerHpFlag = false;
            /// <summary>
            /// 压血精灵数
            /// </summary>
            public int lowerHpPetLen = 0;
            /// <summary>
            /// 是否自动使用技能
            /// </summary>
            public bool autoUseSkillFlg = false;
            /// <summary>
            /// 自动使用技能的精灵catchtime
            /// </summary>
            public int autoUseSkillPetCatchTime { get; set; }
            /// <summary>
            /// 自动使用的技能ID
            /// </summary>
            public int autoUseSkillId { get; set; }
            /// <summary>
            /// 自动使用技能时是否自动回pp
            /// </summary>
            public bool autoUseSkillAddPPFlg { get; set; }
        }
        public static SendDataController sendDataController = new SendDataController();
        public static FireBuff fireBuffCopyObj = new FireBuff();
        public class FireBuff
        {
            public int copyFireBuffType = 0;
            public Dictionary<int, int> greenFireBuffDic = new Dictionary<int, int>();
            public bool[] copyGreenBuff = new bool[3] { false, false, false };
            public int copyGreenBuffUserId = 0;
            public int greenFireUserId = 949386603;
        }
        public static List<PetInfo> pets { get; set; } = new List<PetInfo>();
        public static List<PetInfo> awaitPets { get; set; } = new List<PetInfo>();
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
        public static Thread fireCountThread { get; set; }

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
        public static UserInfo loginUserInfo { get; set; } = new UserInfo();

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
