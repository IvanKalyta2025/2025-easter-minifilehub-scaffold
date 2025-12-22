using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Api.Repositories;
using Microsoft.AspNetCore.Routing.Constraints;
using System.IO;

namespace Api.Services
{
    public class ProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IUserRepository _userRepository;

        private readonly FileService _fileService;

        public ProfileService(IProfileRepository profileRepository, IUserRepository userRepository, FileService fileService)
        {
            _profileRepository = profileRepository;
            _userRepository = userRepository;
            _fileService = fileService;
        }

        public async Task CreateProfileAsync(Stream fileData, string objectName)
        {
            var fileUrl = await _fileService.UploadFileAsync(objectName, fileData);
        }
    }
}
