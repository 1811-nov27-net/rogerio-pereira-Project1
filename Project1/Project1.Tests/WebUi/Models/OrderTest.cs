using Project1.WebUi.Controllers.Exceptions;
using Project1.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Project1.Tests.WebUi.Models
{
    public class OrderTest
    {
        [Fact]
        public void CanAdd12Pizzas()
        {
            Order o = new Order();

            for (int i=0; i<12; i++)
            {
                Pizza p = new Pizza();
                p.Id = 1;
                p.Name = "Pizza";
                p.Price = 10;

                o.AddPizza(p);
            }

            Assert.Equal(12, o.OrderPizzas.Count);
        }

        [Fact]
        public void CannotAdd13Pizzas()
        {
            Order o = new Order();

            for (int i=0; i<12; i++)
            {
                Pizza p = new Pizza();
                p.Id = 1;
                p.Name = "Pizza";
                p.Price = 10;

                o.AddPizza(p);
            }

            //Pizza 13
            Pizza p13 = new Pizza();
            p13.Id = 1;
            p13.Name = "Pizza";
            p13.Price = 10;

            Assert.Throws<MaximumQuantityException>(() => o.AddPizza(p13));
            Assert.Equal(12, o.OrderPizzas.Count);
        }


        [Fact]
        public void ValueCannotBeMoreThan500()
        {
            Order o = new Order();

            for (int i = 0; i < 10; i++)
            {
                Pizza p = new Pizza();
                p.Id = 1;
                p.Name = "Pizza";
                p.Price = 50;

                o.AddPizza(p);
            }

            //Pizza 11
            Pizza p11 = new Pizza();
            p11.Id = 1;
            p11.Name = "Pizza";
            p11.Price = 10;

            Assert.Throws<MaximumAmountException>(() => o.AddPizza(p11));
            Assert.Equal(10, o.OrderPizzas.Count);
            Assert.Equal(500, o.Value);
        }

        [Theory]
        [InlineData(0, 1, false)]
        [InlineData(0, 15, false)]
        [InlineData(0, 30, false)]
        [InlineData(0, 45, false)]
        [InlineData(1, 0, false)]
        [InlineData(1, 15, false)]
        [InlineData(1, 30, false)]
        [InlineData(1, 45, false)]
        [InlineData(2, 0, true)]
        [InlineData(2, 1, true)]
        [InlineData(3, 0, true)]
        public void checkTimeBeforeTwoHours(int hours, int minutes, bool pass)
        {
            DateTime time = DateTime.Now;
            time = time.AddHours(-hours).AddMinutes(-minutes);

            Order o = new Order();

            if (pass == true)
            {
                bool canOrder = o.canOrderFromSameAddress(time);

                Assert.Equal(pass, canOrder);
            }
            else
                Assert.Throws<SamePlaceException>(() => o.canOrderFromSameAddress(time));
        }
    }
}
