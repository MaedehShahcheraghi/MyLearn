using System.ComponentModel.DataAnnotations;

namespace MyLearn.DataLayer.Entities.Order
{
    public class UserDiscount
    {
        [Key]
        public int UD_Id { get; set; }
        public int UserId { get; set; }
        public int DiscountId { get; set; }

        #region Relations
        public User.User User { get; set; }
        public Discount Discount { get; set; }
        #endregion
    }
}