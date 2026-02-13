INSERT INTO public."MD#TermMileage" 
    ("TermID", "MileageID", "CreatedBy", "CreatedOn", "LastModifiedBy", "LastModifiedOn")
SELECT 
    t."ID" AS "TermID",
    m."ID" AS "MileageID",
    'q00000' AS "CreatedBy",
    NOW() AS "CreatedOn",
    NULL AS "LastModifiedBy",
    NULL AS "LastModifiedOn"
FROM 
    public."MD#Term" t
CROSS JOIN 
    public."MD#Mileage" m
WHERE NOT EXISTS (
    SELECT 1 
    FROM public."MD#TermMileage" tm
    WHERE tm."TermID" = t."ID" 
      AND tm."MileageID" = m."ID"
);
