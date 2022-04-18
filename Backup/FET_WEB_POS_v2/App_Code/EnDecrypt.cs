using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Text;

/// <summary>
/// EnDecrypt 的摘要描述
/// </summary>
public class EnDecrypt
{
    //public EnDecrypt()
    //{
    //    //
    //    // TODO: 在此加入建構函式的程式碼
    //    //
    //}

    //public void EnDecrypt()
    //{
    //}


	//256 Bit IV Key that is truncated when a smaller keys are required

	private byte[] bytIV = {
		

	};


	public string aa()
	{

		return "AAAA";

	}

	//Supported .Net intrinsic SymmetricAlgorithm classes.

	private enum Providers
	{
		DES,
		RC2,
		Rijndael
	}



	private SymmetricAlgorithm _CryptoService;


	//Constructor for using an intrinsic .Net SymmetricAlgorithm class.

	//ByVal NetSelected As Providers)
	public EnDecrypt()
	{

		//Select Case NetSelected

		//      Case Providers.DES

		_CryptoService = new DESCryptoServiceProvider();

		// /Case Providers.RC2

		//        _CryptoService = New RC2CryptoServiceProvider

		//Case Providers.Rijndael

		//       _CryptoService = New RijndaelManaged

		//End Select

	}



	//Constructor for using a customized SymmetricAlgorithm class.


    public EnDecrypt(SymmetricAlgorithm ServiceProvider)
	{
		_CryptoService = ServiceProvider;

	}



	//Depending on the legal key size limitations of a specific CryptoService provider

	//and length of the private key provided, padding the secret key with a character

	//or triming it to meet the legal size of the algorithm.

	private byte[] GetLegalKey(string Key)
	{

		//key sizes are in bits

		string sTemp = null;


		if ((_CryptoService.LegalKeySizes.Length > 0)) {
			int maxSize = _CryptoService.LegalKeySizes[0].MaxSize;


			if (Key.Length * 8 > maxSize) {
				sTemp = Key.Substring(0, (maxSize / 8));


			} else {
				int moreSize = _CryptoService.LegalKeySizes[0].MinSize;


				while ((Key.Length * 8 > moreSize)) {
					moreSize += _CryptoService.LegalKeySizes[0].SkipSize;

				}

				sTemp = Key.PadRight(moreSize / 8, 'X');

			}


		} else {
			sTemp = Key;

		}



		//Ensure that the IV Block size is also correct for the specific CryptoService provider


		if ((_CryptoService.LegalBlockSizes.Length > 0)) {
			int maxSize = _CryptoService.LegalBlockSizes[0].MaxSize;

			Array.Resize(ref bytIV, sTemp.Length);


			if (sTemp.Length * 8 > maxSize) {
				Array.Resize(ref bytIV, maxSize / 8);

			}

		}

		//convert the secret key to byte array

		return ASCIIEncoding.ASCII.GetBytes(sTemp);

	}



	public string Encrypt(string Source, string Key)
	{

		//Dim bytIn As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(System.Web.HttpUtility.UrlEncode(Source))

		byte[] bytIn = Encoding.UTF8.GetBytes(Source);

		//System.Text.ASCIIEncoding.ASCII.GetBytes(Encoding.Unicode.

		//UTF8.(Source))

		MemoryStream ms = new MemoryStream();



		//set the keys

		_CryptoService.Key = GetLegalKey(Key);

		_CryptoService.IV = bytIV;

		_CryptoService.Mode = CipherMode.ECB;



		//create an Encryptor from the Provider Service instance

		ICryptoTransform encrypto = _CryptoService.CreateEncryptor();



		//create Crypto Stream that transforms a stream using the encryption

		CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);



		//write out encrypted content into MemoryStream

		cs.Write(bytIn, 0, bytIn.Length);

		cs.FlushFinalBlock();

		cs.Close();

		byte[] bytOut = ms.ToArray();

		ms.Close();



		return Convert.ToBase64String(bytOut);
		//convert into Base64 so that the result can be used in xml

	}



	public string Decrypt(string Source, string Key)
	{

		//convert from Base64 to binary

		byte[] bytIn = System.Convert.FromBase64String(Source);

		MemoryStream ms = new MemoryStream(bytIn);



		byte[] bytKey = GetLegalKey(Key);

		byte[] bytTemp = new byte[bytIn.Length + 1];



		//set the private key

		byte[] aa = {
			
		};

		_CryptoService.Key = bytKey;

		_CryptoService.IV = bytIV;

		_CryptoService.Mode = CipherMode.ECB;



		//create a Decryptor from the Provider Service instance

		ICryptoTransform encrypto = _CryptoService.CreateDecryptor();



		//create Crypto Stream that transforms a stream using the decryption

		CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);

		string output = null;


		try {
			//read out the result from the Crypto Stream

			StreamReader sr = new StreamReader(cs);

			output = sr.ReadToEnd();

			sr.Close();

			ms.Close();

			cs.Close();


		} catch (Exception ex) {
            throw ex;
		}

		//Dim objDecode As System.Text.Encoding = System.Text.Encoding.UTF8 ' Memory Decode Object

		return output;
		//ncoding.ASCII.GetString(bytTemp)    'System.Web.HttpUtility.UrlDecode(output) 'Encoding.ASCII.GetString(bytTemp))

	}
}
