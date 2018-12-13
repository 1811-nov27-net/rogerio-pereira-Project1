using Project0.Library.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Project0.Tests.Library.Model
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

            Assert.Equal(12, o.Pizzas.Count);
        }

        [Fact]
        public void CannotAdd13Pizzas()
        {
            Order o = new Order();

            for (int i=0; i<13; i++)
            {
                Pizza p = new Pizza();
                p.Id = 1;
                p.Name = "Pizza";
                p.Price = 10;

                o.AddPizza(p);
            }

            Assert.Equal(12, o.Pizzas.Count);
        }


        [Fact]
        public void ValueCannotBeMoreThan500()
        {
            Order o = new Order();

            for (int i = 0; i < 12; i++)
            {
                Pizza p = new Pizza();
                p.Id = 1;
                p.Name = "Pizza";
                p.Price = 50;

                o.AddPizza(p);
            }

            Assert.Equal(10, o.Pizzas.Count);
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
        public void checkTimeBeforeTwoHours(int hours, int minutes, bool assert)
        {
            DateTime time = DateTime.Now;
            time = time.AddHours(-hours).AddMinutes(-minutes);

            Order o = new Order();

            bool canOrder = o.canOrderFromSameAddress(time);

            Assert.Equal(assert, canOrder);
        }
    }
}
