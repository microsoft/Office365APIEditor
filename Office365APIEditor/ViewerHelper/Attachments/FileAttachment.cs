// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;

namespace Office365APIEditor.ViewerHelper.Attachments
{
    public class FileAttachment
    {
        public string FullPath { get; } // Full file path

        public string Name { get; } // File name

        public string ContentBytes { get; } // base64-encoded contents of the file

        public FileAttachment(string Name, string ContentBytes)
        {
            this.Name = Name;
            this.ContentBytes = ContentBytes;
            FullPath = "";
        }

        public FileAttachment(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                FullPath = Path.GetFullPath(FilePath);
                Name = Path.GetFileName(FilePath);

                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] content = new byte[fileStream.Length];
                    fileStream.Read(content, 0, content.Length);
                    ContentBytes = Convert.ToBase64String(content);
                }
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}
