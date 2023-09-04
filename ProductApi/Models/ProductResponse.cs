﻿namespace ProductApi.Models
{
    public record ProductResponse(Guid Id,
        string Name,
        decimal Price,
        string CurrencyCode);
}