using CatsAndPies.Domain.Abstractions.Repositories;
using CatsAndPies.Domain.Entities;
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
        private readonly IBaseRepository<ExceptionLogEntity> _logger;
        private readonly SemaphoreSlim _signal = new(0);

        public LogQueueService(IBaseRepository<ExceptionLogEntity> logger)
        {
            _logger = logger;
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
                            await _logger.Add(log); // Пишем в базу данных
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