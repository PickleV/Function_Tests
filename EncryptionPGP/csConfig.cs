﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionPGP
{
    public class csConfig
    {
        /// <summary>
        /// Created public and private key folder
        /// </summary>
        public string NewKeysFolder { get; set; }
        /// <summary>
        /// Publick key used to check the file
        /// </summary>
        public string PublicKeyPath { get; set; }
        /// <summary>
        /// private key used to check the file
        /// </summary>
        public string PrivateKeyPath { get; set; }
        /// <summary>
        /// Password to decrypt the file
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// File to check
        /// </summary>
        public string DecryptFilePath { get; set; }

        public csConfig() 
        {
            NewKeysFolder = "";
            PublicKeyPath = "";
            PrivateKeyPath = "";
            Password = "";
        }
    }
}
