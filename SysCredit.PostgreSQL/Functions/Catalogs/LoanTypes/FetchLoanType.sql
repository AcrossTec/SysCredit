CREATE OR REPLACE FUNCTION public."FetchLoanType"()
RETURNS SETOF "LoanType"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT * 
    FROM "public"."LoanType"
    WHERE "IsDelete" = FALSE;
END;
$$;
