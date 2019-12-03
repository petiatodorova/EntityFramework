namespace PetStore.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using PetStore.Data.Models;

    public class ToyOrderConfiguration : IEntityTypeConfiguration<ToyOrder>
    {
        public void Configure(EntityTypeBuilder<ToyOrder> toyOrder)
        {
            toyOrder
               .HasKey(fo => new { fo.ToyId, fo.OrderId });

            toyOrder
                .HasOne(fo => fo.Toy)
                .WithMany(o => o.Orders)
                .HasForeignKey(fo => fo.ToyId)
                .OnDelete(DeleteBehavior.Restrict);

            toyOrder
               .HasOne(fo => fo.Order)
               .WithMany(o => o.Toys)
               .HasForeignKey(fo => fo.OrderId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
