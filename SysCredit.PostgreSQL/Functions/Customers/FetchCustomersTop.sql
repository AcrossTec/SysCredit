CREATE OR REPLACE FUNCTION public."FetchCustomersTop"
(
    "offset" INT,
	"limit"  INT
)
RETURNS SETOF "CustomerInfo"
LANGUAGE plpgsql
AS $$
BEGIN
    RETURN QUERY
    SELECT
        C.*,
        R."ReferenceId"      AS "ReferenceId",
        R."Identification"   AS "ReferenceIdentification",
        R."Name"             AS "ReferenceName",
        R."LastName"         AS "ReferenceLastName",
        R."Gender"           AS "ReferenceGender",
        R."Phone"            AS "ReferencePhone",
        R."Email"            AS "ReferenceEmail",
        R."Address"          AS "ReferenceAddress",
        G."GuarantorId"      AS "GuarantorId",
        G."Identification"   AS "GuarantorIdentification",
        G."Name"             AS "GuarantorName",
        G."LastName"         AS "GuarantorLastName",
        G."Gender"           AS "GuarantorGender",
        G."Email"            AS "GuarantorEmail",
        G."Address"          AS "GuarantorAddress",
        G."Neighborhood"     AS "GuarantorNeighborhood",
        G."BussinessType"    AS "GuarantorBussinessType",
        G."BussinessAddress" AS "GuarantorBussinessAddress",
        G."Phone"            AS "GuarantorPhone",
        RS."RelationshipId"  AS "GuarantorRelationshipId",
        RS."Name"            AS "GuarantorRelationshipName"
   FROM "public"."Customer" AS C
   INNER JOIN "public"."CustomerReference" AS CR ON CR."CustomerId"     =  C."CustomerId"
   INNER JOIN "public"."Reference"         AS R  ON  R."ReferenceId"    = CR."ReferenceId"
   INNER JOIN "public"."CustomerGuarantor" AS CG ON CG."CustomerId"     =  C."CustomerId"
   INNER JOIN "public"."Guarantor"         AS G  ON  G."GuarantorId"    = CG."GuarantorId"
   INNER JOIN "public"."Relationship"      AS RS ON CG."RelationshipId" = RS."RelationshipId"
   WHERE NOT C."IsDelete"
   ORDER BY C."Name" ASC, C."LastName" ASC
   OFFSET "offset" ROWS FETCH NEXT "limit" ROWS ONLY;
END;
$$;