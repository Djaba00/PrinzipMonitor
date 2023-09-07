using Microsoft.AspNetCore.Mvc;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.DAL.ApplicationContext.MsSql;
using PrinzipMonitorService.DAL.Repositories.FlatRepository;
using PrinzipMonitorService.DAL.Repositories.FlatRepository.Helpers;
using PrinzipMonitorService.DAL.Repositories.UserRepository;
using System;

namespace PrinzipMonitorService.PLL.Controllers
{
    public class SubscribeController : Controller
    {

        IUserRepository userRepository;
        IFlatRepository flatRepository;
        public SubscribeController(IUserRepository setUserRepository, IFlatRepository setFlatRepository)
        {
            userRepository = setUserRepository;
            flatRepository = setFlatRepository;
        }

        /// <summary>
        /// Метод, который возвращает актуальные цены на квартиры, для которых выполнена подписка 
        /// </summary>
        /// <returns></returns>
        [Route("Flats")]
        [HttpGet]
        public List<Flat> GetFlats()
        {
            return flatRepository.GetAll().ToList();
        }

        /// <summary>
        /// Метод, который возвращает всех пользователей, которые выполняли подписки; 
        /// </summary>
        /// <returns></returns>
        [Route("Users")]
        [HttpGet]
        public List<User> GetUsers()
        {
            return userRepository.GetAll().ToList();
        }

        /// <summary>
        /// Принимает эмейл и ссылку на квартиру, добавляет подписку
        /// </summary>
        /// <param name="email"></param>
        /// <param name="url"></param>
        [Route("AddSubscription")]
        [HttpPost]
        public void AddSubscription(string email, string url)
        {
            var user = userRepository.Get(email);

            if (user == null)
            {
                user = new User() { Email = email };

                userRepository.Create(user);
            }

            var flat = flatRepository.Get(url);

            if (flat == null)
            {
                flat = new Flat() { Url = url, LastPrice = 0 };

                flatRepository.Create(flat);
            }

            user.Subscriptions.Add(flat);
            flat.Observers.Add(user);

            userRepository.Update(user);
            flatRepository.Update(flat);

            Console.WriteLine($"Подписка добавлена: {email} - {url}");
        }

        /// <summary>
        /// Принимает на вход эмейл, возвращает квартиры, на которые подписанн эмейл
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("GetSubscriptionsByEmail")]
        [HttpGet]
        public List<Flat> GetSubscriptionsByEmail(string email)
        {
            var user = userRepository.Get(email);

            return user.Subscriptions;
        }

        /// <summary>
        /// Проверяет актуальные цены всех квартир, на которые есть подписка
        /// </summary>
        [Route("CheckPrices")]
        [HttpGet]
        public void CheckPrices()
        {
            var flats = flatRepository.GetAll().ToList();

            if (flats != null)
            {
                foreach (var flat in flats)
                {
                    int? newPrice = new PriceChecker(flat.Url).GetPrice();

                    if(!(newPrice is null) && newPrice != flat.LastPrice)
                    {
                        int? oldPrice = flat.LastPrice;
                        flat.LastPrice = newPrice;
                        flatRepository.Update(flat);

                        foreach (var user in flat.Observers)
                        {
                            EmailService.SendEmail(user.Email, oldPrice, newPrice);
                            Console.WriteLine($"Сообщение {user.Email} отправлено"); // логгирование в консоль
                        }
                    }
                }
            }
            else
            {
                // Exception("На данную квартиру нет подписок");
            }
        }
    }
}
