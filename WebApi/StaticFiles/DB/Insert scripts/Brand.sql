-- Insert data into PostgreSQL table "MD#Brand"
INSERT INTO public."MD#Brand" ("ID", "BrandName", "Description", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn")
OVERRIDING SYSTEM VALUE
VALUES
    (1, 'MINI', 'MINI', 'q000000', '2024-08-08 00:00:00', NULL, NULL),
    (2, 'BMWi', 'BMWi', 'q000000', '2024-08-08 00:00:00', NULL, NULL),
    (3, 'BMW', 'BMW', 'q000000', '2024-08-08 00:00:00', NULL, NULL),
    (5, 'BMw-xyc', 'xyc-engine', 'shankar', '2025-09-11 12:19:12', NULL, NULL);
