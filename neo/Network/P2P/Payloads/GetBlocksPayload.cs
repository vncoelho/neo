using Neo.IO;
using System.IO;

namespace Neo.Network.P2P.Payloads
{
    public class GetBlocksPayload : ISerializable
    {
        public UInt256[] HashStart;
        public UInt256 HashStop;
        public int MaxBlocksCount;

        public int Size => HashStart.GetVarSize() + HashStop.Size + sizeof(int);

        public static GetBlocksPayload Create(UInt256 hash_start, int max_block_count = 1, UInt256 hash_stop = null)
        {
            return new GetBlocksPayload
            {
                HashStart = new[] { hash_start },
                MaxBlocksCount = max_block_count,
                HashStop = hash_stop ?? UInt256.Zero
            };
        }

        void ISerializable.Deserialize(BinaryReader reader)
        {
            HashStart = reader.ReadSerializableArray<UInt256>(16);
            HashStop = reader.ReadSerializable<UInt256>();
            MaxBlocksCount = reader.ReadInt32();
        }

        void ISerializable.Serialize(BinaryWriter writer)
        {
            writer.Write(HashStart);
            writer.Write(HashStop);
            writer.Write(MaxBlocksCount);
        }
    }
}
