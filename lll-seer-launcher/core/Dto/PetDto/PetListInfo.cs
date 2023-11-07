using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using lll_seer_launcher.core.Utils;

namespace lll_seer_launcher.core.Dto.PetDto
{
    public class PetListInfo
    {
        public int petId { get; set; }
        public string petName { get; set;}
        public bool isBright { get; set; }
        public int catchTime { get; set; }
        public int level { get; set; }
        public int course { get; set; }
        public int duration { get; set; }
        public int starTime { get; set; }
        public int abilityType { get; set; }
        public bool inBag { get; set; } = false; 
        public int SetPetListInfo(int index , byte[] inputData)
        {
            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.isBright = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) == 1;
            index += 4;

            this.catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.level = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            return index;
        }
        public int SetLovePetListInfo(int index, byte[] inputData)
        {
            this.petId = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;

            this.isBright = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4)) == 1;
            index += 4;

            this.catchTime = ByteConverter.BytesTo10(ByteConverter.TakeBytes(inputData, index, 4));
            index += 4;
            return index;
        }
    }
}
