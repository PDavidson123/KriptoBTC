using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace KriptoBTC
{
    class Block
    {
        public string BlockData { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public string TimeStamp { get; set; }
        public string RootHash { get; set; }

        private Random rnd = new Random(int.MaxValue);
		public Block(string previousHash, string rootHash, bool realBlock)
		{
			PreviousHash = previousHash;
            TimeStamp = GetTimestamp(DateTime.Now);
            RootHash = rootHash;

            if (realBlock)
                BlockData = MineBlock(3);
            else
                BlockData = "";

            /*Console.WriteLine(Data);
            Console.WriteLine(PreviousHash);
            Console.WriteLine(TimeStamp);
            Console.WriteLine(Hash);*/
        }

        public string GetPreviousHash()
        {
            return this.BlockData;
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private string MineBlock(int diff)
        {
            string zeros = new string('0', diff);

            //string Hash = CreateHash();
            while (CreateHash().Substring(0, diff) != zeros)
            {
                this.Nonce = rnd.Next();
                //Hash = CreateHash();
            }
            return CreateHash();
        }



        private string CreateHash()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string rawData = this.PreviousHash + this.TimeStamp + this.RootHash + this.Nonce;
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                return Encoding.Default.GetString(bytes);
            }
        }

    }

	
}
