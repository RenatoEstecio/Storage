using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using Amazon.S3;
using Amazon.S3.Transfer;
using Library.DTO;
using Library.Util;
using Microsoft.Extensions.Configuration;
using SharpCompress.Compressors.Xz;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Library.BLL
{
    public class S3ServiceBLL : IImageStorageService
    {
        private readonly IAmazonS3 _s3;
        
        string bucket = "amzn-s3-storage-rse";      
        string urlBase = "https://amzn-s3-storage-rse.s3.us-east-2.amazonaws.com/uploads/";
        public S3ServiceBLL(IConfiguration config)
        {
            _s3 = new AmazonS3Client(
                config["S3:AccessKey"],
                config["S3:SecretKey"],
                Amazon.RegionEndpoint.USEast2
            );
        }

        public async Task<(string Url, string Base64)> SaveAsync(Stream file, string? name, string? content)
        {                     
            try
            {
                if (name is null)
                    name = GenHash();

                if (content is null)
                    content = "image/jpeg";

                string key = GenHash() + name;

                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucket,
                    Key = $"uploads/{key}",
                    InputStream = file,
                    ContentType = content
                };

                var response = await _s3.PutObjectAsync(request);

                string image64 = Convert.ToBase64String(ms.ToArray());

                string url = urlBase + key;

                return (url, image64);
            }
            catch (Exception e)
            {
                throw new CustomException("Falha ao salvar arquivo(AWS): " + e.Message);
            }
        }

        string GenHash(int length = 8) => Guid.NewGuid().ToString().Substring(0, length);

        
    }
}
