﻿using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyRestaurant.Api.Controllers.V1;
using MyRestaurant.Api.Tests.Controllers.V1.Fixtures;
using MyRestaurant.Business.Dtos.V1;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MyRestaurant.Api.Tests.Controllers.V1
{
    public class PurchaseOrderItemControllerTest : IClassFixture<PurchaseOrderItemControllerFixture>
    {
        private readonly PurchaseOrderItemControllerFixture _fixture;

        public PurchaseOrderItemControllerTest(PurchaseOrderItemControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetPurchaseOrderItems_Returns_OkObjectResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderItemRepository.Setup(x => x.GetPurchaseOrderItemsAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.PurchaseOrderItems.Where(d => d.PurchaseOrderId == 101));

            var controller = new PurchaseOrderItemController(_fixture.MockPurchaseOrderItemRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrderItems(101);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var items = okResult.Value.Should().BeAssignableTo<IEnumerable<GetPurchaseOrderItemDto>>().Subject;

            okResult.StatusCode.Should().Be(200);
            items.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetPurchaseOrderItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderItemRepository.Setup(x => x.GetPurchaseOrderItemAsync(It.IsAny<long>()))
                .ReturnsAsync(_fixture.PurchaseOrderItems.Single(d => d.Id == id));

            var controller = new PurchaseOrderItemController(_fixture.MockPurchaseOrderItemRepository.Object);

            //Act
            var result = await controller.GetPurchaseOrderItem(id);

            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var item = okResult.Value.Should().BeAssignableTo<GetPurchaseOrderItemDto>().Subject;

            okResult.StatusCode.Should().Be(200);
            item.Id.Should().Be(id);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Salt");
            item.ItemUnit.Should().Be(5);
        }

        [Fact]
        public async Task CreatePurchaseOrderItem_Returns_CreatedAtRouteResult()
        {
            //Arrange
            _fixture.MockPurchaseOrderItemRepository.Setup(x => x.CreatePurchaseOrderItemAsync(It.IsAny<CreatePurchaseOrderItemDto>()))
                .ReturnsAsync(_fixture.CreatePurchaseOrderItemDtoResult);

            var controller = new PurchaseOrderItemController(_fixture.MockPurchaseOrderItemRepository.Object);

            //Act
            var result = await controller.CreatePurchaseOrderItem(_fixture.ValidCreatePurchaseOrderItemDto, _fixture.ApiVersion);

            //Assert
            var objectResult = result.Should().BeOfType<CreatedAtRouteResult>().Subject;
            objectResult.StatusCode.Should().Be(201);
            objectResult.RouteValues!["id"].Should().Be(5);
            objectResult.RouteValues["version"].Should().Be($"{_fixture.ApiVersion}");

            var item = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderItemDto>().Subject;
            item.Id.Should().Be(5);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Rice");
            item.UnitOfMeasureCode.Should().Be("kg");
            item.ItemUnit.Should().Be(10);
        }

        [Fact]
        public async Task UpdatePurchaseOrderItem_Returns_OkObjectResult()
        {
            //Arrange
            var id = 1;
            _fixture.MockPurchaseOrderItemRepository.Setup(x => x.UpdatePurchaseOrderItemAsync(It.IsAny<long>(), It.IsAny<EditPurchaseOrderItemDto>()))
                .ReturnsAsync(_fixture.EditPurchaseOrderItemDtoResult);

            var controller = new PurchaseOrderItemController(_fixture.MockPurchaseOrderItemRepository.Object);

            //Act
            var result = await controller.UpdatePurchaseOrderItem(id, _fixture.ValidEditPurchaseOrderItemDto);

            //Assert
            var objectResult = result.Should().BeOfType<OkObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(200);

            var item = objectResult.Value.Should().BeAssignableTo<GetPurchaseOrderItemDto>().Subject;
            item.Id.Should().Be(1);
            item.ItemTypeName.Should().Be("Grocery");
            item.ItemName.Should().Be("Chilli Powder");
            item.UnitOfMeasureCode.Should().Be("g");
            item.ItemUnit.Should().Be(250);
        }

        [Fact]
        public async Task DeletePurchaseOrderItem_Returns_NoContentResult()
        {
            //Arrange
            var id = 2;
            _fixture.MockPurchaseOrderItemRepository.Setup(x => x.DeletePurchaseOrderItemAsync(It.IsAny<long>()));

            var controller = new PurchaseOrderItemController(_fixture.MockPurchaseOrderItemRepository.Object);

            //Act
            var result = await controller.DeletePurchaseOrderItem(id);

            //Assert
            var objectResult = result.Should().BeOfType<NoContentResult>().Subject;
            objectResult.StatusCode.Should().Be(204);
        }
    }
}
