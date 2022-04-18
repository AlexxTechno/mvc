using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mvc.Services;

namespace mvc.Controllers
{
    public class MailerController : Controller
    {
        private readonly string email = "aalexx.007@mail.ru";

        //--- order ---
        private readonly string subjectOrder = " с сайта: cleal.ru";
        private string messageOrder = "Заказ c сайта: cleal.ru.";

        //--- contacts ---
        private readonly string subjectContact = "Сообщение с сайта: cleal.ru";
        private string messageContact = "Письмо отправлено c сайта: cleal.ru. Раздел контакты.";

        //--- subscribe ---
        private readonly string subjectSubscribe = "Подписка на рассылку с сайта: cleal.ru";
        private string messageSubscribe = "Письмо отправлено из футера сайта: cleal.ru. Подписка на рассылку, email клиента: ";

        [Route("/mailer/order")]
        public async Task<IActionResult> Order()
        {
            string name = Request.Form.ElementAt(3).Value;
            string phone = Request.Form.ElementAt(4).Value;

            if (Validation.IsTelValid(phone))
            {
                ViewData["error"] = "";
                ViewData["id"] = Request.Form.ElementAt(0).Value;
                ViewData["sku"] = Request.Form.ElementAt(1).Value;
                ViewData["title"] = Request.Form.ElementAt(2).Value;
                ViewData["name"] = name;
                ViewData["phone"] = phone;
                ViewData["note"] = Request.Form.ElementAt(5).Value;

                messageOrder = messageOrder + "| имя = " + ViewData["name"] +
                                              "| тел.= " + ViewData["phone"] +
                                              "| сообщение = " + ViewData["note"] +
                                              "| id товара = " + ViewData["id"] +
                                              "| sku товара = " + ViewData["sku"] +
                                              "| title товара = " + ViewData["title"];

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(email, subjectOrder, messageOrder);
                return View();
            }
            else
            {
                ViewData["error"] = "некорректный телефон";
                ViewData["name"] = name;
                return View();
            }
        }

        [Route("/mailer/contact")]
        public async Task<IActionResult> Contact()
        {
            string name = Request.Form.ElementAt(0).Value;
            string phone = Request.Form.ElementAt(1).Value;

            if (Validation.IsTelValid(phone))
            {
                ViewData["error"] = "";
                ViewData["name"] = name;
                ViewData["phone"] = phone;
                ViewData["note"] = Request.Form.ElementAt(2).Value;

                messageContact = messageContact + "| имя = " + name +
                                                  "| тел.= " + phone +
                                                  "| сообщение = " + ViewData["note"];

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(email, subjectContact, messageContact);
                return View();
            }
            else
            {
                ViewData["error"] = "некорректный телефон";
                ViewData["name"] = name;
                return View();
            }
        }

        [Route("/mailer/subscribe")]
        public async Task<IActionResult> Subscribe()
        {
            string emailForm = Request.Form.FirstOrDefault().Value.ToString();
            if(Validation.IsEmailValid(emailForm))
            {
                ViewData["error"] = "";
                ViewData["email"] = emailForm;
                messageSubscribe += emailForm;

                EmailService emailService = new EmailService();
                await emailService.SendEmailAsync(email, subjectSubscribe, messageSubscribe);
                return View();
            }
            else
            {
                ViewData["error"] = "некорректный email";
                return View();
            }            
        }
    }
}
