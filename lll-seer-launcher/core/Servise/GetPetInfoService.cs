using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Dto;
using lll_seer_launcher.core.Dto.PetDto;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Servise
{
    public class GetPetInfoService
    {
        public static void OnGetPetInfoByOnce(HeadInfo recvDataHeadInfo)
        {
            GlobalVariable.pets.Clear();
            int index = 0;
            int petCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            index += 4;
            for (int i = 0; i < petCount; i++)
            {
                PetInfo petInfo = new PetInfo();
                index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                GlobalVariable.pets.Add(petInfo);
            }
            
            //foreach (PetInfo petinfo in GlobalVariable.pets)
            //{
            //    Console.WriteLine($"id:{petinfo.petId} name:{petinfo.petName} catchTime:{petinfo.catchTime}");
            //}
            int awaitPetCount = ByteConverter.BytesTo10(ByteConverter.TakeBytes(recvDataHeadInfo.decryptData, index, 4));
            GlobalVariable.awaitPets.Clear();
            if (awaitPetCount > 0)
            {
                index += 4;
                for (int i = 0; i < awaitPetCount; i++)
                {
                    PetInfo petInfo = new PetInfo();
                    index = petInfo.SetPetInfo(index, recvDataHeadInfo.decryptData);
                    GlobalVariable.awaitPets.Add(petInfo);
                }
                //foreach (PetInfo petinfo in GlobalVariable.awaitPets)
                //{
                //    Console.WriteLine($"id:{petinfo.petId} name:{petinfo.petName} catchTime:{petinfo.catchTime}");
                //}
            }
        }
    }
}
