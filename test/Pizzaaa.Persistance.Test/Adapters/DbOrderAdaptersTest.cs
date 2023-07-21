using AutoMapper;
using Moq;
using Pizzaaa.BLL.Models.Exceptions;
using Pizzaaa.Persistance.Adapters;
using Pizzaaa.Persistance.Models;
using Pizzaaa.Persistance.Repositories.Interfaces;
using Pizzaaa.Persistance.Test.Mothers;

namespace Pizzaaa.Persistance.Test.Adapters;

public class DbOrderAdaptersTest
{
	private readonly Mock<IOrderRepository> _mockOrderRepository;
	private readonly Mock<IPizzaRepository> _mockPizzaRepository;
	private readonly Mock<IStoreRepository> _mockStoreRepository;
	private readonly Mock<IMapper> _mockMapper;

	private readonly DbOrderAdapters _adapter;

	public DbOrderAdaptersTest()
	{
		_mockOrderRepository = new Mock<IOrderRepository>();
		_mockPizzaRepository = new Mock<IPizzaRepository>();
		_mockStoreRepository = new Mock<IStoreRepository>();
		_mockMapper = new Mock<IMapper>();

		_adapter = new DbOrderAdapters(_mockOrderRepository.Object, _mockPizzaRepository.Object, _mockStoreRepository.Object, _mockMapper.Object);
	}

	[Fact]
	public async Task FindTodayOrders_EverythingGoesWell_ReturnsTodayOrders()
	{
		List<Order> ordersFromRepo = new() { new() { OrderUser = "tim" } };
		List<BLL.Models.Order> mappedOrders = new() { new() { OrderUser = "tom" } };

		_mockOrderRepository.Setup(mock => mock.FindTodayOrders()).ReturnsAsync(ordersFromRepo);
		_mockMapper.Setup(mock => mock.Map<List<BLL.Models.Order>>(ordersFromRepo)).Returns(mappedOrders);

		List<BLL.Models.Order>? result = await _adapter.FindTodayOrders();

		Assert.Equal(mappedOrders, result);
	}

	[Fact]
	public async Task Insert_OrderMissingPizzaId_ThrowsException()
	{
		BLL.Models.Order orderToInsert = OrderMother.ABllOrder();
		orderToInsert.PizzaId = 0;

		BllException exception = await Assert.ThrowsAsync<BllException>(
			() => _adapter.Insert(orderToInsert));

		Assert.NotNull(exception);
		Assert.NotNull(exception.Errors);
		Assert.Contains(exception.Errors, x => x.Description == "missing PizzaId");
	}

	[Fact]
	public async Task Insert_OrderMissingStoreId_ThrowsException()
	{
		BLL.Models.Order orderToInsert = OrderMother.ABllOrder();
		orderToInsert.StoreId = 0;

		BllException exception = await Assert.ThrowsAsync<BllException>(
			() => _adapter.Insert(orderToInsert));

		Assert.NotNull(exception);
		Assert.NotNull(exception.Errors);
		Assert.Contains(exception.Errors, x => x.Description == "missing StoreId");
	}

	[Fact]
	public async Task Insert_OrderMissingOdserUser_ThrowsException()
	{
		BLL.Models.Order orderToInsert = OrderMother.ABllOrder();
		orderToInsert.OrderUser = "";

		BllException exception = await Assert.ThrowsAsync<BllException>(
			() => _adapter.Insert(orderToInsert));

		Assert.NotNull(exception);
		Assert.NotNull(exception.Errors);
		Assert.Contains(exception.Errors, x => x.Description == "missing OrderUser");
	}

	[Fact]
	public async Task Insert_ValidInput_InsertOrderAndReturnsItCompleted()
	{
		BLL.Models.Order orderToInsert = OrderMother.ABllOrder();
		Order mappedEntityOrder = new()
		{
			OrderUser = "tom",
			StoreId = orderToInsert.StoreId,
			PizzaId = orderToInsert.PizzaId
		};
		BLL.Models.Order mappedBllOrder = new() { OrderUser = "smith" };
		Pizza pizzaFromRepo = new() { Name = "pepperoni" };
		Store storeFromRepo = new() { Name = "domino" };
		Order? orderAfterInsert = null;

		_mockMapper.Setup(mock => mock.Map<Order>(orderToInsert)).Returns(mappedEntityOrder);
		_mockMapper.Setup(mock => mock.Map<BLL.Models.Order>(It.IsAny<Order>()))
			.Callback<object>(input => orderAfterInsert = (Order)input)
			.Returns(mappedBllOrder);
		_mockPizzaRepository.Setup(mock => mock.FindById(orderToInsert.PizzaId)).ReturnsAsync(pizzaFromRepo);
		_mockStoreRepository.Setup(mock => mock.FindById(orderToInsert.StoreId)).ReturnsAsync(storeFromRepo);

		BLL.Models.Order result = await _adapter.Insert(orderToInsert);

		Assert.NotNull(result);
		Assert.NotNull(orderAfterInsert);
		Assert.Equal(pizzaFromRepo, orderAfterInsert.Pizza);
		Assert.Equal(storeFromRepo, orderAfterInsert.Store);
	}

	[Fact]
	public async Task Delete_EverythingGoesWell_CallsDeleteMethod()
	{
		BLL.Models.Order orderToInsert = OrderMother.ABllOrder();
		orderToInsert.ID = 55;

		await _adapter.Delete(orderToInsert);

		_mockOrderRepository.Verify(v => v.Delete(55));
	}
}
