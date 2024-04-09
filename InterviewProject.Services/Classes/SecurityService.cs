using System.Security.Cryptography;
using System.Text;
using InterviewProject.Services.Interfaces;

namespace InterviewProject.Services.Classes
{
    /// <summary>
    /// Additional service for security features
    /// </summary>
    public class SecurityService: ISecurityService
    {
        /// <summary>
        /// Hashes specified string.
        /// </summary>
        /// <param name="value">The target string.</param>
        public string GetHashedValue(string value)
        {
            var sb = new StringBuilder();

            using (var shaM = SHA512.Create())
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashedData = shaM.ComputeHash(data);

                // Convert to string
                foreach (var t in hashedData)
                {
                    sb.Append(t.ToString("X2"));
                }
            }

            return sb.ToString();
        }
    }
}

