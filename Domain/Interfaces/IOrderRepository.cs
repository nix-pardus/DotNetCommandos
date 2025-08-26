using Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Создание нового заказа
        /// </summary>
        Task AddAsync(Order order);
        /// <summary>
        /// Чтение заказа по идентификатору
        /// </summary>
        Task<Order> GetByIdAsync(long id);
        /// <summary>
        /// Чтение всех заказов
        /// </summary>
        Task<IEnumerable<Order>> GetAllAsync();
        /// <summary>
        /// Чтение всех заказов клиента
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetByClientIdAsync(Guid ClientId);
        /// <summary>
        /// Чтение всех заказов Мастера
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Order>> GetByMasterIdAsync(Guid MasterId);
        /// <summary>
        /// Редактирование Заказа
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        Task UpdateAsync(Order order);
        
        //Может ли заказ удаляться из БД?
    }
}
