using BLL.Services.Abstract;
using BLL.Services.Concrete;
using CIL.Models;
using DAL.Repository.Abstract;
using DAL.Repository.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChapterOneTests
{
    [TestClass]
    public class APITests
    {
        private readonly UserService _userService;
        private readonly Mock<UnitOfWork> _unitOfWorkMock;

        public APITests()
        {
            _unitOfWorkMock = new Mock<UnitOfWork>();
           // _userService = new UserService(_unitOfWorkMock.Object);
        }

        [TestMethod]
        public async Task GetAllUsers_ShouldShowList_ReturnsList()
        {
            //Arrange
            var mockUser = new List<User>();
            _unitOfWorkMock.Setup(x => x.UserRepository.Get()).ReturnsAsync(mockUser);

            //Act
            var users = await _userService.Get();

            //Assert
            Assert.Equals(mockUser, users);
        }
    }
}
