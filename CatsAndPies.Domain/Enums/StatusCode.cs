using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndPies.Domain.Enums
{
    public enum StatusCode
    {
        Ok = 200,//Все хорошо
        Created = 201,//Создан ресурс
        Unauthorized = 401,
        Conflict = 409,
        NotFound = 404,//Страница не найдена
        InternalServerError = 500,//Внутренняя ошибка сервера
    }
}
