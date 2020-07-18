using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Cart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly ILogger<CartController> _logger;

        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("AddToCart")]
        public CartResponse AddToCart([FromBody]AddToCarts _cartRequest)
        {

            int quantity = CheckQuantity(_cartRequest.ItemId);


            switch(quantity)
            {
                case -1:
                    return new CartResponse()
                    {
                        Message = "There is No Such Item",
                        OperationSuccess = false
                    };
                break;

                case 0:
                    return new CartResponse()
                    {
                        Message = "Item is currently out of stock",
                        OperationSuccess = false
                    };
                    break;

                default:

                    var item = new Models.Item
                    {
                        ItemId = _cartRequest.ItemId,
                        Quantity = quantity >= _cartRequest.Quantity ? _cartRequest.Quantity : quantity,
                    };
                    _cart.CartItems.Add(item);

                    return new CartResponse()
                    {
                        Message = $"Item with quantity of {item.Quantity} is successfully added to your cart",
                        OperationSuccess = false
                    };
                    break;
            }      
        }



        private int CheckQuantity(long ItemId)
        {
            //Check From Redis
            long modulus = ItemId % 4;

            switch (modulus)
            {
                case 0:
                    return 0;
                    break;
                case 1:
                    return -1;
                    break;
                case 2:
                    return int.MaxValue;
                    break;
                case 3:
                    return 2;
                    break;
                default:
                    return -1;
                    break;
            }
        }
    }
}
