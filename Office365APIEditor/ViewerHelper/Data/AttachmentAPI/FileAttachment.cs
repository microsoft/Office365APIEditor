// Copyright (c) Microsoft. All rights reserved. 
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information. 

using System;
using System.IO;
using System.Runtime.Serialization;

namespace Office365APIEditor.ViewerHelper.Data.AttachmentAPI
{
    [DataContract]
    public class FileAttachment : AttachmentBase
    {
        private string contentBytes;

        public FileAttachment()
        {
        }

        public FileAttachment(string JsonData) : base(JsonData)
        {
        }

        public static FileAttachment CreateFromFilePath(string FilePath)
        {
            if (File.Exists(FilePath))
            {
                FileAttachment fileAttachment = new FileAttachment();

                fileAttachment.FullPath = Path.GetFullPath(FilePath);
                fileAttachment.Name = Path.GetFileName(FilePath);

                using (FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] content = new byte[fileStream.Length];
                    fileStream.Read(content, 0, content.Length);
                    fileAttachment.ContentBytes = Convert.ToBase64String(content);
                }

                return fileAttachment;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public static FileAttachment CreateFromContentBytes(string Name, string ContentBytes)
        {
            if (Name == null)
            {
                throw new ArgumentNullException("Name");
            }

            if (ContentBytes == null)
            {
                throw new ArgumentNullException("ContentBytes");
            }

            FileAttachment fileAttachment = new FileAttachment()
            {
                Name = Name,
                ContentBytes = ContentBytes
            };

            return fileAttachment;
        }

        [DataMember(Name = "@odata.type")]
        public string ODataType
        {
            get
            {
                if (Util.UseMicrosoftGraphInMailboxViewer)
                {
                    return "#microsoft.graph.fileAttachment";
                }
                else
                {
                    return "#Microsoft.OutlookServices.FileAttachment";
                }
            }
            set { }
        }

        public override AttachmentType AttachmentType
        {
            get
            {
                return AttachmentType.FileAttachment;
            }
        }

        // base64-encoded contents of the file
        [DataMember(Name = "contentBytes")]
        public string ContentBytes {
            get
            {
                if (contentBytes == null)
                {
                    ContentBytes = LoadPropertyFromRawJson<string>("contentBytes", null);
                }

                return contentBytes;
            }

            private set => contentBytes = value;
        }

        // Full file path
        public string FullPath { get; private set; }
    }
}