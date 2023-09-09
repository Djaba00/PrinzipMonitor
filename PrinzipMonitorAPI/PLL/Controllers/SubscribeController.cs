using Microsoft.AspNetCore.Mvc;
using PrinzipMonitorService.BLL.Models;
using PrinzipMonitorService.DAL.Repositories.FlatRepository;
using PrinzipMonitorService.DAL.Repositories.UserRepository;
using PrinzipMonitorService.PLL.Services.PriceCheckerService;
using PrinzipMonitorService.PLL.Services.EmailService;
using PrinzipMonitorService.BLL.ViewModels;
using AutoMapper;

namespace PrinzipMonitorService.PLL.Controllers
{
    public class SubscribeController : Controller
    {

        IUserRepository _userRepository;
        IFlatRepository _flatRepository;

        IMapper _mapper;

        public SubscribeController(IUserRepository setUserRepository, IFlatRepository setFlatRepository, IMapper setMapper)
        {
            _userRepository = setUserRepository;
            _flatRepository = setFlatRepository;
            _mapper = setMapper;
        }

        /// <summary>
        /// Метод, который возвращает актуальные цены на квартиры, для которых выполнена подписка 
        /// </summary>
        /// <returns></returns>
        [Route("Flats")]
        [HttpGet]
        public async Task<List<FlatViewModel>> GetFlats()
        {
            var flats = await _flatRepository.GetAllAsync();
            var result = new List<FlatViewModel>();

            foreach (var flat in flats)
            {
                result.Add(_mapper.Map<FlatViewModel>(flat));
            }

            return result;
        }

        /// <summary>
        /// Метод, который возвращает всех пользователей, которые выполняли подписки; 
        /// </summary>
        /// <returns></returns>
        [Route("Users")]
        [HttpGet]
        public async Task<List<UserViewModel>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var result = new List<UserViewModel>();

            foreach (var user in users)
            {
                result.Add(_mapper.Map<UserViewModel>(user));
            }

            return result;
        }

        /// <summary>
        /// Принимает эмейл и ссылку на квартиру, добавляет подписку
        /// </summary>
        /// <param name="email"></param>
        /// <param name="url"></param>
        [Route("AddSubscription")]
        [HttpPost]
        public async Task AddSubscription(string email, string url)
        {
            var user = await _userRepository.GetAsync(email);

            if (user == null)
            {
                user = new User() { Email = email };

                await _userRepository.CreateAsync(user);
            }

            var flat = await _flatRepository.GetAsync(url);

            if (flat == null)
            {
                flat = new Flat() { Url = url, LastPrice = 0 };

                await _flatRepository.CreateAsync(flat);
            }

            user.Flats.Add(flat);
            flat.Users.Add(user);

            await _userRepository.UpdateAsync(user);
            await _flatRepository.UpdateAsync(flat);

            Console.WriteLine($"Подписка добавлена: {email} - {url}");
        }

        /// <summary>
        /// Принимает на вход эмейл, возвращает квартиры, на которые подписанн эмейл
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("GetSubscriptionsByEmail")]
        [HttpGet]
        public async Task<List<string>> GetSubscriptionsByEmail(string email)
        {
            var user = await _userRepository.GetAsync(email);

            var flats = user.Flats;

            var result = new List<string>();

            foreach (var flat in flats)
            {
                result.Add(_mapper.Map<FlatViewModel>(flat).Url);
            }

            return result;
        }

        /// <summary>
        /// Проверяет актуальные цены всех квартир, на которые есть подписка
        /// </summary>
        [Route("CheckPrices")]
        [HttpGet]
        public async Task CheckPrices()
        {
            var flats = await _flatRepository.GetAllAsync();

            if (flats != null)
            {
                foreach (var flat in flats)
                {
                    int? newPrice = new PriceChecker(flat.Url).GetPrice();

                    if(!(newPrice is null) && newPrice != flat.LastPrice)
                    {
                        int? oldPrice = flat.LastPrice;
                        flat.LastPrice = newPrice;
                        await _flatRepository.UpdateAsync(flat);

                        foreach (var user in flat.Users)
                        {
                            var result = await EmailService.SendEmailAsync(user.Email, oldPrice, newPrice);
                            if(result)
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
