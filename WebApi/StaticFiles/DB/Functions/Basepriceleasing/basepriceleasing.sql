---DROP FUNCTION IF EXISTS public.fn_get_modelbaseprice_paginated(integer, integer);

CREATE OR REPLACE FUNCTION public.fn_get_modelbaseprice_paginated(
    p_page_number integer,
    p_page_size integer)
RETURNS TABLE(
    brandname varchar,
    series varchar,
    modelrange varchar,
    modelcode varchar,
    modeldescription varchar,
    modelbaseprice double precision,
    calculationtypevalue double precision,
    validfrom timestamptz,
    validto timestamptz,
    status varchar,
    approvalstatus varchar,
    discounts text,
    margins text,
    leasingrates text,
    leasingfactor text
)
LANGUAGE plpgsql
AS $BODY$

DECLARE
    v_offset INT;

BEGIN

    v_offset := (p_page_number - 1) * p_page_size;

    RETURN QUERY
    SELECT
        b."BrandName",
        s."Series",
        mr."ModelRange",
        mbd."ModelCode",
        md."ModelDescription",
        mpd."ModelBasePrice",
        lpc."CalculationTypeValue",

        lpc."LPC_ValidFrom",
        lpc."LPC_ValidTo",

        CASE
            WHEN lpc."LPC_ValidFrom" IS NULL AND mpd."ModelBasePrice" IS NOT NULL
            THEN 'New'
            ELSE COALESCE(lpc."Status",'New')
        END AS status,

        lpc."ApprovalStatus",

        -- Discounts
        COALESCE(
            jsonb_agg(DISTINCT jsonb_build_object(
                'MILEAGE', ml."MileageValue",
                'DISCOUNT', lcr."LeasingDiscount"
            )) FILTER (WHERE lcr."LeasingDiscount" IS NOT NULL),
            '[]'::jsonb
        )::text AS discounts,

        -- Margins
        COALESCE(
            jsonb_agg(DISTINCT jsonb_build_object(
                'MILEAGE', ml."MileageValue",
                'MARGIN', lcr."Margin"
            )) FILTER (WHERE lcr."Margin" IS NOT NULL),
            '[]'::jsonb
        )::text AS margins,

        -- Leasing Rates
        COALESCE(
            jsonb_agg(DISTINCT jsonb_build_object(
                'MILEAGE', ml."MileageValue",
                'LEASINGRATE', lcr."LeasingRate"
            )) FILTER (WHERE lcr."LeasingRate" IS NOT NULL),
            '[]'::jsonb
        )::text AS leasingrates,

        -- Leasing Factor
        COALESCE(
            jsonb_agg(DISTINCT jsonb_build_object(
                'MILEAGE', ml."MileageValue",
                'LEASINGFACTOR', lcr."LeasingFactor"
            )) FILTER (WHERE lcr."LeasingFactor" IS NOT NULL),
            '[]'::jsonb
        )::text AS leasingfactor

    FROM public."MD#ModelBaseData.MBD" mbd

    LEFT JOIN public."MD#Brand" b
        ON mbd."BrandID" = b."ID"

    LEFT JOIN public."MD#Series" s
        ON mbd."SeriesID" = s."ID"

    LEFT JOIN public."MD#ModelRange" mr
        ON mbd."ModelRangeID" = mr."ID"

    LEFT JOIN public."MD#ModelDescription" md
        ON mbd."ModelDescriptionID" = md."ID"

    LEFT JOIN public."MD#ModelPriceData.MPD" mpd
        ON mbd."ID" = mpd."ModelBaseDataID"
        AND mpd."MPD_ValidTo" IS NULL

    LEFT JOIN public."BP#LeasingPricingConditions.LPC" lpc
        ON lpc."ModelBaseDataID" = mbd."ID"

    LEFT JOIN public."BP#LeasingCalculationResults.LCR" lcr
        ON lcr."LeasingPricingConditionsID" = lpc."ID"

    LEFT JOIN public."MD#TermMileage" tm
        ON tm."ID" = lcr."TermMileageID"

    LEFT JOIN public."MD#Mileage" ml
        ON ml."ID" = tm."MileageID"

    WHERE mpd."ModelBasePrice" IS NOT NULL

    GROUP BY
        b."BrandName",
        s."Series",
        mr."ModelRange",
        mbd."ModelCode",
        md."ModelDescription",
        mpd."ModelBasePrice",
        lpc."CalculationTypeValue",
        lpc."LPC_ValidFrom",
        lpc."LPC_ValidTo",
        lpc."Status",
        lpc."ApprovalStatus"

    ORDER BY mbd."ModelCode"

    OFFSET v_offset
    LIMIT p_page_size;

END;
$BODY$;