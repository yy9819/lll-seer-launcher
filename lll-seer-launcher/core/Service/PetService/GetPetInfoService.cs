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
    public class GetPetInfoService
    {
        /// <summary>
        /// 获取精灵背包内的精灵
        /// </summary>
        /// <param name="recvDataHeadInfo"></param>
        public static void OnGetPetInfoByOnce(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.pets.Clear();
            int index = 0;
            // 获取当前背包内精灵总数
            int petCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            index += 4;
            // 获取每只精灵的详细信息
            for (int i = 0; i < petCount; i++)
            {
                PetInfo petInfo = new PetInfo();
                lock (GlobalVariable.lockObjs["setPetInfo"])
                {
                    index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                }
                //index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                GlobalVariable.pets.Add(petInfo.catchTime, petInfo);
            }

            foreach (PetInfo petinfo in GlobalVariable.pets.Values)
            {
                if(!GlobalVariable.petCatchTimeDic.ContainsKey(petinfo.catchTime)) GlobalVariable.petCatchTimeDic.Add(petinfo.catchTime,petinfo);
            }
            int awaitPetCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            GlobalVariable.awaitPets.Clear();
            if (awaitPetCount > 0)
            {
                index += 4;
                for (int i = 0; i < awaitPetCount; i++)
                {
                    PetInfo petInfo = new PetInfo();
                    lock (GlobalVariable.lockObjs["setPetInfo"])
                    {
                        index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                    }
                    //index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                    GlobalVariable.awaitPets.Add(petInfo.catchTime, petInfo);
                }
                foreach (PetInfo petinfo in GlobalVariable.awaitPets.Values)
                {
                    if (!GlobalVariable.petCatchTimeDic.ContainsKey(petinfo.catchTime)) GlobalVariable.petCatchTimeDic.Add(petinfo.catchTime, petinfo);
                }
            }
            GlobalVariable.gameConfigFlag.getPetBag = false;
        }

        public static void OnGetPetInfo(HeadInfo recvDataHeadInfo)
        {
            PetInfo petInfo = new PetInfo();
            lock (GlobalVariable.lockObjs["setPetInfo"])
            {
                petInfo.SetPetInfo(0, recvDataHeadInfo.decryptData);
            }
            //petInfo.SetPetInfo(0, recvDataHeadInfo.decryptData);
            if (!GlobalVariable.petCatchTimeDic.ContainsKey(petInfo.catchTime))
            {
                GlobalVariable.petCatchTimeDic.Add(petInfo.catchTime, petInfo);
            }
            else
            {
                GlobalVariable.petCatchTimeDic[petInfo.catchTime] = petInfo;
            }
        }
    }
}
