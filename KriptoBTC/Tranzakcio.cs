using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace KriptoBTC
{
	class Tranzakcio
	{
		public string TimeStamp;
		public User Sender;
		public User Receiver;
		public int Amount;

		ASCIIEncoding ByteConverter = new ASCIIEncoding();

		public Tranzakcio(User sender, User receiver, int amount)
		{
			TimeStamp = GetTimestamp(DateTime.Now);
			Sender = sender;
			Receiver = receiver;
			Amount = amount;
		}

		public void Send()
		{
			Sender.Balance -= Amount;
			Receiver.Balance += Amount;
		}

		public string computeRealHash()
		{
			return SHA256Class.ComputeSha256Hash(this.TimeStamp + this.Sender + this.Receiver + this.Amount);
		}

		public byte[] GetSignature()
		{
			RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
			RSAParameters Key = Sender.MyRSA.ExportParameters(true);

			byte[] originalData = ByteConverter.GetBytes(this.TimeStamp + this.Sender + this.Receiver + this.Amount); //this.TimeStamp + this.Sender + this.Receiver + this.Amount

			RSA.ImportParameters(Key);
			HashAlgorithm hashalg = new SHA1CryptoServiceProvider();
			//Console.WriteLine(BitConverter.ToString(RSA.SignData(originalData, hashalg)));
			return RSA.SignData(originalData, hashalg);
		}

		public bool Verify(byte[] signature)
		{
			RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
			RSAParameters Key = Sender.MyRSA.ExportParameters(true);

			byte[] originalData = ByteConverter.GetBytes(this.TimeStamp + this.Sender + this.Receiver + this.Amount); //this.TimeStamp + this.Sender + this.Receiver + this.Amount
			Console.WriteLine(this.TimeStamp + this.Sender.Name + this.Receiver.Name);
			RSA.ImportParameters(Key);

			HashAlgorithm hashalg = new SHA1CryptoServiceProvider();
			return RSA.VerifyData(originalData, hashalg, signature);
		}

		public static String GetTimestamp(DateTime value)
		{
			return value.ToString("yyyyMMddHHmmssffff");
		}
	}
}
