using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace KriptoBTC
{
    class Block
    {
        public int BlockNumber { get; set; }
        public string HashB { get; set; }
        public string PreviousHash { get; set; }
        public int Nonce { get; set; }
        public string TimeStamp { get; set; }

        public string RootHash { get; set; }

        private int number = 0;

        private Random rnd = new Random(int.MaxValue);
		public Block(string previousHash, string rootHash)
		{
            BlockNumber = number;
            number++;
			PreviousHash = previousHash;
            HashB = SHA256Class.ComputeSha256Hash(this.PreviousHash + this.TimeStamp + this.RootHash + this.Nonce);
            TimeStamp = GetTimestamp(DateTime.Now);
            RootHash = rootHash;

            /*Console.WriteLine(Data);
            Console.WriteLine(PreviousHash);
            Console.WriteLine(TimeStamp);
            Console.WriteLine(Hash);*/
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        public string MineBlock(int diff)
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

        public string CreateHash()
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
