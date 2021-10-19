using System;
using System.Collections.Generic;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace KriptoBTC
{
	class User
	{
		public string Name;
		public int Balance;
		public RSACryptoServiceProvider MyRSA;

		public User(string name, int balance = 100)
		{
			Name = name;
			Balance = balance;
			MyRSA = new RSACryptoServiceProvider(384);
			
			BigInteger PubKey = new BigInteger(MyRSA.ExportRSAPublicKey());
			BigInteger PrivKey = new BigInteger(MyRSA.ExportRSAPrivateKey());
			
			Console.WriteLine(Name);
			Console.WriteLine(Balance);
			Console.WriteLine(PubKey);
			Console.WriteLine(PrivKey);
		}
	}
}
