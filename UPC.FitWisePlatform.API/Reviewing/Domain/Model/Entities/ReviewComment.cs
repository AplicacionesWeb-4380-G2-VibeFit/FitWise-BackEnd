using System;

namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Entities;

public class ReviewComment
{
    public int Id { get; private set; }
    public int ReviewId { get; private set; }
    public int UserId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Constructor por defecto (requerido por EF Core)
    public ReviewComment()
    {
        UserId = 0;
        Content = string.Empty;
        CreatedAt = DateTime.UtcNow;
    }

    // Constructor completo para creación de comentarios
    public ReviewComment(int reviewId, int userId, string content)
    {
        ReviewId = reviewId;
        UserId = userId;
        Content = content;
        CreatedAt = DateTime.UtcNow;
    }

    // Método para actualizar el contenido del comentario
    public void Update(string content)
    {
        Content = content;
    }
}