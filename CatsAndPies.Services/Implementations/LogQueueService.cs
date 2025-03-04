using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Services.Implementations
{
    public class LogQueueService
    {
        private readonly ConcurrentQueue<ExceptionLogEntity> _logQueue = new();
        private readonly IServiceProvider _serviceProvider; // Внедрение IServiceProvider
        private readonly SemaphoreSlim _signal = new(0);

        public LogQueueService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            StartProcessing();
        }

        public void Enqueue(ExceptionLogEntity log)
        {
            _logQueue.Enqueue(log);
            _signal.Release();
        }

        private void StartProcessing()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await _signal.WaitAsync(); // Ждём события в очереди
                    if (_logQueue.TryDequeue(out var log))
                    {
                        try
                        {
                            // Создаём новый Scope для работы с репозиторием
                            using var scope = _serviceProvider.CreateScope();
                            var logger = scope.ServiceProvider.GetRequiredService<IBaseRepository<ExceptionLogEntity>>();

                            // Записываем лог в базу данных
                            await logger.Add(log);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Ошибка записи лога: {ex.Message}");
                        }
                    }
                }
            });
        }
    }


}