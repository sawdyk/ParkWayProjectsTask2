using Microsoft.AspNetCore.Http;
using ParkWayProjectsTask2.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkWayProjectsTask2.Repository.Configuration.Repository.Interface
{
    public interface IUploadConfigurationRepo
    {
        Task<GenericResponseModel> uploadConfigurationFileAsync(IFormFile configurationFile);
    }
}
