using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChapterOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        private IConfiguration configuration { get; set; }

        public BackupController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult> GenerateBackupFile()
        {
            try
            {
                await Task.Run(() =>
                {
                    string dbConnectionString = configuration.GetConnectionString("DefaultConnection");
                    string backupDestination = "C:\\SQLBackUpFolder\\";

                    if (!System.IO.Directory.Exists(backupDestination))
                    {
                        System.IO.Directory.CreateDirectory("C:\\SQLBackUpFolder");
                    }
                    SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(dbConnectionString);
                    var backupFileName = $"{backupDestination}{sqlConnectionStringBuilder.InitialCatalog}-{DateTime.Now.ToString("yyyy-MM-dd")}.bak";
                    if (System.IO.File.Exists(backupFileName))
                        System.IO.File.Delete(backupFileName);
                    using (SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
                    {
                        string backupQuery = $"BACKUP DATABASE {sqlConnectionStringBuilder.InitialCatalog} TO DISK='{backupFileName}'";
                        using (SqlCommand command = new SqlCommand(backupQuery, connection))
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                });
                return StatusCode(200);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
