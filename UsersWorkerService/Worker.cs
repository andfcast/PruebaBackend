using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Utilities;
using WebAPIBusiness.Interface;
using WebApiEntities.Context;
using WebApiEntities.DTO;
using WebApiEntities.Models;

namespace UsersWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private int count = 0;        
        private DbUsersContext _context;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory factory)
        {
            _logger = logger;            
            _context = factory.CreateScope().ServiceProvider.GetRequiredService<DbUsersContext>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                count++;
                _logger.LogInformation("Inicio proceso carga a las: {time}", DateTimeOffset.Now);
                _logger.LogInformation("Página:" + (count).ToString());
                SaveData(count);
                _logger.LogInformation("Fin proceso carga a las: {time}", DateTimeOffset.Now);
                await Task.Delay(300000, stoppingToken);                                
            }
        }

        private void SaveData(int count) {
            string urlService = "https://reqres.in/api/users?page=" + count.ToString();
            ExternalApiResponseDto res = new ExternalApiResponseDto();
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(urlService).Result;
            if (response.IsSuccessStatusCode)
            {
                res = JsonConvert.DeserializeObject<ExternalApiResponseDto>(response.Content.ReadAsStringAsync().Result);
            }
            if(res.data.Count > 0) {
                List<User> lstUsers = new List<User>();
                foreach (UserDto dto in res.data)
                {
                    if (_context.Users.Count(x => x.Id == dto.id) > 0)
                    {
                        _context.Users.Update(Converter.ConvertToEntity(dto));
                    }
                    else
                    {
                        lstUsers.Add(Converter.ConvertToEntity(dto));
                    }
                }
                try
                {
                    if (lstUsers.Count > 0)
                    {
                        _context.Users.AddRange(lstUsers);
                    }
                    _context.SaveChanges();

                }
                catch (Exception)
                {

                }
            }            
        }
    }
}