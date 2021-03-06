using System;
using System.Collections.Generic;

namespace KriptoBTC
{
	class Program
	{
		static void Main(string[] args)
		{
			List <Block> blockchain = new List<Block>();
			blockchain.Add(new Block("","", false));

			User garry = new User("Garry");
			User avokado = new User("Avocado");
			User kismacs = new User("Kismacs");

			List<Tranzakcio> tranzlist = new List<Tranzakcio>();
			List<byte[]> signatures = new List<byte[]>();

			tranzlist.Add(new Tranzakcio(garry, avokado, 10));
			tranzlist.Add(new Tranzakcio(garry, kismacs, 10));

			List<string> hashes = new List<string>();

			for(int i=0;i<tranzlist.Count;i++)
			{
				signatures.Add(tranzlist[i].GetSignature());
				Console.WriteLine(BitConverter.ToString(tranzlist[i].GetSignature()));
			}
			for(int i=0;i<tranzlist.Count;i++)
			{
				if(tranzlist[i].Verify(signatures[i]))
				{
					//Console.WriteLine("!!"+tranzlist[i].Verify(signatures[i])); //DEBUG
					hashes.Add(tranzlist[i].computeRealHash());
					tranzlist[i].Send();
				}
				else
				{
					Console.WriteLine("Invalid tranzakció");
				}
			}

			Console.WriteLine("Merkle root: " + BuildMerkleRoot(hashes));
			string root = BuildMerkleRoot(hashes);
			Console.WriteLine("Block létrehozása");
			Block newblock = new Block(blockchain[blockchain.Count-1].GetPreviousHash(), root, true);
			Console.WriteLine("Block létrehozva");
			blockchain.Add(newblock);

			Console.WriteLine("Block (mined) data: " + blockchain[blockchain.Count - 1].BlockData);
		}

		private static string BuildMerkleRoot(List<string> merkelLeaves)
		{
			if (merkelLeaves.Count == 1)
				return merkelLeaves[0];

			if (merkelLeaves.Count % 2 > 0)
				merkelLeaves.Add(merkelLeaves[merkelLeaves.Count-1]);

			var merkleBranches = new List<string>();

			for (int i = 0; i < merkelLeaves.Count; i += 2)
			{
				var leafPair = string.Concat(merkelLeaves[i], merkelLeaves[i + 1]);
				merkleBranches.Add(SHA256Class.ComputeSha256Hash(leafPair));
			}
			return BuildMerkleRoot(merkleBranches);
		}
	}
}
