SELECT TOP (1000) [Id]
      ,[Code]
      ,[Name]
      ,[RegionImageUrl]
  FROM [NZWalksDb].[dbo].[Regions]

  -- Insert random data into the Regions table
INSERT INTO [NZWalksDb].[dbo].[Regions] ([Id], [Code], [Name], [RegionImageUrl])
VALUES
  ('6F9619FF-8B86-D011-B42D-00C04FC964FF', 'AKL', 'Auckland', 'auckland.jpg'),
  ('7A0A3571-FA8A-45CE-A03A-29A661E0B5E7', 'WLG', 'Wellington', 'wellington.jpg'),
  ('C1EE39E9-477A-4F4B-B35D-4E14554D3F93', 'CHC', 'Christchurch', 'christchurch.jpg'),
  ('D73E6F3F-3AAE-4DD0-9D52-864FC05FCC9D', 'DUD', 'Dunedin', 'dunedin.jpg'),
  ('E5D4E6F2-852A-41F7-9B90-4B5E5E38F7C6', 'QTN', 'Queenstown', 'queenstown.jpg'),
  ('F8B36EF4-AC49-4ECC-9366-8A3E4F6E8DDE', 'NPL', 'Napier', 'napier.jpg');


  SELECT * FROM dbo.Regions;