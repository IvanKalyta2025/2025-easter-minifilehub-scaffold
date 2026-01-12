using System.IO;
using System.Threading.Tasks;
using Minio;
using Microsoft.Extensions.Configuration;
using Minio.DataModel.Args;
using Microsoft.AspNetCore.StaticFiles;

namespace Api
{
    public class FileService
    {
        private readonly IMinioClient _minioClient;
        private readonly FileExtensionContentTypeProvider _contentTypeProvider;

        public FileService(IConfiguration configuration)
        {
            _minioClient = new MinioClient()
                .WithEndpoint(configuration["Minio:Endpoint"])
                .WithCredentials(configuration["Minio:AccessKey"], configuration["Minio:SecretKey"])
                .Build();

            _contentTypeProvider = new FileExtensionContentTypeProvider();
        }

        public async Task<string> UploadFileAsync(string bucketName, string objectName, byte[] fileData)
        {
            var existsArgs = new BucketExistsArgs().WithBucket(bucketName);
            bool found = await _minioClient.BucketExistsAsync(existsArgs).ConfigureAwait(false);

            if (!found)
            {
                var makeArgs = new MakeBucketArgs().WithBucket(bucketName);
                await _minioClient.MakeBucketAsync(makeArgs).ConfigureAwait(false);
            }
            if (!_contentTypeProvider.TryGetContentType(objectName, out string contentType))
            {
                contentType = "application/octet-stream";
            }

            using var stream = new MemoryStream(fileData);

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithContentType(contentType);

            var result = await _minioClient.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
            //Console.WriteLine($"File '{objectName}' uploaded to bucket '{bucketName}'.");
            //ругался на отсутствие return string
            var fileUrl = $"http://file-service.localhost/{bucketName}/{objectName}";
            return fileUrl;
        }
        public async Task<(byte[] Data, string ContentType)> DownloadFileAsync(string bucketName, string objectName) //добавлен ContentType
        {
            try
            {
                // Создаем MemoryStream для хранения данных
                using var memoryStream = new MemoryStream();

                // 1. Создаем аргументы для GET-запроса
                var getObjectArgs = new GetObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithCallbackStream(async (stream) =>
                    {
                        // 2. Копируем поток из MinIO в наш MemoryStream
                        await stream.CopyToAsync(memoryStream);
                    });

                // 3. Выполняем запрос
                await _minioClient.GetObjectAsync(getObjectArgs).ConfigureAwait(false);

                // 4. Возвращаем null, если файл пустой (0 байт),
                //    чтобы это было обработано в контроллере как "не найдено/проблема".
                if (memoryStream.Length == 0)
                {
                    // Это необходимо, чтобы поймать случаи, когда файл не найден,
                    // но исключение ObjectNotFoundException не было брошено.
                    return (null, null);
                }
                if (!_contentTypeProvider.TryGetContentType(objectName, out string contentType))
                {
                    contentType = "application/octet-stream";
                }

                // 6. ВОЗВРАЩАЕМ ДАННЫЕ И ТИП
                return (memoryStream.ToArray(), contentType);

                // 5. Возвращаем данные в виде массива байтов
            }
            catch (Minio.Exceptions.ObjectNotFoundException)
            {
                // Если MinIO не нашел объект, возвращаем null (как ожидает контроллер)
                return (null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[CRITICAL ERROR] Тип ошибки: {ex.GetType().Name}");
                Console.WriteLine($"[CRITICAL ERROR] Сообщение: {ex.Message}");
                Console.WriteLine($"Ошибка при скачивании файла: {ex.Message}");

                // Если MinIO не смог соединиться, здесь будет указана причина.
                return (null, null);
            }
        }

    }
}