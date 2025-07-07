
using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Aggregate;

public partial class UserAudit : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}