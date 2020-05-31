using Cloudbash.Domain.SeedWork;
using System;
using System.Linq.Expressions;

namespace Cloudbash.Domain.Carts
{
    public class ItemInCartSpec : SpecificationBase<CartItem>
    {
        readonly CartItem _item;

        public ItemInCartSpec(CartItem item)
        {
            this._item = item;
        }

        public override Expression<Func<CartItem, bool>> SpecExpression
        {
            get
            {
                return cartItem => cartItem.Id == this._item.Id;
            }
        }
    }
}