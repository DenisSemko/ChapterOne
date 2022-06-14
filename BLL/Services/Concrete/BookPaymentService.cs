using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Helpers;
using CIL.Models;
using DAL;
using DAL.Repository.Abstract;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class BookPaymentService : IBookPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationContext _applicationContext;
        private readonly EmailConfiguration _emailConfig;

        public BookPaymentService(IUnitOfWork unitOfWork, ApplicationContext applicationContext, EmailConfiguration emailConfig)
        {
            this._unitOfWork = unitOfWork;
            this._applicationContext = applicationContext;
            this._emailConfig = emailConfig;
        }

        public async Task<IEnumerable<BookPayment>> GetByUserId(Guid userId)
        {
            var result = await _unitOfWork.BookPaymentRepository.GetByUserId(userId);
            return result;
        }

        public async Task<BookPayment> Add(BookPaymentDto bookDto)
        {
            try
            {
                var user = await _applicationContext.Users.Where(x => x.Id == bookDto.User).FirstOrDefaultAsync();
                var book = await _applicationContext.Book.Where(x => x.Id == bookDto.Book).FirstOrDefaultAsync();
                var bookPayment = new BookPayment
                {
                    Id = Guid.NewGuid(),
                    User = user,
                    Book = book,
                    Type = bookDto.Type
                };
                await _unitOfWork.BookPaymentRepository.Add(bookPayment);
                return bookPayment;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<BookPayment>> DeleteByUserId(Guid id)
        {
            var result = await _unitOfWork.BookPaymentRepository.DeleteByUserId(id);
            return result;
        }

        public async Task<bool> SendBook(Guid userId)
        {
            var user = await _unitOfWork.UserRepository.GetById(userId);
            var bookPayments = await GetByUserId(userId);
            var paths = new List<string>();

            foreach(var bookPayment in bookPayments)
            {
                if(bookPayment.Type == "Web")
                {
                    var file = bookPayment.Book.BookWebFile.Substring(16);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files", file);
                    paths.Add(filePath);
                } else if (bookPayment.Type == "Audio")
                {
                    var file = bookPayment.Book.BookAudioFile.Substring(16);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources\Files", file);
                    paths.Add(filePath);
                }
            }
            
            if (paths.Count > 0)
            {
                SendBookFile(user, "pay-book-success.html", paths);
                var removeBookPayments = await DeleteByUserId(userId);
                if(removeBookPayments != null)
                {
                    return true;
                }
            }
            return false;
        }

        private async void SendBookFile(User user, string name, List<string> filenames)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("ChapterOne", _emailConfig.From));
            emailMessage.To.Add(new MailboxAddress(user.UserName, user.Email));
            emailMessage.Subject = "ChapterOne - Book";

            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), @"Resources", name);
            string EmailTemplateText = File.ReadAllText(FilePath);
            BodyBuilder emailBodyBuilder = new BodyBuilder();
            emailBodyBuilder.HtmlBody = EmailTemplateText;
            
            foreach(var filename in filenames)
            {
                emailBodyBuilder.Attachments.Add(filename);
            }
            emailMessage.Body = emailBodyBuilder.ToMessageBody();



            var client = new SmtpClient();
            await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
            //client.AuthenticationMechanisms.Remove("XOAUTH2");
            await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
            await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
