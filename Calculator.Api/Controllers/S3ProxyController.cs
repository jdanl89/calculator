namespace Calculator.Api.Controllers
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;
    using Amazon.S3;
    using Amazon.S3.Model;

    /// <summary>
    /// ASP.NET Core controller acting as a S3 Proxy.
    /// </summary>
    [Route("api/[controller]")]
    public class S3ProxyController : ControllerBase
    {
        IAmazonS3 S3Client { get; }

        ILogger Logger { get; }

        string BucketName { get; }

        public S3ProxyController(IConfiguration configuration, ILogger<S3ProxyController> logger, IAmazonS3 s3Client)
        {
            this.Logger = logger;
            this.S3Client = s3Client;

            this.BucketName = configuration[Startup.AppS3BucketKey];
            if(string.IsNullOrEmpty(this.BucketName))
            {
                logger.LogCritical("Missing configuration for S3 bucket. The AppS3Bucket configuration must be set to a S3 bucket.");
                throw new Exception("Missing configuration for S3 bucket. The AppS3Bucket configuration must be set to a S3 bucket.");
            }

            logger.LogInformation($"Configured to use bucket {this.BucketName}");
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            ListObjectsV2Response _listResponse = await this.S3Client.ListObjectsV2Async(new ListObjectsV2Request
            {
                BucketName = this.BucketName
            });

            try
            {
                this.Response.ContentType = "text/json";
                return new JsonResult(_listResponse.S3Objects);
            }
            catch(AmazonS3Exception _ex)
            {
                this.Response.StatusCode = (int)_ex.StatusCode;
                return new JsonResult(_ex.Message);
            }
        }

        [HttpGet("{key}")]
        public async Task Get(string key)
        {
            try
            {
                GetObjectResponse _getResponse = await this.S3Client.GetObjectAsync(new GetObjectRequest
                {
                    BucketName = this.BucketName,
                    Key = key
                });

                this.Response.ContentType = _getResponse.Headers.ContentType;
                _getResponse.ResponseStream.CopyTo(this.Response.Body);
            }
            catch (AmazonS3Exception _e)
            {
                this.Response.StatusCode = (int)_e.StatusCode;
                StreamWriter _writer = new StreamWriter(this.Response.Body);
                _writer.Write(_e.Message);
            }
        }

        [HttpPut("{key}")]
        public async Task Put(string key)
        {
            // Copy the request body into a seekable stream required by the AWS SDK for .NET.
            MemoryStream _seekableStream = new MemoryStream();
            await this.Request.Body.CopyToAsync(_seekableStream);
            _seekableStream.Position = 0;

            PutObjectRequest _putRequest = new PutObjectRequest
            {
                BucketName = this.BucketName,
                Key = key,
                InputStream = _seekableStream
            };

            try
            {
                PutObjectResponse _response = await this.S3Client.PutObjectAsync(_putRequest);
                Logger.LogInformation($"Uploaded object {key} to bucket {this.BucketName}. Request Id: {_response.ResponseMetadata.RequestId}");
            }
            catch (AmazonS3Exception _ex)
            {
                this.Response.StatusCode = (int)_ex.StatusCode;
                StreamWriter _writer = new StreamWriter(this.Response.Body);
                _writer.Write(_ex.Message);
            }
        }

        [HttpDelete("{key}")]
        public async Task Delete(string key)
        {
            DeleteObjectRequest _deleteRequest = new DeleteObjectRequest
            {
                 BucketName = this.BucketName,
                 Key = key
            };

            try
            {
                DeleteObjectResponse _response = await this.S3Client.DeleteObjectAsync(_deleteRequest);
                Logger.LogInformation($"Deleted object {key} from bucket {this.BucketName}. Request Id: {_response.ResponseMetadata.RequestId}");
            }
            catch (AmazonS3Exception _ex)
            {
                this.Response.StatusCode = (int)_ex.StatusCode;
                StreamWriter _writer = new StreamWriter(this.Response.Body);
                _writer.Write(_ex.Message);
            }
        }
    }
}
