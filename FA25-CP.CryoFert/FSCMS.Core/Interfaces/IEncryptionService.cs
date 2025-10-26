using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Interfaces
{
    /// <summary>
    /// Interface for encryption and decryption operations.
    /// </summary>
    public interface IEncryptionService
    {
        /// <summary>
        /// Encrypts the specified plain text.
        /// </summary>
        /// <param name="plainText">The plain text to encrypt.</param>
        /// <returns>The encrypted text.</returns>
        string Encrypt(string plainText);

        /// <summary>
        /// Decrypts the specified encrypted text.
        /// </summary>
        /// <param name="encryptedText">The encrypted text to decrypt.</param>
        /// <returns>The plain text.</returns>
        string Decrypt(string encryptedText);
    }
}
