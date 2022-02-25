using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTMDTLibrary.DTO
{
    public class Cart
    {
        public int TotalItem { get; set; }
        public double TotalPrice { get; set; }
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
            TotalItem = 0;
            TotalPrice = 0;
        }
    }

    public class CartItem
    {
        public int BookId { get; set; }
        public string ImgUrl { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? PromotionPercent { get; set; }
        public string? PromotionAmount { get; set; }

    }

    public static class CartHelper
    {
        public static Cart CalculateCartTotal(List<CartItem> items)
        {
            Cart cart = new Cart();
            foreach (var item in items)
            {

                if (item.PromotionPercent == null && item.PromotionAmount == null)
                {
                    cart.TotalPrice += item.Price * item.Quantity;
                }
                else if (item.PromotionPercent != null)
                {
                    cart.TotalPrice += (item.Price - (item.Price * Double.Parse(item.PromotionPercent)) / 100) * item.Quantity;
                }
                else
                {
                    cart.TotalPrice += (item.Price - Double.Parse(item.PromotionAmount)) * item.Quantity;
                }
                cart.TotalItem += item.Quantity;
            }
            cart.Items = items;
            return cart;
        }
        public static Cart AddCartItem(CartItem cartItem, Cart cart)
        {
            if (cart.Items.Any(q => q.Title == cartItem.Title))
            {
                foreach (var item in cart.Items)
                {
                    if (item.Title == cartItem.Title)
                    {
                        item.Quantity += cartItem.Quantity;
                        break;
                    }
                }
            }
            else
            {
                cart.Items.Add(cartItem);
            }
            return cart;
        }
        public static Cart RemoveCartItem(CartItem cartItem, Cart cart)
        {

            foreach (var item in cart.Items)
            {
                if (item.Title == cartItem.Title)
                {
                    item.Quantity -= 1;
                    if (item.Quantity == 0)
                    {
                        cart.Items.Remove(item);
                    }
                    break;
                }
            }
            return cart;
        }
        public static Cart DeleteCartItem(CartItem cartItem, Cart cart)
        {
            foreach (var item in cart.Items)
            {
                if (item.Title == cartItem.Title)
                {
                    cart.Items.Remove(item);
                    break;
                }
            }
            return cart;
        }
    }
}
