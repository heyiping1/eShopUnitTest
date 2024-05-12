using System.Runtime.Intrinsics.X86;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using ApplicationCore.RepositoryContracts;
using EcommerceAPI.Controllers;
using Infrastructure.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerceApplication.UnitTest;

[TestClass]
public class CategoryControllerUnitTest
{
    private CategoryController _sut;
    private Mock<ICategoryServiceAsync> _mockCategoryServiceAsync;

    [TestInitialize]
    public void Setup()
    {
        //Arrange
        _mockCategoryServiceAsync = new Mock<ICategoryServiceAsync>();
        _sut = new CategoryController(_mockCategoryServiceAsync.Object);
    }

    [TestMethod]
    public async Task TestOf_Get_ReturnsOkResult()
    {
        // Arrange
        var categories = new List<CategoryResponseModel>
            {
                new CategoryResponseModel { Id = 1, CategoryName = "Electronics" }
            };
        _mockCategoryServiceAsync.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _sut.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
    }

    [TestMethod]
    public async Task TestOf_Get_ReturnsListOfCategories()
    {
        // Arrange
        var categories = new List<CategoryResponseModel>
            {
                new CategoryResponseModel { Id = 1, CategoryName = "Electronics" },
                new CategoryResponseModel { Id = 2, CategoryName = "Clothing" },
                new CategoryResponseModel { Id = 3, CategoryName = "Books" }
            };
        _mockCategoryServiceAsync.Setup(x => x.GetAllCategoriesAsync()).ReturnsAsync(categories);

        // Act
        var result = await _sut.Get();

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(categories, okResult.Value);
    }

    [DataTestMethod]
    [DataRow("Shoes")]
    [DataRow("Outdoors")]
    public async Task TestOf_Post_ReturnsOkResult(string categoryName)
    {
        // Arrange
        var categoryRequestModel = new CategoryRequestModel { CategoryName = "categoryName" };
        _mockCategoryServiceAsync.Setup(x => x.InsertCategoryAsync(categoryRequestModel)).ReturnsAsync(1);

        // Act
        var result = await _sut.Post(categoryRequestModel);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(1, okResult.Value);
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(null)]
    public async Task TestOf_Post_ReturnsBadRequestResult(string categoryName)
    {
        // Arrange
        var categoryRequestModel = new CategoryRequestModel { CategoryName = categoryName };
        _mockCategoryServiceAsync.Setup(x => x.InsertCategoryAsync(categoryRequestModel)).ThrowsAsync(new ArgumentException());

        // Act
        var result = await _sut.Post(categoryRequestModel);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.AreEqual("Arguments are not valid.", badRequestResult.Value);
    }

    [DataTestMethod]
    [DataRow("Shoes", 1)]
    [DataRow("Outdoors", 2)]
    public async Task TestOf_Put_ReturnsOkResult(string categoryName, int id)
    {
        // Arrange
        var categoryRequestModel = new CategoryRequestModel { CategoryName = categoryName };
        _mockCategoryServiceAsync.Setup(x => x.UpdateCategoryAsync(categoryRequestModel, id)).ReturnsAsync(1);

        // Act
        var result = await _sut.Put(categoryRequestModel, id);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(1, okResult.Value);
    }


    [DataTestMethod]
    [DataRow("Shoes", 100)]
    [DataRow("", 2)]
    public async Task TestOf_Put_ReturnsBadRequestResult(string categoryName, int id)
    {
        // Arrange
        var categoryRequestModel = new CategoryRequestModel { CategoryName = categoryName };
        _mockCategoryServiceAsync.Setup(x => x.UpdateCategoryAsync(categoryRequestModel, id)).ThrowsAsync(new ArgumentException());

        // Act
        var result = await _sut.Put(categoryRequestModel, id);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.AreEqual("Arguments are not valid.", badRequestResult.Value);
    }

    [DataTestMethod]
    [DataRow(1)]
    [DataRow(2)]
    public async Task TestOf_Delete_ReturnsOkResult(int id)
    {
        // Arrange
        _mockCategoryServiceAsync.Setup(x => x.DeleteCategoryAsync(id)).ReturnsAsync(1);

        // Act
        var result = await _sut.Delete(id);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        Assert.AreEqual(1, okResult.Value);
    }

    [DataTestMethod]
    [DataRow(-1)]
    [DataRow(999)]
    public async Task TestOf_Delete_ReturnsBadRequestResult(int id)
    {
        // Arrange
        _mockCategoryServiceAsync.Setup(x => x.DeleteCategoryAsync(id)).ThrowsAsync(new Exception());

        // Act
        var result = await _sut.Delete(id);

        // Assert
        var badRequestResult = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequestResult);
        Assert.AreEqual(400, badRequestResult.StatusCode);
        Assert.AreEqual($"Category with Id {id} not found", badRequestResult.Value);
    }
}