using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// TamperProofQueryString 的摘要描述
/// </summary>
public class TamperProofQueryString
{
    private const string SecretSalt = "6bh19hv46xj7ubo9";
    // A static constructor is used to initialize any static data, or to perform a particular action that needs performed once only. 
    // It is called automatically before the first instance is created or any static members are referenced.
    static TamperProofQueryString()
    {
        
    }

    public static string CreateTamperProofURL(string url, string nonTamperProofParams, string tamperProofParams)
    {
        System.Text.StringBuilder tpUrl = new System.Text.StringBuilder(Utils.ResolveUrl(url));

        if (!string.IsNullOrEmpty(nonTamperProofParams) || !string.IsNullOrEmpty(tamperProofParams))
        {
            tpUrl.Append("?");
        }

        // Add on the tamper & non-tamper proof parameters, if any
        if (!string.IsNullOrEmpty(nonTamperProofParams))
        {
            tpUrl.Append(nonTamperProofParams);

            if (tamperProofParams.Length > 0)
            {
                tpUrl.Append("&");
            }
        }
        
        if (!string.IsNullOrEmpty(tamperProofParams))
        {
            tpUrl.Append(tamperProofParams);
            // Add on the tamper-proof digest, if needed
            tpUrl.Append(String.Concat("&Digest=", 
                HttpUtility.UrlEncode(GetDigest(tamperProofParams))
                )
            );
        }
                
        return tpUrl.ToString();
    }

    public static void EnsureURLNotTampered(string tamperProofParams)
    {
        //Determine what the digest SHOULD be
        string expectedDigest = GetDigest(tamperProofParams);

        // Any + in the digest passed through the querystring would be
        // convereted into spaces, so 'uncovert' them
        string receivedDigest = HttpContext.Current.Request.QueryString["Digest"];
        if (receivedDigest == null)
        {
            // Oh my, we didn't get a Digest!
            throw new ArgumentNullException("Digest", "You must pass in a digest.");
        }
        receivedDigest = receivedDigest.Replace(" ", "+");

        // Now, see if the received and expected digests match up
        if (string.Compare(expectedDigest, receivedDigest) != 0)
        {
            // Don't match up, egad          
            throw new ArgumentException("The URL has been tampered with.");
        }
    }
    
    private static string GetDigest(string tamperProofParams)
    {
        string digest = string.Empty;

        string input = string.Concat(SecretSalt, tamperProofParams, SecretSalt);

        // The array of bytes that will contain the encrypted value of input
        byte[] hashedDataBytes;

        // The encoder class used to convert strPlainText to an array of bytes
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

        // Create an instance of the MD5CryptoServiceProvider class
        System.Security.Cryptography.MD5CryptoServiceProvider md5Hasher = new System.Security.Cryptography.MD5CryptoServiceProvider();

        // Call ComputeHash, passing in the plain-text string as an array of bytes
        // The return value is the encrypted value, as an array of bytes
        hashedDataBytes = md5Hasher.ComputeHash(encoder.GetBytes(input));

        // Base-64 Encode the results and strip off ending '==', if it exists
        digest = Convert.ToBase64String(hashedDataBytes).TrimEnd("=".ToCharArray());

        return digest;
    }
}
