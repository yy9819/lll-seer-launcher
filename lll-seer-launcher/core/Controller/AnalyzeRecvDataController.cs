using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using lll_seer_launcher.core.Servise;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Controller
{
    class AnalyzeRecvDataController
    {
        /// <summary>
        /// 对需在字典里调用的方法设置一个通用格式委托方法
        /// </summary>
        /// <param name="recvDataHeadInfo">封包</param>
        private delegate void AnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo);

        /// <summary>
        /// key:cmdId
        /// value:需要执行的封包controller
        /// </summary>
        private Dictionary<int, AnalyzeRecvDataMethod> methodDictionary;
        public AnalyzeRecvDataController()
        {
            methodDictionary = new Dictionary<int, AnalyzeRecvDataMethod>
            {
                { 3403,(param) => AnalyzeAchieveTitleList(param) },
                { 4475,(param) =>  AnalyzeItemList(param) }
            };
        }

        /// <summary>
        /// 封包解析controller
        /// 根据headInfo内的cmdId选择调用相应的解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">封包信息</param>
        public void RunAnalyzeRecvDataMethod(HeadInfo recvDataHeadInfo)
        {
            if (methodDictionary.TryGetValue(recvDataHeadInfo.cmdId, out AnalyzeRecvDataMethod method))
            {
                Thread methodThread = new Thread(()=> { method(recvDataHeadInfo); });
                methodThread.Start();
                //Console.WriteLine($"Start method for value {recvDataHeadInfo.cmdId}");
            }
            else
            {
                //Console.WriteLine($"No method defined for value {recvDataHeadInfo.cmdId}");
            }
        }

        /// <summary>
        /// 称号封包解析controller
        /// </summary>
        /// <param name="recvDataHeadInfo">带有称号信息的封包</param>
        private void AnalyzeAchieveTitleList(HeadInfo recvDataHeadInfo)
        {

            AnalyzeRecvDataServise.AnalyzeAchieveTitleList(recvDataHeadInfo);
        }

        /// <summary>
        /// 物品查询封包解析controller
        /// 根据传入的封包判断执行的解析程序
        /// </summary>
        /// <param name="recvDataHeadInfo">带有物品list的封包</param>
        private void AnalyzeItemList(HeadInfo recvDataHeadInfo)
        {
            if (recvDataHeadInfo.decryptData.Length < 8) return;
            int fristItemId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, 4, 4));
            if(fristItemId >= 1300083 & fristItemId < 1400000)
            {
                AnalyzeRecvDataServise.AnalyzeSuit(recvDataHeadInfo);
            }
        }
    }
}
