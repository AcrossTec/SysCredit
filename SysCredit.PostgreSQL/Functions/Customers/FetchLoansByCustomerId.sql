CREATE OR REPLACE FUNCTION public."FetchLoansByCustomerId" (
    customer_id BIGINT
)
RETURNS SETOF "LoansByCustomerIdInfo"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        L.*,
        LT."Name" AS "LoanTypeName",
        PF."Name" AS "PaymentFrequencyName"
    FROM "public"."Loan" AS L
    INNER JOIN "public"."PaymentFrequency" AS PF ON PF."PaymentFrequencyId" = L."PaymentFrequencyId"
    INNER JOIN "public"."LoanType"         AS LT ON LT."LoanTypeId"         = L."LoanTypeId"
    INNER JOIN "public"."Customer"         AS  C ON  L."CustomerId"         = C."CustomerId"
    WHERE C."IsDelete" = FALSE AND C."CustomerId" = customer_id;
END;
$$;