using System.Collections.Generic;
using Abstraction;

namespace IEC_60870
{
    public class ItemBridge: Item
    {
        public Item60870 Item { get; }
        public override Dictionary<ItemSource, ItemDestination> Dictionary { get; set; }

        public ItemBridge(Item60870 item, Dictionary<ItemSource, ItemDestination> dictionary)
        {
            Item = item;
            Dictionary = dictionary;
        }
    }

    public class Item60870
    {
        public int TypeId { get; }
        public bool Sq { get; }
        public int Length { get; }
        public int Cot { get; }
        public bool IsNegative { get; }
        public bool IsTest { get; }
        public int Oa { get; }
        public int Ca { get; }

        public Item60870(int typeId, bool sq, int length, int cot, bool isNegative, bool isTest, int oa, int ca)
        {
            TypeId = typeId;
            Sq = sq;
            Length = length;
            Cot = cot;
            IsNegative = isNegative;
            IsTest = isTest;
            Oa = oa;
            Ca = ca;
        }
    }

    public class Obj : ItemDestination
    {
        public int AddrObj { get; }
        public string TypeElement { get; }
        public int IndexElement { get; }

        public Obj(int addrObj, string typeElemen, int indexElement)
        {
            AddrObj = addrObj;
            TypeElement = typeElemen;
            IndexElement = indexElement;
        }
    }
}
