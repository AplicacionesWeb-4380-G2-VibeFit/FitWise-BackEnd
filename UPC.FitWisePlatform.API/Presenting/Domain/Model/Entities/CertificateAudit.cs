using System.ComponentModel.DataAnnotations.Schema;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts;

namespace UPC.FitWisePlatform.API.Presenting.Domain.Model.Entities;

public partial class CertificateAudit : IEntityWithCreatedUpdatedDate
{
    [Column("CreatedAt")] public DateTimeOffset? CreatedDate { get; set; }
    [Column("UpdatedAt")] public DateTimeOffset? UpdatedDate { get; set; }
}