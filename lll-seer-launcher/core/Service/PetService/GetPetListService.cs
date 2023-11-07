using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Service.PetService
{
    public class GetPetListService
    {
        public static void OnGetPetList(HeadInfo recvDataHeadInfo)
        {
            if (!GlobalVariable.petList.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.petList.Add(GlobalVariable.loginUserInfo.userId, new Dictionary<int, Dictionary<int, PetListInfo>>());
            }
            int index = 0;
            int len = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            index += 4;
            for(int i = 0; i < len; i++)
            {
                PetListInfo info = new PetListInfo();
                index = info.SetPetListInfo(index, recvDataHeadInfo.decryptData);
                if (!GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].ContainsKey(info.petId))
                {
                    GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].Add(info.petId, new Dictionary<int, PetListInfo>());
                }
                if (!GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][info.petId].ContainsKey(info.catchTime))
                {
                    GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][info.petId].Add(info.catchTime, info);
                }
                if (!GlobalVariable.petCatchTimeDic.ContainsKey(info.catchTime)) GlobalVariable.petCatchTimeDic.Add(info.catchTime, new PetInfo(info));
            }
        }

        public static void OnGetLovePetList(HeadInfo recvDataHeadInfo)
        {
            if (!GlobalVariable.petList.ContainsKey(GlobalVariable.loginUserInfo.userId))
            {
                GlobalVariable.petList.Add(GlobalVariable.loginUserInfo.userId, new Dictionary<int, Dictionary<int, PetListInfo>>());
            }
            int index = 0;
            int len = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            index += 4;
            for (int i = 0; i < len; i++)
            {
                PetListInfo info = new PetListInfo();
                index = info.SetLovePetListInfo(index, recvDataHeadInfo.decryptData);
                if (!GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].ContainsKey(info.petId))
                {
                    GlobalVariable.petList[GlobalVariable.loginUserInfo.userId].Add(info.petId, new Dictionary<int, PetListInfo>());
                }
                if (!GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][info.petId].ContainsKey(info.catchTime))
                {
                    GlobalVariable.petList[GlobalVariable.loginUserInfo.userId][info.petId].Add(info.catchTime, info);
                }
            }
        }
    }
}
