using OrderApi.Models;
using OrderApi.Validation;
using OrderApi.Exceptions;
using Xunit;

namespace OrderApi.Tests;

public class OrderValidatorTests
{
    private static Order BuildValidOrder()
    {
        Order order = new Order();
        order.CustomerName = "Quan Nguyen";
        order.TotalAmount = 25.5m;
        order.Items = new List<OrderItem>
        {
            new OrderItem { ProductName = "Keyboard", Quantity = 1, UnitPrice = 25.5m }
        };
        return order;
    }

    [Fact]
    public void Null_Order_Is_Invalid()
    {
        Assert.Throws<OrderValidationException>(() => OrderValidator.Validate(null));
    }

    [Fact]
    public void Empty_CustomerName_Is_Invalid()
    {
        Order order = BuildValidOrder();
        order.CustomerName = "   ";
        Assert.Throws<OrderValidationException>(() => OrderValidator.Validate(order));
    }

    [Fact]
    public void No_Items_Is_Invalid()
    {
        Order order = BuildValidOrder();
        order.Items = new List<OrderItem>();
        Assert.Throws<OrderValidationException>(() => OrderValidator.Validate(order));
    }

    [Fact]
    public void Zero_Quantity_Item_Is_Invalid()
    {
        Order order = BuildValidOrder();
        order.Items[0].Quantity = 0;
        Assert.Throws<OrderValidationException>(() => OrderValidator.Validate(order));
    }

    [Fact]
    public void Zero_Total_Is_Invalid()
    {
        Order order = BuildValidOrder();
        order.TotalAmount = 0m;
        Assert.Throws<OrderValidationException>(() => OrderValidator.Validate(order));
    }

    [Fact]
    public void Valid_Order_Passes()
    {
        Order order = BuildValidOrder();
        Exception? recorded = Record.Exception(() => OrderValidator.Validate(order));
        Assert.Null(recorded);
    }
}
