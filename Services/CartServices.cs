using System.Diagnostics;
using AutoMapper;
using CarRental.DATA.DTOs;
using CarRental.Entities;
using CarRental.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Services
{
    public interface ICartService
    {
        Task<(CartDto? data,String? error)> GetMyCart(Guid userId);
        Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm);
        
        Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity);
    }
    
    public class CartService : ICartService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public CartService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public async Task<(CartDto? data, string? error)> GetMyCart(Guid userId)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId);
            
            cart.TotalPrice = (decimal)cart.CartProducts.Sum(x => x.Car.Price * x.Quantity);

            if (cart == null)
            {
                return (null, "Cart Not Found");
            }
            var cartDto = _mapper.Map<CartDto>(cart);
            return (cartDto, null);
        }
        public async Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm)
        {
            
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.CartProducts));
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    CartProducts = new List<CartOrder>()
                };
                await _repositoryWrapper.Cart.Add(cart);
            }
            
            // check if product is already in cart
            
            foreach (var cartOrder in cartForm.OrderCarForm)
            {
                var product = await _repositoryWrapper.Car.Get(x => x.Id == cartOrder.CarId);
                if (product == null)
                {
                    return (null, "Car Not Found");
                }
                
                var cartProductEntity = await _repositoryWrapper.CartOrder.Get(x =>
                    x.CartId == cart.Id && x.CarId == cartOrder.CarId);
                
                if (cartProductEntity == null) // add new product to cart
                {
                    var newCartProduct = new CartOrder()
                    {
                        CartId = cart.Id,
                        CarId = cartOrder.CarId,
                    };
                    await _repositoryWrapper.CartOrder.Add(newCartProduct);
                }
                else // update product quantity
                {
                    return (null, "المنتج موجود بالفعل في السلة");
                }
                
            }
            
            var result = await _repositoryWrapper.Cart.Update(cart);
            if (result == null)
            {
                return (null, "لا يمكن اضافة المنتجات الى السلة");
            }
            
            return ("تم اضافة المنتجات الى السلة", null);
            
            
        }
        public async Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.CartProducts));
            if (cart == null)
            {
                return (null, "لم يتم العثور على السلة");
            }
            
            var cartProduct = cart.CartProducts.FirstOrDefault(x => x.CarId == cartId);
            if (cartProduct == null)
            {
                return (null, "لم يتم العثور على المنتج في السلة");
            }
            
            if (cartProduct.Quantity > quantity)
            {
                cartProduct.Quantity -= quantity;
                await _repositoryWrapper.CartOrder.Update(cartProduct);
            }
            else
            {
                await _repositoryWrapper.CartOrder.Delete(cartProduct.Id);
            }
            
            return ("تم حذف المنتج من السلة", null);
        }
    }
}

public class PdfService
{
    public void GeneratePdfFromHtml(string html, string outputPdfPath)
    {
        // Save the HTML to a temporary file
        var tempHtmlPath = Path.GetTempFileName() + ".html";
        File.WriteAllText(tempHtmlPath, html);

        // Set up the Wkhtmltopdf process
        var startInfo = new ProcessStartInfo
        {
            FileName = "wkhtmltopdf",
            Arguments = $"{tempHtmlPath} {outputPdfPath}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Start the process
        using var process = Process.Start(startInfo);

        // Wait for the process to finish
        process.WaitForExit();

        // Clean up the temporary HTML file
        File.Delete(tempHtmlPath);
    }
}