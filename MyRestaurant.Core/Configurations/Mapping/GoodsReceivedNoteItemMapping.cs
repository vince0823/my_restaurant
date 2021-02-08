﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Core.Configurations.Base;
using MyRestaurant.Models;

namespace MyRestaurant.Core.Configurations.Mapping
{
    internal class GoodsReceivedNoteItemMapping : GoodsReceivedNoteItemMappingBase
    {
        public override void Configure(EntityTypeBuilder<GoodsReceivedNoteItem> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseIdentityColumn();
            builder.Property(e => e.GoodsReceivedNoteId).IsRequired();
            builder.Property(e => e.ItemId).IsRequired();
            builder.Property(e => e.ItemUnitPrice).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Quantity).IsRequired();
            builder.Property(e => e.Nbt).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Vat).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            builder.ToTable("GoodsReceivedNoteItems");

            builder.HasOne(d => d.GoodsReceivedNote)
                .WithMany(d => d.GoodsReceivedNoteItems)
                .HasForeignKey(d => d.GoodsReceivedNoteId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNoteItems_GoodsReceivedNotes");

            builder.HasOne(d => d.Item)
                .WithMany(p => p.GoodsReceivedNoteItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientCascade)
                .HasConstraintName("FK_GoodsReceivedNoteItems_StockItems");

            base.Configure(builder);
        }
    }
}
