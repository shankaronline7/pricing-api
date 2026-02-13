-- Insert data into PostgreSQL table "MD#FuelType"
INSERT INTO public."MD#FuelType" 
    ("ID", "FuelType", "Description", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn", "HybridVersion")
OVERRIDING SYSTEM VALUE
VALUES
    (1, 'B', 'BEZIN', 'q00000', '2024-08-08 00:00:00', NULL, NULL, NULL),
    (2, 'D', 'Diesel', 'q00000', '2024-08-08 00:00:00', NULL, NULL, NULL),
    (3, NULL, 'Hybrid EV', 'q00000', '2024-08-08 00:00:00', NULL, NULL, 'PHEV'),
    (4, NULL, 'Battery EV', 'q00000', '2024-08-08 00:00:00', NULL, NULL, 'BEV');
