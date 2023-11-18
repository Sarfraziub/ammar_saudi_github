
--Insert Currency Data
INSERT [dbo].[Currencies] ([Id], [Name], [Code], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (1, N'Saudi Riyal', N'SAR', CAST(N'2023-04-03T10:14:11.9550485' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[Currencies] ([Id], [Name], [Code], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (2, N'USD', N'USD', CAST(N'2023-04-03T10:14:11.9550485' AS DateTime2), NULL, NULL, NULL, 1)


--Insert ExchangeRate Data
SET IDENTITY_INSERT [dbo].[ExchangeRates] ON 
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCurrencyId], [ToCurrencyId], [Rate], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (1, 1, 2, CAST(0.266667 AS Decimal(18, 6)), CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCurrencyId], [ToCurrencyId], [Rate], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (2, 2, 1, CAST(3.750000 AS Decimal(18, 6)), CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCurrencyId], [ToCurrencyId], [Rate], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (3, 1, 1, CAST(1.000000 AS Decimal(18, 6)), CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 1)
GO
INSERT [dbo].[ExchangeRates] ([Id], [FromCurrencyId], [ToCurrencyId], [Rate], [Created], [CreatedBy], [Updated], [UpdatedBy], [Active]) VALUES (4, 2, 2, CAST(1.000000 AS Decimal(18, 6)), CAST(N'2023-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[ExchangeRates] OFF
GO
