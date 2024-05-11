using System.Runtime.Intrinsics.X86;
using ApplicationCore.Entities;
using ApplicationCore.Model.Request;
using ApplicationCore.Model.Response;
using ApplicationCore.RepositoryContracts;
using Infrastructure.Service;
using Moq;

namespace ECommerceApplication.UnitTest;

[TestClass]
public class CategoryServiceUnitTest
{
    private CategoryServiceAsync _sut;
    private Mock<ICategoryRepositoryAsync> _mockCategoryRepositoryAsync;

    [TestInitialize]
    public void Setup()
    {
        //Arrange
        _mockCategoryRepositoryAsync = new Mock<ICategoryRepositoryAsync>();
        _sut = new CategoryServiceAsync(_mockCategoryRepositoryAsync.Object);
    }
    
    [TestMethod]
    public async Task TestOf_GetAll_Categories_Returns_CategoryResponseModel()
    {
        //CategoryServiceAsync.cs ==> GetAllCategoriesAsync()
        //System Under Test: SUT

        List<Category> _categories = new List<Category>
        {
            new Category { Id = 1, CategoryName = "Electronics", },
            new Category { Id = 2, CategoryName = "Clothing" },
            new Category { Id = 3, CategoryName = "Books" },
            new Category { Id = 4, CategoryName = "Baby" },
            new Category { Id = 5, CategoryName = "Pets" }

        };
        
       //arrange
       _mockCategoryRepositoryAsync.Setup(x => x.GetAllAsync()).ReturnsAsync(_categories);
       
        //act
        var categories = await _sut.GetAllCategoriesAsync();
        
        //Assert
        Assert.IsNotNull(categories);
        Assert.IsInstanceOfType(categories, typeof(IEnumerable<CategoryResponseModel>));
        Assert.AreEqual(5, categories.Count());
    }

    [TestMethod]
    public async Task TestOf_DeleteCategory_Returns_Integer()
    {
        //arrange
        _mockCategoryRepositoryAsync.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Category{ Id = 1, CategoryName = "Books"});
        _mockCategoryRepositoryAsync.Setup(x => x.DeleteAsync(It.IsAny<int>()))
            .ReturnsAsync(1);
        
        //Act
        var y = await _sut.DeleteCategoryAsync(1);
        
        //Assert
        Assert.IsNotNull(y);
        Assert.AreEqual(1, y);
        _mockCategoryRepositoryAsync.Verify(x=>x.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [TestMethod]
    public async Task TestOf_DeleteCategory_Throws_Exception()
    {
        //Arrange
        int CatgeoryThatDoesNotExist = 999;
        Category category = null;

        _mockCategoryRepositoryAsync.Setup(x => x.GetByIdAsync(CatgeoryThatDoesNotExist))
            .ReturnsAsync(category);
        
        //Assert && Act
        await Assert.ThrowsExceptionAsync<Exception>(() =>
        {
            return _sut.DeleteCategoryAsync(CatgeoryThatDoesNotExist);
        });
    }

    [TestMethod]
    public async Task TestOf_GetCategoryById_ReturnsCategoryResponseModel()
    {
        //arrange
        var value = new Category { CategoryName = "books", Id = 1 };
        _mockCategoryRepositoryAsync.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(value);
        //act
        var x = await _sut.GetCategoryByIdAsync(1);
        //Assert 
        Assert.IsInstanceOfType(x, typeof(CategoryResponseModel), "type did not match");
        Assert.IsNotNull(x);
    }

    [TestMethod]
    public async Task TestOf_GetCategoryById_ReturnsNull()
    {
        Category category = null;
        _mockCategoryRepositoryAsync.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(category);
        var result = await _sut.GetCategoryByIdAsync(3);
        Assert.IsNull(result, "the value is not null");
        Assert.IsFalse(result != null);
    }

    // [TestMethod]
    // public async Task Testof_InsertCategory_ReturnsInteger()
    // {
    //     //Arrange
    //     _mockCategoryRepositoryAsync.Setup(x => x.InsertAsync(It.IsAny<Category>()))
    //         .ReturnsAsync(It.IsAny<int>());
    //     
    //     //Act
    //     CategoryRequestModel model = new CategoryRequestModel
    //     {
    //         CategoryName = "Books"
    //     };
    //     var x =_sut.InsertCategoryAsync(model);
    //     
    //     //Assert
    //     
    //     Assert.IsNotNull(x);
    //     Assert.IsNotInstanceOfType(x, typeof(int));
    // }
    
    [DataTestMethod]
    [DataRow("Shoes")]
    [DataRow("Outdoors")]
    public async Task Testof_InsertCategory_ReturnsInteger(string categoryName)
    {
        //Arrange
        _mockCategoryRepositoryAsync.Setup(x => x.InsertAsync(It.IsAny<Category>()))
            .ReturnsAsync(It.IsAny<int>());
        
        //Act
        CategoryRequestModel model = new CategoryRequestModel
        {
            CategoryName = categoryName
        };
        var x =_sut.InsertCategoryAsync(model);
        //Assert
        Assert.IsNotNull(x);
        Assert.IsNotInstanceOfType(x, typeof(int));
    }

    [DataTestMethod]
    [DataRow("")]
    [DataRow(null)]
    public async Task Testof_InsertCategory_ThrowsException(string categoryName)
    {
        CategoryRequestModel model = new CategoryRequestModel
        {
            CategoryName = categoryName
        };
        
        //Assert
        await Assert.ThrowsExceptionAsync<ArgumentException>(() =>
        {
            return _sut.InsertCategoryAsync(model);
        });
    }
    
}

