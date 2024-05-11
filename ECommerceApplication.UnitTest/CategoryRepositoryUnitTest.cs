using ApplicationCore.Entities;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ECommerceApplication.UnitTest;

[TestClass]
public class CategoryRepositoryUnitTest
{
    private CategoryRepositoryAsync _sut;
    private Mock<EcommerceDbContext> _dbContextMock;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<EcommerceDbContext>()
            .UseInMemoryDatabase(databaseName: "testDatabase")
            .Options;
        _dbContextMock = new Mock<EcommerceDbContext>(options);
        
        //mocking DbSet
        var mockDbSet = new Mock<DbSet<Category>>();
        
        //Setting for Set<T>
        _dbContextMock.Setup(c => c.Set<Category>()).Returns(mockDbSet.Object);

        _sut = new CategoryRepositoryAsync(_dbContextMock.Object);
        
    }

    [TestMethod]
    public async Task GetByIdAsync_Returns_Category()
    {
        _dbContextMock.Setup(x => x.Set<Category>().FindAsync(1))
            .ReturnsAsync(new Category { Id = 1, CategoryName = "Books" });
        var x = _sut.GetByIdAsync(1);
        Assert.IsNotNull(x);
    }
    
}