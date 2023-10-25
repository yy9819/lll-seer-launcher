using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Utils;
using lll_seer_launcher.core.Dto;

namespace lll_seer_launcher.core.Servise
{
    class AnalyzeRecvDataServise
    {
        /// <summary>
        /// 加载当前账号所持有的称号
        /// </summary>
        /// <param name="recvDataHeadInfo">称号封包</param>
        public static void AnalyzeAchieveTitleList(HeadInfo recvDataHeadInfo)
        {
            int achieveTitlesLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0,4));
            ArrayList achieveTitles = new ArrayList();
            int achieveTitleId;
            for (int i = 1; i <= achieveTitlesLen; i++)
            {
                achieveTitleId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, i*4, 4));
                if(achieveTitleId > 150 ) achieveTitles.Add(achieveTitleId);
            }
            if(GlobalVariable.userAchieveTitleDictionary.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.userAchieveTitleDictionary[GlobalVariable.loginUserInfo.userId] = achieveTitles;
            }
            else
            {
                GlobalVariable.userAchieveTitleDictionary.Add(GlobalVariable.loginUserInfo.userId, achieveTitles);
            }
            //GlobalVariable.userAchieveTitleDictionary.TryGetValue(GlobalVariable.userId, out achieveTitles);
            GlobalVariable.initSuitGroupBoxsCompleteFlg[0] = true;
        }

        /// <summary>
        /// 加载当前账号所持有的装备
        /// </summary>
        /// <param name="recvDataHeadInfo">装备封包</param>
        public static void AnalyzeSuit(HeadInfo recvDataHeadInfo)
        {
            int clothLen = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 0, 4));
            Dictionary<int, int> clothDic = new Dictionary<int, int>();
            int clothId;
            int index = 4;
            for (int i = 0; i < clothLen; i++)
            {
                clothId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
                if(ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index + 4 , 4)) == 1)clothDic.Add(clothId, clothId);
                index += 20;
            }
            if (GlobalVariable.userSuitClothDictionary.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.userSuitClothDictionary[GlobalVariable.loginUserInfo.userId] = clothDic;
            }
            else
            {
                GlobalVariable.userSuitClothDictionary.Add(GlobalVariable.loginUserInfo.userId, clothDic);
            }
            GlobalVariable.initSuitGroupBoxsCompleteFlg[1] = true;
            //Console.WriteLine(BitConverter.ToString(recvDataHeadInfo.decryptData));
        }
    }
}
