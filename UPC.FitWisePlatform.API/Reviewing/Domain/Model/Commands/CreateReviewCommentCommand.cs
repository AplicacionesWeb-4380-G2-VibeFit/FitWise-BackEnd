﻿namespace UPC.FitWisePlatform.API.Reviewing.Domain.Model.Commands;

public record CreateReviewCommentCommand(int ReviewId, int UserId, string Content);