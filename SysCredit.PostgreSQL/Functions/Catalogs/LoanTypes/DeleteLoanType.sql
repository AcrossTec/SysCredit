CREATE OR REPLACE FUNCTION "public"."DeleteLoanType"("LoanTypeId" BIGINT) RETURNS VOID AS $$
BEGIN
				-- Realiza la eliminación lógica
	UPDATE "public"."LoanType"
	SET
		IsDelete = TRUE, --Establece is_delete en 1 para indicar una eliminación lógica
		IsEdit = TRUE, --Establece is_edit en 1 para indicar una eliminación lógica
		ModifiedDate = CURRENT_TIMESTAMP, --Actualiza modified_date con la marca de tiempo actual
		DeletedDate = CURRENT_TIMESTAMP    -- Establece deleted_date con la marca de tiempo actual
	WHERE
	"LoanTypeId" = LoanTypeId;
END;
$$ LANGUAGE plpgsql;