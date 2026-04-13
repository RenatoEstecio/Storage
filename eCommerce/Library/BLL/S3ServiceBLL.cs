using Amazon.S3;
using Amazon.S3.Transfer;
using Library.Util;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL
{
    public class S3ServiceBLL
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

        public async Task<string> UploadArquivoAsync(Stream file, string? name, string? content)
        {
            try
            {
                if (name is null)
                    name = GenHash();

                if (content is null)
                    content = "image/jpeg";

                string key = GenHash() + name;

                var request = new Amazon.S3.Model.PutObjectRequest
                {
                    BucketName = bucket,
                    Key = $"uploads/{key}",
                    InputStream = file,
                    ContentType = content
                };

                var response = await _s3.PutObjectAsync(request);

                return urlBase + key;
            }
            catch (Exception e)
            {
                throw new CustomException("Falha ao salvar arquivo(AWS): " + e.Message);
            }
        }

        string GenHash() => Guid.NewGuid().ToString().Substring(0, 8);
    }
}
